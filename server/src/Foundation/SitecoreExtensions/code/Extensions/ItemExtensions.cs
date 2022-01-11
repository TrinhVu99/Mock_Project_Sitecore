using MockProject.Foundation.SitecoreExtensions.Helpers;
using Sitecore;
using Sitecore.Collections;
using Sitecore.Configuration;
using Sitecore.Data;
using Sitecore.Data.Fields;
using Sitecore.Data.Items;
using Sitecore.Data.Managers;
using Sitecore.Data.Templates;
using Sitecore.Diagnostics;
using Sitecore.Globalization;
using Sitecore.Links;
using Sitecore.Links.UrlBuilders;
using Sitecore.Resources.Media;
using Sitecore.SecurityModel;
using Sitecore.Web;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;

namespace MockProject.Foundation.SitecoreExtensions.Extensions
{
    public static class ItemExtensions
    {
        /// <summary>
        /// returns the shortid as string
        /// </summary>
        /// <param name="pItem"></param>
        /// <returns></returns>
        public static string ShortId(this Item pItem)
        {
            return pItem.ID.ToShortID().ToString();
        }

        /// <summary>
        /// This method returns the Descendants of of an item based on the provided template name. 
        /// It uses Sitecore.Data.Items.ItemAxes.SelectItems based on the provided item as Context item. 
        /// Standard SelectItems returns null when it has no correct Descendants, 
        /// but in this method we return a empty list instead
        /// </summary>
        /// <param name="pItem">Item you want the descendants of</param>
        /// <param name="pTemplateName">Template name of the descendants</param>
        /// <returns></returns>
        public static ICollection<Item> GetDescendantOfTemplate(this Item pItem, string pTemplateName)
        {
            string lSearchString = string.Format(".//*[@@templatename='{0}']", pTemplateName);
            lSearchString = lSearchString.Replace(" and ", " #and# ").Replace(" or ", " #or# ");
            var lDescendants = pItem.Axes.SelectItems(lSearchString);
            return lDescendants != null ? lDescendants.ToList() : new List<Item>();
        }
        public static ICollection<Item> GetDescendantOfTemplate(this Item pItem, TemplateID pTemplateId)
        {
            string lSearchString = string.Format(".//*[@@templateid='{0}']", pTemplateId.ID);
            lSearchString = lSearchString.Replace(" and ", " #and# ").Replace(" or ", " #or# ");
            var lDescendants = pItem.Axes.SelectItems(lSearchString);
            return lDescendants != null ? lDescendants.ToList() : new List<Item>();
        }

        public static ICollection<Item> GetDescendantsInheritOfTemplate(this Item pItem, TemplateID pTemplateId)
        {
            return (pItem.Axes.GetDescendants().OfType<Item>().Where(pSubItem => pSubItem.InheritsTemplate(pTemplateId))).ToList();
        }

        public static ICollection<Item> GetChildrenInheritOfTemplate(this Item pItem, TemplateID pTemplateId)
        {
            return (pItem.GetChildren().OfType<Item>().Where(pSubItem => pSubItem.InheritsTemplate(pTemplateId))).ToList();
        }

        public static ICollection<Item> GetChildrenInheritOfTemplate(this Item pItem, string pTemplateName)
        {
            return (pItem.GetChildren().OfType<Item>().Where(pSubItem => pSubItem.InheritsTemplate(pTemplateName))).ToList();
        }
        public static ICollection<Item> GetChildrenInheritOfTemplate(this Item pItem, string pTemplateName, int pLevelsDeep)
        {
            List<Item> lResult = new List<Item>();
            lResult.AddRange(pItem.GetChildrenInheritOfTemplate(pTemplateName));
            if (pLevelsDeep > 0)
            {
                foreach (Item lChild in pItem.GetChildren().InnerChildren)
                {
                    lResult.AddRange(lChild.GetChildrenInheritOfTemplate(pTemplateName, --pLevelsDeep));
                }
            }
            return lResult;
        }
        public static Item GetChildByName(this Item pItem, string pName)
        {
            foreach (Item lChild in pItem.GetChildren())
            {
                if (lChild.Name.EqualsIgnoreCase(pName))
                {
                    return lChild;
                }
            }
            return null;
        }
        /// <summary>
        /// Check if item inherit from a template
        /// </summary>
        /// <param name="item"></param>
        /// <param name="templateId"></param>
        /// <returns></returns>
        public static bool IsDerived(this Item item, ID templateId)
        {
            var template = TemplateManager.GetTemplate(item);
            if (template == null)
            {
                return false;
            }
            return template.IsDerived(templateId);
        }

