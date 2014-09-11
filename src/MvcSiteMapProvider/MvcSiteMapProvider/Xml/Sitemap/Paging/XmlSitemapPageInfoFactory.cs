using System;

namespace MvcSiteMapProvider.Xml.Sitemap.Paging
{
    public class XmlSitemapPageInfoFactory
        : IXmlSitemapPageInfoFactory
    {
        public IXmlSitemapPageInfo Create(int page, DateTime lastModifiedDate)
        {
            return new XmlSitemapPageInfo(page, lastModifiedDate);
        }
    }
}
