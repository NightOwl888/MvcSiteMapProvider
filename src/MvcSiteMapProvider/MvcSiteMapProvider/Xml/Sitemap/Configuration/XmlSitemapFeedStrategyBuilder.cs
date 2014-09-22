using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MvcSiteMapProvider.Reflection;

namespace MvcSiteMapProvider.Xml.Sitemap.Configuration
{
    public class XmlSitemapFeedStrategyBuilder
        : XmlSitemapFeedStrategy_WithXmlSitemapUrlResolver, IXmlSitemapFeedStrategy_Starter
    {
        public XmlSitemapFeedStrategyBuilder()
            : this(
            xmlSitemapProviderFactory: new XmlSitemapProviderFactory(),
            xmlSitemapUrlResolverFactory: new XmlSitemapUrlResolverFactoryBuilder().Create(),
            assemblyProviderFactory: new AssemblyProviderFactoryBuilder().Create(),
            xmlSitemapFeedPageNameProvider: new XmlSitemapFeedPageNameProvider(),
            xmlSitemapFeeds: new Dictionary<string, XmlSitemapFeedToXmlSitemapFeedBuilderRelation>()
            )
        {
        }

        private XmlSitemapFeedStrategyBuilder(
            IXmlSitemapProviderFactory xmlSitemapProviderFactory,
            IAssemblyProviderFactory assemblyProviderFactory,
            IXmlSitemapUrlResolverFactory xmlSitemapUrlResolverFactory,
            IXmlSitemapFeedPageNameProvider xmlSitemapFeedPageNameProvider,
            IDictionary<string, XmlSitemapFeedToXmlSitemapFeedBuilderRelation> xmlSitemapFeeds
            )
        {
            if (xmlSitemapProviderFactory == null)
                throw new ArgumentNullException("xmlSitemapProviderFactory");
            if (assemblyProviderFactory == null)
                throw new ArgumentNullException("assemblyProviderFactory");
            if (xmlSitemapUrlResolverFactory == null)
                throw new ArgumentNullException("xmlSitemapUrlResolverFactory");
            if (xmlSitemapFeedPageNameProvider == null)
                throw new ArgumentNullException("xmlSitemapFeedPageNameProvider");
            if (xmlSitemapFeeds == null)
                throw new ArgumentNullException("xmlSitemapFeeds");

            this.xmlSitemapProviderFactory = xmlSitemapProviderFactory;
            this.assemblyProviderFactory = assemblyProviderFactory;
            this.xmlSitemapUrlResolverFactory = xmlSitemapUrlResolverFactory;
            this.xmlSitemapFeedPageNameProvider = xmlSitemapFeedPageNameProvider;
            this.xmlSitemapFeeds = xmlSitemapFeeds;
        }
        private readonly IXmlSitemapProviderFactory xmlSitemapProviderFactory;
        private readonly IAssemblyProviderFactory assemblyProviderFactory;
        private readonly IXmlSitemapUrlResolverFactory xmlSitemapUrlResolverFactory;
        private readonly IXmlSitemapFeedPageNameProvider xmlSitemapFeedPageNameProvider;
        private readonly IDictionary<string, XmlSitemapFeedToXmlSitemapFeedBuilderRelation> xmlSitemapFeeds;

        public override IXmlSitemapFeedStrategy_WithXmlSitemapProvider SetupXmlSitemapProviderScan(Func<IXmlSitemap_SetupXmlSitemapProviderScan_Starter, IXmlSitemap_SetupXmlSitemapProviderScan_Finalizer> expression)
        {
            var starter = new XmlSitemap_SetupXmlSitemapProviderScan_Builder();
            var builder = expression(starter);
            var assemblyProviderFactory = builder.Create();
            var xmlSitemapProviderFactory = builder.XmlSitemapProviderFactory;

            return (IXmlSitemapFeedStrategy_WithXmlSitemapProvider)new XmlSitemapFeedStrategyBuilder(xmlSitemapProviderFactory,
                assemblyProviderFactory, this.xmlSitemapUrlResolverFactory, this.xmlSitemapFeedPageNameProvider, this.xmlSitemapFeeds);
        }

        public override IXmlSitemapFeedStrategy_WithXmlSitemapFeedPageNameProvider SetupPageNameTempates(Func<IXmlSitemap_SetupPageTemplates_Starter, IXmlSitemap_SetupPageTemplates_Finalizer> expression)
        {
            var starter = new XmlSitemap_SetupPageTemplates_Builder();
            var builder = expression(starter);
            var xmlSitemapFeedPageNameProvider = builder.Create();

            return (IXmlSitemapFeedStrategy_WithXmlSitemapFeedPageNameProvider)new XmlSitemapFeedStrategyBuilder(this.xmlSitemapProviderFactory,
                this.assemblyProviderFactory, this.xmlSitemapUrlResolverFactory, xmlSitemapFeedPageNameProvider, this.xmlSitemapFeeds);
        }

        public override IXmlSitemapFeedStrategy_WithXmlSitemapUrlResolver SetupUrlResolver(Func<IXmlSitemap_SetupUrlResolver_Starter, IXmlSitemap_SetupUrlResolver_Finalizer> expression)
        {
            var starter = new XmlSitemap_SetupUrlResolver_Builder();
            var builder = expression(starter);
            var xmlSitemapUrlResolverFactory = builder.Create();


            return (IXmlSitemapFeedStrategy_WithXmlSitemapUrlResolver)new XmlSitemapFeedStrategyBuilder(this.xmlSitemapProviderFactory,
                this.assemblyProviderFactory, xmlSitemapUrlResolverFactory, this.xmlSitemapFeedPageNameProvider, this.xmlSitemapFeeds);
        }

        public override IXmlSitemapFeedStrategy_WithXmlSitemapUrlResolver SetupUrlResolver(IXmlSitemapUrlResolverFactory xmlSitemapUrlResolverFactory)
        {
            return (IXmlSitemapFeedStrategy_WithXmlSitemapUrlResolver)new XmlSitemapFeedStrategyBuilder(this.xmlSitemapProviderFactory,
                this.assemblyProviderFactory, xmlSitemapUrlResolverFactory, this.xmlSitemapFeedPageNameProvider, this.xmlSitemapFeeds);
        }

        public override IXmlSitemapFeedStrategy_Finalizer AddNamedFeed(string feedName, Func<IXmlSitemap_SetupFeed_Starter, IXmlSitemap_SetupFeed_Finalizer> expression)
        {
            var starter = new XmlSitemapFeedBuilder(feedName, this.xmlSitemapProviderFactory, this.xmlSitemapUrlResolverFactory,
                this.assemblyProviderFactory, this.xmlSitemapFeedPageNameProvider);
            var builder = expression(starter);
            var feed = builder.Create();
            this.xmlSitemapFeeds.Add(feed.Name, new XmlSitemapFeedToXmlSitemapFeedBuilderRelation(feed, builder));

            return new XmlSitemapFeedStrategyBuilder(this.xmlSitemapProviderFactory,
                this.assemblyProviderFactory, this.xmlSitemapUrlResolverFactory, this.xmlSitemapFeedPageNameProvider, this.xmlSitemapFeeds);
        }

        public override IXmlSitemapFeedStrategy_Finalizer AddDefaultFeed()
        {
            return this.AddNamedFeed("default", x => x);
        }

        public override IXmlSitemapFeedStrategy_Finalizer RemoveNamedFeed(string feedName)
        {
            this.xmlSitemapFeeds.Remove(feedName);
            return new XmlSitemapFeedStrategyBuilder(this.xmlSitemapProviderFactory,
                this.assemblyProviderFactory, this.xmlSitemapUrlResolverFactory, this.xmlSitemapFeedPageNameProvider, this.xmlSitemapFeeds);
        }

        public override IXmlSitemapFeedStrategy_Finalizer RemoveDefaultFeed()
        {
            return this.RemoveNamedFeed("default");
        }

        public override IXmlSitemapFeedStrategy_Finalizer ClearFeeds()
        {
            this.xmlSitemapFeeds.Clear();
            return new XmlSitemapFeedStrategyBuilder(this.xmlSitemapProviderFactory,
                this.assemblyProviderFactory, this.xmlSitemapUrlResolverFactory, this.xmlSitemapFeedPageNameProvider, this.xmlSitemapFeeds);
        }

        public override IEnumerable<IXmlSitemapFeed> XmlSitemapFeeds
        {
            get { return this.xmlSitemapFeeds.Values.Select(x => x.XmlSitemapFeed).ToArray(); }
        }

        public override IXmlSitemapProviderFactory XmlSitemapProviderFactory
        {
            get { return this.xmlSitemapProviderFactory; }
        }

        public override IXmlSitemapUrlResolverFactory XmlSitemapUrlResolverFactory
        {
            get { return this.xmlSitemapUrlResolverFactory; }
        }

        public override IXmlSitemapFeedPageNameProvider XmlSitemapFeedPageNameProvider
        {
            get { return this.xmlSitemapFeedPageNameProvider; }
        }

        public override IAssemblyProviderFactory AssemblyProviderFactory
        {
            get { return this.assemblyProviderFactory; }
        }

        public override IXmlSitemapFeedStrategy Create()
        {
            // Copy the dictionary
            var feeds = new Dictionary<string, XmlSitemapFeedToXmlSitemapFeedBuilderRelation>(this.xmlSitemapFeeds);

            // Rebuild the feeds that have a builder using the latest state
            foreach (var xmlSitemapFeedWrapper in feeds)
            {
                if (xmlSitemapFeedWrapper.Value.XmlSitemapFeedBuilder != null)
                {
                    var feed = xmlSitemapFeedWrapper.Value.XmlSitemapFeed;
                    var builder = xmlSitemapFeedWrapper.Value.XmlSitemapFeedBuilder;

                    builder = new XmlSitemapFeedBuilder(
                        builder,
                        this.xmlSitemapProviderFactory,
                        this.xmlSitemapUrlResolverFactory,
                        this.assemblyProviderFactory,
                        this.xmlSitemapFeedPageNameProvider);

                    // Reset the dictionary to the latest state.
                    this.xmlSitemapFeeds[feed.Name] = new XmlSitemapFeedToXmlSitemapFeedBuilderRelation(builder.Create(), builder);
                }
            }

            return new XmlSitemapFeedStrategy(this.XmlSitemapFeeds.ToArray());
        }



        //public IXmlSitemapFeedStrategy_WithXmlSitemapProvider SetupXmlSitemapProviderScan(Func<IXmlSitemap_SetupXmlSitemapProviderScan_Starter, IXmlSitemap_SetupXmlSitemapProviderScan_Finalizer> expression)
        //{
        //    var starter = new XmlSitemap_SetupXmlSitemapProviderScan_Builder();
        //    var builder = expression(starter);
        //    var assemblyProviderFactory = builder.Create();
        //    var xmlSitemapProviderFactory = builder.XmlSitemapProviderFactory;

        //    return (IXmlSitemapFeedStrategy_WithXmlSitemapProvider)new XmlSitemapFeedStrategyBuilder(xmlSitemapProviderFactory,
        //        assemblyProviderFactory, this.xmlSitemapUrlResolverFactory, this.xmlSitemapFeedPageNameProvider, this.xmlSitemapFeeds);
        //}

        //public IXmlSitemapFeedStrategy_WithXmlSitemapFeedPageNameProvider SetupPageNameTempates(Func<IXmlSitemap_SetupPageTemplates_Starter, IXmlSitemap_SetupPageTemplates_Finalizer> expression)
        //{
        //    var starter = new XmlSitemap_SetupPageTemplates_Builder();
        //    var builder = expression(starter);
        //    var xmlSitemapFeedPageNameProvider = builder.Create();

        //    return (IXmlSitemapFeedStrategy_WithXmlSitemapFeedPageNameProvider)new XmlSitemapFeedStrategyBuilder(this.xmlSitemapProviderFactory,
        //        this.assemblyProviderFactory, this.xmlSitemapUrlResolverFactory, xmlSitemapFeedPageNameProvider, this.xmlSitemapFeeds);
        //}

        //public IXmlSitemapFeedStrategy_WithXmlSitemapUrlResolver SetupUrlResolver(Func<IXmlSitemap_SetupUrlResolver_Starter, IXmlSitemap_SetupUrlResolver_Finalizer> expression)
        //{
        //    var starter = new XmlSitemap_SetupUrlResolver_Builder();
        //    var builder = expression(starter);
        //    var xmlSitemapUrlResolverFactory = builder.Create();


        //    return (IXmlSitemapFeedStrategy_WithXmlSitemapUrlResolver)new XmlSitemapFeedStrategyBuilder(this.xmlSitemapProviderFactory,
        //        this.assemblyProviderFactory, xmlSitemapUrlResolverFactory, this.xmlSitemapFeedPageNameProvider, this.xmlSitemapFeeds);
        //}

        //public IXmlSitemapFeedStrategy_WithXmlSitemapUrlResolver SetupUrlResolver(IXmlSitemapUrlResolverFactory xmlSitemapUrlResolverFactory)
        //{
        //    return (IXmlSitemapFeedStrategy_WithXmlSitemapUrlResolver)new XmlSitemapFeedStrategyBuilder(this.xmlSitemapProviderFactory,
        //        this.assemblyProviderFactory, this.xmlSitemapUrlResolverFactory, this.xmlSitemapFeedPageNameProvider, this.xmlSitemapFeeds);
        //}

        //public IXmlSitemapFeedStrategy_Finalizer AddNamedFeed(string feedName, Func<IXmlSitemap_SetupFeed_Starter, IXmlSitemap_SetupFeed_Finalizer> expression)
        //{
        //    var starter = new XmlSitemapFeedBuilderFacade(feedName, this.xmlSitemapProviderFactory, this.xmlSitemapUrlResolverFactory,
        //        this.assemblyProviderFactory, this.xmlSitemapFeedPageNameProvider);
        //    var builder = expression(starter);
        //    var feed = builder.Create();
        //    this.xmlSitemapFeeds.Add(feed.Name, new XmlSitemapFeedToXmlSitemapFeedBuilderRelation(feed, builder));

        //    return new XmlSitemapFeedStrategyBuilder(this.xmlSitemapProviderFactory,
        //        this.assemblyProviderFactory, this.xmlSitemapUrlResolverFactory, this.xmlSitemapFeedPageNameProvider, this.xmlSitemapFeeds);
        //}

        //public IXmlSitemapFeedStrategy_Finalizer AddDefaultFeed()
        //{
        //    return this.AddNamedFeed("default", x => x);
        //}

        //public IXmlSitemapFeedStrategy_Finalizer RemoveNamedFeed(string feedName)
        //{
        //    this.xmlSitemapFeeds.Remove(feedName);
        //    return new XmlSitemapFeedStrategyBuilder(this.xmlSitemapProviderFactory,
        //        this.assemblyProviderFactory, this.xmlSitemapUrlResolverFactory, this.xmlSitemapFeedPageNameProvider, this.xmlSitemapFeeds);
        //}

        //public IXmlSitemapFeedStrategy_Finalizer RemoveDefaultFeed()
        //{
        //    return this.RemoveNamedFeed("default");
        //}

        //public IXmlSitemapFeedStrategy_Finalizer ClearFeeds()
        //{
        //    this.xmlSitemapFeeds.Clear();
        //    return new XmlSitemapFeedStrategyBuilder(this.xmlSitemapProviderFactory,
        //        this.assemblyProviderFactory, this.xmlSitemapUrlResolverFactory, this.xmlSitemapFeedPageNameProvider, this.xmlSitemapFeeds);
        //}

        //public IEnumerable<IXmlSitemapFeed> XmlSitemapFeeds
        //{
        //    get { return this.xmlSitemapFeeds.Values.Select(x => x.XmlSitemapFeed).ToArray(); }
        //}

        //public IXmlSitemapProviderFactory XmlSitemapProviderFactory
        //{
        //    get { return this.xmlSitemapProviderFactory; }
        //}

        //public IXmlSitemapUrlResolverFactory XmlSitemapUrlResolverFactory
        //{
        //    get { return this.xmlSitemapUrlResolverFactory; }
        //}

        //public IXmlSitemapFeedPageNameProvider XmlSitemapFeedPageNameProvider
        //{
        //    get { return this.xmlSitemapFeedPageNameProvider; }
        //}

        //public IAssemblyProviderFactory AssemblyProviderFactory
        //{
        //    get { return this.assemblyProviderFactory; }
        //}

        //public IXmlSitemapFeedStrategy Create()
        //{
        //    // Copy the dictionary
        //    var feeds = new Dictionary<string, XmlSitemapFeedToXmlSitemapFeedBuilderRelation>(this.xmlSitemapFeeds);

        //    // Rebuild the feeds that have a builder using the latest state
        //    foreach (var xmlSitemapFeedWrapper in feeds)
        //    {
        //        if (xmlSitemapFeedWrapper.Value.XmlSitemapFeedBuilderFacade != null)
        //        {
        //            var feed = xmlSitemapFeedWrapper.Value.XmlSitemapFeed;
        //            var builder = xmlSitemapFeedWrapper.Value.XmlSitemapFeedBuilderFacade;

        //            builder = new XmlSitemapFeedBuilderFacade(
        //                builder,
        //                this.xmlSitemapProviderFactory,
        //                this.xmlSitemapUrlResolverFactory,
        //                this.assemblyProviderFactory,
        //                this.xmlSitemapFeedPageNameProvider);

        //            // Reset the dictionary to the latest state.
        //            this.xmlSitemapFeeds[feed.Name] = new XmlSitemapFeedToXmlSitemapFeedBuilderRelation(builder.Create(), builder);
        //        }
        //    }

        //    return new XmlSitemapFeedStrategy(this.XmlSitemapFeeds.ToArray());
        //}

        private class XmlSitemapFeedToXmlSitemapFeedBuilderRelation
        {
            public XmlSitemapFeedToXmlSitemapFeedBuilderRelation(
                IXmlSitemapFeed xmlSitemapFeed,
                IXmlSitemap_SetupFeed_Finalizer xmlSitemapFeedBuilder
                )
            {
                if (xmlSitemapFeed == null)
                    throw new ArgumentNullException("xmlSitemapFeed");

                this.XmlSitemapFeed = xmlSitemapFeed;
                this.XmlSitemapFeedBuilder = xmlSitemapFeedBuilder;
            }

            public IXmlSitemapFeed XmlSitemapFeed { get; private set; }

            public IXmlSitemap_SetupFeed_Finalizer XmlSitemapFeedBuilder { get; private set; }
        }


        
    }


