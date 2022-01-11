using Sitecore.Collections;
using Sitecore.Data;
using Sitecore.Data.Items;
using Sitecore.Data.Fields;
using Sitecore.Resources.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sitecore.Links;
using Sitecore.Data.Managers;
using MockProject.Foundation.SitecoreExtensions.Helpers;
using Sitecore.Links.UrlBuilders;

namespace MockProject.Foundation.SitecoreExtensions.Extensions
{
    public static class FieldExtensions
    {
        #region Boolean field

        public static bool GetBooleanField(this Item item, string field)
        {
            return item.GetField<CheckboxField>(field).Checked;
        }

        public static bool GetBooleanField(this Item item, int index)
        {
            return item.GetField<CheckboxField>(index).Checked;
        }

        public static bool GetBooleanField(this Item item, ID fieldId)
        {
            return item.GetField<CheckboxField>(fieldId).Checked;
        }

        #endregion

        #region File Field

        public static Item GetFileFieldMediaItem(this Item item, string field)
        {
            return item.GetField<FileField>(field).MediaItem;
        }

        public static Item GetFileFieldMediaItem(this Item item, int index)
        {
            return item.GetField<FileField>(index).MediaItem;
        }

        public static Item GetFileFieldMediaItem(this Item item, ID fieldId)
        {
            return item.GetField<FileField>(fieldId).MediaItem;
        }

        public static string GetFileFieldMediaUrl(this Item item, string field, MediaUrlBuilderOptions options)
        {
            return item.GetField<FileField>(field).MediaItem.GetMediaUrl(options);
        }
        public static string GetFileFieldMediaUrl(this Item item)
        {
            return GetFileFieldMediaUrl(item, null);
        }

        public static string GetFileFieldMediaUrl(this Item item, int index, MediaUrlBuilderOptions options)
        {
            return item.GetField<FileField>(index).MediaItem.GetMediaUrl(options);
        }
        public static string GetFileFieldMediaUrl(this Item item, int index)
        {
            return GetFileFieldMediaUrl(item, index, null);
        }

        public static string GetFileFieldMediaUrl(this Item item, ID fieldId, MediaUrlBuilderOptions options)
        {
            return item.GetField<FileField>(fieldId).MediaItem.GetMediaUrl(options);
        }
        public static string GetFileFieldMediaUrl(this Item item, ID fieldId)
        {
            return GetFileFieldMediaUrl(item, fieldId, null);
        }

        #endregion

        #region Image Field

        public static string GetUrl(this Item item, string field)
        {
            var MediaUrlBuilderOptions = new MediaUrlBuilderOptions
            {
                AbsolutePath = false,
                AlwaysIncludeServerUrl = false,
            };
            var url = item.GetImageFieldMediaUrl(field, MediaUrlBuilderOptions) ?? string.Empty;
            return url;
        }

        public static Item GetImageFieldMediaItem(this Item item, string field)
        {
            return item.GetField<ImageField>(field).MediaItem;
        }

        public static Item GetImageFieldMediaItem(this Item item, int index)
        {
            return item.GetField<ImageField>(index).MediaItem;
        }

        public static Item GetImageFieldMediaItem(this Item item, ID fieldId)
        {
            return item.GetField<ImageField>(fieldId).MediaItem;
        }

        public static string GetImageFieldMediaUrl(this Item item, string field, MediaUrlBuilderOptions options)
        {
            if (string.IsNullOrEmpty(item.Fields[field]?.Value))
            {
                return null;
            }
            return item.GetField<ImageField>(field).MediaItem.GetMediaUrl(options) ?? string.Empty;
        }
        public static string GetImageFieldMediaUrl(this Item item, string field)
        {
            return GetImageFieldMediaUrl(item, field, null);
        }

        public static string GetImageFieldMediaUrl(this Item item, int index, MediaUrlBuilderOptions options)
        {
            return item.GetField<ImageField>(index).MediaItem.GetMediaUrl(options);
        }
        public static string GetImageFieldMediaUrl(this Item item, int index)
        {
            return GetImageFieldMediaUrl(item, index, null);
        }

        public static string GetImageFieldMediaUrl(this Item item, ID fieldId, MediaUrlBuilderOptions options)
        {
            return item.GetField<ImageField>(fieldId).MediaItem.GetMediaUrl(options);
        }
        public static string GetImageFieldMediaUrl(this Item item, ID fieldId)
        {
            return GetImageFieldMediaUrl(item, fieldId, null);
        }

        #endregion

        #region File Drop Area Field

        public static ItemList GetFileDropFieldMediaItems(this Item item, string field)
        {
            return item.GetField<FileDropAreaField>(field).GetMediaItems();
        }

        public static ItemList GetFileDropFieldMediaItems(this Item item, int index)
        {
            return item.GetField<FileDropAreaField>(index).GetMediaItems();
        }

        public static ItemList GetFileDropFieldMediaItems(this Item item, ID fieldId)
        {
            return item.GetField<FileDropAreaField>(fieldId).GetMediaItems();
        }

