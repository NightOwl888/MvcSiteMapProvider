using System;
using System.Collections.Generic;
using System.Linq;
using MvcSiteMapProvider.ComponentModel;

namespace MvcSiteMapProvider.Xml.Sitemap.Configuration
{
    public interface IXmlSitemapFeedStrategyBuilder
        : IFluentInterface
    {
        IXmlSitemapFeedStrategyBuilder AddFeed(string feedName, Func<IXmlSitemapFeedBuilderFacade, IXmlSitemapFeedBuilderFacade> expression);

        IXmlSitemapFeedStrategyBuilder AddFeed(string feedName);

        IXmlSitemapFeedStrategyBuilder AddFeed(IXmlSitemapFeed xmlSitemapFeed);

        IXmlSitemapFeedStrategyBuilder AddFeeds(IEnumerable<IXmlSitemapFeed> xmlSitemapFeeds);

        IXmlSitemapFeedStrategyBuilder RemoveFeed(string feedName);

        IXmlSitemapFeedStrategyBuilder ClearFeeds();

        IEnumerable<IXmlSitemapFeed> XmlSitemapFeeds { get; }

        IXmlSitemapProviderFactory XmlSitemapProviderFactory { get; }

        string DefaultFeedRootPageName { get; }

        string DefaultFeedPageName { get; }

        string NamedFeedRootPageName { get; }

        string NamedFeedPageName { get; }

        IEnumerable<string> AssembliesToScanForXmlSitemapProvider { get; }

        IXmlSitemapFeedStrategy Create();
    }
}
