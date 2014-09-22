using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MvcSiteMapProvider.ComponentModel;
using MvcSiteMapProvider.Reflection;

namespace MvcSiteMapProvider.Xml.Sitemap.Configuration
{
    

    public interface IXmlSitemap_SetupXmlSitemapProviderScan<TRemainder>
    {
        TRemainder SetupXmlSitemapProviderScan(Func<IXmlSitemap_SetupXmlSitemapProviderScan_Starter, IXmlSitemap_SetupXmlSitemapProviderScan_Finalizer> expression);
    }

    public interface IXmlSitemap_SetupPageNameTemplates<TRemainder>
    {
        TRemainder SetupPageNameTempates(Func<IXmlSitemap_SetupPageTemplates_Starter, IXmlSitemap_SetupPageTemplates_Finalizer> expression);
    }

    public interface IXmlSitemap_SetupUrlResolver<TRemainder>
    {
        TRemainder SetupUrlResolver(Func<IXmlSitemap_SetupUrlResolver_Starter, IXmlSitemap_SetupUrlResolver_Finalizer> expression);

        TRemainder SetupUrlResolver(IXmlSitemapUrlResolverFactory xmlSitemapUrlResolverFactory);
    }

    public interface IXmlSitemapFeedStrategy_Finalizer
        : IFluentInterface
    {
        IXmlSitemapFeedStrategy_Finalizer AddNamedFeed(string feedName, Func<IXmlSitemap_SetupFeed_Starter, IXmlSitemap_SetupFeed_Finalizer> expression);

        IXmlSitemapFeedStrategy_Finalizer AddDefaultFeed();

        IXmlSitemapFeedStrategy_Finalizer RemoveNamedFeed(string feedName);

        IXmlSitemapFeedStrategy_Finalizer RemoveDefaultFeed();

        IXmlSitemapFeedStrategy_Finalizer ClearFeeds();

        IEnumerable<IXmlSitemapFeed> XmlSitemapFeeds { get; }

        IXmlSitemapProviderFactory XmlSitemapProviderFactory { get; }

        IXmlSitemapUrlResolverFactory XmlSitemapUrlResolverFactory { get; }

        IXmlSitemapFeedPageNameProvider XmlSitemapFeedPageNameProvider { get; }

        IAssemblyProviderFactory AssemblyProviderFactory { get; }

        IXmlSitemapFeedStrategy Create();
    }

    public interface IXmlSitemapFeedStrategy_Starter
        : IXmlSitemap_SetupXmlSitemapProviderScan<IXmlSitemapFeedStrategy_WithXmlSitemapProvider>,
        IXmlSitemap_SetupPageNameTemplates<IXmlSitemapFeedStrategy_WithXmlSitemapFeedPageNameProvider>,
        IXmlSitemap_SetupUrlResolver<IXmlSitemapFeedStrategy_WithXmlSitemapUrlResolver>,
        IXmlSitemapFeedStrategy_Finalizer
    {
    }


    // 1 item set

    public interface IXmlSitemapFeedStrategy_WithXmlSitemapProvider
        : IXmlSitemap_SetupPageNameTemplates<IXmlSitemapFeedStrategy_WithXmlSitemapFeedPageNameProvider_WithXmlSitemapProvider>,
        IXmlSitemap_SetupUrlResolver<IXmlSitemapFeedStrategy_WithXmlSitemapUrlResolver_WithXmlSitemapProvider>,
        IXmlSitemapFeedStrategy_Finalizer
    {
    }

    public interface IXmlSitemapFeedStrategy_WithXmlSitemapFeedPageNameProvider
        : IXmlSitemap_SetupXmlSitemapProviderScan<IXmlSitemapFeedStrategy_WithXmlSitemapFeedPageNameProvider_WithXmlSitemapProvider>,
        IXmlSitemap_SetupUrlResolver<IXmlSitemapFeedStrategy_WithXmlSitemapUrlResolver_WithXmlSitemapFeedPageNameProvider>,
        IXmlSitemapFeedStrategy_Finalizer
    {
    }

    public interface IXmlSitemapFeedStrategy_WithXmlSitemapUrlResolver
        : IXmlSitemap_SetupXmlSitemapProviderScan<IXmlSitemapFeedStrategy_WithXmlSitemapUrlResolver_WithXmlSitemapProvider>,
        IXmlSitemap_SetupPageNameTemplates<IXmlSitemapFeedStrategy_WithXmlSitemapUrlResolver_WithXmlSitemapFeedPageNameProvider>,
        IXmlSitemapFeedStrategy_Finalizer
    {
    }

    // 2 items set

    public interface IXmlSitemapFeedStrategy_WithXmlSitemapUrlResolver_WithXmlSitemapFeedPageNameProvider
        : IXmlSitemap_SetupXmlSitemapProviderScan<IXmlSitemapFeedStrategy_Finalizer>,
        IXmlSitemapFeedStrategy_Finalizer
    {
    }

    public interface IXmlSitemapFeedStrategy_WithXmlSitemapUrlResolver_WithXmlSitemapProvider
        : IXmlSitemap_SetupPageNameTemplates<IXmlSitemapFeedStrategy_Finalizer>,
        IXmlSitemapFeedStrategy_Finalizer
    {
    }

    public interface IXmlSitemapFeedStrategy_WithXmlSitemapFeedPageNameProvider_WithXmlSitemapProvider
        : IXmlSitemap_SetupUrlResolver<IXmlSitemapFeedStrategy_Finalizer>,
        IXmlSitemapFeedStrategy_Finalizer
    {
    }

}