        public static Item GetFileDropFieldMediaFolder(this Item item, string field)
        {
            return item.GetField<FileDropAreaField>(field).MediaFolder;
        }

        public static Item GetFileDropFieldMediaFolder(this Item item, int index)
        {
            return item.GetField<FileDropAreaField>(index).MediaFolder;
        }

        public static Item GetFileDropFieldMediaFolder(this Item item, ID fieldId)
        {
            return item.GetField<FileDropAreaField>(fieldId).MediaFolder;
        }

        public static ID GetFileDropFieldMediaFolderID(this Item item, string field)
        {
            return item.GetField<FileDropAreaField>(field).MediaFolderID;
        }

        public static ID GetFileDropFieldMediaFolderID(this Item item, int index)
        {
            return item.GetField<FileDropAreaField>(index).MediaFolderID;
        }

        public static ID GetFileDropFieldMediaFolderID(this Item item, ID fieldId)
        {
            return item.GetField<FileDropAreaField>(fieldId).MediaFolderID;
        }

        public static IEnumerable<string> GetFileDropFieldMediaItemsUrl(this Item item, string field,
            MediaUrlBuilderOptions options)
        {
            return item.GetFileDropFieldMediaItems(field).Select(x => x.GetMediaUrl(options));
        }
        public static IEnumerable<string> GetFileDropFieldMediaItemsUrl(this Item item, string field)
        {
            return GetFileDropFieldMediaItemsUrl(item, field, null);
        }

        public static IEnumerable<string> GetFileDropFieldMediaItemsUrl(this Item item, int index,
            MediaUrlBuilderOptions options)
        {
            return item.GetFileDropFieldMediaItems(index).Select(x => x.GetMediaUrl(options));
        }
        public static IEnumerable<string> GetFileDropFieldMediaItemsUrl(this Item item, int index)
        {
            return GetFileDropFieldMediaItemsUrl(item, index, null);
        }

        public static IEnumerable<string> GetFileDropFieldMediaItemsUrl(this Item item, ID fieldId,
            MediaUrlBuilderOptions options)
        {
            return item.GetFileDropFieldMediaItems(fieldId).Select(x => x.GetMediaUrl(options));
        }
        public static IEnumerable<string> GetFileDropFieldMediaItemsUrl(this Item item, ID fieldId)
        {
            return GetFileDropFieldMediaItemsUrl(item, fieldId, null);
        }

        #endregion

        #region Date Field

        public static DateTime GetDateFieldValue(this Item item, string field)
        {
            return item.GetField<DateField>(field).DateTime.ToLocalTime();
        }

        #endregion

        #region Link Field

        public static string GetLinkFieldUrl(this Item item, string field, ItemUrlBuilderOptions options)
        {
            return item.GetField<LinkField>(field).GetLinkFieldUrl(options);
        }

        public static string GetGeneralLinkFieldUrl(this Item item, string field, ItemUrlBuilderOptions options)
        {
            LinkField linkField = item.Fields[field];
            if (linkField == null)
            {
                return null;
            }
            switch (linkField.LinkType.ToLower())
            {
                case "internal":
                    return linkField.TargetItem.GetPublicUrl(null);
                case "media":
                    return linkField.TargetItem != null ? MediaManager.GetMediaUrl(linkField.TargetItem, new MediaUrlBuilderOptions
                    {
                        AbsolutePath = false,
                    }) : string.Empty;
                case "external":
                    return linkField.Url;
                case "anchor":
                    return !string.IsNullOrEmpty(linkField.Anchor) ? "#" + linkField.Anchor : string.Empty;
                case "mailto":
                    return linkField.Url;
                case "javascript":
                    return linkField.Url;
                default:
                    return string.Empty;
            }
        }
        public static string GetTargetLinkFieldUrl(this Item item, string field)
        {
            LinkField linkField = item.Fields[field];
            if (linkField == null)
            {
                return null;
            }
            return linkField.Target.ToLower();
        }

        public static string GetLinkFieldUrl(this Item item, int index, ItemUrlBuilderOptions options)
        {
            return item.GetField<LinkField>(index).GetLinkFieldUrl(options);
        }

        public static string GetLinkFieldUrl(this Item item, ID fieldId, ItemUrlBuilderOptions options)
        {
            return item.GetField<LinkField>(fieldId).GetLinkFieldUrl(options);
        }

        public static string GetLinkFieldUrl(this LinkField linkField, ItemUrlBuilderOptions options)
        {
            string url;
            var targetItem = linkField.TargetItem;

            if (linkField.IsInternal && targetItem != null)
            {
                url = LinkManager.GetItemUrl(targetItem, options);

                if (!string.IsNullOrWhiteSpace(linkField.QueryString))
                {
                    url = string.Format("{0}?{1}", url, linkField.QueryString);
                }

                if (!string.IsNullOrWhiteSpace(linkField.Anchor))
                {
                    url = string.Format("{0}#{1}", url, linkField.Anchor);
                }
            }
            else if (linkField.IsMediaLink && targetItem != null)
            {
                url = targetItem.GetMediaUrl(options);
            }
            else
            {
                url = linkField.Url;
            }

            return url;
        }

