using System;
using MvcSiteMapProvider.Caching;

namespace MvcSiteMapProvider.Xml.Sitemap.Configuration
{
    public interface IRequestCachingXmlSitemapProviderDecoratorFactoryBuilder
    {
        IRequestCachingXmlSitemapProviderDecoratorFactoryBuilder WithRequestCache(IRequestCache requestCache);

        IXmlSitemapProviderDecoratorFactory Create();
    }
}
