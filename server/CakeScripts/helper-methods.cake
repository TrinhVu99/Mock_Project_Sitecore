using System.Text.RegularExpressions;

/*===============================================
================= HELPER METHODS ================
===============================================*/

public class Configuration
{
	private MSBuildToolVersion _msBuildToolVersion;

	public string WebsiteRoot {get;set;}
	public string AppRoot {get;set;}
	public string SSRProxyAppRoot {get;set;}
	
	public string AppSrcFolder {get;set;}	
	public string XConnectRoot {get;set;}
	public string XConnectIndexerRoot {get;set;}
	public string XConnectAutomationServiceRoot {get;set;}
	public string InstanceUrl {get;set;}
	public string SolutionName {get;set;}
	public string ProjectFolder {get;set;}
	public string BuildConfiguration {get;set;}
	public string MessageStatisticsApiKey {get;set;}
	public string MarketingDefinitionsApiKey {get;set;}
	public bool RunCleanBuilds {get;set;}
    public string Environment {get;set;}
	public int DeployExmTimeout {get;set;}
	public string WebsiteDeploymentFolder { get; set;}
	public string XconnectDeploymentFolder { get; set;}
	public string XConnectAutomationServiceRootDeploymentFolder { get; set;}
	public string XconnectIndexerRootDeploymentFolder { get; set;}
	public string BuildToolVersions
	{
		set
		{
			if(!Enum.TryParse(value, out this._msBuildToolVersion))
			{
				this._msBuildToolVersion = MSBuildToolVersion.Default;
			}
		}
	}

	public string SourceFolder => $"{ProjectFolder}\\src";
	public string ProjectSrcFolder => $"{SourceFolder}\\Project";
	public string FoundationSrcFolder => $"{SourceFolder}\\Foundation";
	public string FeatureSrcFolder => $"{SourceFolder}\\Feature";
	public string SolutionFile => $"{ProjectFolder}\\{SolutionName}";
	public MSBuildToolVersion MSBuildToolVersion => this._msBuildToolVersion;
	public string BuildTargets => this.RunCleanBuilds ? "Clean;Build" : "Build";
}

public void PrintHeader(ConsoleColor foregroundColor)
{
	cakeConsole.ForegroundColor = foregroundColor;
	cakeConsole.ResetColor();
}

public void PublishProjects(string rootFolder, string publishRoot)
{
	Information("Publishing " + rootFolder + " to " + publishRoot);

	Func<IFileSystemInfo, bool> excludedProjects = null; 
	// fileSystemInfo => !fileSystemInfo.Path.FullPath.Contains("Fitness.Automation.Plugins");

	var projects = GetFiles($"{rootFolder}\\**\\code\\*.csproj", excludedProjects);

	foreach (var project in projects) {
		Information("Publishing " + project + " to " + publishRoot);

		MSBuild(project, cfg => InitializeMSBuildSettings(cfg)
														.WithTarget(configuration.BuildTargets)
														.WithProperty("DeployOnBuild", "true")
														.WithProperty("DeployDefaultTarget", "WebPublish")
														.WithProperty("WebPublishMethod", "FileSystem")
														.WithProperty("DeleteExistingFiles", "false")
														.WithProperty("publishUrl", publishRoot)
														.WithProperty("BuildProjectReferences", "false"));
	}
}

public FilePathCollection GetTransformFiles(string rootFolder)
{
	Func<IFileSystemInfo, bool> exclude_obj_bin_folder =fileSystemInfo => !fileSystemInfo.Path.FullPath.Contains("/obj/") || !fileSystemInfo.Path.FullPath.Contains("/bin/");

	Information($"Collecting transforms from: {rootFolder}");
	var xdtFiles = GetFiles($"{rootFolder}\\**\\*.xdt", exclude_obj_bin_folder);

	return xdtFiles;
}
public FilePathCollection GetOldTransformFiles(string rootFolder, string environment)
{
	Func<IFileSystemInfo, bool> exclude_obj_bin_folder =fileSystemInfo => !fileSystemInfo.Path.FullPath.Contains("/obj/") || !fileSystemInfo.Path.FullPath.Contains("/bin/");
	Information($"Environment: {environment}");
	Information($"Collecting transforms from: {rootFolder}");
	
	var xdtFiles = GetFiles($"{rootFolder}\\**\\*.{environment}.\\*.xdt", exclude_obj_bin_folder);
	var commonXdtFiles = GetFiles($"{rootFolder}\\**\\*.xdt", exclude_obj_bin_folder);

	if(commonXdtFiles != null)
	{
		foreach (var file in commonXdtFiles) {
			xdtFiles.Add(file);
		}
	}

	return xdtFiles;
}

