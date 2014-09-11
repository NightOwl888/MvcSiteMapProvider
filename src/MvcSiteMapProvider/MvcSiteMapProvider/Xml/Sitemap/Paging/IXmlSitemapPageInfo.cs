using System;

namespace MvcSiteMapProvider.Xml.Sitemap.Paging
{
    public interface IXmlSitemapPageInfo
    {
        int Page { get; }
        DateTime LastModifiedDate { get; }
    }
}
