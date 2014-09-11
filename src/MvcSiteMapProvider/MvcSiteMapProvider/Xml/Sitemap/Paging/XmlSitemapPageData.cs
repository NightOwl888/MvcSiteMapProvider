using System;
using System.Collections.Generic;

namespace MvcSiteMapProvider.Xml.Sitemap.Paging
{
    public class XmlSitemapPageData
        : IXmlSitemapPageData
    {
        public XmlSitemapPageData(
            int totalRecordCount,
            IEnumerable<IXmlSitemapPageInfo> pages
            )
        {
            if (totalRecordCount < 0)
                throw new ArgumentOutOfRangeException("totalRecordCount");
            if (pages == null)
                throw new ArgumentNullException("pages");

            this.TotalRecordCount = totalRecordCount;
            this.Pages = pages;
        }

        public int TotalRecordCount { get; private set; }

        public IEnumerable<IXmlSitemapPageInfo> Pages { get; private set; }
    }
}
