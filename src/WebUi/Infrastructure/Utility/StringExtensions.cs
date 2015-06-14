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
            var dataDictionary = doc.Descendants("property")
                  .ToDictionary(d => (string)d.Attribute("key"),
                                d => (string)d);
            return dataDictionary;
        }
    }
}