public FilePathCollection GetTransformFiles(string rootFolder, string environment)
{
	Func<IFileSystemInfo, bool> exclude_obj_bin_folder =fileSystemInfo => !fileSystemInfo.Path.FullPath.Contains("/obj/") || !fileSystemInfo.Path.FullPath.Contains("/bin/");
	Information($"Environment: {environment}");
	Information($"Collecting transforms from: {rootFolder}");
	
	var xdt1Files = GetFiles($"{rootFolder}\\**\\*.{environment}.config.xdt", exclude_obj_bin_folder);
	var xdt2Files = GetFiles($"{rootFolder}\\**\\*.{environment}.xml.xdt", exclude_obj_bin_folder);

	FilePathCollection xdtFiles = null;
	if(xdt1Files != null)
	{
		xdtFiles = xdt1Files;
	}
	else if(xdt2Files != null && xdt1Files == null)
	{
		xdtFiles = xdt2Files;
	}

	if(xdt1Files != null && xdt2Files != null)
	{
		foreach (var xdt2file in xdt2Files) {
			xdtFiles.Add(xdt2file);
		}
	}


	var commonXdtFiles = GetFiles($"{rootFolder}\\**\\*.xdt", exclude_obj_bin_folder);
	var availableEnvs = new List<string> (){"Local", "Test", "UAT", "PROD"};
	if(commonXdtFiles != null)
	{
		foreach (var file in commonXdtFiles) {
		var shouldAdded = true;
		  foreach(var availableEnv in availableEnvs)
		  {
			var endWith1 = $".{availableEnv}.config.xdt";
			var endWith2 = $".{availableEnv}.xml.xdt";
			Information($"endWith1: {endWith1}");
			Information($"endWith2: {endWith2}");
			if(file.FullPath.Contains("/obj/") || file.FullPath.Contains("/bin/") || file.FullPath.Contains(endWith1) || file.FullPath.Contains(endWith2))
			 {
				shouldAdded = false;
			 }		 
		  }	
		  if(shouldAdded)
			{
				Information($"commonXdtFiles: {file.FullPath}");
				xdtFiles.Add(file);
			}
		}
	}

	return xdtFiles;
}
public void Transform(string rootFolder, string destinationRootFolder) {
	var xdtFiles = GetTransformFiles(rootFolder);

	foreach (var file in xdtFiles) {
		Information($"Applying configuration transform:{file.FullPath}");
		var fileToTransform = Regex.Replace(file.FullPath, ".+/(.+)/*.xdt", "$1");
		fileToTransform = Regex.Replace(fileToTransform, ".sc-internal", "");
		var sourceTransform = $"{destinationRootFolder}\\{fileToTransform}";

		XdtTransformConfig(sourceTransform       // Source File
												, file.FullPath      // Tranforms file (*.xdt)
												, sourceTransform);  // Target File
	}
}

