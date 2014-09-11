using System;
using System.Collections.Generic;
using System.Xml;
using MvcSiteMapProvider.Xml.Sitemap.Paging;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    public interface IXmlSitemapPageManager
    {
        IXmlSitemapPageData GetPageData(string feedName);
        bool WritePage(XmlWriter writer, string feedName, int page);
    }
}
