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

        // TODO: Add a way to override default protocol and hostname

        IXmlSitemapFeedStrategyStarter WithDefaultFeedPageNameTemplates(string rootPageTemplate, string pageTemplate);

        IXmlSitemapFeedStrategyStarter WithNamedFeedPageNameTemplates(string rootPageTemplate, string pageTemplate);

        // From AttributeAssemblyProvider
        IXmlSitemapFeedStrategyStarter WithAssemblyForXmlSitemapProviderScan(string assemblyName);

        IXmlSitemapFeedStrategyStarter OmitAssemblyFromXmlSitemapProviderScan(string assemblyName);

        IXmlSitemapFeedStrategyStarter WithXmlSitemapProviderFactory(IXmlSitemapProviderFactory xmlSitemapProviderFactory);

        IXmlSitemapFeedStrategyStarter WithXmlSitemapUrlResolverFactory(IXmlSitemapUrlResolverFactory xmlSitemapUrlResolverFactory);
    }
}