        /// <summary>
        /// Check if item has language version
        /// </summary>
        /// <param name="item"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public static bool HasLanguage(this Item item, string language)
        {
            var checkingLanguage = string.IsNullOrWhiteSpace(language)
                ? ContextHelper.Language
                : Language.Parse(language);

            return item.HasLanguage(checkingLanguage);
        }

        /// <summary>
        /// Check if item has language version
        /// </summary>
        /// <param name="item"></param>
        /// <param name="language"></param>
        /// <returns></returns>
        public static bool HasLanguage(this Item item, Language language)
        {
            return
                item.Languages.Any(
                    x =>
                        x.CultureInfo.ThreeLetterISOLanguageName ==
                        language.CultureInfo.ThreeLetterISOLanguageName);
        }

        /// <summary>
        /// Get public url of an item
        /// </summary>
        /// <param name="item"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static string GetPublicUrl(this Item item, ItemUrlBuilderOptions options)
        {
            if (item == null)
            {
                return null;
            }

            if (options == null)
            {
                options = new ItemUrlBuilderOptions();
            }

            return LinkManager.GetItemUrl(item, options).Replace(" ", "-");
        }

        /// <summary>
        /// Get media url of an item
        /// </summary>
        /// <param name="item"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static string GetMediaUrl(this Item item, MediaUrlBuilderOptions options)
        {
            if (item == null)
            {
                return null;
            }
            if (item.Fields["Extension"] != null)
            {
                return MediaManager.GetMediaUrl(item, options).Replace(" ", "-");
            }

            var exception = string.Format("Item {0} is not MediaItem", item.Paths.FullPath);
            throw new InvalidDataException(exception);
        }

        /// <summary>
        /// Get media url of an item link field
        /// </summary>
        /// <param name="item"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static string GetMediaUrl(this Item item, ItemUrlBuilderOptions options)
        {
            if (item == null)
            {
                return null;
            }

            if (item.Fields["Extension"] != null)
            {
                var MediaUrlBuilderOptions = options == null
                    ? null
                    : new MediaUrlBuilderOptions
                    {
                        AlwaysIncludeServerUrl = options.AlwaysIncludeServerUrl
                    };
                return MediaManager.GetMediaUrl(item, MediaUrlBuilderOptions).Replace(" ", "-");
            }

            var exception = string.Format("Item {0} is not MediaItem", item.Paths.FullPath);
            throw new InvalidDataException(exception);
        }

        /// <summary>
        /// Get items which reference to current item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static IEnumerable<Item> GetRelatedItems(this Item item)
        {
            var links = Globals.LinkDatabase.GetReferrers(item);
            return links.Select(i => i.GetTargetItem()).Where(i => i != null);
        }

        /// <summary>
        /// Get list of children of an item that inherit from a template
        /// </summary>
        /// <param name="item"></param>
        /// <param name="templateId"></param>
        /// <returns></returns>
        public static IEnumerable<Item> ChildrenDerivedFrom(this Item item, ID templateId)
        {
            var children = item.Children.InnerChildren;
            return children.Where(child => child.IsDerived(templateId)).ToList();
        }

        /// <summary>
        /// Get short url of item
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string GetShortUrl(this string url)
        {
            var shortUrl = url.Replace("sitecore/shell/MockProject/home/", "");
            shortUrl = url.Replace("sitecore/content/MockProject/home/", "");
            return shortUrl;
        }

        /// <summary>
        /// checked: item is a page
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static bool HasPresentationDetails(this Item item)
        {
            return item.Fields[Sitecore.FieldIDs.LayoutField] != null
                 && !String.IsNullOrEmpty(item.Fields[Sitecore.FieldIDs.LayoutField].Value);
        }

        /// <summary>
        /// Returns the selected item if the type of the specified field is one of the following:
        /// droplink, lookup, droptree, tree, reference, link, general link, grouped droplink.
        /// Note: droplist and grouped droplist will not work on account of these types being value types.
        /// </summary>
        /// <param name="pItem"></param>
        /// <param name="pFieldName"></param>
        /// <returns></returns>
        public static Item GetSelectedItem(this Item pItem, string pFieldName)
        {
            return GetSelectedItem(pItem, pFieldName, false, false);
        }

