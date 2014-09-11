using System;
using System.Collections.Generic;

namespace MvcSiteMapProvider.Xml.Sitemap.Paging
{
    public class XmlSitemapPageDataFactory
        : IXmlSitemapPageDataFactory
    {
        public IXmlSitemapPageData Create(int totalRecordCount, IEnumerable<IXmlSitemapPageInfo> pages)
        {
            return new XmlSitemapPageData(totalRecordCount, pages);
        }
    }
}