    public abstract class XmlSitemapFeedStrategy_WithXmlSitemapUrlResolver : XmlSitemapFeedStrategy_WithXmlSitemapFeedPageNameProvider,
        IXmlSitemapFeedStrategy_WithXmlSitemapUrlResolver,
        IXmlSitemapFeedStrategy_WithXmlSitemapUrlResolver_WithXmlSitemapFeedPageNameProvider,
        IXmlSitemapFeedStrategy_WithXmlSitemapUrlResolver_WithXmlSitemapProvider
    {
        //public new IXmlSitemapFeedStrategy_WithXmlSitemapUrlResolver_WithXmlSitemapProvider SetupXmlSitemapProviderScan(Func<IXmlSitemap_SetupXmlSitemapProviderScan_Starter, IXmlSitemap_SetupXmlSitemapProviderScan_Finalizer> expression)
        //{
        //    return this.SetupXmlSitemapProviderScan(expression);
        //}

        //public new IXmlSitemapFeedStrategy_WithXmlSitemapUrlResolver_WithXmlSitemapFeedPageNameProvider SetupPageNameTempates(Func<IXmlSitemap_SetupPageTemplates_Starter, IXmlSitemap_SetupPageTemplates_Finalizer> expression)
        //{
        //    return this.SetupPageNameTempates(expression);
        //}





        IXmlSitemapFeedStrategy_WithXmlSitemapUrlResolver_WithXmlSitemapProvider IXmlSitemap_SetupXmlSitemapProviderScan<IXmlSitemapFeedStrategy_WithXmlSitemapUrlResolver_WithXmlSitemapProvider>.SetupXmlSitemapProviderScan(Func<IXmlSitemap_SetupXmlSitemapProviderScan_Starter, IXmlSitemap_SetupXmlSitemapProviderScan_Finalizer> expression)
        {
            return (IXmlSitemapFeedStrategy_WithXmlSitemapUrlResolver_WithXmlSitemapProvider)this.SetupXmlSitemapProviderScan(expression);
        }

        IXmlSitemapFeedStrategy_WithXmlSitemapUrlResolver_WithXmlSitemapFeedPageNameProvider IXmlSitemap_SetupPageNameTemplates<IXmlSitemapFeedStrategy_WithXmlSitemapUrlResolver_WithXmlSitemapFeedPageNameProvider>.SetupPageNameTempates(Func<IXmlSitemap_SetupPageTemplates_Starter, IXmlSitemap_SetupPageTemplates_Finalizer> expression)
        {
            return (IXmlSitemapFeedStrategy_WithXmlSitemapUrlResolver_WithXmlSitemapFeedPageNameProvider)this.SetupPageNameTempates(expression);
        }



        IXmlSitemapFeedStrategy_Finalizer IXmlSitemap_SetupXmlSitemapProviderScan<IXmlSitemapFeedStrategy_Finalizer>.SetupXmlSitemapProviderScan(Func<IXmlSitemap_SetupXmlSitemapProviderScan_Starter, IXmlSitemap_SetupXmlSitemapProviderScan_Finalizer> expression)
        {
            return this.SetupXmlSitemapProviderScan(expression);
        }



        IXmlSitemapFeedStrategy_Finalizer IXmlSitemap_SetupPageNameTemplates<IXmlSitemapFeedStrategy_Finalizer>.SetupPageNameTempates(Func<IXmlSitemap_SetupPageTemplates_Starter, IXmlSitemap_SetupPageTemplates_Finalizer> expression)
        {
            return this.SetupPageNameTempates(expression);
        }

    }

