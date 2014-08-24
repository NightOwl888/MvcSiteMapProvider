using System;
using System.Collections.Generic;
using System.Xml;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    public interface ISitemapPageManager
    {
        IEnumerable<int> GetPageNumbers();
        bool WritePage(int page, XmlWriter writer);
    }
}
