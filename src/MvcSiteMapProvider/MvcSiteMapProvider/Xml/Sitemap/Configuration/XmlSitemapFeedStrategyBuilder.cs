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
            xmlSitemapFeeds: new Dictionary<string, IXmlSitemapFeed>(),
            xmlSitemapProviderFactory: new XmlSitemapProviderFactory()
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
            IDictionary<string, IXmlSitemapFeed> xmlSitemapFeeds,
            IXmlSitemapProviderFactory xmlSitemapProviderFactory
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
            if (xmlSitemapProviderFactory == null)
                throw new ArgumentNullException("xmlSitemapProviderFactory");
            if (xmlSitemapFeeds == null)
                throw new ArgumentNullException("xmlSitemapFeeds");

            this.defaultFeedRootPageName = defaultFeedRootPageName;
            this.defaultFeedPageName = defaultFeedPageName;
            this.namedFeedRootPageName = namedFeedRootPageName;
            this.namedFeedPageName = namedFeedPageName;
            this.includeAssembliesForScan = includeAssembliesForScan;
            this.excludeAssembliesForScan = excludeAssembliesForScan;
            this.xmlSitemapProviderFactory = xmlSitemapProviderFactory;
            this.xmlSitemapFeeds = xmlSitemapFeeds;
        }
        private readonly string defaultFeedRootPageName;
        private readonly string defaultFeedPageName;
        private readonly string namedFeedRootPageName;
        private readonly string namedFeedPageName;
        private readonly IList<string> includeAssembliesForScan;
        private readonly IList<string> excludeAssembliesForScan;
        private readonly IDictionary<string, IXmlSitemapFeed> xmlSitemapFeeds;
        private readonly IXmlSitemapProviderFactory xmlSitemapProviderFactory;
        

        #region IXmlSitemapFeedStrategyStarter members

        public IXmlSitemapFeedStrategyStarter WithDefaultFeedPageNameTemplates(string rootPageTemplate, string pageTemplate)
        {
            return new XmlSitemapFeedStrategyBuilder(rootPageTemplate, pageTemplate, this.namedFeedRootPageName, this.namedFeedPageName, this.includeAssembliesForScan, this.excludeAssembliesForScan, this.xmlSitemapFeeds, this.xmlSitemapProviderFactory);
        }

        public IXmlSitemapFeedStrategyStarter WithNamedFeedPageNameTemplates(string rootPageTemplate, string pageTemplate)
        {
            return new XmlSitemapFeedStrategyBuilder(this.defaultFeedRootPageName, this.defaultFeedPageName, rootPageTemplate, pageTemplate, this.includeAssembliesForScan, this.excludeAssembliesForScan, this.xmlSitemapFeeds, this.xmlSitemapProviderFactory);
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


        public IXmlSitemapFeedStrategyStarter AddAssemblyForXmlSitemapProviderScan(string assemblyName)
        {
            if (!string.IsNullOrEmpty(assemblyName))
            {
                this.includeAssembliesForScan.Add(assemblyName);
            }
            return new XmlSitemapFeedStrategyBuilder(this.defaultFeedRootPageName, this.defaultFeedPageName, this.namedFeedRootPageName, this.namedFeedPageName, this.includeAssembliesForScan, this.excludeAssembliesForScan, this.xmlSitemapFeeds, this.xmlSitemapProviderFactory);
        }

        public IXmlSitemapFeedStrategyStarter RemoveAssemblyFromXmlSitemapProviderScan(string assemblyName)
        {
            if (!string.IsNullOrEmpty(assemblyName))
            {
                if (this.includeAssembliesForScan.Contains(assemblyName))
                {
                    this.includeAssembliesForScan.Remove(assemblyName);
                }
                this.excludeAssembliesForScan.Add(assemblyName);
            }
            return new XmlSitemapFeedStrategyBuilder(this.defaultFeedRootPageName, this.defaultFeedPageName, this.namedFeedRootPageName, this.namedFeedPageName, this.includeAssembliesForScan, this.excludeAssembliesForScan, this.xmlSitemapFeeds, this.xmlSitemapProviderFactory);
        }

        public IEnumerable<string> AssembliesToScanForXmlSitemapProvider
        {
            get { return this.includeAssembliesForScan; }
        }

        public IXmlSitemapFeedStrategyBuilder WithXmlSitemapProviderFactory(IXmlSitemapProviderFactory xmlSitemapProviderFactory)
        {
            return new XmlSitemapFeedStrategyBuilder(this.defaultFeedRootPageName, this.defaultFeedPageName, this.namedFeedRootPageName, this.namedFeedPageName, this.includeAssembliesForScan, this.excludeAssembliesForScan, this.xmlSitemapFeeds, xmlSitemapProviderFactory);
        }

        #endregion

        public IXmlSitemapFeedStrategyBuilder AddFeed(string feedName, Func<IXmlSitemapFeedBuilderFacade, IXmlSitemapFeedBuilderFacade> expression)
        {
            var assemblyProvider = new AttributeAssemblyProvider(this.includeAssembliesForScan.ToArray(), this.excludeAssembliesForScan.ToArray());
            var xmlSitemapFeedPageNameProvider = new XmlSitemapFeedPageNameProvider(this.defaultFeedRootPageName, this.defaultFeedPageName, this.namedFeedRootPageName, this.namedFeedPageName);

            var starter = new XmlSitemapFeedBuilderFacade(feedName, this.xmlSitemapProviderFactory, assemblyProvider, xmlSitemapFeedPageNameProvider);
            var builder = expression(starter);
            var feed = builder.Create();
            this.xmlSitemapFeeds.Add(feed.Name, feed);

            return new XmlSitemapFeedStrategyBuilder(this.defaultFeedRootPageName, this.defaultFeedPageName, this.namedFeedRootPageName, this.namedFeedPageName, this.includeAssembliesForScan, this.excludeAssembliesForScan, this.xmlSitemapFeeds, this.xmlSitemapProviderFactory);
        }

        public IXmlSitemapFeedStrategyBuilder AddFeed(string feedName)
        {
            return this.AddFeed(feedName, x => x);
        }

        public IXmlSitemapFeedStrategyBuilder AddFeed(IXmlSitemapFeed xmlSitemapFeed)
        {
            if (xmlSitemapFeed != null)
            {
                this.xmlSitemapFeeds.Add(xmlSitemapFeed.Name, xmlSitemapFeed);
            }
            return new XmlSitemapFeedStrategyBuilder(this.defaultFeedRootPageName, this.defaultFeedPageName, this.namedFeedRootPageName, this.namedFeedPageName, this.includeAssembliesForScan, this.excludeAssembliesForScan, this.xmlSitemapFeeds, this.xmlSitemapProviderFactory);
        }

        public IXmlSitemapFeedStrategyBuilder AddFeeds(IEnumerable<IXmlSitemapFeed> xmlSitemapFeeds)
        {
            foreach (var feed in xmlSitemapFeeds)
            {
                if (feed != null)
                {
                    this.xmlSitemapFeeds.Add(feed.Name, feed);
                }
            }
            return new XmlSitemapFeedStrategyBuilder(this.defaultFeedRootPageName, this.defaultFeedPageName, this.namedFeedRootPageName, this.namedFeedPageName, this.includeAssembliesForScan, this.excludeAssembliesForScan, this.xmlSitemapFeeds, this.xmlSitemapProviderFactory);
        }

        public IXmlSitemapFeedStrategyBuilder RemoveFeed(string feedName)
        {
            this.xmlSitemapFeeds.Remove(feedName);
            return new XmlSitemapFeedStrategyBuilder(this.defaultFeedRootPageName, this.defaultFeedPageName, this.namedFeedRootPageName, this.namedFeedPageName, this.includeAssembliesForScan, this.excludeAssembliesForScan, this.xmlSitemapFeeds, this.xmlSitemapProviderFactory);
        }

        public IXmlSitemapFeedStrategyBuilder ClearFeeds()
        {
            this.xmlSitemapFeeds.Clear();
            return new XmlSitemapFeedStrategyBuilder(this.defaultFeedRootPageName, this.defaultFeedPageName, this.namedFeedRootPageName, this.namedFeedPageName, this.includeAssembliesForScan, this.excludeAssembliesForScan, this.xmlSitemapFeeds, this.xmlSitemapProviderFactory);
        }

        public IEnumerable<IXmlSitemapFeed> XmlSitemapFeeds
        {
            get { return this.xmlSitemapFeeds.Values.ToArray(); }
        }

        public IXmlSitemapProviderFactory XmlSitemapProviderFactory
        {
            get { return this.xmlSitemapProviderFactory; }
        }

        public IXmlSitemapFeedStrategy Create()
        {
            return new XmlSitemapFeedStrategy(this.xmlSitemapFeeds.Values.ToArray());
        }  
    }
}