    public abstract class XmlSitemapFeedStrategy_WithXmlSitemapFeedPageNameProvider : XmlSitemapFeedStrategy_WithXmlSitemapProvider,
        IXmlSitemapFeedStrategy_WithXmlSitemapFeedPageNameProvider,
        IXmlSitemapFeedStrategy_WithXmlSitemapFeedPageNameProvider_WithXmlSitemapProvider
    {
        IXmlSitemapFeedStrategy_WithXmlSitemapFeedPageNameProvider_WithXmlSitemapProvider IXmlSitemap_SetupXmlSitemapProviderScan<IXmlSitemapFeedStrategy_WithXmlSitemapFeedPageNameProvider_WithXmlSitemapProvider>.SetupXmlSitemapProviderScan(Func<IXmlSitemap_SetupXmlSitemapProviderScan_Starter, IXmlSitemap_SetupXmlSitemapProviderScan_Finalizer> expression)
        {
            return (IXmlSitemapFeedStrategy_WithXmlSitemapFeedPageNameProvider_WithXmlSitemapProvider)this.SetupXmlSitemapProviderScan(expression);
        }

        IXmlSitemapFeedStrategy_WithXmlSitemapUrlResolver_WithXmlSitemapFeedPageNameProvider IXmlSitemap_SetupUrlResolver<IXmlSitemapFeedStrategy_WithXmlSitemapUrlResolver_WithXmlSitemapFeedPageNameProvider>.SetupUrlResolver(Func<IXmlSitemap_SetupUrlResolver_Starter, IXmlSitemap_SetupUrlResolver_Finalizer> expression)
        {
            return (IXmlSitemapFeedStrategy_WithXmlSitemapUrlResolver_WithXmlSitemapFeedPageNameProvider)this.SetupUrlResolver(expression);
        }

        IXmlSitemapFeedStrategy_WithXmlSitemapUrlResolver_WithXmlSitemapFeedPageNameProvider IXmlSitemap_SetupUrlResolver<IXmlSitemapFeedStrategy_WithXmlSitemapUrlResolver_WithXmlSitemapFeedPageNameProvider>.SetupUrlResolver(IXmlSitemapUrlResolverFactory xmlSitemapUrlResolverFactory)
        {
            return (IXmlSitemapFeedStrategy_WithXmlSitemapUrlResolver_WithXmlSitemapFeedPageNameProvider)this.SetupUrlResolver(xmlSitemapUrlResolverFactory);
        }



        IXmlSitemapFeedStrategy_Finalizer IXmlSitemap_SetupUrlResolver<IXmlSitemapFeedStrategy_Finalizer>.SetupUrlResolver(Func<IXmlSitemap_SetupUrlResolver_Starter, IXmlSitemap_SetupUrlResolver_Finalizer> expression)
        {
            return this.SetupUrlResolver(expression);
        }

        IXmlSitemapFeedStrategy_Finalizer IXmlSitemap_SetupUrlResolver<IXmlSitemapFeedStrategy_Finalizer>.SetupUrlResolver(IXmlSitemapUrlResolverFactory xmlSitemapUrlResolverFactory)
        {
            return this.SetupUrlResolver(xmlSitemapUrlResolverFactory);
        }
    }

