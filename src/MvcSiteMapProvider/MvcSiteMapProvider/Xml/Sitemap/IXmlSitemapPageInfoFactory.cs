using System;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    public interface IXmlSitemapPageInfoFactory
    {
        IXmlSitemapPageInfo Create(int page, DateTime lastModifiedDate);
    }
}
