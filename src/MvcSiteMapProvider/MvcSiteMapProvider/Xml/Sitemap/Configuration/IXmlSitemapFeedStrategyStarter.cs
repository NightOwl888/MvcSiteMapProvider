//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//using MvcSiteMapProvider.Reflection;

//namespace MvcSiteMapProvider.Xml.Sitemap.Configuration
//{
//    public interface IXmlSitemapFeedStrategyStarter
//        : IXmlSitemapFeedStrategyBuilder
//    {
//        // Globals:
//        // IXmlSitemapUrlResolver
//        // IAttributeAssemblyProvider (with include assemblies)
//        // ICultureContextFactory
//        // IXmlSitemapFeedUrlResolver (and IXmlSitemapFeedPageNameProvider, IXmlSitemapUrlResolver)

//        //IXmlSitemapFeedStrategyStarter WithDefaultProtocol(string defaultProtocol);

//        //IXmlSitemapFeedStrategyStarter WithDefaultHostName(string defaultHostName);

//        //IXmlSitemapFeedStrategyStarter WithXmlSitemapUrlResolverFactory(IXmlSitemapUrlResolverFactory xmlSitemapUrlResolverFactory);


//        //IXmlSitemapFeedStrategyStarter ForUrlResolving(Func<IXmlSitemap_SetupUrlResolver_Starter, IXmlSitemap_SetupUrlResolver_Finalizer> expression);

        

//        // From AssemblyProviderFactory
//        //IXmlSitemapFeedStrategyStarter WithAssemblyForXmlSitemapProviderScan(string assemblyName);

//        //IXmlSitemapFeedStrategyStarter WithAssemblyForXmlSitemapProviderScan(Assembly assembly);

//        //IXmlSitemapFeedStrategyStarter OmitAssemblyFromXmlSitemapProviderScan(string assemblyName);

//        //IXmlSitemapFeedStrategyStarter OmitAssemblyForXmlSitemapProviderScan(Assembly assembly);

//        //IXmlSitemapFeedStrategyStarter WithAssemblyProviderFactory(IAssemblyProviderFactory assemblyProviderFactory);


//        //IXmlSitemapFeedStrategyStarter ForXmlSitemapProvider(Func<IXmlSitemapFeedStrategyForXmlSitemapProviderStarter, IXmlSitemapFeedStrategyForXmlSitemapProviderFinalizer> expression);

//        IXmlSitemapFeedStrategyStarter SetupXmlSitemapProviderScan(Func<IXmlSitemap_SetupXmlSitemapProviderScan_Starter, IXmlSitemap_SetupXmlSitemapProviderScan_Finalizer> expression);

//        IXmlSitemapFeedStrategyStarter SetupPageNameTempates(Func<IXmlSitemap_SetupPageTemplates_Starter, IXmlSitemap_SetupPageTemplates_Finalizer> expression);


//        // Move these to IXmlSitemapFeedstrategyBuilder

//        //IXmlSitemapFeedStrategyStarter WithDefaultFeedPageNameTemplates(string rootPageTemplate, string pageTemplate);

//        //IXmlSitemapFeedStrategyStarter WithNamedFeedPageNameTemplates(string rootPageTemplate, string pageTemplate);

//        //IXmlSitemapFeedStrategyStarter WithXmlSitemapProviderFactory(IXmlSitemapProviderFactory xmlSitemapProviderFactory);


//        IXmlSitemapFeedStrategyStarter WithUrlResolver(Func<IXmlSitemap_SetupUrlResolver_Starter, IXmlSitemap_SetupUrlResolver_Finalizer> expression);

//        IXmlSitemapFeedStrategyStarter WithUrlResolver(IXmlSitemapUrlResolverFactory xmlSitemapUrlResolverFactory);

//    }
//}
