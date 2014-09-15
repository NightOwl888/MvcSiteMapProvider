using System;
using MvcSiteMapProvider.ComponentModel;

namespace MvcSiteMapProvider.Xml.Sitemap.Configuration
{
    public interface IXmlSitemapFeedUrlResolverBuilder
        : IFluentInterface
    {
        IXmlSitemapFeedUrlResolverBuilder WithXmlSitemapUrlResolver(IXmlSitemapUrlResolver xmlSitemapUrlResolver);

        IXmlSitemapFeedUrlResolverBuilder WithXmlSitemapFeedPageNameProvider(IXmlSitemapFeedPageNameProvider xmlSitemapFeedPageNameProvider);

        IXmlSitemapFeedUrlResolver Create();
    }
}