    public abstract class XmlSitemapFeedStrategy_WithXmlSitemapProvider : XmlSitemapFeedStrategyBuilderBase,
        IXmlSitemapFeedStrategy_WithXmlSitemapProvider
    {
        IXmlSitemapFeedStrategy_WithXmlSitemapFeedPageNameProvider_WithXmlSitemapProvider IXmlSitemap_SetupPageNameTemplates<IXmlSitemapFeedStrategy_WithXmlSitemapFeedPageNameProvider_WithXmlSitemapProvider>.SetupPageNameTempates(Func<IXmlSitemap_SetupPageTemplates_Starter, IXmlSitemap_SetupPageTemplates_Finalizer> expression)
        {
            return (IXmlSitemapFeedStrategy_WithXmlSitemapFeedPageNameProvider_WithXmlSitemapProvider)this.SetupPageNameTempates(expression);
        }

        IXmlSitemapFeedStrategy_WithXmlSitemapUrlResolver_WithXmlSitemapProvider IXmlSitemap_SetupUrlResolver<IXmlSitemapFeedStrategy_WithXmlSitemapUrlResolver_WithXmlSitemapProvider>.SetupUrlResolver(Func<IXmlSitemap_SetupUrlResolver_Starter, IXmlSitemap_SetupUrlResolver_Finalizer> expression)
        {
            return (IXmlSitemapFeedStrategy_WithXmlSitemapUrlResolver_WithXmlSitemapProvider)this.SetupUrlResolver(expression);
        }

        IXmlSitemapFeedStrategy_WithXmlSitemapUrlResolver_WithXmlSitemapProvider IXmlSitemap_SetupUrlResolver<IXmlSitemapFeedStrategy_WithXmlSitemapUrlResolver_WithXmlSitemapProvider>.SetupUrlResolver(IXmlSitemapUrlResolverFactory xmlSitemapUrlResolverFactory)
        {
            return (IXmlSitemapFeedStrategy_WithXmlSitemapUrlResolver_WithXmlSitemapProvider)this.SetupUrlResolver(xmlSitemapUrlResolverFactory);
        }
    }

    public abstract class XmlSitemapFeedStrategyBuilderBase
        : IXmlSitemapFeedStrategy_Starter
    {
        public abstract IXmlSitemapFeedStrategy_WithXmlSitemapProvider SetupXmlSitemapProviderScan(Func<IXmlSitemap_SetupXmlSitemapProviderScan_Starter, IXmlSitemap_SetupXmlSitemapProviderScan_Finalizer> expression);

        public abstract IXmlSitemapFeedStrategy_WithXmlSitemapFeedPageNameProvider SetupPageNameTempates(Func<IXmlSitemap_SetupPageTemplates_Starter, IXmlSitemap_SetupPageTemplates_Finalizer> expression);

        public abstract IXmlSitemapFeedStrategy_WithXmlSitemapUrlResolver SetupUrlResolver(Func<IXmlSitemap_SetupUrlResolver_Starter, IXmlSitemap_SetupUrlResolver_Finalizer> expression);

        public abstract IXmlSitemapFeedStrategy_WithXmlSitemapUrlResolver SetupUrlResolver(IXmlSitemapUrlResolverFactory xmlSitemapUrlResolverFactory);

        public abstract IXmlSitemapFeedStrategy_Finalizer AddNamedFeed(string feedName, Func<IXmlSitemap_SetupFeed_Starter, IXmlSitemap_SetupFeed_Finalizer> expression);

        public abstract IXmlSitemapFeedStrategy_Finalizer AddDefaultFeed();

        public abstract IXmlSitemapFeedStrategy_Finalizer RemoveNamedFeed(string feedName);

        public abstract IXmlSitemapFeedStrategy_Finalizer RemoveDefaultFeed();

        public abstract IXmlSitemapFeedStrategy_Finalizer ClearFeeds();

        public abstract IEnumerable<IXmlSitemapFeed> XmlSitemapFeeds { get; }

        public abstract IXmlSitemapProviderFactory XmlSitemapProviderFactory { get; }

        public abstract IXmlSitemapUrlResolverFactory XmlSitemapUrlResolverFactory { get; }

        public abstract IXmlSitemapFeedPageNameProvider XmlSitemapFeedPageNameProvider { get; }

        public abstract IAssemblyProviderFactory AssemblyProviderFactory { get; }

        public abstract IXmlSitemapFeedStrategy Create();
    }






        //#region IXmlSitemapFeedStrategyStarter members

        ////public IXmlSitemapFeedStrategyStarter WithDefaultProtocol(string defaultProtocol)
        ////{
        ////    return new XmlSitemapFeedStrategyBuilder(defaultProtocol, this.defaultHostName, this.defaultFeedRootPageName,
        ////        this.defaultFeedPageName, this.namedFeedRootPageName, this.namedFeedPageName, this.includeAssembliesForScan,
        ////        this.excludeAssembliesForScan, this.xmlSitemapFeeds, this.xmlSitemapProviderFactory,
        ////        this.xmlSitemapUrlResolverFactory, this.appDomainFactory);
        ////}

        ////public IXmlSitemapFeedStrategyStarter WithDefaultHostName(string defaultHostName)
        ////{
        ////    return new XmlSitemapFeedStrategyBuilder(this.defaultProtocol, defaultHostName, this.defaultFeedRootPageName,
        ////        this.defaultFeedPageName, this.namedFeedRootPageName, this.namedFeedPageName, this.includeAssembliesForScan,
        ////        this.excludeAssembliesForScan, this.xmlSitemapFeeds, this.xmlSitemapProviderFactory,
        ////        this.xmlSitemapUrlResolverFactory, this.appDomainFactory);
        ////}

        //public IXmlSitemapFeedStrategyStarter ForUrlResolving(Func<IXmlSitemap_SetupUrlResolver_Starter, IXmlSitemap_SetupUrlResolver_Finalizer> expression)
        //{
        //    var starter = new XmlSitemap_SetupUrlResolver_Builder();
        //    var builder = expression(starter);
        //    var xmlSitemapUrlResolverFactory = builder.Create();

        //    return new XmlSitemapFeedStrategyBuilder(this.defaultFeedRootPageName, this.defaultFeedPageName,
        //        this.namedFeedRootPageName, this.namedFeedPageName, this.xmlSitemapFeeds, this.xmlSitemapProviderFactory,
        //        xmlSitemapUrlResolverFactory, this.assemblyProviderFactory);
        //}

        ////public IXmlSitemapFeedStrategyStarter ForXmlSitemapProvider(Func<IXmlSitemapFeedStrategyForXmlSitemapProviderStarter,IXmlSitemapFeedStrategyForXmlSitemapProviderFinalizer> expression)
        ////{
        ////    var starter = new XmlSitemapFeedStrategyForXmlSitemapProviderBuilder();
        ////    var builder = expression(starter);
        ////    var assemblyProviderFactory = builder.Create();
        ////    var xmlSitemapProviderFactory = builder.XmlSitemapProviderFactory;

        ////    return new XmlSitemapFeedStrategyBuilder(this.defaultFeedRootPageName, this.defaultFeedPageName,
        ////        this.namedFeedRootPageName, this.namedFeedPageName, this.xmlSitemapFeeds, xmlSitemapProviderFactory,
        ////        this.xmlSitemapUrlResolverFactory, assemblyProviderFactory);
        ////}

        //public IXmlSitemapFeedStrategyStarter ForXmlSitemapProvider(Func<IXmlSitemap_SetupXmlSitemapProviderScan_Starter, IXmlSitemap_SetupXmlSitemapProviderScan_Finalizer> expression)
        //{
        //    //var starter = new XmlSitemapFeedStrategyForXmlSitemapProviderBuilder();
        //    //var builder = expression(starter);
        //    //var assemblyProviderFactory = builder.Create();
        //    //var xmlSitemapProviderFactory = builder.XmlSitemapProviderFactory;

        //    return new XmlSitemapFeedStrategyBuilder(this.defaultFeedRootPageName, this.defaultFeedPageName,
        //        this.namedFeedRootPageName, this.namedFeedPageName, this.xmlSitemapFeeds, xmlSitemapProviderFactory,
        //        this.xmlSitemapUrlResolverFactory, assemblyProviderFactory);
        //}

        //public IXmlSitemapFeedStrategyStarter WithDefaultFeedPageNameTemplates(string rootPageTemplate, string pageTemplate)
        //{
        //    return new XmlSitemapFeedStrategyBuilder(rootPageTemplate, pageTemplate,
        //        this.namedFeedRootPageName, this.namedFeedPageName, this.xmlSitemapFeeds, this.xmlSitemapProviderFactory,
        //        xmlSitemapUrlResolverFactory, this.assemblyProviderFactory);
        //}

        //public IXmlSitemapFeedStrategyStarter WithNamedFeedPageNameTemplates(string rootPageTemplate, string pageTemplate)
        //{
        //    return new XmlSitemapFeedStrategyBuilder(this.defaultFeedRootPageName, this.defaultFeedPageName,
        //        rootPageTemplate, pageTemplate, this.xmlSitemapFeeds, this.xmlSitemapProviderFactory,
        //        xmlSitemapUrlResolverFactory, this.assemblyProviderFactory);
        //}

        ////public IXmlSitemapFeedStrategyStarter WithAssemblyForXmlSitemapProviderScan(string assemblyName)
        ////{
        ////    if (!string.IsNullOrEmpty(assemblyName))
        ////    {
        ////        var assembly = this.GetAssemblyNamed(assemblyName);
        ////        if (assembly != null)
        ////        {
        ////            this.includeAssembliesForScan.Add(assembly);
        ////        }
        ////        if (this.excludeAssembliesForScan.Contains(assemblyName))
        ////        {
        ////            this.excludeAssembliesForScan.Remove(assemblyName);
        ////        }
        ////    }

