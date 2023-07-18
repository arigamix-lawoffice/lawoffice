using System;
using System.Globalization;
using System.Xml.Linq;

namespace Tessa.Extensions.Default.Server.Test
{
    public static class Xml1CProvider
    {
        public static string GetXml(string name, string driver)
        {
            var element = new XElement(
                "doc",
                new XElement("name", name),
                new XElement("driver", driver),
                new XElement("time", DateTime.Now.ToString("T", CultureInfo.InvariantCulture)));

            return element.ToString();
        }
    }
}