        public static Item GetSelectedItem(this Item pItem, string pFieldName, bool pNullableSelectedItem, bool pUseContextLanguage)
        {
            if (pUseContextLanguage)
            {
                return GetSelectedItem(pItem, pFieldName, pNullableSelectedItem, (pItemId) => pItem.Database.GetItem(pItemId, Context.Language));

            }
            else
            {
                return GetSelectedItem(pItem, pFieldName, pNullableSelectedItem);
            }
        }

        public static Item GetSelectedItem(this Item pItem, string pFieldName, bool pNullableSelectedItem)
        {
            return GetSelectedItem(pItem, pFieldName, pNullableSelectedItem, (pItemId) => pItem.Database.GetItem(pItemId, pItem.Language));
        }

        public static Item GetSelectedItem(this Item pItem, string pFieldName, Language pLanguage)
        {
            return GetSelectedItem(pItem, pFieldName, false, (pItemId) => pItem.Database.GetItem(pItemId, pLanguage));
        }

        private static Item GetSelectedItem(this Item pItem, string pFieldName, bool pNullableSelectedItem, Func<ID, Item> pGetItemFunction)
        {
            Item lItem = null;
            Field lField = pItem.Fields[pFieldName];
            Assert.IsNotNull(lField, string.Format("Field {1} was not found on item {0}", pItem.Paths.FullPath, pFieldName));
            if (lField != null)
            {
                //TypeKey contains the name of the field as it is defined in fieldtypes.config (but in lowercase)

                // Instead of getting the targetitem for a lookupfield, referencefield or linkfield, we'll use the following construction
                // pItem.Database.GetItem(lId,pItem.Language). We get the reference, lookup or link for the same language as the item, when
                // we're in a pipeline the context.language is 'en' even if we're requesting a reference, lookup or language for a dutch item. 
                // That's why we do this explicitly
                ID lID = null;
                switch (lField.Definition.TypeKey)
                {
                    case "droplink":
                    case "lookup":
                        LookupField lLookup = lField;
                        lID = lLookup.TargetID;
                        lItem = pGetItemFunction(lID);
                        break;
                    case "droptree":
                    case "tree":
                    case "reference":
                        ReferenceField lReferenceField = lField;
                        lID = lReferenceField.TargetID;
                        lItem = pGetItemFunction(lID);
                        break;

                    case "general topic link":
                    case "topic link":
                    case "link":
                    case "general link":
                        LinkField lLinkField = lField;
                        lID = lLinkField.TargetID;
                        if (lLinkField.LinkType.Equals("internal") || lLinkField.LinkType.Equals("media"))
                        {
                            lItem = pGetItemFunction(lID);
                        }
                        break;
                    case "internal link":
                        InternalLinkField lInternalLinkField = lField;
                        lID = lInternalLinkField.TargetID;
                        lItem = pGetItemFunction(lID);
                        break;
                    case "grouped droplink":
                        GroupedDroplinkField lGdf = lField;
                        lID = lGdf.TargetID;
                        lItem = pGetItemFunction(lID);
                        break;
                    case "droplist":
                    case "grouped droplist":
                        //Droplists might be expected to store items, but they really don't. 
                        //They store the name of the selected item and are therefore aren't usefull in this method at all
                        break;
                    default:
                        throw new Exception(
                            string.Format("Field {1} of Item {0} is of fieldtype {2}, which is not specifically handled by extention method 'GetSelectedItem'",
                                          pItem.Paths.FullPath, pFieldName, lField.Definition.TypeKey));
                }
            }
            return lItem != null || pNullableSelectedItem ? lItem : pItem.Database.GetItem("/sitecore/Content");
        }

