using System;
using System.Collections.Generic;
using System.Xml;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    public interface IXmlSitemapPageManager
    {
        IEnumerable<int> GetPageNumbers();
        bool WritePage(int page, XmlWriter writer);
    }
}
