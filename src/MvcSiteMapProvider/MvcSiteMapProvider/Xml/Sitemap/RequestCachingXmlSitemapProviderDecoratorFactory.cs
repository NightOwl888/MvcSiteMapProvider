using System;
using MvcSiteMapProvider.Caching;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    public class RequestCachingXmlSitemapProviderDecoratorFactory
        : IRequestCachingXmlSitemapProviderDecoratorFactory
    {
        public RequestCachingXmlSitemapProviderDecoratorFactory(
            IRequestCache requestCache
            )
        {
            if (requestCache == null)
                throw new ArgumentNullException("requestCache");

            this.requestCache = requestCache;
        }
        private readonly IRequestCache requestCache;

        public IXmlSitemapProvider Create(IXmlSitemapProvider xmlSitemapProvider)
        {
            return new RequestCachingXmlSitemapProviderDecorator(xmlSitemapProvider, this.requestCache);
        }
    }
}
