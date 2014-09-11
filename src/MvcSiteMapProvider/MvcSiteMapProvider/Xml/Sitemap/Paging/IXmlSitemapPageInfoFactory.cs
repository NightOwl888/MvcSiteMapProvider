using System;

namespace MvcSiteMapProvider.Xml.Sitemap.Paging
{
    public interface IXmlSitemapPageInfoFactory
    {
        IXmlSitemapPageInfo Create(int page, DateTime lastModifiedDate);
    }
}
