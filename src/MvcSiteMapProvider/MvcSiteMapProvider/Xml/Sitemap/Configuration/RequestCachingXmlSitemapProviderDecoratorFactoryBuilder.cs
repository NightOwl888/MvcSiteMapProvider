using System;
using MvcSiteMapProvider.Web.Mvc;
using MvcSiteMapProvider.Caching;

namespace MvcSiteMapProvider.Xml.Sitemap.Configuration
{
    public class RequestCachingXmlSitemapProviderDecoratorFactoryBuilder
        : IRequestCachingXmlSitemapProviderDecoratorFactoryBuilder
    {
        public RequestCachingXmlSitemapProviderDecoratorFactoryBuilder()
            : this(requestCache: new RequestCacheBuilder().Create())
        {
        }

        private RequestCachingXmlSitemapProviderDecoratorFactoryBuilder(
            IRequestCache requestCache
            )
        {
            if (requestCache == null)
                throw new ArgumentNullException("requestCache");

            this.requestCache = requestCache;
        }
        private readonly IRequestCache requestCache;

        public IRequestCachingXmlSitemapProviderDecoratorFactoryBuilder WithRequestCache(IRequestCache requestCache)
        {
            return new RequestCachingXmlSitemapProviderDecoratorFactoryBuilder(requestCache);
        }

        public IXmlSitemapProviderDecoratorFactory Create()
        {
            return new RequestCachingXmlSitemapProviderDecoratorFactory(this.requestCache);
        }
    }
}