        #endregion

        #region Reference Field

        public static Item GetReferenceItem(this Item item, string field)
        {
            return item.GetField<ReferenceField>(field).TargetItem;
        }

        public static Item GetReferenceItem(this Item item, int index)
        {
            return item.GetField<ReferenceField>(index).TargetItem;
        }

        public static Item GetReferenceItem(this Item item, ID fieldId)
        {
            return item.GetField<ReferenceField>(fieldId).TargetItem;
        }

        public static ID GetReferenceItemID(this Item item, string field)
        {
            return item.GetField<ReferenceField>(field).TargetID;
        }

        public static ID GetReferenceItemID(this Item item, int index)
        {
            return item.GetField<ReferenceField>(index).TargetID;
        }

        public static ID GetReferenceItemID(this Item item, ID fieldId)
        {
            return item.GetField<ReferenceField>(fieldId).TargetID;
        }

        #endregion

        #region Multilist Field

        public static Item[] GetMultilistFieldItems(this Item item, string field)
        {
            return item.GetField<MultilistField>(field).GetItems();
        }

        public static Item[] GetMultilistFieldItems(this Item item, int index)
        {
            return item.GetField<MultilistField>(index).GetItems();
        }

        public static Item[] GetMultilistFieldItems(this Item item, ID fieldId)
        {
            return item.GetField<MultilistField>(fieldId).GetItems();
        }

        public static ID[] GetMultilistFieldItemsID(this Item item, string field)
        {
            return item.GetField<MultilistField>(field).TargetIDs;
        }

        public static ID[] GetMultilistFieldItemsID(this Item item, int index)
        {
            return item.GetField<MultilistField>(index).TargetIDs;
        }

        public static ID[] GetMultilistFieldItemsID(this Item item, ID fieldId)
        {
            return item.GetField<MultilistField>(fieldId).TargetIDs;
        }

        #endregion

        #region Generic field type mapping

        public static T GetField<T>(this Item item, string field) where T : CustomField
        {
            var currentField = item.Fields[field];
            if (currentField != null)
            {
                return ConvertTo<T>(currentField);
            }

            var exception = string.Format("Item {0} does not has field {1}", item.Paths.FullPath, field);

            throw new KeyNotFoundException(exception);
        }

        public static T GetField<T>(this Item item, int index) where T : CustomField
        {
            var currentField = item.Fields[index];
            if (currentField != null)
            {
                return ConvertTo<T>(currentField);
            }

            var exception = string.Format("Item {0} has less than {1} fields", item.Paths.FullPath, index + 1);

            throw new IndexOutOfRangeException(exception);
        }

        public static T GetField<T>(this Item item, ID fieldId) where T : CustomField
        {
            var templateField = TemplateManager.GetTemplateField(fieldId, item);
            if (templateField != null)
            {
                return ConvertTo<T>(item.Fields[fieldId]);
            }

            var exception = string.Format("Item {0} does not has field {1}", item.Paths.FullPath, fieldId);

            throw new KeyNotFoundException(exception);
        }

        #endregion

        #region Raw value

        public static string GetFieldRawValue(this Item item, string field)
        {
            return item.Fields[field]?.Value;
        }

        public static string GetFieldRawValue(this Item item, int index)
        {
            return item.Fields[index].Value;
        }

        public static string GetFieldRawValue(this Item item, ID fieldId)
        {
            return item.Fields[fieldId].Value;
        }

        public static string TransformRelativeSourcePath(this string itemId, string source)
        {
            if (string.IsNullOrWhiteSpace(source) || string.IsNullOrWhiteSpace(itemId) || !source.StartsWith("query:"))
            {
                return source;
            }
            var item = ContextHelper.Database.GetItem(itemId);
            return item.Paths.FullPath + source.Substring(7);
        }

        public static string TransformRelativeSourcePath(this Item item, string source)
        {
            if (string.IsNullOrWhiteSpace(source) || item == null || !source.StartsWith("query:"))
            {
                return source;
            }

            return item.Paths.FullPath + source.Substring(7);
        }
        #endregion

        #region Private methods

        private static T ConvertTo<T>(Field field) where T : CustomField
        {
            var converter = typeof(T).GetMethod("op_Implicit");

            if (converter != null)
            {
                return (T)converter.Invoke(null, new object[] { field });
            }

            var exception = string.Format("Type {0} is not convertible to {1}", typeof(T).FullName,
                field.GetType().FullName);

            throw new InvalidCastException(exception);
        }

        #endregion

        public static void SetInternalLink(this LinkField pLinkField, Item pTarget)
        {
            if (pLinkField == null || pTarget == null)
            {
                return;
            }
            pLinkField.TargetID = pTarget.ID;
            pLinkField.Url = pTarget.Paths.ContentPath;
            pLinkField.LinkType = "internal";
        }
    }
}
