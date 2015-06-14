namespace Errs.WebUi.Infrastructure.Utility
{
    using System.Linq;
    using System.Xml.Linq;
    using System.Collections.Generic;

    public static class StringExtensions
    {
        public static Dictionary<string, string> GetProperties(this string data)
        {
            var doc = XDocument.Parse(data);
            var dataDictionary = new Dictionary<string, string>();

            foreach (XElement element in doc.Descendants().Where(p => p.HasElements == false))
            {
                int keyInt = 0;
                string keyName = element.Name.LocalName;

                while (dataDictionary.ContainsKey(keyName))
                {
                    keyName = element.FirstAttribute.Value; //.Name.LocalName + "_" + keyInt++;
                }

                dataDictionary.Add(keyName, element.Value);
            }
            return dataDictionary;
        }
    }
}