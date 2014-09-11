using System;
using System.Collections.Generic;

namespace MvcSiteMapProvider.Xml.Sitemap.Paging
{
    public interface IXmlSitemapPageDataFactory
    {
        IXmlSitemapPageData Create(int totalRecordCount, IEnumerable<IXmlSitemapPageInfo> pages);
    }
}