        ////    return new XmlSitemapFeedStrategyBuilder(this.defaultProtocol, this.defaultHostName, this.defaultFeedRootPageName, 
        ////        this.defaultFeedPageName, this.namedFeedRootPageName, this.namedFeedPageName, this.includeAssembliesForScan, 
        ////        this.excludeAssembliesForScan, this.xmlSitemapFeeds, this.xmlSitemapProviderFactory, 
        ////        this.xmlSitemapUrlResolverFactory, this.appDomainFactory);
        ////}

        ////public IXmlSitemapFeedStrategyStarter WithAssemblyForXmlSitemapProviderScan(Assembly assembly)
        ////{
        ////    if (assembly != null)
        ////    {
        ////        var assemblyName = new AssemblyName(assembly.FullName).Name;
        ////        if (!this.includeAssembliesForScan.Contains(assembly))
        ////        {
        ////            this.includeAssembliesForScan.Add(assembly);
        ////        }
        ////        if (this.excludeAssembliesForScan.Contains(assemblyName))
        ////        {
        ////            this.excludeAssembliesForScan.Remove(assemblyName);
        ////        }
        ////    }
        ////    return new XmlSitemapFeedStrategyBuilder(this.defaultProtocol, this.defaultHostName, this.defaultFeedRootPageName,
        ////        this.defaultFeedPageName, this.namedFeedRootPageName, this.namedFeedPageName, this.includeAssembliesForScan,
        ////        this.excludeAssembliesForScan, this.xmlSitemapFeeds, this.xmlSitemapProviderFactory,
        ////        this.xmlSitemapUrlResolverFactory, this.appDomainFactory);
        ////}

        ////public IXmlSitemapFeedStrategyStarter OmitAssemblyFromXmlSitemapProviderScan(string assemblyName)
        ////{
        ////    if (!string.IsNullOrEmpty(assemblyName))
        ////    {
        ////        var assembly = this.includeAssembliesForScan.Where(a => new AssemblyName(a.FullName).Name.Equals(assemblyName)).FirstOrDefault();
        ////        if (assembly != null && this.includeAssembliesForScan.Contains(assembly))
        ////        {
        ////            this.includeAssembliesForScan.Remove(assembly);
        ////        }
        ////        if (!this.excludeAssembliesForScan.Contains(assemblyName))
        ////        {
        ////            this.excludeAssembliesForScan.Add(assemblyName);
        ////        }
        ////    }
        ////    return new XmlSitemapFeedStrategyBuilder(this.defaultProtocol, this.defaultHostName, this.defaultFeedRootPageName,
        ////        this.defaultFeedPageName, this.namedFeedRootPageName, this.namedFeedPageName, this.includeAssembliesForScan,
        ////        this.excludeAssembliesForScan, this.xmlSitemapFeeds, this.xmlSitemapProviderFactory,
        ////        this.xmlSitemapUrlResolverFactory, this.appDomainFactory);
        ////}

        ////public IXmlSitemapFeedStrategyStarter OmitAssemblyForXmlSitemapProviderScan(Assembly assembly)
        ////{
        ////    var assemblyName = new AssemblyName(assembly.FullName).Name;
        ////    if (assembly != null && this.includeAssembliesForScan.Contains(assembly))
        ////    {
        ////        this.includeAssembliesForScan.Remove(assembly);
        ////    }
        ////    if (!this.excludeAssembliesForScan.Contains(assemblyName))
        ////    {
        ////        this.excludeAssembliesForScan.Add(assemblyName);
        ////    }
        ////    return new XmlSitemapFeedStrategyBuilder(this.defaultProtocol, this.defaultHostName, this.defaultFeedRootPageName,
        ////        this.defaultFeedPageName, this.namedFeedRootPageName, this.namedFeedPageName, this.includeAssembliesForScan,
        ////        this.excludeAssembliesForScan, this.xmlSitemapFeeds, this.xmlSitemapProviderFactory,
        ////        this.xmlSitemapUrlResolverFactory, this.appDomainFactory);
        ////}

        ////public IXmlSitemapFeedStrategyStarter WithXmlSitemapProviderFactory(IXmlSitemapProviderFactory xmlSitemapProviderFactory)
        ////{
        ////    return new XmlSitemapFeedStrategyBuilder(this.defaultProtocol, this.defaultHostName, this.defaultFeedRootPageName,
        ////        this.defaultFeedPageName, this.namedFeedRootPageName, this.namedFeedPageName, this.includeAssembliesForScan,
        ////        this.excludeAssembliesForScan, this.xmlSitemapFeeds, xmlSitemapProviderFactory,
        ////        this.xmlSitemapUrlResolverFactory, this.appDomainFactory);
        ////}

        ////public IXmlSitemapFeedStrategyStarter WithXmlSitemapUrlResolverFactory(IXmlSitemapUrlResolverFactory xmlSitemapUrlResolverFactory)
        ////{
        ////    return new XmlSitemapFeedStrategyBuilder(this.defaultProtocol, this.defaultHostName, this.defaultFeedRootPageName,
        ////        this.defaultFeedPageName, this.namedFeedRootPageName, this.namedFeedPageName, this.includeAssembliesForScan,
        ////        this.excludeAssembliesForScan, this.xmlSitemapFeeds, this.xmlSitemapProviderFactory,
        ////        xmlSitemapUrlResolverFactory, this.appDomainFactory);
        ////}

        ////public IXmlSitemapFeedStrategyStarter WithAssemblyProviderFactory(IAssemblyProviderFactory assemblyProviderFactory)
        ////{
        ////    throw new NotImplementedException();
        ////}

        //#endregion

        //public IXmlSitemapFeedStrategyBuilder AddNamedFeed(string feedName, Func<IXmlSitemap_SetupFeed_Starter, IXmlSitemap_SetupFeed_Finalizer> expression)
        //{
        //    //var assemblyProviderFactory = CreateAssemblyProviderFactory();
        //    var xmlSitemapFeedPageNameProvider = new XmlSitemapFeedPageNameProvider(
        //        this.defaultFeedRootPageName, this.defaultFeedPageName, this.namedFeedRootPageName, this.namedFeedPageName);

        //    var starter = new XmlSitemapFeedBuilderFacade(feedName, this.xmlSitemapProviderFactory, this.xmlSitemapUrlResolverFactory,
        //        this.assemblyProviderFactory, xmlSitemapFeedPageNameProvider);
        //    var builder = expression(starter);
        //    var feed = builder.Create();
        //    this.xmlSitemapFeeds.Add(feed.Name, new XmlSitemapFeedToXmlSitemapFeedBuilderRelation(feed, builder));

        //    return new XmlSitemapFeedStrategyBuilder(this.defaultFeedRootPageName, this.defaultFeedPageName,
        //        this.namedFeedRootPageName, this.namedFeedPageName, this.xmlSitemapFeeds, this.xmlSitemapProviderFactory,
        //        this.xmlSitemapUrlResolverFactory, this.assemblyProviderFactory);
        //}

        //public IXmlSitemapFeedStrategyBuilder AddDefaultFeed()
        //{
        //    return this.AddNamedFeed("default", x => x);
        //}

        //public IXmlSitemapFeedStrategyBuilder RemoveNamedFeed(string feedName)
        //{
        //    this.xmlSitemapFeeds.Remove(feedName);
        //    return new XmlSitemapFeedStrategyBuilder(this.defaultFeedRootPageName, this.defaultFeedPageName,
        //        this.namedFeedRootPageName, this.namedFeedPageName, this.xmlSitemapFeeds, this.xmlSitemapProviderFactory,
        //        this.xmlSitemapUrlResolverFactory, this.assemblyProviderFactory);
        //}

        //public IXmlSitemapFeedStrategyBuilder RemoveDefaultFeed()
        //{
        //    return this.RemoveNamedFeed("default");
        //}

        //public IXmlSitemapFeedStrategyBuilder ClearFeeds()
        //{
        //    this.xmlSitemapFeeds.Clear();
        //    return new XmlSitemapFeedStrategyBuilder(this.defaultFeedRootPageName, this.defaultFeedPageName,
        //        this.namedFeedRootPageName, this.namedFeedPageName, this.xmlSitemapFeeds, this.xmlSitemapProviderFactory,
        //        this.xmlSitemapUrlResolverFactory, this.assemblyProviderFactory);
        //}

        //public IEnumerable<IXmlSitemapFeed> XmlSitemapFeeds
        //{
        //    get { return this.xmlSitemapFeeds.Values.Select(x => x.XmlSitemapFeed).ToArray(); }
        //}

        //public IXmlSitemapProviderFactory XmlSitemapProviderFactory
        //{
        //    get { return this.xmlSitemapProviderFactory; }
        //}

