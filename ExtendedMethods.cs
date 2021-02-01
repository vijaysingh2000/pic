using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
namespace InsuranceCalculator
{
    public static class ExtentionMethods
    {
        public static bool IsStringEqual(this string compareText1, string compareText2)
        {
            if (compareText2 == null)
                compareText2 = string.Empty;

            bool returnVal = string.Compare(compareText1.Trim(), compareText2.Trim(), StringComparison.OrdinalIgnoreCase) == 0 ? true : false;
            return returnVal;
        }
        public static bool IsStringContains(this string compareText1, string comparetext2)
        {
            return compareText1.ToUpper().Trim().Contains(comparetext2.Trim().ToUpper());
        }
        public static bool IsStringContainsAny(this string compareText1, params string[] compareTexts)
        {
            foreach (string sCompare in compareTexts)
            {
                if (compareText1.IsStringContains(sCompare))
                    return true;
            }

            return false;
        }
        public static string ReplaceAll(this string sourceText, params string[] replacedTexts)
        {
            int i = 0;
            string sText = string.Empty;
            string sReplaceText = string.Empty;
            int cnt = replacedTexts.Count();

            while (i < cnt)
            {
                sText = replacedTexts[i];

                if (i + 1 < cnt)
                {
                    sReplaceText = replacedTexts[i + 1];
                    sourceText = sourceText.Replace(sText, sReplaceText);
                }

                i = i + 2;
            }

            return sourceText;
        }
        public static string GetAttribute(this XElement element, string attributeName)
        {
            if (element == null || string.IsNullOrEmpty(attributeName))
                return string.Empty;

            XAttribute attribute = element.Attribute(attributeName);
            if (attribute != null)
                return attribute.Value;

            return string.Empty;
        }
        public static string GetElementValue(this IEnumerable<XElement> items, string key, string keyAttributeName = "key", string valueAttributeName = "value")
        {
            XElement item = items.FirstOrDefault(x => x.GetAttribute(keyAttributeName).IsStringEqual(key));
            if (item != null)
                return item.GetAttribute(valueAttributeName);

            return string.Empty;
        }
    }
}
