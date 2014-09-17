using System;
using MvcSiteMapProvider.ComponentModel;

namespace MvcSiteMapProvider.Xml.Sitemap.Configuration
{
    public interface IXmlSitemapFeedUrlResolverBuilder
        : IFluentInterface
    {
        IXmlSitemapFeedUrlResolverBuilder WithXmlSitemapUrlResolverFactory(IXmlSitemapUrlResolverFactory xmlSitemapUrlResolverFactory);

        IXmlSitemapFeedUrlResolverBuilder WithXmlSitemapFeedPageNameProvider(IXmlSitemapFeedPageNameProvider xmlSitemapFeedPageNameProvider);

        IXmlSitemapFeedUrlResolver Create();
    }
}