        //public IXmlSitemapUrlResolverFactory XmlSitemapUrlResolverFactory
        //{
        //    get { return this.xmlSitemapUrlResolverFactory; }
        //}

        ////public string DefaultProtocol
        ////{
        ////    get { return this.defaultProtocol; }
        ////}

        ////public string DefaultHostName
        ////{
        ////    get { return this.defaultHostName; }
        ////}

        //public string DefaultFeedRootPageName
        //{
        //    get { return this.defaultFeedRootPageName; }
        //}

        //public string DefaultFeedPageName
        //{
        //    get { return this.defaultFeedPageName; }
        //}

        //public string NamedFeedRootPageName
        //{
        //    get { return this.namedFeedRootPageName; }
        //}

        //public string NamedFeedPageName
        //{
        //    get { return this.namedFeedPageName; }
        //}

        ////public IEnumerable<Assembly> AssembliesToScanForXmlSitemapProvider
        ////{
        ////    get { return this.includeAssembliesForScan; }
        ////}

        //public IXmlSitemapFeedStrategy Create()
        //{
        //    //var assemblyProvider = new AttributeAssemblyProvider(this.includeAssembliesForScan, this.excludeAssembliesForScan);
        //    //var assemblyProviderFactory = this.CreateAssemblyProviderFactory();
        //    //var xmlSitemapFeedPageNameProvider = new XmlSitemapFeedPageNameProvider(
        //    //    this.DefaultFeedRootPageName, this.DefaultFeedPageName, this.NamedFeedRootPageName, this.NamedFeedPageName);

        //    // Copy the dictionary
        //    var feeds = new Dictionary<string, XmlSitemapFeedToXmlSitemapFeedBuilderRelation>(this.xmlSitemapFeeds);

        //    // Rebuild the feeds that have a builder using the latest state
        //    foreach (var xmlSitemapFeedWrapper in feeds)
        //    {
        //        if (xmlSitemapFeedWrapper.Value.XmlSitemapFeedBuilderFacade != null)
        //        {
        //            var feed = xmlSitemapFeedWrapper.Value.XmlSitemapFeed;
        //            var builder = xmlSitemapFeedWrapper.Value.XmlSitemapFeedBuilderFacade;

        //            builder = new XmlSitemapFeedBuilderFacade(
        //                builder,
        //                this.xmlSitemapProviderFactory,
        //                this.xmlSitemapUrlResolverFactory,
        //                this.assemblyProviderFactory,
        //                this.xmlSitemapFeedPageNameProvider);

        //            // Reset the dictionary to the latest state.
        //            this.xmlSitemapFeeds[feed.Name] = new XmlSitemapFeedToXmlSitemapFeedBuilderRelation(builder.Create(), builder);
        //        }
        //    }

        //    return new XmlSitemapFeedStrategy(this.XmlSitemapFeeds.ToArray());
        //}

        //private Assembly GetAssemblyNamed(string assemblyName)
        //{
        //    var appDomain = this.appDomainFactory.Create();
        //    try
        //    {
        //        return appDomain.GetAssemblies().Where(a => new AssemblyName(a.FullName).Name.Equals(assemblyName)).FirstOrDefault();
        //    }
        //    finally
        //    {
        //        this.appDomainFactory.Release(appDomain);
        //    }
        //}

        //private IAssemblyProviderFactory CreateAssemblyProviderFactory()
        //{
        //    return new AssemblyProviderFactory(this.includeAssembliesForScan, this.excludeAssembliesForScan, new AttributeAssemblyProviderFactory());
        //}




    //public class XmlSitemapFeedStrategyBuilder
    //    : IXmlSitemapFeedStrategyStarter, IXmlSitemapFeedStrategyBuilder
    //{
    //    public XmlSitemapFeedStrategyBuilder()
    //        : this(
    //        defaultFeedRootPageName: "sitemap.xml",
    //        defaultFeedPageName: "sitemap-{page}.xml", 
    //        namedFeedRootPageName: "{feedName}-sitemap.xml", 
    //        namedFeedPageName: "{feedName}-sitemap-{page}.xml",
    //        xmlSitemapFeeds: new Dictionary<string, XmlSitemapFeedToXmlSitemapFeedBuilderRelation>(),
    //        xmlSitemapProviderFactory: new XmlSitemapProviderFactory(),
    //        xmlSitemapUrlResolverFactory: new XmlSitemapUrlResolverFactoryBuilder().Create(),
    //        assemblyProviderFactory: new AssemblyProviderFactoryBuilder().Create()
    //        )
    //    {
    //    }

    //    private XmlSitemapFeedStrategyBuilder(
    //        string defaultFeedRootPageName,
    //        string defaultFeedPageName,
    //        string namedFeedRootPageName,
    //        string namedFeedPageName,
    //        IDictionary<string, XmlSitemapFeedToXmlSitemapFeedBuilderRelation> xmlSitemapFeeds,
    //        IXmlSitemapProviderFactory xmlSitemapProviderFactory,
    //        IXmlSitemapUrlResolverFactory xmlSitemapUrlResolverFactory,
    //        IAssemblyProviderFactory assemblyProviderFactory
    //        )
    //    {
    //        if (string.IsNullOrEmpty(defaultFeedRootPageName))
    //            throw new ArgumentNullException("defaultFeedRootPageName");
    //        if (string.IsNullOrEmpty(defaultFeedPageName))
    //            throw new ArgumentNullException("defaultFeedPageName");
    //        if (string.IsNullOrEmpty(namedFeedRootPageName))
    //            throw new ArgumentNullException("namedFeedRootPageName");
    //        if (string.IsNullOrEmpty(namedFeedPageName))
    //            throw new ArgumentNullException("namedFeedPageName");
    //        if (xmlSitemapFeeds == null)
    //            throw new ArgumentNullException("xmlSitemapFeeds");
    //        if (xmlSitemapProviderFactory == null)
    //            throw new ArgumentNullException("xmlSitemapProviderFactory");
    //        if (xmlSitemapUrlResolverFactory == null)
    //            throw new ArgumentNullException("xmlSitemapUrlResolverFactory");
    //        if (assemblyProviderFactory == null)
    //            throw new ArgumentNullException("assemblyProviderFactory");

    //        this.defaultFeedRootPageName = defaultFeedRootPageName;
    //        this.defaultFeedPageName = defaultFeedPageName;
    //        this.namedFeedRootPageName = namedFeedRootPageName;
    //        this.namedFeedPageName = namedFeedPageName;
    //        this.xmlSitemapFeeds = xmlSitemapFeeds;
    //        this.xmlSitemapProviderFactory = xmlSitemapProviderFactory;
    //        this.xmlSitemapUrlResolverFactory = xmlSitemapUrlResolverFactory;
    //        this.assemblyProviderFactory = assemblyProviderFactory;
    //    }
    //    private readonly string defaultFeedRootPageName;
    //    private readonly string defaultFeedPageName;
    //    private readonly string namedFeedRootPageName;
    //    private readonly string namedFeedPageName;
    //    private readonly IDictionary<string, XmlSitemapFeedToXmlSitemapFeedBuilderRelation> xmlSitemapFeeds;
    //    private readonly IXmlSitemapProviderFactory xmlSitemapProviderFactory;
    //    private readonly IXmlSitemapUrlResolverFactory xmlSitemapUrlResolverFactory;
    //    private readonly IAssemblyProviderFactory assemblyProviderFactory;

        

    //    #region IXmlSitemapFeedStrategyStarter members

    //    //public IXmlSitemapFeedStrategyStarter WithDefaultProtocol(string defaultProtocol)
    //    //{
    //    //    return new XmlSitemapFeedStrategyBuilder(defaultProtocol, this.defaultHostName, this.defaultFeedRootPageName,
    //    //        this.defaultFeedPageName, this.namedFeedRootPageName, this.namedFeedPageName, this.includeAssembliesForScan,
    //    //        this.excludeAssembliesForScan, this.xmlSitemapFeeds, this.xmlSitemapProviderFactory,
    //    //        this.xmlSitemapUrlResolverFactory, this.appDomainFactory);
    //    //}

    //    //public IXmlSitemapFeedStrategyStarter WithDefaultHostName(string defaultHostName)
    //    //{
    //    //    return new XmlSitemapFeedStrategyBuilder(this.defaultProtocol, defaultHostName, this.defaultFeedRootPageName,
    //    //        this.defaultFeedPageName, this.namedFeedRootPageName, this.namedFeedPageName, this.includeAssembliesForScan,
    //    //        this.excludeAssembliesForScan, this.xmlSitemapFeeds, this.xmlSitemapProviderFactory,
    //    //        this.xmlSitemapUrlResolverFactory, this.appDomainFactory);
    //    //}

