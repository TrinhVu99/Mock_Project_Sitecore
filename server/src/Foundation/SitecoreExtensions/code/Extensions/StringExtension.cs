using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MockProject.Foundation.SitecoreExtensions.Extensions
{
    public static class StringExtension
    {

        /// <summary>
        /// NewLines must be \r\n. But in the import, sometimes \n is used.
        /// This method ensures that all \n characters are preceded by a \r character.
        /// </summary>
        /// <param name="pSource">Source string.</param>
        /// <returns>The same string, but with valid newlines.</returns>
        public static string EnsureNewLines(this string pSource)
        {
            return pSource != null
                ? pSource.Replace("\r\n", "\n").Replace("\n", "\r\n")
                : null;
        }
        public static string StripHtmlTags(this string source)
        {
            int arrayIndex = 0;
            bool inside = false;
            char[] charArray = new char[source.Length];

            for (int i = 0; i < source.Length; i++)
            {
                char letter = source[i];

                if (letter == '<')
                {
                    inside = true;
                    continue;
                }

                if (letter == '>')
                {
                    inside = false;
                    continue;
                }

                if (!inside)
                {
                    charArray[arrayIndex] = letter;
                    arrayIndex++;
                }
            }

            return new string(charArray, 0, arrayIndex);
        }
        public static string GetValidSitesoreQueryPath(this string query)
        {
            //Escape xpath keywords from sitecore query  
            return query.ToLower().Replace(" and ", " #and# ").Replace(" or ", " #or# ")
                .Replace(" true ", "#true# ").Replace(" false ", " #false# ")
                .Replace(" div ", " #div# ").Replace(" mod ", " #mod# ").Replace(" - ", " #-# ")
                .Replace("-", " #-# ").Replace("$", " #$# ");
        }
        public static string FormatString(this string temp)
        {
            return temp.Replace("  ", " ").Replace("%20", "-").Replace(" ", "-").ToLower();
        }
        public static string ToMediaLinkServerUrl(this string mediaUrl)
        {
            return !string.IsNullOrEmpty(mediaUrl) ? CombineUriToString(mediaUrl) : string.Empty;
        }
        public static string[] ToMediaLinkServerUrls(this string[] mediaUrls)
        {
            return (mediaUrls != null && mediaUrls.Length > 0) ? mediaUrls.Select(t => t.ToMediaLinkServerUrl()).ToArray() : new string[] { };
        }
        public static string CombineUriToString(string relativeOrAbsoluteUri)
        {
            Uri uriResult;
            if (Uri.TryCreate(relativeOrAbsoluteUri, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
            {
                return uriResult.ToString();
            }
            string baseUri = Sitecore.Context.Site.GetSettingValue("Media.MediaLinkServerUrl");
            if (Uri.TryCreate(baseUri, UriKind.Absolute, out uriResult)
                && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps))
            {
                return new Uri(uriResult, relativeOrAbsoluteUri ?? string.Empty).ToString();
            }
            return relativeOrAbsoluteUri ?? string.Empty;
        }

        public static bool EqualsIgnoreCase(this string pValue, string pOtherValue)
        {
            return pValue.Equals(pOtherValue, StringComparison.OrdinalIgnoreCase);
        }
        /// <summary>
        /// remove all characters in pCharsToReplace
        /// </summary>
        /// <param name="pSource"></param>
        /// <param name="pCharsToRemove"></param>
        /// <param name="escape"></param>
        /// <returns></returns>
        public static string RemoveChars(this string pSource, char[] pCharsToRemove)
        {
            StringBuilder s = new StringBuilder();
            int j = 0;
            int i = pSource.IndexOfAny(pCharsToRemove);
            while (i != -1)
            {
                s.Append(pSource.Substring(j, i - j));
                j = i;
                i = pSource.IndexOfAny(pCharsToRemove, j + 1);
            }
            s.Append(pSource.Substring(j));
            return s.ToString();
        }
    }
}
