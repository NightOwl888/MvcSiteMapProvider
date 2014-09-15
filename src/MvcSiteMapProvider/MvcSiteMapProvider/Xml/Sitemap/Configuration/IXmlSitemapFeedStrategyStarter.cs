using System;
using System.Collections.Generic;
using System.Linq;
using MvcSiteMapProvider.Reflection;

namespace MvcSiteMapProvider.Xml.Sitemap.Configuration
{
    public interface IXmlSitemapFeedStrategyStarter
        : IXmlSitemapFeedStrategyBuilder
    {
        // Globals:
        // IXmlSitemapUrlResolver
        // IAttributeAssemblyProvider (with include assemblies)
        // ICultureContextFactory
        // IXmlSitemapFeedUrlResolver (and IXmlSitemapFeedPageNameProvider, IXmlSitemapUrlResolver)

        //IXmlSitemapFeedStrategyStarter WithDefaultFeedRootPageNameTemplate(string defaultFeedRootPageName);

        //IXmlSitemapFeedStrategyStarter WithDefaultFeedPageNameTemplate(string defaultFeedPageName);

        //IXmlSitemapFeedStrategyStarter WithNamedFeedRootPageNameTemplate(string namedFeedRootPageName);

        //IXmlSitemapFeedStrategyStarter WithNamedFeedPageNameTemplate(string namedFeedPageName);

        IXmlSitemapFeedStrategyStarter WithDefaultFeedPageNameTemplates(string rootPageTemplate, string pageTemplate);

        IXmlSitemapFeedStrategyStarter WithNamedFeedPageNameTemplates(string rootPageTemplate, string pageTemplate);

        string DefaultFeedRootPageName { get; }

        string DefaultFeedPageName { get; }

        string NamedFeedRootPageName { get; }

        string NamedFeedPageName { get; }

        // From AttributeAssemblyProvider
        IXmlSitemapFeedStrategyStarter AddAssemblyForXmlSitemapProviderScan(string assemblyName);

        IXmlSitemapFeedStrategyStarter RemoveAssemblyFromXmlSitemapProviderScan(string assemblyName);

        IEnumerable<string> AssembliesToScanForXmlSitemapProvider { get; }

        // TODO: Ensure the factory is wrapped with the request caching decorator.
        IXmlSitemapFeedStrategyBuilder WithXmlSitemapProviderFactory(IXmlSitemapProviderFactory xmlSitemapProviderFactory);

        

        //// NOTE: all of the above must be called before these methods.

        //IXmlSitemapFeedStrategyStarter AddFeed(IXmlSitemapFeed xmlSitemapFeed);

        //IXmlSitemapFeedStrategyStarter AddFeed(Func<IXmlSitemapFeedBuilderFacade, IXmlSitemapFeedBuilderFacade> expression);

        //IXmlSitemapFeedStrategyStarter AddFeeds(IEnumerable<IXmlSitemapFeed> xmlSitemapFeeds);

        //IXmlSitemapFeedStrategyStarter RemoveFeed(string feedName);

        //IXmlSitemapFeedStrategyStarter ClearFeeds();

        //IEnumerable<IXmlSitemapFeed> XmlSitemapFeeds { get; }


        //IXmlSitemapFeedStrategy Create();
    }
}