    //    public IXmlSitemapFeedStrategyStarter ForUrlResolving(Func<IXmlSitemap_SetupUrlResolver_Starter, IXmlSitemap_SetupUrlResolver_Finalizer> expression)
    //    {
    //        var starter = new XmlSitemap_SetupUrlResolver_Builder();
    //        var builder = expression(starter);
    //        var xmlSitemapUrlResolverFactory = builder.Create();

    //        return new XmlSitemapFeedStrategyBuilder(this.defaultFeedRootPageName, this.defaultFeedPageName, 
    //            this.namedFeedRootPageName, this.namedFeedPageName, this.xmlSitemapFeeds, this.xmlSitemapProviderFactory,
    //            xmlSitemapUrlResolverFactory, this.assemblyProviderFactory);
    //    }

    //    //public IXmlSitemapFeedStrategyStarter ForXmlSitemapProvider(Func<IXmlSitemapFeedStrategyForXmlSitemapProviderStarter,IXmlSitemapFeedStrategyForXmlSitemapProviderFinalizer> expression)
    //    //{
    //    //    var starter = new XmlSitemapFeedStrategyForXmlSitemapProviderBuilder();
    //    //    var builder = expression(starter);
    //    //    var assemblyProviderFactory = builder.Create();
    //    //    var xmlSitemapProviderFactory = builder.XmlSitemapProviderFactory;

    //    //    return new XmlSitemapFeedStrategyBuilder(this.defaultFeedRootPageName, this.defaultFeedPageName,
    //    //        this.namedFeedRootPageName, this.namedFeedPageName, this.xmlSitemapFeeds, xmlSitemapProviderFactory,
    //    //        this.xmlSitemapUrlResolverFactory, assemblyProviderFactory);
    //    //}

    //    public IXmlSitemapFeedStrategyStarter ForXmlSitemapProvider(Func<IXmlSitemap_SetupXmlSitemapProviderScan_Starter, IXmlSitemap_SetupXmlSitemapProviderScan_Finalizer> expression)
    //    {
    //        //var starter = new XmlSitemapFeedStrategyForXmlSitemapProviderBuilder();
    //        //var builder = expression(starter);
    //        //var assemblyProviderFactory = builder.Create();
    //        //var xmlSitemapProviderFactory = builder.XmlSitemapProviderFactory;

    //        return new XmlSitemapFeedStrategyBuilder(this.defaultFeedRootPageName, this.defaultFeedPageName,
    //            this.namedFeedRootPageName, this.namedFeedPageName, this.xmlSitemapFeeds, xmlSitemapProviderFactory,
    //            this.xmlSitemapUrlResolverFactory, assemblyProviderFactory);
    //    }

    //    public IXmlSitemapFeedStrategyStarter WithDefaultFeedPageNameTemplates(string rootPageTemplate, string pageTemplate)
    //    {
    //        return new XmlSitemapFeedStrategyBuilder(rootPageTemplate, pageTemplate,
    //            this.namedFeedRootPageName, this.namedFeedPageName, this.xmlSitemapFeeds, this.xmlSitemapProviderFactory,
    //            xmlSitemapUrlResolverFactory, this.assemblyProviderFactory);
    //    }

    //    public IXmlSitemapFeedStrategyStarter WithNamedFeedPageNameTemplates(string rootPageTemplate, string pageTemplate)
    //    {
    //        return new XmlSitemapFeedStrategyBuilder(this.defaultFeedRootPageName, this.defaultFeedPageName,
    //            rootPageTemplate, pageTemplate, this.xmlSitemapFeeds, this.xmlSitemapProviderFactory,
    //            xmlSitemapUrlResolverFactory, this.assemblyProviderFactory);
    //    }

    //    //public IXmlSitemapFeedStrategyStarter WithAssemblyForXmlSitemapProviderScan(string assemblyName)
    //    //{
    //    //    if (!string.IsNullOrEmpty(assemblyName))
    //    //    {
    //    //        var assembly = this.GetAssemblyNamed(assemblyName);
    //    //        if (assembly != null)
    //    //        {
    //    //            this.includeAssembliesForScan.Add(assembly);
    //    //        }
    //    //        if (this.excludeAssembliesForScan.Contains(assemblyName))
    //    //        {
    //    //            this.excludeAssembliesForScan.Remove(assemblyName);
    //    //        }
    //    //    }

    //    //    return new XmlSitemapFeedStrategyBuilder(this.defaultProtocol, this.defaultHostName, this.defaultFeedRootPageName, 
    //    //        this.defaultFeedPageName, this.namedFeedRootPageName, this.namedFeedPageName, this.includeAssembliesForScan, 
    //    //        this.excludeAssembliesForScan, this.xmlSitemapFeeds, this.xmlSitemapProviderFactory, 
    //    //        this.xmlSitemapUrlResolverFactory, this.appDomainFactory);
    //    //}

    //    //public IXmlSitemapFeedStrategyStarter WithAssemblyForXmlSitemapProviderScan(Assembly assembly)
    //    //{
    //    //    if (assembly != null)
    //    //    {
    //    //        var assemblyName = new AssemblyName(assembly.FullName).Name;
    //    //        if (!this.includeAssembliesForScan.Contains(assembly))
    //    //        {
    //    //            this.includeAssembliesForScan.Add(assembly);
    //    //        }
    //    //        if (this.excludeAssembliesForScan.Contains(assemblyName))
    //    //        {
    //    //            this.excludeAssembliesForScan.Remove(assemblyName);
    //    //        }
    //    //    }
    //    //    return new XmlSitemapFeedStrategyBuilder(this.defaultProtocol, this.defaultHostName, this.defaultFeedRootPageName,
    //    //        this.defaultFeedPageName, this.namedFeedRootPageName, this.namedFeedPageName, this.includeAssembliesForScan,
    //    //        this.excludeAssembliesForScan, this.xmlSitemapFeeds, this.xmlSitemapProviderFactory,
    //    //        this.xmlSitemapUrlResolverFactory, this.appDomainFactory);
    //    //}

    //    //public IXmlSitemapFeedStrategyStarter OmitAssemblyFromXmlSitemapProviderScan(string assemblyName)
    //    //{
    //    //    if (!string.IsNullOrEmpty(assemblyName))
    //    //    {
    //    //        var assembly = this.includeAssembliesForScan.Where(a => new AssemblyName(a.FullName).Name.Equals(assemblyName)).FirstOrDefault();
    //    //        if (assembly != null && this.includeAssembliesForScan.Contains(assembly))
    //    //        {
    //    //            this.includeAssembliesForScan.Remove(assembly);
    //    //        }
    //    //        if (!this.excludeAssembliesForScan.Contains(assemblyName))
    //    //        {
    //    //            this.excludeAssembliesForScan.Add(assemblyName);
    //    //        }
    //    //    }
    //    //    return new XmlSitemapFeedStrategyBuilder(this.defaultProtocol, this.defaultHostName, this.defaultFeedRootPageName,
    //    //        this.defaultFeedPageName, this.namedFeedRootPageName, this.namedFeedPageName, this.includeAssembliesForScan,
    //    //        this.excludeAssembliesForScan, this.xmlSitemapFeeds, this.xmlSitemapProviderFactory,
    //    //        this.xmlSitemapUrlResolverFactory, this.appDomainFactory);
    //    //}

    //    //public IXmlSitemapFeedStrategyStarter OmitAssemblyForXmlSitemapProviderScan(Assembly assembly)
    //    //{
    //    //    var assemblyName = new AssemblyName(assembly.FullName).Name;
    //    //    if (assembly != null && this.includeAssembliesForScan.Contains(assembly))
    //    //    {
    //    //        this.includeAssembliesForScan.Remove(assembly);
    //    //    }
    //    //    if (!this.excludeAssembliesForScan.Contains(assemblyName))
    //    //    {
    //    //        this.excludeAssembliesForScan.Add(assemblyName);
    //    //    }
    //    //    return new XmlSitemapFeedStrategyBuilder(this.defaultProtocol, this.defaultHostName, this.defaultFeedRootPageName,
    //    //        this.defaultFeedPageName, this.namedFeedRootPageName, this.namedFeedPageName, this.includeAssembliesForScan,
    //    //        this.excludeAssembliesForScan, this.xmlSitemapFeeds, this.xmlSitemapProviderFactory,
    //    //        this.xmlSitemapUrlResolverFactory, this.appDomainFactory);
    //    //}

    //    //public IXmlSitemapFeedStrategyStarter WithXmlSitemapProviderFactory(IXmlSitemapProviderFactory xmlSitemapProviderFactory)
    //    //{
    //    //    return new XmlSitemapFeedStrategyBuilder(this.defaultProtocol, this.defaultHostName, this.defaultFeedRootPageName,
    //    //        this.defaultFeedPageName, this.namedFeedRootPageName, this.namedFeedPageName, this.includeAssembliesForScan,
    //    //        this.excludeAssembliesForScan, this.xmlSitemapFeeds, xmlSitemapProviderFactory,
    //    //        this.xmlSitemapUrlResolverFactory, this.appDomainFactory);
    //    //}

