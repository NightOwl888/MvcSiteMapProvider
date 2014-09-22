using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using MvcSiteMapProvider.Globalization;
using MvcSiteMapProvider.Reflection;
using MvcSiteMapProvider.Xml.Sitemap.Index;
using MvcSiteMapProvider.Xml.Sitemap.Paging;
using MvcSiteMapProvider.Xml.Sitemap.Specialized;
using MvcSiteMapProvider.Xml.Sitemap.Specialized.Image;
using MvcSiteMapProvider.Xml.Sitemap.Specialized.Mobile;
using MvcSiteMapProvider.Xml.Sitemap.Specialized.News;
using MvcSiteMapProvider.Xml.Sitemap.Specialized.Video;

namespace MvcSiteMapProvider.Xml.Sitemap.Configuration
{
    public class XmlSitemapFeedBuilderFacade
        : IXmlSitemapFeedBuilderFacade
    {
        public XmlSitemapFeedBuilderFacade(string feedName)
            : this(
                feedName: feedName, 
                xmlSitemapProviderFactory: new XmlSitemapProviderFactory(), 
                xmlSitemapUrlResolverFactory: new XmlSitemapUrlResolverFactoryBuilder().Create(),
                assemblyProviderFactory: new AssemblyProviderFactoryBuilder().Create(), 
                xmlSitemapFeedPageNameProvider: new XmlSitemapFeedPageNameProvider())
        {
        }

        public XmlSitemapFeedBuilderFacade(
            string feedName, 
            IXmlSitemapProviderFactory xmlSitemapProviderFactory, 
            IXmlSitemapUrlResolverFactory xmlSitemapUrlResolverFactory,
            IAssemblyProviderFactory assemblyProviderFactory, 
            IXmlSitemapFeedPageNameProvider xmlSitemapFeedPageNameProvider
            )
            : this(
                feedName: feedName, 
                maximumPageSize: 50000,
                omitRequestCaching: false,
                omitUrlsWithoutMatchingContent: false,
                // Default encoding to get rid of the BOM
                xmlWriterSettings: new XmlWriterSettings() { Encoding = new UTF8Encoding(false) }, 
                xmlSitemapProviderFactory: xmlSitemapProviderFactory,
                xmlSitemapUrlResolverFactory: xmlSitemapUrlResolverFactory,
                assemblyProviderFactory: assemblyProviderFactory, 
                xmlSitemapFeedPageNameProvider: xmlSitemapFeedPageNameProvider, 
                specializedContentDictionary: new Dictionary<Type, ISpecializedContentXmlWriterFactory>()
            )
        {
        }

        public XmlSitemapFeedBuilderFacade(
            IXmlSitemapFeedBuilderFacade builder,
            IXmlSitemapProviderFactory xmlSitemapProviderFactory,
            IXmlSitemapUrlResolverFactory xmlSitemapUrlResolverFactory,
            IAssemblyProviderFactory assemblyProviderFactory,
            IXmlSitemapFeedPageNameProvider xmlSitemapFeedPageNameProvider
            )
            : this(
                feedName: builder.FeedName,
                maximumPageSize: builder.MaximumPageSize,
                omitRequestCaching: builder.RequestCachingOmitted,
                omitUrlsWithoutMatchingContent: builder.UrlsWithNonMatchingContentOmitted,
                xmlWriterSettings: builder.XmlWriterSettings,
                xmlSitemapProviderFactory: xmlSitemapProviderFactory,
                xmlSitemapUrlResolverFactory: xmlSitemapUrlResolverFactory,
                assemblyProviderFactory: assemblyProviderFactory,
                xmlSitemapFeedPageNameProvider: xmlSitemapFeedPageNameProvider,
                specializedContentDictionary: new Dictionary<Type, ISpecializedContentXmlWriterFactory>()
            )
        {
            // If the services were overridden in the builder, use the overridden instance.
            if (!builder.XmlSitemapProviderFactory.GetType().Equals(typeof(XmlSitemapProviderFactory)))
            {
                this.xmlSitemapProviderFactory = builder.XmlSitemapProviderFactory;
            }
            if (!builder.XmlSitemapUrlResolverFactory.GetType().Equals(typeof(XmlSitemapUrlResolverFactory)))
            {
                this.xmlSitemapUrlResolverFactory = builder.XmlSitemapUrlResolverFactory;
            }
            if (!builder.AssemblyProviderFactory.GetType().Equals(typeof(AssemblyProviderFactory)))
            {
                this.assemblyProviderFactory = builder.AssemblyProviderFactory;
            }

            // Populate the specialized content dictionary
            foreach (var factory in builder.SpecializedContentXmlWriterFactories)
            {
                this.specializedContentDictionary.Add(factory.GetType(), factory);
            }
        }

        private XmlSitemapFeedBuilderFacade(
            string feedName,
            int maximumPageSize,
            bool omitRequestCaching,
            bool omitUrlsWithoutMatchingContent,
            XmlWriterSettings xmlWriterSettings,
            IXmlSitemapProviderFactory xmlSitemapProviderFactory,
            IXmlSitemapUrlResolverFactory xmlSitemapUrlResolverFactory,
            IAssemblyProviderFactory assemblyProviderFactory,
            IXmlSitemapFeedPageNameProvider xmlSitemapFeedPageNameProvider,
            IDictionary<Type, ISpecializedContentXmlWriterFactory> specializedContentDictionary
            )
        {
            if (string.IsNullOrEmpty(feedName))
                throw new ArgumentNullException("feedName");
            if (xmlWriterSettings == null)
                throw new ArgumentNullException("xmlWriterSettings");
            if (xmlSitemapProviderFactory == null)
                throw new ArgumentNullException("xmlSitemapProviderFactory");
            if (xmlSitemapUrlResolverFactory == null)
                throw new ArgumentNullException("xmlSitemapUrlResolverFactory");
            if (assemblyProviderFactory == null)
                throw new ArgumentNullException("assemblyProviderFactory");
            if (xmlSitemapFeedPageNameProvider == null)
                throw new ArgumentNullException("xmlSitemapFeedPageNameProvider");
            if (specializedContentDictionary == null)
                throw new ArgumentNullException("specializedContentDictionary");

            this.feedName = feedName;
            this.maximumPageSize = maximumPageSize;
            this.omitRequestCaching = omitRequestCaching;
            this.omitUrlsWithoutMatchingContent = omitUrlsWithoutMatchingContent;
            this.xmlWriterSettings = xmlWriterSettings;
            this.xmlSitemapProviderFactory = xmlSitemapProviderFactory;
            this.xmlSitemapUrlResolverFactory = xmlSitemapUrlResolverFactory;
            this.assemblyProviderFactory = assemblyProviderFactory;
            this.xmlSitemapFeedPageNameProvider = xmlSitemapFeedPageNameProvider;
            this.specializedContentDictionary = specializedContentDictionary;
        }

        private readonly string feedName;
        private readonly int maximumPageSize;
        private readonly bool omitRequestCaching;
        private readonly bool omitUrlsWithoutMatchingContent;
        private readonly XmlWriterSettings xmlWriterSettings;
        private readonly IXmlSitemapProviderFactory xmlSitemapProviderFactory;
        private readonly IXmlSitemapUrlResolverFactory xmlSitemapUrlResolverFactory;
        private readonly IAssemblyProviderFactory assemblyProviderFactory;
        private readonly IXmlSitemapFeedPageNameProvider xmlSitemapFeedPageNameProvider;
        private readonly IDictionary<Type, ISpecializedContentXmlWriterFactory> specializedContentDictionary;

        public IXmlSitemapFeedBuilderFacade WithEncoding(Encoding encoding)
        {
            if (encoding == null)
                throw new ArgumentNullException("encoding");

            this.xmlWriterSettings.Encoding = encoding;
            return new XmlSitemapFeedBuilderFacade(this.feedName, this.maximumPageSize, this.omitRequestCaching, this.omitUrlsWithoutMatchingContent, 
                this.xmlWriterSettings, this.xmlSitemapProviderFactory, this.xmlSitemapUrlResolverFactory, this.assemblyProviderFactory, 
                this.xmlSitemapFeedPageNameProvider, this.specializedContentDictionary);
        }

        public IXmlSitemapFeedBuilderFacade WithIndentation()
        {
            this.xmlWriterSettings.Indent = true;
            return new XmlSitemapFeedBuilderFacade(this.feedName, this.maximumPageSize, this.omitRequestCaching, this.omitUrlsWithoutMatchingContent,
                this.xmlWriterSettings, this.xmlSitemapProviderFactory, this.xmlSitemapUrlResolverFactory, this.assemblyProviderFactory,
                this.xmlSitemapFeedPageNameProvider, this.specializedContentDictionary);
        }

        public IXmlSitemapFeedBuilderFacade WithIndentationCharacters(string indentChars)
        {
            this.xmlWriterSettings.IndentChars = indentChars;
            return new XmlSitemapFeedBuilderFacade(this.feedName, this.maximumPageSize, this.omitRequestCaching, this.omitUrlsWithoutMatchingContent,
                this.xmlWriterSettings, this.xmlSitemapProviderFactory, this.xmlSitemapUrlResolverFactory, this.assemblyProviderFactory,
                this.xmlSitemapFeedPageNameProvider, this.specializedContentDictionary);
        }

        public IXmlSitemapFeedBuilderFacade WithNewLineHandling(NewLineHandling newLineHandling)
        {
            this.xmlWriterSettings.NewLineHandling = newLineHandling;
            return new XmlSitemapFeedBuilderFacade(this.feedName, this.maximumPageSize, this.omitRequestCaching, this.omitUrlsWithoutMatchingContent,
                this.xmlWriterSettings, this.xmlSitemapProviderFactory, this.xmlSitemapUrlResolverFactory, this.assemblyProviderFactory,
                this.xmlSitemapFeedPageNameProvider, this.specializedContentDictionary);
        }

        public IXmlSitemapFeedBuilderFacade WithNewLineCharacters(string newLineChars)
        {
            this.xmlWriterSettings.NewLineChars = newLineChars;
            return new XmlSitemapFeedBuilderFacade(this.feedName, this.maximumPageSize, this.omitRequestCaching, this.omitUrlsWithoutMatchingContent,
                this.xmlWriterSettings, this.xmlSitemapProviderFactory, this.xmlSitemapUrlResolverFactory, this.assemblyProviderFactory,
                this.xmlSitemapFeedPageNameProvider, this.specializedContentDictionary);
        }

        public IXmlSitemapFeedBuilderFacade OmitXmlDeclaration()
        {
            this.xmlWriterSettings.OmitXmlDeclaration = true;
            return new XmlSitemapFeedBuilderFacade(this.feedName, this.maximumPageSize, this.omitRequestCaching, this.omitUrlsWithoutMatchingContent,
                this.xmlWriterSettings, this.xmlSitemapProviderFactory, this.xmlSitemapUrlResolverFactory, this.assemblyProviderFactory,
                this.xmlSitemapFeedPageNameProvider, this.specializedContentDictionary);
        }

        public IXmlSitemapFeedBuilderFacade WithMaximumPageSize(int maximumPageSize)
        {
            return new XmlSitemapFeedBuilderFacade(this.feedName, maximumPageSize, this.omitRequestCaching, this.omitUrlsWithoutMatchingContent,
                this.xmlWriterSettings, this.xmlSitemapProviderFactory, this.xmlSitemapUrlResolverFactory, this.assemblyProviderFactory,
                this.xmlSitemapFeedPageNameProvider, this.specializedContentDictionary);
        }

        public IXmlSitemapFeedBuilderFacade OmitRequestCaching()
        {
            var omitRequestCaching = true;
            return new XmlSitemapFeedBuilderFacade(this.feedName, this.maximumPageSize, omitRequestCaching, this.omitUrlsWithoutMatchingContent,
                this.xmlWriterSettings, this.xmlSitemapProviderFactory, this.xmlSitemapUrlResolverFactory, this.assemblyProviderFactory,
                this.xmlSitemapFeedPageNameProvider, this.specializedContentDictionary);
        }

        public IXmlSitemapFeedBuilderFacade OmitUrlsWithoutMatchingContent()
        {
            var omitUrlsWithoutMatchingContent = true;
            return new XmlSitemapFeedBuilderFacade(this.feedName, this.maximumPageSize, this.omitRequestCaching, omitUrlsWithoutMatchingContent,
                this.xmlWriterSettings, this.xmlSitemapProviderFactory, this.xmlSitemapUrlResolverFactory, this.assemblyProviderFactory,
                this.xmlSitemapFeedPageNameProvider, this.specializedContentDictionary);
        }

        public IXmlSitemapFeedBuilderFacade WithNewsContent()
        {
            var newsContentXmlWriterFactory = new NewsContentXmlWriterFactory();
            if (!this.specializedContentDictionary.ContainsKey(newsContentXmlWriterFactory.ContentType))
            {
                this.specializedContentDictionary.Add(newsContentXmlWriterFactory.ContentType, newsContentXmlWriterFactory);
            }
            return new XmlSitemapFeedBuilderFacade(this.feedName, this.maximumPageSize, this.omitRequestCaching, this.omitUrlsWithoutMatchingContent,
                this.xmlWriterSettings, this.xmlSitemapProviderFactory, this.xmlSitemapUrlResolverFactory, this.assemblyProviderFactory,
                this.xmlSitemapFeedPageNameProvider, this.specializedContentDictionary);
        }

        public IXmlSitemapFeedBuilderFacade WithMobileContent()
        {
            var mobileContentXmlWriterFactory = new MobileContentXmlWriterFactory();
            if (!this.specializedContentDictionary.ContainsKey(mobileContentXmlWriterFactory.ContentType))
            {
                this.specializedContentDictionary.Add(mobileContentXmlWriterFactory.ContentType, mobileContentXmlWriterFactory);
            }
            return new XmlSitemapFeedBuilderFacade(this.feedName, this.maximumPageSize, this.omitRequestCaching, this.omitUrlsWithoutMatchingContent,
                this.xmlWriterSettings, this.xmlSitemapProviderFactory, this.xmlSitemapUrlResolverFactory, this.assemblyProviderFactory,
                this.xmlSitemapFeedPageNameProvider, this.specializedContentDictionary);
        }

        public IXmlSitemapFeedBuilderFacade WithImageContent()
        {
            var imageContentXmlWriterFactory = new ImageContentXmlWriterFactory();
            if (!this.specializedContentDictionary.ContainsKey(imageContentXmlWriterFactory.ContentType))
            {
                this.specializedContentDictionary.Add(imageContentXmlWriterFactory.ContentType, imageContentXmlWriterFactory);
            }
            return new XmlSitemapFeedBuilderFacade(this.feedName, this.maximumPageSize, this.omitRequestCaching, this.omitUrlsWithoutMatchingContent,
                this.xmlWriterSettings, this.xmlSitemapProviderFactory, this.xmlSitemapUrlResolverFactory, this.assemblyProviderFactory,
                this.xmlSitemapFeedPageNameProvider, this.specializedContentDictionary);
        }

        public IXmlSitemapFeedBuilderFacade WithVideoContent()
        {
            var videoContentXmlWriterFactory = new VideoContentXmlWriterFactory();
            if (!this.specializedContentDictionary.ContainsKey(videoContentXmlWriterFactory.ContentType))
            {
                this.specializedContentDictionary.Add(videoContentXmlWriterFactory.ContentType, videoContentXmlWriterFactory);
            }
            return new XmlSitemapFeedBuilderFacade(this.feedName, this.maximumPageSize, this.omitRequestCaching, this.omitUrlsWithoutMatchingContent,
                this.xmlWriterSettings, this.xmlSitemapProviderFactory, this.xmlSitemapUrlResolverFactory, this.assemblyProviderFactory,
                this.xmlSitemapFeedPageNameProvider, this.specializedContentDictionary);
        }

        public IXmlSitemapFeedBuilderFacade WithCustomContent(ISpecializedContentXmlWriterFactory specializedContentXmlWriterFactory)
        {
            if (specializedContentXmlWriterFactory == null)
                throw new ArgumentNullException("specializedContentXmlWriterFactory");

            if (!this.specializedContentDictionary.ContainsKey(specializedContentXmlWriterFactory.ContentType))
            {
                this.specializedContentDictionary.Add(specializedContentXmlWriterFactory.ContentType, specializedContentXmlWriterFactory);
            }
            return new XmlSitemapFeedBuilderFacade(this.feedName, this.maximumPageSize, this.omitRequestCaching, this.omitUrlsWithoutMatchingContent,
                this.xmlWriterSettings, this.xmlSitemapProviderFactory, this.xmlSitemapUrlResolverFactory, this.assemblyProviderFactory,
                this.xmlSitemapFeedPageNameProvider, this.specializedContentDictionary);
        }

        public IEnumerable<ISpecializedContentXmlWriterFactory> SpecializedContentXmlWriterFactories
        {
            get { return this.specializedContentDictionary.Values; }
        }

        public IXmlSitemapFeedBuilderFacade WithXmlSitemapProviderFactory(IXmlSitemapProviderFactory xmlSitemapProviderFactory)
        {
            return new XmlSitemapFeedBuilderFacade(this.feedName, this.maximumPageSize, this.omitRequestCaching, this.omitUrlsWithoutMatchingContent,
                this.xmlWriterSettings, xmlSitemapProviderFactory, this.xmlSitemapUrlResolverFactory, this.assemblyProviderFactory,
                this.xmlSitemapFeedPageNameProvider, this.specializedContentDictionary);
        }

        public IXmlSitemapProviderFactory XmlSitemapProviderFactory
        {
            get { return this.xmlSitemapProviderFactory; }
        }

        public IXmlSitemapFeedBuilderFacade WithXmlSitemapUrlResolverFactory(IXmlSitemapUrlResolverFactory xmlSitemapUrlResolverFactory)
        {
            return new XmlSitemapFeedBuilderFacade(this.feedName, this.maximumPageSize, this.omitRequestCaching, this.omitUrlsWithoutMatchingContent,
                this.xmlWriterSettings, this.xmlSitemapProviderFactory, xmlSitemapUrlResolverFactory, this.assemblyProviderFactory,
                this.xmlSitemapFeedPageNameProvider, this.specializedContentDictionary);
        }

        public IXmlSitemapUrlResolverFactory XmlSitemapUrlResolverFactory
        {
            get { return this.xmlSitemapUrlResolverFactory; }
        }

        public IXmlSitemapFeedBuilderFacade WithAssemblyProviderFactory(IAssemblyProviderFactory assemblyProviderFactory)
        {
            return new XmlSitemapFeedBuilderFacade(this.feedName, this.maximumPageSize, this.omitRequestCaching, this.omitUrlsWithoutMatchingContent,
                this.xmlWriterSettings, this.xmlSitemapProviderFactory, this.xmlSitemapUrlResolverFactory, assemblyProviderFactory,
                this.xmlSitemapFeedPageNameProvider, this.specializedContentDictionary);
        }

        public IAssemblyProviderFactory AssemblyProviderFactory
        {
            get { return this.assemblyProviderFactory; }
        }

        public IXmlSitemapFeedPageNameProvider XmlSitemapFeedPageNameProvider
        {
            get { return this.xmlSitemapFeedPageNameProvider; }
        }

        public string FeedName
        {
            get { return this.feedName; }
        }

        public int MaximumPageSize
        {
            get { return this.maximumPageSize; }
        }

        public bool RequestCachingOmitted
        {
            get { return this.omitRequestCaching; }
        }

        public bool UrlsWithNonMatchingContentOmitted
        {
            get { return this.omitUrlsWithoutMatchingContent; }
        }

        public XmlWriterSettings XmlWriterSettings
        {
            get { return this.xmlWriterSettings; }
        }

        public IXmlSitemapFeed Create()
        {
            var xmlWriterFactory = new XmlWriterFactory();

            // Pager
            var xmlSitemapPager = new XmlSitemapPagerBuilder().WithMaximumPageSize(this.maximumPageSize).Create();

            // XML Sitemap Providers (scan) 
            var xmlSitemapProviderTypeStrategy = new XmlSitemapProviderTypeStrategy(this.assemblyProviderFactory);

            var xmlSitemapProviderFactory = this.xmlSitemapProviderFactory;
            if (!this.omitRequestCaching)
            {
                // Wrap the factory so it will request cache the page count and last modified date
                xmlSitemapProviderFactory = new XmlSitemapProviderFactoryDecoratorBuilder().Create(xmlSitemapProviderFactory);
            }

            var xmlSitemapProviderStrategy = new XmlSitemapProviderStrategy(xmlSitemapProviderFactory, xmlSitemapProviderTypeStrategy);

            // URL Resolver
            var xmlSitemapFeedUrlResolver = new XmlSitemapFeedUrlResolver(this.xmlSitemapUrlResolverFactory, this.xmlSitemapFeedPageNameProvider);

            // Specialized Content
            var specializedContentXmlWriterFactoryStrategy = new SpecializedContentXmlWriterFactoryStrategy(this.specializedContentDictionary.Values.ToArray());
            var specializedContentWriter = new SpecializedContentWriterBuilder()
                .WithSpecializedContentXmlWriterFactoryStrategy(specializedContentXmlWriterFactoryStrategy)
                .WithXmlSitemapUrlResolverFactory(this.xmlSitemapUrlResolverFactory).Create();
            var preparedUrlEntryFactory = new PreparedUrlEntryFactoryBuilder().WithXmlSitemapUrlResolverFactory(this.xmlSitemapUrlResolverFactory).Create();
            var xmlSitemapWriterFactory = new XmlSitemapWriterFactory(this.omitUrlsWithoutMatchingContent, specializedContentWriter, preparedUrlEntryFactory);

            // Writers
            var xmlSitemapPageWriter = new XmlSitemapPageWriterBuilder().WithXmlSitemapWriterFactory(xmlSitemapWriterFactory).Create();
            var xmlSitemapIndexPageWriter = new XmlSitemapIndexPageWriterBuilder().WithXmlSitemapFeedUrlResolver(xmlSitemapFeedUrlResolver).Create();
            
            var xmlSitemapPageManager = new XmlSitemapPageManager(xmlSitemapPager, xmlSitemapProviderStrategy, xmlSitemapPageWriter, xmlSitemapIndexPageWriter);
            return new XmlSitemapFeed(this.feedName, this.xmlWriterSettings, xmlWriterFactory, xmlSitemapPageManager);
        }
    }
}