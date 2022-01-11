//using Sitecore.Annotations;
//using Sitecore.Pipelines;
//using Sitecore.Xml;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;
//using System.Web.Optimization;
//using System.Xml;

//namespace MockProject.Foundation.Mvc.Pipelines.Initialize
//{
//    public class BundleDetail
//    {
//        public string Type { get; set; }
//        public string Name { get; set; }
//        public List<string> Patterns { get; set; }
//        public List<IncludeDirectory> IncludeDirectories { get; set; }
//    }

//    public class IncludeDirectory
//    {
//        public bool SearchSubDirectory { get; set; }
//        public string Pattern { get; set; }
//        public string DirectoryVirtualPath { get; set; }
//    }

//    /// <summary>
//    /// Register bundles for all sites
//    /// </summary>
//    public class RegisterBundles
//    {
//        private readonly List<BundleDetail> _bundles;

//        public RegisterBundles()
//        {
//            _bundles = new List<BundleDetail>();
//        }

//        public virtual void AddBundle(XmlNode node)
//        {
//            var bundle = new BundleDetail
//            {
//                Type = XmlUtil.GetAttribute("type", node),
//                Name = XmlUtil.GetAttribute("name", node),
//                Patterns = new List<string>(),
//                IncludeDirectories = new List<IncludeDirectory>()
//            };
//            for (var i = 0; i < node.ChildNodes.Count; i++)
//            {
//                var childNode = node.ChildNodes.Item(i);
//                if (childNode.Name == "Pattern")
//                {
//                    bundle.Patterns.Add(childNode.InnerText);
//                }
//                else if (childNode.Name == "IncludeDirectory")
//                {
//                    var includeDirectory = new IncludeDirectory
//                    {
//                        Pattern = XmlUtil.GetAttribute("pattern", childNode),
//                        SearchSubDirectory = Convert.ToBoolean(XmlUtil.GetAttribute("searchSubDirectory", childNode)),
//                        DirectoryVirtualPath = XmlUtil.GetAttribute("directoryVirtualPath", childNode)
//                    };
//                    bundle.IncludeDirectories.Add(includeDirectory);
//                }
//            }
//            _bundles.Add(bundle);
//        }

//        [UsedImplicitly]
//        public virtual void Process(PipelineArgs args)
//        {
//            Register(BundleTable.Bundles);
//            BundleTable.EnableOptimizations = true;
//#if DEBUG
//            BundleTable.EnableOptimizations = false;
//#endif
//        }

//        public virtual void Register(BundleCollection bundles)
//        {
//            foreach (var bundleDetail in _bundles)
//            {
//                var path = string.Format("~/bundles/{0}/{1}", bundleDetail.Type, bundleDetail.Name);

//                Bundle bundle;
//                if (bundleDetail.Type == "script")
//                {
//                    bundle = new ScriptBundle(path);
//                }
//                else
//                {
//                    bundle = new StyleBundle(path);
//                }

//                bundle.Include(bundleDetail.Patterns.ToArray());

//                foreach (var include in bundleDetail.IncludeDirectories)
//                {
//                    bundle.IncludeDirectory(include.DirectoryVirtualPath, include.Pattern, include.SearchSubDirectory);
//                }

//                bundles.Add(bundle);
//            }
//        }
//    }
//}