        public static bool HasExternalLink(this Item pItem, string pFieldName)
        {
            var lLinkField = (LinkField)pItem.Fields[pFieldName];
            if (lLinkField.LinkType.Equals("external"))
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Returns the selected items if the type of the specified field is one of the following:
        /// Multilist, TreeList, TreeListEx, Checklist
        /// </summary>
        /// <param name="pItem"></param>
        /// <param name="pFieldName"></param>
        /// <returns></returns>
        /// <exception cref="Exception"><c>Exception when unhandled type is encountered.</c>.</exception>
        public static IList<Item> GetSelectedItems(this Item pItem, string pFieldName)
        {
            return pItem.GetSelectedItems(pFieldName, pItem.Language);
        }

        /// <summary>
        /// Returns the selected items if the type of the specified field is one of the following:
        /// Multilist, TreeList, TreeListEx, Checklist
        /// </summary>
        /// <param name="pItem"></param>
        /// <param name="pFieldName"></param>
        /// <param name="pLanguage"></param>
        /// <returns></returns>
        /// <exception cref="Exception"><c>Exception when unhandled type is encountered.</c>.</exception>
        public static IList<Item> GetSelectedItems(this Item pItem, string pFieldName, Language pLanguage)
        {
            return pItem.GetSelectedItems(pFieldName, (pItemId) => pItem.Database.GetItem(pItemId, pLanguage));
        }

        private static IList<Item> GetSelectedItems(this Item pItem, string pFieldName, Func<ID, Item> pGetItemFunction)
        {
            Field lField = pItem.Fields[pFieldName];
            //Assert.IsNotNull(lField, string.Format("Field {1} was not found on item {0}", pItem.Paths.FullPath, pFieldName));
            if (lField == null)
            {
                return new List<Item>();
            }
            var lItems = new List<Item>();
            //TypeKey contains the name of the field as it is defined in fieldtypes.config (but in lowercase)
            switch (lField.Definition.TypeKey)
            {
                case "multilist":
                case "treelist":
                case "tree list":
                case "checklist":
                case "treelistex":
                case "topictreelistex":
                    MultilistField lMultilistField = lField;

                    // Instead of doing: lItems.AddRange(lMultilistField.GetItems()); we'll use the following construction
                    // pItem.Database.GetItem(lId,pItem.Language). We get the traget for the same language as the item, cause when
                    // we're in a pipeline the context.language is 'en' even if we're requesting the items for a multilist for a dutch item. 
                    // That's why we do this explicitly
                    foreach (ID lID in lMultilistField.TargetIDs)
                    {
                        Item lItem = pGetItemFunction(lID);
                        if (lItem != null)
                        {
                            lItems.Add(lItem);
                        }
                    }

                    break;
                default:
                    throw new Exception(
                        string.Format("Field {1} of Item {0} is of fieldtype {2}, which is not specifically handled by extention method 'GetSelectedItems'",
                                      pItem.Paths.FullPath, pFieldName, lField.Definition.TypeKey));
            }
            return lItems;
        }

        public static string AbsoluteUrl(this Item pItem)
        {
            string urlOfItem = LinkManager.GetItemUrl(pItem);
            return string.Format("{0}://{1}{2}", HttpContext.Current.Request.Url.Scheme, HttpContext.Current.Request.Url.Authority, urlOfItem);
        }

        /// <summary>
        /// Same as InheritsTemplate, but throws an ArgumentException if that returns false
        /// </summary>
        /// <param name="pItem">The item to check</param>
        /// <param name="pShouldBeTemplateName">The template that it should inherit</param>
        public static void CheckInheritsTemplate(this Item pItem, string pShouldBeTemplateName)
        {
            if (!pItem.InheritsTemplate(pShouldBeTemplateName))
            {
                throw new ArgumentException(string.Format("The item's template was of type {0}, but should be of type {1}",
                                                          pItem.TemplateName,
                                                          pShouldBeTemplateName));
            }
        }

        /// <summary>
        /// Same as InheritsTemplate, but throws an ArgumentException if that returns false
        /// </summary>
        /// <param name="pItem">The item to check</param>
        /// <param name="pShouldBeTemplateName">The template that it should inherit</param>
        public static void CheckInheritsTemplate(this Item pItem, TemplateID pTemplate)
        {
            if (!pItem.InheritsTemplate(pTemplate))
            {
                throw new ArgumentException(string.Format("The item's template was of type {0}, but should be of type {1}",
                                                          pItem.TemplateName,
                                                          pTemplate));
            }
        }

        /// <summary>
        /// Checks if the items enherits a template by TemplateID
        /// </summary>
        /// <param name="pItem"></param>
        /// <param name="pTargetTemplateId">Template ID</param>
        /// <returns></returns>
        public static bool InheritsTemplate(this Item pItem, TemplateID pTargetTemplateId)
        {
            return pItem != null && InnerInheritsTemplate(TemplateManager.GetTemplate(pItem), pTargetTemplateId);
        }

        /// <summary>
        /// Checks if the items enherits a template by TemplateID
        /// </summary>
        /// <param name="pItem"></param>
        /// <param name="pTargetTemplateId">Template ID</param>
        /// <returns></returns>
        public static bool InheritsTemplate(this Item pItem, IEnumerable<TemplateID> pTargetTemplateIds)
        {
            return pItem != null &&
                pTargetTemplateIds.Aggregate(false, (current, pTargetTemplateId) => current || InnerInheritsTemplate(TemplateManager.GetTemplate(pItem), pTargetTemplateId));
        }

        /// <summary>
        /// checks if the items enherits a template
        /// </summary>
        /// <param name="pItem"></param>
        /// <param name="pTargetTemplateName"></param>
        /// <returns></returns>
        public static bool InheritsTemplate(this Item pItem, string pTargetTemplateName)
        {
            if (pItem == null || string.IsNullOrEmpty(pTargetTemplateName))
            {
                return false;
            }
            else
            {
                return InnerInheritsTemplate(TemplateManager.GetTemplate(pItem), pTargetTemplateName);
            }
        }

        /// <summary>
        /// checks if the items enherits a template collection
        /// </summary>
        /// <param name="pItem"></param>
        /// <param name="pTargetTemplateNames"></param>
        /// <returns></returns>
        public static bool InheritsTemplate(this Item pItem, ICollection<string> pTargetTemplateNames)
        {
            if (pItem == null || pTargetTemplateNames == null || pTargetTemplateNames.Count == 0)
            {
                return false;
            }
            Template lTemplate = TemplateManager.GetTemplate(pItem);
            foreach (string lTargetTemplate in pTargetTemplateNames)
            {
                if (InnerInheritsTemplate(lTemplate, lTargetTemplate))
                {
                    return true;
                }
            }
            return false;
        }

        private static bool InnerInheritsTemplate(Template pTemplateItem, TemplateID pTargetTemplateId)
        {
            if (pTemplateItem == null)
            {
                return false;
            }
            if (pTemplateItem.ID.Equals(pTargetTemplateId.ID))
            {
                return true;
            }

            foreach (Template lBaseTemplate in pTemplateItem.GetBaseTemplates())
            {
                if (lBaseTemplate.ID.Equals(pTargetTemplateId.ID))
                {
                    return true;
                }
            }
            return false;
        }

        private static bool InnerInheritsTemplate(Template pTemplateItem, string pTargetTemplateName)
        {
            if (pTemplateItem == null)
            {
                return false;
            }
            if (pTemplateItem.Name.EqualsIgnoreCase(pTargetTemplateName))
            {
                return true;
            }

            foreach (Template lBaseTemplate in pTemplateItem.GetBaseTemplates())
            {
                if (lBaseTemplate.Name.EqualsIgnoreCase(pTargetTemplateName))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// checks if the items is of a template collection
        /// </summary>
        /// <param name="pItem"></param>
        /// <param name="pTargetTemplateNames"></param>
        /// <returns></returns>
        public static bool IsOfTemplate(this Item pItem, IList<string> pTargetTemplateNames)
        {
            if (pItem == null || pTargetTemplateNames == null || pTargetTemplateNames.Count == 0)
            {
                return false;
            }
            foreach (string lTargetTemplate in pTargetTemplateNames)
            {
                if (InnerIsOfTemplate(pItem.Template, lTargetTemplate))
                {
                    return true;
                }
            }
            return false;
        }

        private static bool InnerIsOfTemplate(TemplateItem pTemplateItem, string pTargetTemplateName)
        {
            if (pTemplateItem.Name.EqualsIgnoreCase(pTargetTemplateName))
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// gets the shortest url of an item
        /// </summary>
        /// <param name="pItem"></param>
        /// <returns></returns>
        public static string GetFriendliestUrl(this Item pItem)
        {
            return GetFriendliestUrlPrivate(pItem, false);
        }
        private static string GetFriendliestUrlPrivate(Item pItem, bool pThroughPipeline)
        {
            var lUrl = string.Empty;
            //var lCacheKey = string.Format("{0}<{1}><{2}><{3}><{4}>", pItem.ID, pItem.Database.Name, pItem.Language.Name, pItem.Statistics.Updated, Language.Current);
            //var lUrl = SitecoreCache.GetValue<string>("IQuality.FriendliestUrlCache", lCacheKey);
            //if (!string.IsNullOrEmpty(lUrl))
            //{
            //    return lUrl;
            //}
            if (pItem != null)
            {
                lUrl = LinkManager.GetItemUrl(pItem);
            }

            //if (pThroughPipeline)
            //{
            //    var lArgs = new GetFriendliestUrlPipelineArgs { Item = pItem, Url = lUrl };
            //    Pipeline.Start("getFriendliestUrl", lArgs, true);
            //    lUrl = lArgs.Url;
            //}
            //lUrl = lUrl.ToLowerInvariant();
            //SitecoreCache.SetValue("IQuality.FriendliestUrlCache", lCacheKey, lUrl);
            return lUrl.ToLowerInvariant();
        }
        /// <summary>
        /// gets the url of an target (link or media pItem)
        /// </summary>
        /// <param name="pItem"></param>
        /// <param name="pFieldName"></param>
        /// <returns></returns>
        public static string GetFriendliestUrl(this Item pItem, string pFieldName)
        {
            return pItem.GetFriendliestUrl(pFieldName, false);
        }
        public static string GetFriendliestUrl(this Item pItem, string pFieldName, bool pIncludeServer)
        {
            if (pItem == null)
            {
                Log.Error(string.Format("null item passed, field is {0}.", pFieldName), typeof(ItemExtensions));
                return "#error getting url - item null.";
            }
            Field lField = pItem.Fields[pFieldName];
            if (lField == null)
            {
                Log.Error(string.Format("Field passed ({1}) did not extist on item({0}).", pItem.Paths.FullPath, pFieldName), typeof(ItemExtensions));
                return "#error getting url - field null.";
            }
            string url = string.Empty;
            bool makeUrlLowerCase = true;
            string lQueryString = null;
            Item item = lField.GetItemInforByField(pItem.Database, ref url, ref makeUrlLowerCase, ref lQueryString);
            if (item != null)
            {
                if (item.IsInMediaLibrary())
                {
                    url = MediaManager.GetMediaUrl(item);
                }
                else
                {
                    ItemUrlBuilderOptions lUrlOptions = new ItemUrlBuilderOptions();
                    lUrlOptions.SiteResolving = true;
                    url = LinkManager.GetItemUrl(item, lUrlOptions);
                }
                if (!string.IsNullOrWhiteSpace(lQueryString))
                {
                    url = WebUtil.AddQueryString(url, lQueryString.Split(new[] { "&amp;", "&", "=" }, StringSplitOptions.None));
                }
            }

            if (pIncludeServer && !string.IsNullOrEmpty(url))
            {
                url = WebUtil.GetFullUrl(url);
            }
            return makeUrlLowerCase ? url.ToLowerInvariant() : url;
        }
        private static Item GetItemInforByField(this Field lField, Database database, ref string url, ref bool makeUrlLowerCase, ref string lQueryString)
        {
            Item item = null;
            switch (lField.TypeKey)
            {
                case "file":
                    item = ((FileField)lField).MediaItem;
                    break;
                case "image":
                    item = ((FileField)lField).MediaItem;
                    break;
                case "internal link":
                    item = ((InternalLinkField)lField).TargetItem;
                    break;
                case "general link":
                    LinkField lLinkField = lField;
                    if (!string.IsNullOrWhiteSpace(lLinkField.QueryString))
                    {
                        lQueryString = lLinkField.QueryString;
                    }
                    item = lLinkField.GetItemInforByLinkField(database, ref url, ref makeUrlLowerCase);
                    break;
                default:
                    // all other lField.TypeKeys
                    item = ((LinkField)lField).TargetItem;
                    break;
            }
            return item;
        }
        private static Item GetItemInforByLinkField(this LinkField lLinkField, Database database, ref string url, ref bool makeUrlLowerCase)
        {
            Item item = null;
            if (lLinkField.LinkType == "internal")
            {
                item = lLinkField.TargetItem;
            }
            else if (lLinkField.LinkType == "media")
            {
                item = lLinkField.TargetItem ?? database.GetItem(lLinkField.TargetID);
            }
            else if (lLinkField.LinkType == "anchor")
            {
                if (lLinkField.Anchor.Contains("="))
                {
                    url = Context.Item.IsSiteStartItem() ? "?" + lLinkField.Anchor : Context.Item.GetFriendliestUrl() + "?" + lLinkField.Anchor;
                }
                else
                {
                    url = Context.Item.IsSiteStartItem() ? "#" + lLinkField.Anchor : Context.Item.GetFriendliestUrl() + "#" + lLinkField.Anchor;
                }
            }
            else if (lLinkField.LinkType == "javascript" || lLinkField.LinkType == "external")
            {
                makeUrlLowerCase = false;
                url = lLinkField.Url;
            }
            else
            {
                item = lLinkField.TargetItem;
            }
            return item;
        }
        /// <summary>
        /// Gets full Url of the sitecore Item
        /// </summary>
        /// <param name="pItem">Item to get Url of</param>
        /// <returns></returns>
        public static string GetFriendliestFullUrl(this Item pItem)
        {
            return WebUtil.GetFullUrl(pItem.GetFriendliestUrl());
        }
        public static string GetFriendliestFullUrlForIneo(this Item pItem)
        {
            var url = pItem.GetFriendliestUrl();
            if (pItem.IsInMediaLibrary())
            {
                url = MediaManager.GetMediaUrl(pItem);
            }
            return WebUtil.GetFullUrl(url);
        }
        public static string GetFriendliestUrlWithServer(this Item pItem)
        {
            ItemUrlBuilderOptions lUrlOptions = new ItemUrlBuilderOptions();
            lUrlOptions.SetDefaultOptions(new DefaultItemUrlBuilderOptions());
            lUrlOptions.AlwaysIncludeServerUrl = true;

            return LinkManager.GetItemUrl(pItem, lUrlOptions).ToLowerInvariant();
        }

        /// <summary>
        /// returns a string for debugging use
        /// </summary>
        /// <param name="pItem"></param>
        /// <returns></returns>
        public static bool IsSiteStartItem(this Item pItem)
        {
            return pItem.Paths.FullPath.EqualsIgnoreCase(Context.Site.StartPath);
        }

        public static bool HasField(this Item pItem, string pFieldName)
        {
            return pItem != null && pItem.Fields[pFieldName] != null;
        }
        public static ICollection<Item> GetChildrenOfTemplate(this Item pItem, string pTemplateName)
        {
            return (pItem.GetChildren().OfType<Item>().Where(pSubItem => pSubItem.TemplateName.Equals(pTemplateName))).ToList();
        }
        public static ICollection<Item> GetChildrenOfTemplate(this Item pItem, TemplateID pTemplateId)
        {
            return (pItem.GetChildren().OfType<Item>().Where(pSubItem => pSubItem.TemplateID.Guid.Equals(pTemplateId.ID.Guid))).ToList();
        }
        public static ICollection<Item> GetChildrenOfTemplates(this Item pItem, IEnumerable<string> pTemplateNames)
        {
            var lResult = new List<Item>();
            foreach (Item lChild in pItem.GetChildren())
            {
                if (pTemplateNames.Contains(lChild.TemplateName))
                {
                    lResult.Add(lChild);
                }
            }
            return lResult;
        }

        public static ICollection<Item> GetChildrenOfTemplate(this Item pItem, string pTemplateName, int pLevelsDeep)
        {
            List<Item> lResult = new List<Item>();
            lResult.AddRange(pItem.GetChildrenOfTemplate(pTemplateName));
            if (pLevelsDeep > 0)
            {
                foreach (Item lChild in pItem.GetChildren().InnerChildren)
                {
                    lResult.AddRange(lChild.GetChildrenOfTemplate(pTemplateName, --pLevelsDeep));
                }
            }
            return lResult;
        }

        public static ICollection<Item> GetChildrenInheritOfTemplates(this Item pItem, IList<string> pTemplateNames, int pLevelsDeep)
        {
            return pItem.GetChildren().Where(pChild =>
                pTemplateNames.Any(pTemplate => pChild.InheritsTemplate(pTemplate))).ToList();
        }

        public static ICollection<Item> GetChildrenInheritingTemplate(this Item pItem, string pTemplateName)
        {
            return (from lSubItem in pItem.GetChildren().OfType<Item>()
                    where lSubItem.InheritsTemplate(pTemplateName)
                    select lSubItem).ToList();
        }
        public static ICollection<Item> GetChildrenInheritingTemplate(this Item pItem, string pName, Language pLanguageContext)
        {
            return pItem.GetChildrenInheritingTemplate(pName)
                .Select(pChildItem => pChildItem.GetInLanguage(pLanguageContext))
                .Where(pChildItem => pChildItem != null).ToList();
        }
        public static bool FieldHasValue(this Item pItem, string pFieldName)
        {
            return pItem != null && !string.IsNullOrEmpty(pItem[pFieldName]);
        }
        public static bool IsInMediaLibrary(this Item pItem)
        {
            var lSegments = pItem.Paths.FullPath.Split(new[] { '/' }, 4);
            return lSegments.Length > 2 && lSegments[2].EqualsIgnoreCase("media library");
        }

        public static Item GetFirstParentOfTemplate(this Item pItem, TemplateID pTemplateId)
        {
            while (pItem != null && pItem.Parent != null)
            {
                if (pItem.InheritsTemplate(pTemplateId))
                {
                    return pItem;
                }
                pItem = pItem.Parent;
            }
            return null;
        }

        public static Item GetFirstParentOfTemplate(this Item pItem, string pTemplateName)
        {
            while (pItem != null && pItem.Parent != null)
            {
                if (pItem.InheritsTemplate(pTemplateName))
                {
                    return pItem;
                }
                pItem = pItem.Parent;
            }
            return null;
        }

        public static Item GetFirstParentOfTemplate(this Item pItem, ICollection<string> pTemplateNames)
        {
            while (pItem != null && pItem.Parent != null)
            {
                if (pItem.InheritsTemplate(pTemplateNames))
                {
                    return pItem;
                }
                pItem = pItem.Parent;
            }
            return null;
        }
        public static Item GetInCurrentSite(this Item pItem)
        {
            if (pItem == null)
            {
                return null;
            }
            var lStartItems = Factory.GetSiteInfoList().Where(si => !si.StartItem.IsNullOrEmpty()).Select(si => si.StartItem);
            foreach (var lStartItem in lStartItems)
            {
                if (pItem.Paths.ContentPath.StartsWith(lStartItem, StringComparison.InvariantCultureIgnoreCase))
                {
                    return pItem.Database.GetItem(Context.Site.ContentStartPath + pItem.Paths.ContentPath.Replace(lStartItem, Context.Site.StartItem));
                }
            }
            return null;
        }
        public static Item GetInLanguage(this Item pItem, Language pLanguage) => GetInLanguage(pItem, pLanguage, true);

        public static Item GetInLanguage(this Item pItem, Language pLanguage, bool withFallback)
        {
            if (pItem == null)
            {
                return null;
            }
            var item = pItem.Database.GetItem(pItem.ID, pLanguage);
            if (!withFallback && (item.IsFallback || item.Uri.Language != pLanguage))
            {
                return null;
            }
            return item;
        }

        public static Item GetPreviousSiblingOfTemplate(this Item pItem, IList<string> pTemplateNames)
        {
            ChildList lList = pItem.Parent.GetChildren();
            int lIndex = lList.IndexOf(pItem.ID);
            for (int i = lIndex - 1; i >= 0; i--)
            {
                if (lList[i].InheritsTemplate(pTemplateNames))
                {
                    return lList[i];
                }
            }
            return null;
        }
        public static Item GetNextSiblingOfTemplate(this Item pItem, IList<string> pTemplateNames)
        {
            ChildList lList = pItem.Parent.GetChildren();
            int lIndex = lList.IndexOf(pItem.ID) + 1;
            for (int i = lIndex; i < lList.Count && i > 0; i++)
            {
                if (lList[i].InheritsTemplate(pTemplateNames))
                {
                    return lList[i];
                }
            }
            return null;
        }
        public static IEnumerable<string> GetSelectedCountries(this Item pItem)
        {
            if (pItem.Fields["Countries"] != null)
            {
                var lCountriesField = FieldTypeManager.GetField(pItem.Fields["Countries"]) as MultilistField;
                //list of reference items that represent the the countries, convert to array of strings to easy intersect.
                return string.Join("|", (from id in lCountriesField.Items
                                         let lCountry = Config.GetDatabase().GetItem(id, LanguageManager.GetLanguage("en"))
                                         where lCountry != null && !string.IsNullOrWhiteSpace(lCountry["Short names"])
                                         select lCountry["Short names"].ToLowerInvariant())).Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries);
            }
            return new List<string>();
        }

        /// <summary>
        /// Gets the children of an item, but only those that are allowed by the language context.
        /// </summary>
        /// <param name="pItem">Item to get the children from</param>
        /// <param name="pLanguageContext">Language in which it is preferred to get the children, or any fallbacks</param>
        /// <returns>A collection of children which are allowed within the language context</returns>
        public static IEnumerable<Item> GetChildren(this Item pItem, Language pLanguageContext)
        {
            return pItem.Children.InnerChildren
                .Select(pChild => pChild.GetInLanguage(pLanguageContext))
                .Where(pChild => pChild != null);
        }
    }
}