    //    //public IXmlSitemapFeedStrategyStarter WithXmlSitemapUrlResolverFactory(IXmlSitemapUrlResolverFactory xmlSitemapUrlResolverFactory)
    //    //{
    //    //    return new XmlSitemapFeedStrategyBuilder(this.defaultProtocol, this.defaultHostName, this.defaultFeedRootPageName,
    //    //        this.defaultFeedPageName, this.namedFeedRootPageName, this.namedFeedPageName, this.includeAssembliesForScan,
    //    //        this.excludeAssembliesForScan, this.xmlSitemapFeeds, this.xmlSitemapProviderFactory,
    //    //        xmlSitemapUrlResolverFactory, this.appDomainFactory);
    //    //}

    //    //public IXmlSitemapFeedStrategyStarter WithAssemblyProviderFactory(IAssemblyProviderFactory assemblyProviderFactory)
    //    //{
    //    //    throw new NotImplementedException();
    //    //}

    //    #endregion

    //    public IXmlSitemapFeedStrategyBuilder AddNamedFeed(string feedName, Func<IXmlSitemap_SetupFeed_Starter, IXmlSitemap_SetupFeed_Finalizer> expression)
    //    {
    //        //var assemblyProviderFactory = CreateAssemblyProviderFactory();
    //        var xmlSitemapFeedPageNameProvider = new XmlSitemapFeedPageNameProvider(
    //            this.defaultFeedRootPageName, this.defaultFeedPageName, this.namedFeedRootPageName, this.namedFeedPageName);

    //        var starter = new XmlSitemapFeedBuilderFacade(feedName, this.xmlSitemapProviderFactory, this.xmlSitemapUrlResolverFactory,
    //            this.assemblyProviderFactory, xmlSitemapFeedPageNameProvider);
    //        var builder = expression(starter);
    //        var feed = builder.Create();
    //        this.xmlSitemapFeeds.Add(feed.Name, new XmlSitemapFeedToXmlSitemapFeedBuilderRelation(feed, builder));

    //        return new XmlSitemapFeedStrategyBuilder(this.defaultFeedRootPageName, this.defaultFeedPageName,
    //            this.namedFeedRootPageName, this.namedFeedPageName, this.xmlSitemapFeeds, this.xmlSitemapProviderFactory,
    //            this.xmlSitemapUrlResolverFactory, this.assemblyProviderFactory);
    //    }

    //    public IXmlSitemapFeedStrategyBuilder AddDefaultFeed()
    //    {
    //        return this.AddNamedFeed("default", x => x);
    //    }

    //    public IXmlSitemapFeedStrategyBuilder RemoveNamedFeed(string feedName)
    //    {
    //        this.xmlSitemapFeeds.Remove(feedName);
    //        return new XmlSitemapFeedStrategyBuilder(this.defaultFeedRootPageName, this.defaultFeedPageName,
    //            this.namedFeedRootPageName, this.namedFeedPageName, this.xmlSitemapFeeds, this.xmlSitemapProviderFactory,
    //            this.xmlSitemapUrlResolverFactory, this.assemblyProviderFactory);
    //    }

    //    public IXmlSitemapFeedStrategyBuilder RemoveDefaultFeed()
    //    {
    //        return this.RemoveNamedFeed("default");
    //    }

    //    public IXmlSitemapFeedStrategyBuilder ClearFeeds()
    //    {
    //        this.xmlSitemapFeeds.Clear();
    //        return new XmlSitemapFeedStrategyBuilder(this.defaultFeedRootPageName, this.defaultFeedPageName,
    //            this.namedFeedRootPageName, this.namedFeedPageName, this.xmlSitemapFeeds, this.xmlSitemapProviderFactory,
    //            this.xmlSitemapUrlResolverFactory, this.assemblyProviderFactory);
    //    }

    //    public IEnumerable<IXmlSitemapFeed> XmlSitemapFeeds
    //    {
    //        get { return this.xmlSitemapFeeds.Values.Select(x => x.XmlSitemapFeed).ToArray(); }
    //    }

    //    public IXmlSitemapProviderFactory XmlSitemapProviderFactory
    //    {
    //        get { return this.xmlSitemapProviderFactory; }
    //    }

    //    public IXmlSitemapUrlResolverFactory XmlSitemapUrlResolverFactory
    //    {
    //        get { return this.xmlSitemapUrlResolverFactory; }
    //    }

    //    //public string DefaultProtocol
    //    //{
    //    //    get { return this.defaultProtocol; }
    //    //}

    //    //public string DefaultHostName
    //    //{
    //    //    get { return this.defaultHostName; }
    //    //}

    //    public string DefaultFeedRootPageName
    //    {
    //        get { return this.defaultFeedRootPageName; }
    //    }

    //    public string DefaultFeedPageName
    //    {
    //        get { return this.defaultFeedPageName; }
    //    }

    //    public string NamedFeedRootPageName
    //    {
    //        get { return this.namedFeedRootPageName; }
    //    }

    //    public string NamedFeedPageName
    //    {
    //        get { return this.namedFeedPageName; }
    //    }

    //    //public IEnumerable<Assembly> AssembliesToScanForXmlSitemapProvider
    //    //{
    //    //    get { return this.includeAssembliesForScan; }
    //    //}

    //    public IXmlSitemapFeedStrategy Create()
    //    {
    //        //var assemblyProvider = new AttributeAssemblyProvider(this.includeAssembliesForScan, this.excludeAssembliesForScan);
    //        //var assemblyProviderFactory = this.CreateAssemblyProviderFactory();
    //        var xmlSitemapFeedPageNameProvider = new XmlSitemapFeedPageNameProvider(
    //            this.DefaultFeedRootPageName, this.DefaultFeedPageName, this.NamedFeedRootPageName, this.NamedFeedPageName);

    //        // Copy the dictionary
    //        var feeds = new Dictionary<string, XmlSitemapFeedToXmlSitemapFeedBuilderRelation>(this.xmlSitemapFeeds);

    //        // Rebuild the feeds that have a builder using the latest state
    //        foreach (var xmlSitemapFeedWrapper in feeds)
    //        {
    //            if (xmlSitemapFeedWrapper.Value.XmlSitemapFeedBuilderFacade != null)
    //            {
    //                var feed = xmlSitemapFeedWrapper.Value.XmlSitemapFeed;
    //                var builder = xmlSitemapFeedWrapper.Value.XmlSitemapFeedBuilderFacade;

    //                builder = new XmlSitemapFeedBuilderFacade(
    //                    builder, 
    //                    this.xmlSitemapProviderFactory, 
    //                    this.xmlSitemapUrlResolverFactory, 
    //                    this.assemblyProviderFactory, 
    //                    xmlSitemapFeedPageNameProvider);

    //                // Reset the dictionary to the latest state.
    //                this.xmlSitemapFeeds[feed.Name] = new XmlSitemapFeedToXmlSitemapFeedBuilderRelation(builder.Create(), builder);
    //            }
    //        }

    //        return new XmlSitemapFeedStrategy(this.XmlSitemapFeeds.ToArray());
    //    }

    //    //private Assembly GetAssemblyNamed(string assemblyName)
    //    //{
    //    //    var appDomain = this.appDomainFactory.Create();
    //    //    try
    //    //    {
    //    //        return appDomain.GetAssemblies().Where(a => new AssemblyName(a.FullName).Name.Equals(assemblyName)).FirstOrDefault();
    //    //    }
    //    //    finally
    //    //    {
    //    //        this.appDomainFactory.Release(appDomain);
    //    //    }
    //    //}

    //    //private IAssemblyProviderFactory CreateAssemblyProviderFactory()
    //    //{
    //    //    return new AssemblyProviderFactory(this.includeAssembliesForScan, this.excludeAssembliesForScan, new AttributeAssemblyProviderFactory());
    //    //}

    //    private class XmlSitemapFeedToXmlSitemapFeedBuilderRelation
    //    {
    //        public XmlSitemapFeedToXmlSitemapFeedBuilderRelation(
    //            IXmlSitemapFeed xmlSitemapFeed,
    //            IXmlSitemapFeedBuilderFacade xmlSitemapFeedBuilderFacade
    //            )
    //        {
    //            if (xmlSitemapFeed == null)
    //                throw new ArgumentNullException("xmlSitemapFeed");

    //            this.XmlSitemapFeed = xmlSitemapFeed;
    //            this.XmlSitemapFeedBuilderFacade = xmlSitemapFeedBuilderFacade;
    //        }

    //        public IXmlSitemapFeed XmlSitemapFeed { get; private set; }

    //        public IXmlSitemapFeedBuilderFacade XmlSitemapFeedBuilderFacade { get; private set; }
    //    }








}