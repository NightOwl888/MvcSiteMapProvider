using System;
using System.Collections.Generic;

namespace MvcSiteMapProvider.Xml.Sitemap.Paging
{
    public interface IXmlSitemapPageData
    {
        int TotalRecordCount { get; }
        IEnumerable<IXmlSitemapPageInfo> Pages { get; }
    }
}
