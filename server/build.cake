#addin nuget:?package=Cake.XdtTransform&version=0.16.0
#addin nuget:?package=Cake.Powershell&version=0.4.8
#addin nuget:?package=Cake.Http&version=0.7.0
#addin nuget:?package=Cake.Json&version=4.0.0
#addin nuget:?package=Newtonsoft.Json&version=11.0.2
#addin nuget:?package=Cake.Incubator&version=5.1.0
#addin nuget:?package=Cake.Services&version=0.3.5

#load "local:?path=CakeScripts/helper-methods.cake"

var target = Argument<string>("Target", "Default");
var outputFolder = Argument<string>("OutputFolder", "");
var azureAppServiceWebsiteRoot = Argument<string>("AzureAppServiceWebsiteRoot", "");
var projectFolder = Argument<string>("ProjectFolder", "");
var env = Argument<string>("Env", "");
var instanceUrl = Argument<string>("InstanceUrl", "");
Information("outputFolder: " + outputFolder);
var configuration = new Configuration();
//var envConfig = new Configuration();
var cakeConsole = new CakeConsole();
var configJsonFile = "cake-config.json";
var userConfigJsonFile= "cake-config.json.user";
//var envJsonFile = "env.json";
var unicornSyncScript = $"./scripts/Unicorn/Sync.ps1";

/*===============================================
================ MAIN TASKS =====================
===============================================*/

Setup(context =>
{
	cakeConsole.ForegroundColor = ConsoleColor.Yellow;
	PrintHeader(ConsoleColor.DarkGreen);

	
	FilePath configFile;
	if (FileExists(userConfigJsonFile))
	{
		Information("userConfigJsonFile: " + userConfigJsonFile);
		configFile = new FilePath(userConfigJsonFile);
	}
	else
	{
		configFile = new FilePath(configJsonFile);
	}
	//var configFile = new FilePath(configJsonFile);
	Information("configJsonFileName: " + configJsonFile);
	configuration = DeserializeJsonFromFile<Configuration>(configFile);
	if(configuration != null)
	{
		configuration.WebsiteRoot = configuration.WebsiteRoot.Replace("$OutputFolder",outputFolder);
		Information("configuration.WebsiteRoot: " + configuration.WebsiteRoot);

		configuration.ProjectFolder = configuration.ProjectFolder.Replace("$ProjectFolder",projectFolder);
		Information("configuration.ProjectFolder: " + configuration.ProjectFolder);

		configuration.Environment = configuration.Environment.Replace("$Env",env);
		Information("configuration.Environment: " + configuration.Environment);
	}

});

private string GetXconnectServiceName()
{
	var connectionStringFile = new FilePath($"{configuration.WebsiteRoot}/App_config/ConnectionStrings.config");
	var xPath = "connectionStrings/add[@name='xconnect.collection']/@connectionString";
	string xConnectUrl = XmlPeek(connectionStringFile, xPath);
	var uri = new Uri(xConnectUrl);
	return uri.Host + "-MarketingAutomationService";
}

Task("Default")
.WithCriteria(configuration != null)
.IsDependentOn("Copy-Sitecore-Lib")
.IsDependentOn("Modify-PublishSettings")
.IsDependentOn("Publish-All-Projects")
.IsDependentOn("Apply-Xml-Transform");
//.IsDependentOn("Modify-Unicorn-Source-Folder")
//.IsDependentOn("Post-Deploy");

Task("Post-Deploy")
.IsDependentOn("Sync-Unicorn");

Task("Quick-Deploy")
.WithCriteria(configuration != null)
.IsDependentOn("Copy-Sitecore-Lib")
.IsDependentOn("Modify-PublishSettings")
.IsDependentOn("Publish-All-Projects")
.IsDependentOn("Apply-Xml-Transform")
.IsDependentOn("Modify-Unicorn-Source-Folder");

/*===============================================
================= SUB TASKS =====================
===============================================*/

Task("Copy-Sitecore-Lib")
.WithCriteria(()=>(configuration.BuildConfiguration == "Local"))
.Does(()=> {
	var files = GetFiles(
		$"{configuration.WebsiteRoot}/bin/Sitecore*.dll");
	var destination = "./sc.lib";
	EnsureDirectoryExists(destination);
	CopyFiles(files, destination);
});

Task("Build-Artifact")
.IsDependentOn("Build-Solution")
.IsDependentOn("Publish-Projects")
.IsDependentOn("Apply-Xml-Transform-CD");

Task("Publish-All-Projects")
.IsDependentOn("Build-Solution")
.IsDependentOn("Publish-Projects");

Task("Build-Solution").Does(() => {
	MSBuild(configuration.SolutionFile, cfg => InitializeMSBuildSettings(cfg));
});

Task("Publish-Projects").Does(() => {
	var layers = new string[] { configuration.FoundationSrcFolder, configuration.FeatureSrcFolder, configuration.ProjectSrcFolder};
	foreach (var layer in layers){
		PublishProjects(layer, configuration.WebsiteRoot);
	}
});


Task("Modify-Unicorn-Source-Folder").Does(() => {
	Information("Modify-Unicorn-Source-Folder: " + configuration.FoundationSrcFolder);
	var zzzDevSettingsFile = File($"{configuration.WebsiteRoot}/App_config/Include/Foundation/Foundation.Serialization.Unicorn.DevSettings.config");

	var rootXPath = "configuration/sitecore/sc.variable[@name='{0}']/@value";
	var sourceFolderXPath = string.Format(rootXPath, "MockProjectSourceFolder");
	var directoryPath = MakeAbsolute(new DirectoryPath(configuration.SourceFolder)).FullPath;

	var xmlSetting = new XmlPokeSettings {
		Namespaces = new Dictionary<string, string> {
			{"patch", @"http://www.sitecore.net/xmlconfig/"}
		}
	};
	XmlPoke(zzzDevSettingsFile, sourceFolderXPath, directoryPath, xmlSetting);
});

