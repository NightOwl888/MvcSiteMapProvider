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
                assemblyProvider: new AttributeAssemblyProviderBuilder().Create(), 
                xmlSitemapFeedPageNameProvider: new XmlSitemapFeedPageNameProvider())
        {
        }

        public XmlSitemapFeedBuilderFacade(
            string feedName, 
            IXmlSitemapProviderFactory xmlSitemapProviderFactory, 
            IAttributeAssemblyProvider assemblyProvider, 
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
                assemblyProvider: assemblyProvider, 
                xmlSitemapFeedPageNameProvider: xmlSitemapFeedPageNameProvider, 
                specializedContentDictionary: new Dictionary<Type, ISpecializedContentXmlWriterFactory>()
            )
        {
        }

        private XmlSitemapFeedBuilderFacade(
            string feedName,
            int maximumPageSize,
            bool omitRequestCaching,
            bool omitUrlsWithoutMatchingContent,
            XmlWriterSettings xmlWriterSettings,
            IXmlSitemapProviderFactory xmlSitemapProviderFactory,
            IAttributeAssemblyProvider assemblyProvider,
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
            if (assemblyProvider == null)
                throw new ArgumentNullException("assemblyProvider");
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
            this.assemblyProvider = assemblyProvider;
            this.xmlSitemapFeedPageNameProvider = xmlSitemapFeedPageNameProvider;
            this.specializedContentDictionary = specializedContentDictionary;
        }

        private readonly string feedName;
        private readonly int maximumPageSize;
        private readonly bool omitRequestCaching;
        private readonly bool omitUrlsWithoutMatchingContent;
        private readonly XmlWriterSettings xmlWriterSettings;
        private readonly IXmlSitemapProviderFactory xmlSitemapProviderFactory;
        private readonly IAttributeAssemblyProvider assemblyProvider;
        private readonly IXmlSitemapFeedPageNameProvider xmlSitemapFeedPageNameProvider;
        private readonly IDictionary<Type, ISpecializedContentXmlWriterFactory> specializedContentDictionary;



        public IXmlSitemapFeedBuilderFacade WithEncoding(Encoding encoding)
        {
            if (encoding != null)
            {
                this.xmlWriterSettings.Encoding = encoding;
            }
            return new XmlSitemapFeedBuilderFacade(this.feedName, this.maximumPageSize, this.omitRequestCaching, this.omitUrlsWithoutMatchingContent, 
                this.xmlWriterSettings, this.xmlSitemapProviderFactory, this.assemblyProvider, this.xmlSitemapFeedPageNameProvider, this.specializedContentDictionary);
        }

        public IXmlSitemapFeedBuilderFacade WithIndentation()
        {
            this.xmlWriterSettings.Indent = true;
            return new XmlSitemapFeedBuilderFacade(this.feedName, this.maximumPageSize, this.omitRequestCaching, this.omitUrlsWithoutMatchingContent, 
                this.xmlWriterSettings, this.xmlSitemapProviderFactory, this.assemblyProvider, this.xmlSitemapFeedPageNameProvider, this.specializedContentDictionary);
        }

        //public IXmlSitemapFeedBuilderFacade OmitIndentation()
        //{
        //    this.xmlWriterSettings.Indent = false;
        //    return new XmlSitemapFeedBuilderFacade(this.feedName, this.maximumPageSize, this.xmlWriterSettings, this.xmlSitemapProviderFactory, this.assemblyProvider, this.xmlSitemapFeedPageNameProvider, this.specializedContentDictionary);
        //}

        public IXmlSitemapFeedBuilderFacade WithIndentationCharacters(string indentChars)
        {
            this.xmlWriterSettings.IndentChars = indentChars;
            return new XmlSitemapFeedBuilderFacade(this.feedName, this.maximumPageSize, this.omitRequestCaching, this.omitUrlsWithoutMatchingContent, 
                this.xmlWriterSettings, this.xmlSitemapProviderFactory, this.assemblyProvider, this.xmlSitemapFeedPageNameProvider, this.specializedContentDictionary);
        }

        public IXmlSitemapFeedBuilderFacade WithNewLineHandling(NewLineHandling newLineHandling)
        {
            this.xmlWriterSettings.NewLineHandling = newLineHandling;
            return new XmlSitemapFeedBuilderFacade(this.feedName, this.maximumPageSize, this.omitRequestCaching, this.omitUrlsWithoutMatchingContent, 
                this.xmlWriterSettings, this.xmlSitemapProviderFactory, this.assemblyProvider, this.xmlSitemapFeedPageNameProvider, this.specializedContentDictionary);
        }

        public IXmlSitemapFeedBuilderFacade WithNewLineCharacters(string newLineChars)
        {
            this.xmlWriterSettings.NewLineChars = newLineChars;
            return new XmlSitemapFeedBuilderFacade(this.feedName, this.maximumPageSize, this.omitRequestCaching, this.omitUrlsWithoutMatchingContent, 
                this.xmlWriterSettings, this.xmlSitemapProviderFactory, this.assemblyProvider, this.xmlSitemapFeedPageNameProvider, this.specializedContentDictionary);
        }

        //public IXmlSitemapFeedBuilderFacade WithXmlDeclaration()
        //{
        //    this.xmlWriterSettings.OmitXmlDeclaration = false;
        //    return new XmlSitemapFeedBuilderFacade(this.feedName, this.maximumPageSize, this.xmlWriterSettings, this.xmlSitemapProviderFactory, this.assemblyProvider, this.xmlSitemapFeedPageNameProvider, this.specializedContentDictionary);
        //}

        public IXmlSitemapFeedBuilderFacade OmitXmlDeclaration()
        {
            this.xmlWriterSettings.OmitXmlDeclaration = true;
            return new XmlSitemapFeedBuilderFacade(this.feedName, this.maximumPageSize, this.omitRequestCaching, this.omitUrlsWithoutMatchingContent, 
                this.xmlWriterSettings, this.xmlSitemapProviderFactory, this.assemblyProvider, this.xmlSitemapFeedPageNameProvider, this.specializedContentDictionary);
        }

        public IXmlSitemapFeedBuilderFacade WithMaximumPageSize(int maximumPageSize)
        {
            return new XmlSitemapFeedBuilderFacade(this.feedName, maximumPageSize, this.omitRequestCaching, this.omitUrlsWithoutMatchingContent, 
                this.xmlWriterSettings, this.xmlSitemapProviderFactory, this.assemblyProvider, this.xmlSitemapFeedPageNameProvider, this.specializedContentDictionary);
        }

        public IXmlSitemapFeedBuilderFacade OmitRequestCaching()
        {
            var omitRequestCaching = true;
            return new XmlSitemapFeedBuilderFacade(this.feedName, this.maximumPageSize, omitRequestCaching, this.omitUrlsWithoutMatchingContent,
                this.xmlWriterSettings, this.xmlSitemapProviderFactory, this.assemblyProvider, this.xmlSitemapFeedPageNameProvider, this.specializedContentDictionary);
        }

        public IXmlSitemapFeedBuilderFacade OmitUrlsWithoutMatchingContent()
        {
            var omitUrlsWithoutMatchingContent = true;
            return new XmlSitemapFeedBuilderFacade(this.feedName, this.maximumPageSize, this.omitRequestCaching, omitUrlsWithoutMatchingContent,
                this.xmlWriterSettings, this.xmlSitemapProviderFactory, this.assemblyProvider, this.xmlSitemapFeedPageNameProvider, this.specializedContentDictionary);
        }

        public IXmlSitemapFeedBuilderFacade WithNewsContent()
        {
            // TODO: Make the prepared content the default for this class
            var newsContentXmlWriterFactory = new NewsContentXmlWriterFactory(new PreparedNewsContentFactory());
            if (!this.specializedContentDictionary.ContainsKey(newsContentXmlWriterFactory.ContentType))
            {
                this.specializedContentDictionary.Add(newsContentXmlWriterFactory.ContentType, newsContentXmlWriterFactory);
            }
            return new XmlSitemapFeedBuilderFacade(this.feedName, this.maximumPageSize, this.omitRequestCaching, this.omitUrlsWithoutMatchingContent, 
                this.xmlWriterSettings, this.xmlSitemapProviderFactory, this.assemblyProvider, this.xmlSitemapFeedPageNameProvider, this.specializedContentDictionary);
        }

        public IXmlSitemapFeedBuilderFacade WithMobileContent()
        {
            var mobileContentXmlWriterFactory = new MobileContentXmlWriterFactory();
            if (!this.specializedContentDictionary.ContainsKey(mobileContentXmlWriterFactory.ContentType))
            {
                this.specializedContentDictionary.Add(mobileContentXmlWriterFactory.ContentType, mobileContentXmlWriterFactory);
            }
            return new XmlSitemapFeedBuilderFacade(this.feedName, this.maximumPageSize, this.omitRequestCaching, this.omitUrlsWithoutMatchingContent, 
                this.xmlWriterSettings, this.xmlSitemapProviderFactory, this.assemblyProvider, this.xmlSitemapFeedPageNameProvider, this.specializedContentDictionary);
        }

        public IXmlSitemapFeedBuilderFacade WithImageContent()
        {
            // TODO: Make the prepared content the default for this class
            var imageContentXmlWriterFactory = new ImageContentXmlWriterFactory(new PreparedImageContentFactory());
            if (!this.specializedContentDictionary.ContainsKey(imageContentXmlWriterFactory.ContentType))
            {
                this.specializedContentDictionary.Add(imageContentXmlWriterFactory.ContentType, imageContentXmlWriterFactory);
            }
            return new XmlSitemapFeedBuilderFacade(this.feedName, this.maximumPageSize, this.omitRequestCaching, this.omitUrlsWithoutMatchingContent, 
                this.xmlWriterSettings, this.xmlSitemapProviderFactory, this.assemblyProvider, this.xmlSitemapFeedPageNameProvider, this.specializedContentDictionary);
        }

        public IXmlSitemapFeedBuilderFacade WithVideoContent()
        {
            // TODO: Make the prepared content the default for this class
            var videoContentXmlWriterFactory = new VideoContentXmlWriterFactory(new PreparedVideoContentFactory());
            if (!this.specializedContentDictionary.ContainsKey(videoContentXmlWriterFactory.ContentType))
            {
                this.specializedContentDictionary.Add(videoContentXmlWriterFactory.ContentType, videoContentXmlWriterFactory);
            }
            return new XmlSitemapFeedBuilderFacade(this.feedName, this.maximumPageSize, this.omitRequestCaching, this.omitUrlsWithoutMatchingContent, 
                this.xmlWriterSettings, this.xmlSitemapProviderFactory, this.assemblyProvider, this.xmlSitemapFeedPageNameProvider, this.specializedContentDictionary);
        }

        public IXmlSitemapFeedBuilderFacade WithCustomContent(ISpecializedContentXmlWriterFactory specializedContentXmlWriterFactory)
        {
            if (!this.specializedContentDictionary.ContainsKey(specializedContentXmlWriterFactory.ContentType))
            {
                this.specializedContentDictionary.Add(specializedContentXmlWriterFactory.ContentType, specializedContentXmlWriterFactory);
            }
            return new XmlSitemapFeedBuilderFacade(this.feedName, this.maximumPageSize, this.omitRequestCaching, this.omitUrlsWithoutMatchingContent, 
                this.xmlWriterSettings, this.xmlSitemapProviderFactory, this.assemblyProvider, this.xmlSitemapFeedPageNameProvider, this.specializedContentDictionary);
        }

        //public IXmlSitemapFeedBuilderFacade RemoveNewsContent()
        //{
        //    this.specializedContentDictionary.Remove(typeof(INewsContent));
        //    return new XmlSitemapFeedBuilderFacade(this.feedName, this.maximumPageSize, this.xmlWriterSettings, this.xmlSitemapProviderFactory, this.assemblyProvider, this.xmlSitemapFeedPageNameProvider, this.specializedContentDictionary);
        //}

        //public IXmlSitemapFeedBuilderFacade RemoveMobileContent()
        //{
        //    this.specializedContentDictionary.Remove(typeof(IMobileContent));
        //    return new XmlSitemapFeedBuilderFacade(this.feedName, this.maximumPageSize, this.xmlWriterSettings, this.xmlSitemapProviderFactory, this.assemblyProvider, this.xmlSitemapFeedPageNameProvider, this.specializedContentDictionary);
        //}

        //public IXmlSitemapFeedBuilderFacade RemoveImageContent()
        //{
        //    this.specializedContentDictionary.Remove(typeof(IImageContent));
        //    return new XmlSitemapFeedBuilderFacade(this.feedName, this.maximumPageSize, this.xmlWriterSettings, this.xmlSitemapProviderFactory, this.assemblyProvider, this.xmlSitemapFeedPageNameProvider, this.specializedContentDictionary);
        //}

        //public IXmlSitemapFeedBuilderFacade RemoveVideoContent()
        //{
        //    this.specializedContentDictionary.Remove(typeof(IVideoContent));
        //    return new XmlSitemapFeedBuilderFacade(this.feedName, this.maximumPageSize, this.xmlWriterSettings, this.xmlSitemapProviderFactory, this.assemblyProvider, this.xmlSitemapFeedPageNameProvider, this.specializedContentDictionary);
        //}

        //public IXmlSitemapFeedBuilderFacade RemoveSpecializedContentXmlWriterFactory(Type specializedContentType)
        //{
        //    this.specializedContentDictionary.Remove(specializedContentType);
        //    return new XmlSitemapFeedBuilderFacade(this.feedName, this.maximumPageSize, this.xmlWriterSettings, this.xmlSitemapProviderFactory, this.assemblyProvider, this.xmlSitemapFeedPageNameProvider, this.specializedContentDictionary);
        //}

        //public IXmlSitemapFeedBuilderFacade ClearSpecializedContent()
        //{
        //    this.specializedContentDictionary.Clear();
        //    return new XmlSitemapFeedBuilderFacade(this.feedName, this.maximumPageSize, this.xmlWriterSettings, this.xmlSitemapProviderFactory, this.assemblyProvider, this.xmlSitemapFeedPageNameProvider, this.specializedContentDictionary);
        //}

        public IEnumerable<ISpecializedContentXmlWriterFactory> SpecializedContentXmlWriterFactories
        {
            get { return this.specializedContentDictionary.Values; }
        }

        public IXmlSitemapFeedBuilderFacade WithXmlSitemapProviderFactory(IXmlSitemapProviderFactory xmlSitemapProviderFactory)
        {
            return new XmlSitemapFeedBuilderFacade(this.feedName, this.maximumPageSize, this.omitRequestCaching, this.omitUrlsWithoutMatchingContent,
                this.xmlWriterSettings, xmlSitemapProviderFactory, this.assemblyProvider, this.xmlSitemapFeedPageNameProvider, this.specializedContentDictionary);
        }

        public IXmlSitemapProviderFactory XmlSitemapProviderFactory
        {
            get { return this.xmlSitemapProviderFactory; }
        }

        public IXmlSitemapFeed Create()
        {
            var xmlWriterFactory = new XmlWriterFactory();

            // Pager
            var xmlSitemapPager = new XmlSitemapPagerBuilder().WithMaximumPageSize(this.maximumPageSize).Create();

            // XML Sitemap Providers (scan) 
            var xmlSitemapProviderTypeStrategy = new XmlSitemapProviderTypeStrategy(this.assemblyProvider);

            var xmlSitemapProviderFactory = this.xmlSitemapProviderFactory;
            if (!this.omitRequestCaching)
            {
                xmlSitemapProviderFactory = new XmlSitemapProviderFactoryDecoratorBuilder().Create(xmlSitemapProviderFactory);
            }

            var xmlSitemapProviderStrategy = new XmlSitemapProviderStrategy(xmlSitemapProviderFactory, xmlSitemapProviderTypeStrategy);

            // URL Resolver
            var xmlSitemapUrlResolver = new XmlSitemapUrlResolverBuilder().Create();
            var xmlSitemapFeedUrlResolver = new XmlSitemapFeedUrlResolver(xmlSitemapUrlResolver, this.xmlSitemapFeedPageNameProvider);

            // Specialized Content
            var specializedContentXmlWriterFactoryStrategy = new SpecializedContentXmlWriterFactoryStrategy(this.specializedContentDictionary.Values.ToArray());
            var preparedUrlEntryFactory = new PreparedUrlEntryFactoryBuilder().WithXmlSitemapUrlResolver(xmlSitemapUrlResolver).Create();
            var xmlSitemapWriterFactory = new XmlSitemapWriterFactory(this.omitUrlsWithoutMatchingContent, specializedContentXmlWriterFactoryStrategy, preparedUrlEntryFactory);

            // Writers
            var xmlSitemapPageWriter = new XmlSitemapPageWriterBuilder().WithXmlSitemapWriterFactory(xmlSitemapWriterFactory).Create();
            var xmlSitemapIndexPageWriter = new XmlSitemapIndexPageWriterBuilder().WithXmlSitemapFeedUrlResolver(xmlSitemapFeedUrlResolver).Create();
            
            var xmlSitemapPageManager = new XmlSitemapPageManager(xmlSitemapPager, xmlSitemapProviderStrategy, xmlSitemapPageWriter, xmlSitemapIndexPageWriter);
            return new XmlSitemapFeed(this.feedName, this.xmlWriterSettings, xmlWriterFactory, xmlSitemapPageManager);
        }
    }
}
