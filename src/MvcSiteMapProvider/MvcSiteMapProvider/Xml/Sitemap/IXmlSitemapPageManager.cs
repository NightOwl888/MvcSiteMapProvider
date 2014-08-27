using System;
using System.Collections.Generic;
using System.Xml;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    public interface IXmlSitemapPageManager
    {
        IEnumerable<int> GetPageNumbers(string feedName);
        bool WritePage(XmlWriter writer, string feedName, int page);
    }
}