public void Transform(string rootFolder, string destinationRootFolder, string environment) {
		//Information($"environment:.{environment}");
		//Information($"rootFolder:{rootFolder}");
		var xdtFiles = GetTransformFiles(rootFolder, environment);
	var rootFolder1 = $"{rootFolder}\\";
	foreach (var file in xdtFiles) {
		if(!file.FullPath.Contains("/obj/") && !file.FullPath.Contains("/bin/") && !file.FullPath.Contains("\\obj\\") && !file.FullPath.Contains("\\bin\\") )
			{
			var xdtFileName = file.GetFilename().ToString();
			//Information($"xdtFileName:{xdtFileName}");
			var fullPath = file.FullPath;
			fullPath = fullPath.Replace(@"/",@"\");
			//Information($"fullPath:{fullPath}");
			//Information($"rootFolder1:{rootFolder1}");
			var xdtRelativePath = "";
			//fullPath.Replace(rootFolder1, ""); 

			string toBeSearched = "\\code\\";
			int ix = fullPath.IndexOf(toBeSearched);

			if (ix != -1) 
			{
				xdtRelativePath = fullPath.Substring(ix + toBeSearched.Length);
				// do something here
			}
			//Regex.Replace(file.FullPath, root, "");
			//Information($"xdtRelativePath:{xdtRelativePath}");	
			var xdtRelativePathWithoutFileName = Regex.Replace(xdtRelativePath, xdtFileName, "");
			//Information($"xdtRelativePathWithoutFileName:{xdtRelativePathWithoutFileName}");
			Information($"Applying configuration transform:{file.FullPath}");
		
			var fileToTransform = Regex.Replace(file.FullPath, ".+/(.+)/*.xdt", "$1");
			//Information($"fileToTransform1:{fileToTransform}");
			fileToTransform = Regex.Replace(fileToTransform, ".sc-internal", "");
			var environmentPattern = $".{environment}.";
			fileToTransform = Regex.Replace(fileToTransform, environmentPattern, ".");

			//Information($"fileToTransform1x:{fileToTransform}");
			fileToTransform = xdtRelativePathWithoutFileName + fileToTransform ;// $@"{xdtRelativePathWithoutFileName}{fileToTransform}"; 
			//Information($"fileToTransform2:{fileToTransform}");
			var sourceTransform = $"{destinationRootFolder}\\{fileToTransform}";
			//Information($"sourceTransform:{sourceTransform}");
			XdtTransformConfig(sourceTransform       // Source File
													, file.FullPath      // Tranforms file (*.xdt)
													, sourceTransform);  // Target File
			}
		//Information($"Applying configuration transform successfully:{file.FullPath}");
	}
}
public void TransformFile(string rootFolder) {
	var xdtFiles = GetTransformFiles(rootFolder);

	foreach (var file in xdtFiles) {
		var directory = file.GetDirectory().ToString();
		Information($"Applying configuration transform:{file.FullPath}");
		var fileToTransform = Regex.Replace(file.FullPath, ".+/(.+)/*.xdt", "$1");
		fileToTransform = Regex.Replace(fileToTransform, ".sc-internal", "");
		var sourceTransform = $"{directory}\\{fileToTransform}";

		XdtTransformConfig(sourceTransform       // Source File
												, file.FullPath      // Tranforms file (*.xdt)
												, sourceTransform);  // Target File
	}
}
public void TransformFile(string folder, string environment) {
		//Information($"environment:.{environment}");
		//Information($"folder:{folder}");
		var xdtFiles = GetTransformFiles(folder, environment);
	var folder1 = $"{folder}\\";
	foreach (var file in xdtFiles) {
		if(!file.FullPath.Contains("/obj/") && !file.FullPath.Contains("/bin/") && !file.FullPath.Contains("\\obj\\") && !file.FullPath.Contains("\\bin\\") )
			{
			var xdtFileName = file.GetFilename().ToString();
			var directory = file.GetDirectory().ToString();

			//Information($"xdtFileName:{xdtFileName}");
			var fullPath = file.FullPath;
			fullPath = fullPath.Replace(@"/",@"\");
			//Information($"fullPath:{fullPath}");
			//Information($"folder1:{folder1}");
			var xdtRelativePath = "";
			//fullPath.Replace(folder1, ""); 

			string toBeSearched = "\\code\\";
			int ix = fullPath.IndexOf(toBeSearched);

			if (ix != -1) 
			{
				xdtRelativePath = fullPath.Substring(ix + toBeSearched.Length);
				// do something here
			}
			//Regex.Replace(file.FullPath, root, "");
			//Information($"xdtRelativePath:{xdtRelativePath}");	
			var xdtRelativePathWithoutFileName = Regex.Replace(xdtRelativePath, xdtFileName, "");
			//Information($"xdtRelativePathWithoutFileName:{xdtRelativePathWithoutFileName}");
			Information($"Applying configuration transform:{file.FullPath}");
		
			var fileToTransform = Regex.Replace(file.FullPath, ".+/(.+)/*.xdt", "$1");
			Information($"fileToTransform1:{fileToTransform}");
			fileToTransform = Regex.Replace(fileToTransform, ".sc-internal", "");
			var environmentPattern = $".{environment}.";
			fileToTransform = Regex.Replace(fileToTransform, environmentPattern, ".");

			//Information($"fileToTransform1x:{fileToTransform}");
			//fileToTransform = xdtRelativePathWithoutFileName + fileToTransform ;// $@"{xdtRelativePathWithoutFileName}{fileToTransform}"; 



			//Information($"fileToTransform2:{fileToTransform}");

			//var sourceTransform = $"{folder}\\{fileToTransform}";

			var sourceTransformFullPath = $"{directory}\\{fileToTransform}";
			Information($"sourceTransform:{sourceTransformFullPath}");
			XdtTransformConfig(sourceTransformFullPath       // Source File
													, file.FullPath      // Tranforms file (*.xdt)
													, sourceTransformFullPath);  // Target File
			}
		//Information($"Applying configuration transform successfully:{file.FullPath}");
	}
}

public void DeployFiles(string source, string destination){
	var files = GetFiles($"{source}");
	EnsureDirectoryExists(destination);
	CopyFiles(files, destination);
}
public void DeployDirectory(string source, string destination){
	CopyDirectory(source, destination);
}
public void RebuildIndex(string indexName)
{
	var url = $"{configuration.InstanceUrl}utilities/indexrebuild.aspx?index={indexName}";
	string responseBody = HttpGet(url);
}

public void DeployExmCampaigns()
{
	var url = $"{configuration.InstanceUrl}utilities/deployemailcampaigns.aspx?apiKey={configuration.MessageStatisticsApiKey}";
	var responseBody = HttpGet(url, settings =>
	{
		settings.AppendHeader("Connection", "keep-alive");
	});

	Information(responseBody);
}

public MSBuildSettings InitializeMSBuildSettings(MSBuildSettings settings)
{
	settings.SetConfiguration(configuration.BuildConfiguration)
					.SetVerbosity(Verbosity.Minimal)
					.SetMSBuildPlatform(MSBuildPlatform.Automatic)
					.SetPlatformTarget(PlatformTarget.MSIL)
					.UseToolVersion(configuration.MSBuildToolVersion)
					.WithRestore();
	return settings;
}

public void CreateFolder(string folderPath)
{
	if (!DirectoryExists(folderPath))
	{
		CreateDirectory(folderPath);
	}
}

public void Spam(Action action, int? timeoutMinutes = null)
{
	Exception lastException = null;
	var startTime = DateTime.Now;
	while (timeoutMinutes == null || (DateTime.Now - startTime).TotalMinutes < timeoutMinutes)
	{
		try {
			action();

			Information($"Completed in {(DateTime.Now - startTime).Minutes} min {(DateTime.Now - startTime).Seconds} sec.");
			return;
		} catch (AggregateException aex) {
			foreach (var x in aex.InnerExceptions) {
				Information($"{x.GetType().FullName}: {x.Message}");
			}
			lastException = aex;
		} catch (Exception ex) {
			Information($"{ex.GetType().FullName}: {ex.Message}");
			lastException = ex;
		}
	}

	throw new System.TimeoutException($"Unable to complete within {timeoutMinutes} minutes.", lastException);
}

public void WriteError(string errorMessage)
{
	cakeConsole.ForegroundColor = ConsoleColor.Red;
	cakeConsole.WriteError(errorMessage);
	cakeConsole.ResetColor();
}