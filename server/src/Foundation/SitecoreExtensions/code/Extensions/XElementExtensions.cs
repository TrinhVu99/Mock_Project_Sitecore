using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;

namespace MockProject.Foundation.SitecoreExtensions.Extensions
{
    public static class XElementExtensions
    {
        /// <summary>
        /// Map country codes from the import to the ISO compliant counterparts.
        /// </summary>
        private static readonly Dictionary<string, string> PlatformMappings = new Dictionary<string, string>()
                                                                  {
                                                                      {"UK", "GB"},
                                                                      {"E1", "AT"},
                                                                      {"IR", "IE"},
                                                                      {"S", "SE"},
                                                                  };
        /// <summary>
        /// Limit a list of countries or products by the limiting elements.
        /// E.g.: [BE,NL,US] limited by [BE,US] gives [BE,US]
        /// E.g.: [BE,NL,US] limited by [NO_BE] gives [NL,US]
        /// </summary>
        /// <param name="pBaseEntries">The list to limit</param>
        /// <param name="pLimitingEntries">The list of limiting entries</param>
        /// <returns>The base entries limited by the limiting entries</returns>
        public static IEnumerable<string> GetAllowedEntries(this IEnumerable<string> pBaseEntries, IEnumerable<string> pLimitingEntries)
        {
            IEnumerable<string> lNoEntries = from lEntry in pLimitingEntries where lEntry.StartsWith("NO_") select lEntry;
            IEnumerable<string> lDisallowedEntries = from lEntry in lNoEntries select lEntry.Substring(3);
            return (from lEntry in pBaseEntries
                    where !lDisallowedEntries.Contains(lEntry.ToUpper())
                          && (pLimitingEntries.Except(lNoEntries).Count() == 0
                              || pLimitingEntries.Except(lNoEntries).Contains(lEntry.ToUpper()))
                    select lEntry);
        }

        public static void LimitByAttribute(this XElement pElement, string pAttributeName, IEnumerable<string> pAttributeIndividualValues)
        {
            if (pElement == null)
            {
                return;
            }
            List<XElement> lElementsToRemove = new List<XElement>();
            pElement.LimitByAttributeRecursive(pAttributeName, pAttributeIndividualValues, lElementsToRemove);
            foreach (XElement lElementToRemove in lElementsToRemove)
            {
                lElementToRemove.Remove();
            }
        }

        private static void LimitByAttributeRecursive(this XElement pElement, string pAttributeName, IEnumerable<string> pAttributeIndividualValues, List<XElement> pElementsToRemove)
        {
            if (pElement.Attribute(pAttributeName) != null && !String.IsNullOrEmpty(pElement.Attribute(pAttributeName).Value))
            {
                string[] lLimitingEntries = pElement.Attribute(pAttributeName).Value.Split(new[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                if (pAttributeIndividualValues.GetAllowedEntries(lLimitingEntries).Count() == 0)
                {
                    pElementsToRemove.Add(pElement);
                    return;
                }
            }
            foreach (XElement lChildElement in pElement.Elements())
            {
                lChildElement.LimitByAttributeRecursive(pAttributeName, pAttributeIndividualValues, pElementsToRemove);
            }
        }
        /// <summary>
        /// Some platform names are not consistent with the ISO codes.
        /// E.g. If the XML includes UK or NO_UK platform attributes, this method will replace them with the correct GB or NO_GB equivalents.
        /// 
        /// All mappings:
        /// England -> UK -> GB
        /// Austria -> E1 -> AT
        /// Ireland -> IR -> IE
        /// Sweden -> S -> SE
        /// </summary>
        /// <param name="pRoot">Root element to start replacement for</param>
        public static void FixPlatformNames(this XElement pRoot)
        {
            IEnumerable<XAttribute> lPlatformAttributes = from lElement in pRoot.DescendantNodesAndSelf()
                                                          where lElement.NodeType == XmlNodeType.Element
                                                                                    && ((XElement)lElement).Attribute("platform") != null
                                                          select ((XElement)lElement).Attribute("platform");
            foreach (XAttribute lPlatformAttribute in lPlatformAttributes.ToList())
            {
                foreach (KeyValuePair<string, string> lPlatformMapping in PlatformMappings)
                {
                    lPlatformAttribute.Value = Regex.Replace(lPlatformAttribute.Value,
                        String.Format(@"\b{0}\b", lPlatformMapping.Key),
                        lPlatformMapping.Value,
                        RegexOptions.IgnoreCase);
                    lPlatformAttribute.Value = Regex.Replace(lPlatformAttribute.Value,
                        String.Format(@"\bNO_{0}\b", lPlatformMapping.Key),
                        String.Format("NO_{0}", lPlatformMapping.Value),
                        RegexOptions.IgnoreCase);
                }
            }
        }
        public static List<string> GetValidPlatformNames(List<string> pPlatformNames)
        {
            List<string> lValidPlatformNames = new List<string>();
            foreach (string pPlatformName in pPlatformNames)
            {
                if (PlatformMappings.ContainsKey(pPlatformName.ToUpper()) && !lValidPlatformNames.Contains(pPlatformName))
                {
                    lValidPlatformNames.Add(PlatformMappings[pPlatformName]);
                }
                else
                {
                    if (!lValidPlatformNames.Contains(pPlatformName))
                    {
                        lValidPlatformNames.Add(pPlatformName);
                    }
                }
            }
            return lValidPlatformNames;
        }
    }
}