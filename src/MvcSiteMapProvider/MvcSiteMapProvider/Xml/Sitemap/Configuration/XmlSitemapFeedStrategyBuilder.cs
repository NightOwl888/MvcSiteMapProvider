using System;
using System.Collections.Generic;
using System.Linq;
using MvcSiteMapProvider.Reflection;

namespace MvcSiteMapProvider.Xml.Sitemap.Configuration
{
    public class XmlSitemapFeedStrategyBuilder
        : IXmlSitemapFeedStrategyStarter, IXmlSitemapFeedStrategyBuilder
    {
        public XmlSitemapFeedStrategyBuilder()
            : this(
            defaultFeedRootPageName: "sitemap.xml",
            defaultFeedPageName: "sitemap-{page}.xml", 
            namedFeedRootPageName: "{feedName}-sitemap.xml", 
            namedFeedPageName: "{feedName}-sitemap-{page}.xml",
            includeAssembliesForScan: new List<string>(),
            excludeAssembliesForScan: new List<string>(),
            xmlSitemapFeeds: new Dictionary<string, XmlSitemapFeedToXmlSitemapFeedBuilderRelation>(),
            xmlSitemapProviderFactory: new XmlSitemapProviderFactory(),
            xmlSitemapUrlResolverFactory: new XmlSitemapUrlResolverFactoryBuilder().Create()
            )
        {
        }

        private XmlSitemapFeedStrategyBuilder(
            string defaultFeedRootPageName,
            string defaultFeedPageName,
            string namedFeedRootPageName,
            string namedFeedPageName,
            IList<string> includeAssembliesForScan,
            IList<string> excludeAssembliesForScan,
            IDictionary<string, XmlSitemapFeedToXmlSitemapFeedBuilderRelation> xmlSitemapFeeds,
            IXmlSitemapProviderFactory xmlSitemapProviderFactory,
            IXmlSitemapUrlResolverFactory xmlSitemapUrlResolverFactory
            )
        {
            if (string.IsNullOrEmpty(defaultFeedRootPageName))
                throw new ArgumentNullException("defaultFeedRootPageName");
            if (string.IsNullOrEmpty(defaultFeedPageName))
                throw new ArgumentNullException("defaultFeedPageName");
            if (string.IsNullOrEmpty(namedFeedRootPageName))
                throw new ArgumentNullException("namedFeedRootPageName");
            if (string.IsNullOrEmpty(namedFeedPageName))
                throw new ArgumentNullException("namedFeedPageName");
            if (includeAssembliesForScan == null)
                throw new ArgumentNullException("includeAssembliesForScan");
            if (excludeAssembliesForScan == null)
                throw new ArgumentNullException("excludeAssembliesForScan");
            if (xmlSitemapFeeds == null)
                throw new ArgumentNullException("xmlSitemapFeeds");
            if (xmlSitemapProviderFactory == null)
                throw new ArgumentNullException("xmlSitemapProviderFactory");
            if (xmlSitemapUrlResolverFactory == null)
                throw new ArgumentNullException("xmlSitemapUrlResolverFactory");

            this.defaultFeedRootPageName = defaultFeedRootPageName;
            this.defaultFeedPageName = defaultFeedPageName;
            this.namedFeedRootPageName = namedFeedRootPageName;
            this.namedFeedPageName = namedFeedPageName;
            this.includeAssembliesForScan = includeAssembliesForScan;
            this.excludeAssembliesForScan = excludeAssembliesForScan;
            this.xmlSitemapFeeds = xmlSitemapFeeds;
            this.xmlSitemapProviderFactory = xmlSitemapProviderFactory;
            this.xmlSitemapUrlResolverFactory = xmlSitemapUrlResolverFactory;
        }
        private readonly string defaultFeedRootPageName;
        private readonly string defaultFeedPageName;
        private readonly string namedFeedRootPageName;
        private readonly string namedFeedPageName;
        private readonly IList<string> includeAssembliesForScan;
        private readonly IList<string> excludeAssembliesForScan;
        private readonly IDictionary<string, XmlSitemapFeedToXmlSitemapFeedBuilderRelation> xmlSitemapFeeds;
        private readonly IXmlSitemapProviderFactory xmlSitemapProviderFactory;
        private readonly IXmlSitemapUrlResolverFactory xmlSitemapUrlResolverFactory;
        

        #region IXmlSitemapFeedStrategyStarter members

        public IXmlSitemapFeedStrategyStarter WithDefaultFeedPageNameTemplates(string rootPageTemplate, string pageTemplate)
        {
            return new XmlSitemapFeedStrategyBuilder(rootPageTemplate, pageTemplate, this.namedFeedRootPageName, 
                this.namedFeedPageName, this.includeAssembliesForScan, this.excludeAssembliesForScan, 
                this.xmlSitemapFeeds, this.xmlSitemapProviderFactory, this.xmlSitemapUrlResolverFactory);
        }

        public IXmlSitemapFeedStrategyStarter WithNamedFeedPageNameTemplates(string rootPageTemplate, string pageTemplate)
        {
            return new XmlSitemapFeedStrategyBuilder(this.defaultFeedRootPageName, this.defaultFeedPageName, rootPageTemplate, 
                pageTemplate, this.includeAssembliesForScan, this.excludeAssembliesForScan, 
                this.xmlSitemapFeeds, this.xmlSitemapProviderFactory, this.xmlSitemapUrlResolverFactory);
        }

        


        public IXmlSitemapFeedStrategyStarter WithAssemblyForXmlSitemapProviderScan(string assemblyName)
        {
            if (!string.IsNullOrEmpty(assemblyName))
            {
                this.includeAssembliesForScan.Add(assemblyName);
            }
            return new XmlSitemapFeedStrategyBuilder(this.defaultFeedRootPageName, this.defaultFeedPageName, this.namedFeedRootPageName,
                this.namedFeedPageName, this.includeAssembliesForScan, this.excludeAssembliesForScan, 
                this.xmlSitemapFeeds, this.xmlSitemapProviderFactory, this.xmlSitemapUrlResolverFactory);
        }

        public IXmlSitemapFeedStrategyStarter OmitAssemblyFromXmlSitemapProviderScan(string assemblyName)
        {
            if (!string.IsNullOrEmpty(assemblyName))
            {
                if (this.includeAssembliesForScan.Contains(assemblyName))
                {
                    this.includeAssembliesForScan.Remove(assemblyName);
                }
                this.excludeAssembliesForScan.Add(assemblyName);
            }
            return new XmlSitemapFeedStrategyBuilder(this.defaultFeedRootPageName, this.defaultFeedPageName, this.namedFeedRootPageName,
                this.namedFeedPageName, this.includeAssembliesForScan, this.excludeAssembliesForScan,
                this.xmlSitemapFeeds, this.xmlSitemapProviderFactory, this.xmlSitemapUrlResolverFactory);
        }

        

        public IXmlSitemapFeedStrategyStarter WithXmlSitemapProviderFactory(IXmlSitemapProviderFactory xmlSitemapProviderFactory)
        {
            return new XmlSitemapFeedStrategyBuilder(this.defaultFeedRootPageName, this.defaultFeedPageName, this.namedFeedRootPageName,
                this.namedFeedPageName, this.includeAssembliesForScan, this.excludeAssembliesForScan,
                this.xmlSitemapFeeds, xmlSitemapProviderFactory, this.xmlSitemapUrlResolverFactory);
        }

        public IXmlSitemapFeedStrategyStarter WithXmlSitemapUrlResolverFactory(IXmlSitemapUrlResolverFactory xmlSitemapUrlResolverFactory)
        {
            return new XmlSitemapFeedStrategyBuilder(this.defaultFeedRootPageName, this.defaultFeedPageName, this.namedFeedRootPageName,
                this.namedFeedPageName, this.includeAssembliesForScan, this.excludeAssembliesForScan,
                this.xmlSitemapFeeds, xmlSitemapProviderFactory, xmlSitemapUrlResolverFactory);
        }




        #endregion

        public IXmlSitemapFeedStrategyBuilder AddFeed(string feedName, Func<IXmlSitemapFeedBuilderFacade, IXmlSitemapFeedBuilderFacade> expression)
        {
            var assemblyProvider = new AttributeAssemblyProvider(this.includeAssembliesForScan.ToArray(), this.excludeAssembliesForScan.ToArray());
            var xmlSitemapFeedPageNameProvider = new XmlSitemapFeedPageNameProvider(
                this.defaultFeedRootPageName, this.defaultFeedPageName, this.namedFeedRootPageName, this.namedFeedPageName);

            var starter = new XmlSitemapFeedBuilderFacade(feedName, this.xmlSitemapProviderFactory, this.xmlSitemapUrlResolverFactory, 
                assemblyProvider, xmlSitemapFeedPageNameProvider);
            var builder = expression(starter);
            var feed = builder.Create();
            this.xmlSitemapFeeds.Add(feed.Name, new XmlSitemapFeedToXmlSitemapFeedBuilderRelation(feed, builder));

            return new XmlSitemapFeedStrategyBuilder(this.defaultFeedRootPageName, this.defaultFeedPageName, this.namedFeedRootPageName,
                this.namedFeedPageName, this.includeAssembliesForScan, this.excludeAssembliesForScan,
                this.xmlSitemapFeeds, this.xmlSitemapProviderFactory, this.xmlSitemapUrlResolverFactory);
        }

        public IXmlSitemapFeedStrategyBuilder AddFeed(string feedName)
        {
            return this.AddFeed(feedName, x => x);
        }

        public IXmlSitemapFeedStrategyBuilder AddFeed(IXmlSitemapFeed xmlSitemapFeed)
        {
            if (xmlSitemapFeed != null)
            {
                this.xmlSitemapFeeds.Add(xmlSitemapFeed.Name, new XmlSitemapFeedToXmlSitemapFeedBuilderRelation(xmlSitemapFeed, null));
            }
            return new XmlSitemapFeedStrategyBuilder(this.defaultFeedRootPageName, this.defaultFeedPageName, this.namedFeedRootPageName,
                this.namedFeedPageName, this.includeAssembliesForScan, this.excludeAssembliesForScan,
                this.xmlSitemapFeeds, this.xmlSitemapProviderFactory, this.xmlSitemapUrlResolverFactory);
        }

        public IXmlSitemapFeedStrategyBuilder AddFeeds(IEnumerable<IXmlSitemapFeed> xmlSitemapFeeds)
        {
            foreach (var feed in xmlSitemapFeeds)
            {
                if (feed != null)
                {
                    this.xmlSitemapFeeds.Add(feed.Name, new XmlSitemapFeedToXmlSitemapFeedBuilderRelation(feed, null));
                }
            }
            return new XmlSitemapFeedStrategyBuilder(this.defaultFeedRootPageName, this.defaultFeedPageName, this.namedFeedRootPageName,
                this.namedFeedPageName, this.includeAssembliesForScan, this.excludeAssembliesForScan,
                this.xmlSitemapFeeds, this.xmlSitemapProviderFactory, this.xmlSitemapUrlResolverFactory);
        }

        public IXmlSitemapFeedStrategyBuilder RemoveFeed(string feedName)
        {
            this.xmlSitemapFeeds.Remove(feedName);
            return new XmlSitemapFeedStrategyBuilder(this.defaultFeedRootPageName, this.defaultFeedPageName, this.namedFeedRootPageName,
                this.namedFeedPageName, this.includeAssembliesForScan, this.excludeAssembliesForScan,
                this.xmlSitemapFeeds, this.xmlSitemapProviderFactory, this.xmlSitemapUrlResolverFactory);
        }

        public IXmlSitemapFeedStrategyBuilder ClearFeeds()
        {
            this.xmlSitemapFeeds.Clear();
            return new XmlSitemapFeedStrategyBuilder(this.defaultFeedRootPageName, this.defaultFeedPageName, this.namedFeedRootPageName,
                this.namedFeedPageName, this.includeAssembliesForScan, this.excludeAssembliesForScan,
                this.xmlSitemapFeeds, this.xmlSitemapProviderFactory, this.xmlSitemapUrlResolverFactory);
        }

        public IEnumerable<IXmlSitemapFeed> XmlSitemapFeeds
        {
            get { return this.xmlSitemapFeeds.Values.Select(x => x.XmlSitemapFeed).ToArray(); }
        }

        public IXmlSitemapProviderFactory XmlSitemapProviderFactory
        {
            get { return this.xmlSitemapProviderFactory; }
        }

        public IXmlSitemapUrlResolverFactory XmlSitemapUrlResolverFactory
        {
            get { return this.xmlSitemapUrlResolverFactory; }
        }

        public string DefaultFeedRootPageName
        {
            get { return this.defaultFeedRootPageName; }
        }

        public string DefaultFeedPageName
        {
            get { return this.defaultFeedPageName; }
        }

        public string NamedFeedRootPageName
        {
            get { return this.namedFeedRootPageName; }
        }

        public string NamedFeedPageName
        {
            get { return this.namedFeedPageName; }
        }

        public IEnumerable<string> AssembliesToScanForXmlSitemapProvider
        {
            get { return this.includeAssembliesForScan; }
        }

        public IXmlSitemapFeedStrategy Create()
        {
            var assemblyProvider = new AttributeAssemblyProvider(this.includeAssembliesForScan, this.excludeAssembliesForScan);
            var xmlSitemapFeedPageNameProvider = new XmlSitemapFeedPageNameProvider(
                this.DefaultFeedRootPageName, this.DefaultFeedPageName, this.NamedFeedRootPageName, this.NamedFeedPageName);

            // Copy the dictionary
            var feeds = new Dictionary<string, XmlSitemapFeedToXmlSitemapFeedBuilderRelation>(this.xmlSitemapFeeds);

            // Rebuild the feeds that have a builder using the latest state
            foreach (var xmlSitemapFeedWrapper in feeds)
            {
                if (xmlSitemapFeedWrapper.Value.XmlSitemapFeedBuilderFacade != null)
                {
                    var feed = xmlSitemapFeedWrapper.Value.XmlSitemapFeed;
                    var builder = xmlSitemapFeedWrapper.Value.XmlSitemapFeedBuilderFacade;

                    // Allow the XmlSitemapProviderFactory and XmlSitemapUrlResolverFactory to be
                    // overridden by the child builder.
                    var xmlSitemapProviderFactory = 
                        (builder.XmlSitemapProviderFactory.GetType() != typeof(XmlSitemapProviderFactory))
                        ? builder.XmlSitemapProviderFactory
                        : this.xmlSitemapProviderFactory;

                    var xmlSitemapUrlResolverFactory =
                        (builder.XmlSitemapUrlResolverFactory.GetType() != typeof(XmlSitemapUrlResolverFactory))
                        ? builder.XmlSitemapUrlResolverFactory
                        : this.xmlSitemapUrlResolverFactory;

                    builder = builder
                        .WithXmlSitemapProviderFactory(xmlSitemapProviderFactory)
                        .WithXmlSitemapUrlResolverFactory(xmlSitemapUrlResolverFactory)
                        .WithAssemblyProvider(assemblyProvider)
                        .WithXmlSitemapFeedPageNameProvider(xmlSitemapFeedPageNameProvider);

                    // Reset the dictionary to the latest state.
                    this.xmlSitemapFeeds[feed.Name] = new XmlSitemapFeedToXmlSitemapFeedBuilderRelation(builder.Create(), builder);
                }
            }

            return new XmlSitemapFeedStrategy(this.XmlSitemapFeeds.ToArray());
        }

        private class XmlSitemapFeedToXmlSitemapFeedBuilderRelation
        {
            public XmlSitemapFeedToXmlSitemapFeedBuilderRelation(
                IXmlSitemapFeed xmlSitemapFeed,
                IXmlSitemapFeedBuilderFacade xmlSitemapFeedBuilderFacade
                )
            {
                if (xmlSitemapFeed == null)
                    throw new ArgumentNullException("xmlSitemapFeed");

                this.XmlSitemapFeed = xmlSitemapFeed;
                this.XmlSitemapFeedBuilderFacade = xmlSitemapFeedBuilderFacade;
            }

            public IXmlSitemapFeed XmlSitemapFeed { get; private set; }

            public IXmlSitemapFeedBuilderFacade XmlSitemapFeedBuilderFacade { get; private set; }
        }
    }
}