Task("Turn-On-Unicorn").Does(() => {
	var webConfigFile = File($"{configuration.WebsiteRoot}/web.config");
	
	if(!string.IsNullOrEmpty(azureAppServiceWebsiteRoot))
	{
		webConfigFile = File($"{azureAppServiceWebsiteRoot}/web.config");
	}

	var xmlSetting = new XmlPokeSettings {
		Namespaces = new Dictionary<string, string> {
			{"patch", @"http://www.sitecore.net/xmlconfig/"}
		}
	};

	var unicornAppSettingXPath = "configuration/appSettings/add[@key='unicorn:define']/@value";
	XmlPoke(webConfigFile, unicornAppSettingXPath, "On", xmlSetting);
});

Task("Modify-PublishSettings").Does(() => {
	var publishSettingsOriginal = File($"{configuration.ProjectFolder}/publishsettings.targets");
	var destination = $"{configuration.ProjectFolder}/publishsettings.targets.user";

	CopyFile(publishSettingsOriginal,destination);

	var importXPath = "/ns:Project/ns:Import";

	var publishUrlPath = "/ns:Project/ns:PropertyGroup/ns:publishUrl";

	var xmlSetting = new XmlPokeSettings {
		Namespaces = new Dictionary<string, string> {
			{"ns", @"http://schemas.microsoft.com/developer/msbuild/2003"}
		}
	};
	XmlPoke(destination,importXPath,null,xmlSetting);
	XmlPoke(destination,publishUrlPath,$"{configuration.InstanceUrl.Replace("$InstanceUrl",instanceUrl)}",xmlSetting);
});

Task("Sync-Unicorn")
.IsDependentOn("Turn-On-Unicorn")
.Does(() => {
	var unicornUrl = configuration.InstanceUrl.Replace("$InstanceUrl",instanceUrl) + "unicorn.aspx";
	Information("Sync Unicorn items from url: " + unicornUrl);

	var authenticationFile = new FilePath($"{configuration.WebsiteRoot}/App_config/Include/Foundation/Foundation.Serialization.Unicorn.SharedSecret.config");
	
	if(!string.IsNullOrEmpty(azureAppServiceWebsiteRoot))
	{
		authenticationFile = new FilePath($"{azureAppServiceWebsiteRoot}/App_config/Include/Foundation/Foundation.Serialization.Unicorn.SharedSecret.config");
	}

	var xPath = "/configuration/sitecore/unicorn/authenticationProvider/SharedSecret";

	string sharedSecret = XmlPeek(authenticationFile, xPath);

	StartPowershellFile(unicornSyncScript, new PowershellSettings()
		.SetFormatOutput()
		.SetLogOutput()
		.WithArguments(args => {
			args.Append("secret", sharedSecret)
					.Append("url", unicornUrl);
		}));
});

Task("Apply-Xml-Transform").Does(() => {
    var environment = configuration.Environment;
	// target website transforms
	Transform($"{configuration.SourceFolder}\\Project\\MockProject\\code", configuration.WebsiteRoot, environment);
	Transform($"{configuration.SourceFolder}\\Foundation\\**\\code", configuration.WebsiteRoot, environment);
	Transform($"{configuration.SourceFolder}\\Feature\\**\\code", configuration.WebsiteRoot, environment);
	

	// TODO://xconnect transforms
	Transform($"{configuration.SourceFolder}\\Feature\\Automation\\xconnect\\App_Data\\Config\\sitecore\\MarketingAutomation", $"{configuration.XConnectAutomationServiceRoot}\\App_Data\\Config\\sitecore\\MarketingAutomation");
});

Task("Apply-Xml-Transform-CD").Does(() => {
    var environment = configuration.Environment;
	// target website transforms
	TransformFile(configuration.WebsiteRoot, environment);
	// TODO://xconnect transforms
	//TransformFile(configuration.XConnectRoot);
});

Task("Upload-Web-Resource").Does(()=>{
Information("configuration.WebsiteDeploymentFolder: " + configuration.WebsiteDeploymentFolder);
Information("configuration.WebsiteRoot: " + configuration.WebsiteRoot);
	CopyDirectory(
		$"{configuration.WebsiteDeploymentFolder}",
		$"{configuration.WebsiteRoot}"
	);

	CopyDirectory(
		$"{configuration.AppSrcFolder}",
		$"{configuration.WebsiteRoot}\\dist\\MockProject"
	);
	
});
Task("Upload-XConnect-Resource").Does(()=>{

	CopyDirectory(
		$"{configuration.XconnectDeploymentFolder}",
		$"{configuration.XConnectRoot}"
	);

	CopyDirectory(
		$"{configuration.XConnectAutomationServiceRootDeploymentFolder}",
		$"{configuration.XConnectAutomationServiceRoot}"
	);

	CopyDirectory(
		$"{configuration.XconnectIndexerRootDeploymentFolder}",
		$"{configuration.XConnectIndexerRoot}"
	);
	
});


Task("Remove-Transform-Files").Does(() => {
});


RunTarget(target);