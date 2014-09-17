using System;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    public interface IXmlSitemapUrlResolverFactory
    {
        IXmlSitemapUrlResolver Create();
        void Release(IXmlSitemapUrlResolver xmlSitemapUrlResolver);
    }
}
