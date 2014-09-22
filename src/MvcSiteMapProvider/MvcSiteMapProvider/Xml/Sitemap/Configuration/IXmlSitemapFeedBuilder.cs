using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using MvcSiteMapProvider.ComponentModel;
using MvcSiteMapProvider.Reflection;
using MvcSiteMapProvider.Xml.Sitemap.Specialized;

namespace MvcSiteMapProvider.Xml.Sitemap.Configuration
{

    public class XmlSitemapFeedBuilder : XmlSitemap_SetupFeed_WithSetupUrlResolver,
        IXmlSitemap_SetupFeed_Starter
    {
        public XmlSitemapFeedBuilder(string feedName)
            : this(
                feedName: feedName, 
                xmlSitemapProviderFactory: new XmlSitemapProviderFactory(), 
                xmlSitemapUrlResolverFactory: new XmlSitemapUrlResolverFactoryBuilder().Create(),
                assemblyProviderFactory: new AssemblyProviderFactoryBuilder().Create(), 
                xmlSitemapFeedPageNameProvider: new XmlSitemapFeedPageNameProvider())
        {
        }

        public XmlSitemapFeedBuilder(
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

        // Overload for rebuilding feed with different dependencies via XmlSitemapFeedStrategyBuilder
        public XmlSitemapFeedBuilder(
            IXmlSitemap_SetupFeed_Finalizer builder,
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

        private XmlSitemapFeedBuilder(
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



        public override IXmlSitemap_SetupFeed_WithSetupXmlWriter WithXmlWriterSettings(Func<IXmlSitemap_SetupXmlWriter_Starter, IXmlSitemap_SetupXmlWriter_Finalizer> expression)
        {
            var starter = new XmlSitemap_SetupXmlWriter_Builder(this.xmlWriterSettings);
            var builder = expression(starter);
            var xmlWriterSettings = builder.Create();

            return new XmlSitemapFeedBuilder(this.feedName, this.maximumPageSize, this.omitRequestCaching, 
                this.omitUrlsWithoutMatchingContent, xmlWriterSettings, this.xmlSitemapProviderFactory, 
                this.xmlSitemapUrlResolverFactory, this.assemblyProviderFactory, this.xmlSitemapFeedPageNameProvider, 
                this.specializedContentDictionary);
        }

        public override IXmlSitemap_SetupFeed_WithSetupXmlWriter WithXmlWriterSettings(XmlWriterSettings xmlWriterSettings)
        {
            return new XmlSitemapFeedBuilder(this.feedName, this.maximumPageSize, this.omitRequestCaching,
                this.omitUrlsWithoutMatchingContent, xmlWriterSettings, this.xmlSitemapProviderFactory,
                this.xmlSitemapUrlResolverFactory, this.assemblyProviderFactory, this.xmlSitemapFeedPageNameProvider,
                this.specializedContentDictionary);
        }

        public override IXmlSitemap_SetupFeed_WithMaximumPageSize WithMaximumPageSize(int maximumPageSize)
        {
            return new XmlSitemapFeedBuilder(this.feedName, maximumPageSize, this.omitRequestCaching,
                this.omitUrlsWithoutMatchingContent, this.xmlWriterSettings, this.xmlSitemapProviderFactory,
                this.xmlSitemapUrlResolverFactory, this.assemblyProviderFactory, this.xmlSitemapFeedPageNameProvider,
                this.specializedContentDictionary);
        }

        public override IXmlSitemap_SetupFeed_WithOmitRequestCaching OmitRequestCaching()
        {
            var omitRequestCaching = true;
            return new XmlSitemapFeedBuilder(this.feedName, this.maximumPageSize, omitRequestCaching,
                this.omitUrlsWithoutMatchingContent, this.xmlWriterSettings, this.xmlSitemapProviderFactory,
                this.xmlSitemapUrlResolverFactory, this.assemblyProviderFactory, this.xmlSitemapFeedPageNameProvider,
                this.specializedContentDictionary);
        }

        public override IXmlSitemap_SetupFeed_WithSetupContent WithContent(Func<IXmlSitemap_SetupContent_Starter, IXmlSitemap_SetupContent_Finalizer> expression)
        {
            var starter = new XmlSitemap_SetupContent_Builder(this.omitUrlsWithoutMatchingContent, this.specializedContentDictionary);
            var builder = expression(starter);
            var specializedContentDictionary = builder.Create();
            var omitUrlsWitoutMatchingContent = builder.UrlsWithNonMatchingContentOmitted;

            return new XmlSitemapFeedBuilder(this.feedName, this.maximumPageSize, this.omitRequestCaching,
                omitUrlsWithoutMatchingContent, this.xmlWriterSettings, this.xmlSitemapProviderFactory,
                this.xmlSitemapUrlResolverFactory, this.assemblyProviderFactory, this.xmlSitemapFeedPageNameProvider,
                specializedContentDictionary);
        }

        public override IXmlSitemap_SetupFeed_WithSetupUrlResolver SetupUrlResolver(Func<IXmlSitemap_SetupUrlResolver_Starter, IXmlSitemap_SetupUrlResolver_Finalizer> expression)
        {
            var starter = new XmlSitemap_SetupUrlResolver_Builder();
            var builder = expression(starter);
            var xmlSitemapUrlResolverFactory = builder.Create();

            return new XmlSitemapFeedBuilder(this.feedName, this.maximumPageSize, this.omitRequestCaching,
                this.omitUrlsWithoutMatchingContent, this.xmlWriterSettings, this.xmlSitemapProviderFactory,
                xmlSitemapUrlResolverFactory, this.assemblyProviderFactory, this.xmlSitemapFeedPageNameProvider,
                this.specializedContentDictionary);
        }

        public override IXmlSitemap_SetupFeed_WithSetupUrlResolver SetupUrlResolver(IXmlSitemapUrlResolverFactory xmlSitemapUrlResolverFactory)
        {
            return new XmlSitemapFeedBuilder(this.feedName, this.maximumPageSize, this.omitRequestCaching,
                this.omitUrlsWithoutMatchingContent, this.xmlWriterSettings, this.xmlSitemapProviderFactory,
                xmlSitemapUrlResolverFactory, this.assemblyProviderFactory, this.xmlSitemapFeedPageNameProvider,
                this.specializedContentDictionary);
        }

        public override IXmlSitemapFeed Create()
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

        public override IEnumerable<ISpecializedContentXmlWriterFactory> SpecializedContentXmlWriterFactories
        {
            get { return this.specializedContentDictionary.Values.ToArray(); }
        }

        public override IXmlSitemapUrlResolverFactory XmlSitemapUrlResolverFactory
        {
            get { return this.xmlSitemapUrlResolverFactory; }
        }

        public override IXmlSitemapProviderFactory XmlSitemapProviderFactory
        {
            get { return this.xmlSitemapProviderFactory; }
        }

        public override IAssemblyProviderFactory AssemblyProviderFactory
        {
            get { return this.assemblyProviderFactory; }
        }

        public override IXmlSitemapFeedPageNameProvider XmlSitemapFeedPageNameProvider
        {
            get { return this.xmlSitemapFeedPageNameProvider; }
        }

        public override string FeedName
        {
            get { return this.feedName; }
        }

        public override int MaximumPageSize
        {
            get { return this.maximumPageSize; }
        }

        public override bool RequestCachingOmitted
        {
            get { return this.omitRequestCaching; }
        }

        public override bool UrlsWithNonMatchingContentOmitted
        {
            get { return this.omitUrlsWithoutMatchingContent; }
        }

        public override XmlWriterSettings XmlWriterSettings
        {
            get { return this.xmlWriterSettings; }
        }
    }


    public abstract class XmlSitemap_SetupFeed_WithSetupUrlResolver : XmlSitemap_SetupFeed_WithSetupContent,
        IXmlSitemap_SetupFeed_WithSetupUrlResolver,
        IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithSetupUrlResolver,
        IXmlSitemap_SetupFeed_WithOmitRequestCaching_WithSetupUrlResolver,
        IXmlSitemap_SetupFeed_WithSetupContent_WithSetupUrlResolver,
        IXmlSitemap_SetupFeed_WithMaximumPageSize_WithSetupUrlResolver,
        IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithSetupUrlResolver,
        IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithOmitRequestCaching_WithSetupUrlResolver,
        IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithSetupContent_WithSetupUrlResolver,
        IXmlSitemap_SetupFeed_WithMaximumPageSize_WithOmitRequestCaching_WithSetupUrlResolver,
        IXmlSitemap_SetupFeed_WithOmitRequestCaching_WithSetupContent_WithSetupUrlResolver,
        IXmlSitemap_SetupFeed_WithMaximumPageSize_WithSetupContent_WithSetupUrlResolver,
        IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithSetupContent_WithSetupUrlResolver,
        IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithOmitRequestCaching_WithSetupUrlResolver,
        IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithOmitRequestCaching_WithSetupContent_WithSetupUrlResolver,
        IXmlSitemap_SetupFeed_WithMaximumPageSize_WithOmitRequestCaching_WithSetupContent_WithSetupUrlResolver
    {
        IXmlSitemap_SetupFeed_WithSetupContent_WithSetupUrlResolver IXmlSitemap_SetupFeed_SetupContent<IXmlSitemap_SetupFeed_WithSetupContent_WithSetupUrlResolver>.WithContent(Func<IXmlSitemap_SetupContent_Starter, IXmlSitemap_SetupContent_Finalizer> expression)
        {
            return (IXmlSitemap_SetupFeed_WithSetupContent_WithSetupUrlResolver)this.WithContent(expression);
        }

        IXmlSitemap_SetupFeed_WithOmitRequestCaching_WithSetupUrlResolver IXmlSitemap_SetupFeed_OmitRequestCaching<IXmlSitemap_SetupFeed_WithOmitRequestCaching_WithSetupUrlResolver>.OmitRequestCaching()
        {
            return (IXmlSitemap_SetupFeed_WithOmitRequestCaching_WithSetupUrlResolver)this.OmitRequestCaching();
        }

        IXmlSitemap_SetupFeed_WithMaximumPageSize_WithSetupUrlResolver IXmlSitemap_SetupFeed_MaximumPageSize<IXmlSitemap_SetupFeed_WithMaximumPageSize_WithSetupUrlResolver>.WithMaximumPageSize(int maximumPageSize)
        {
            return (IXmlSitemap_SetupFeed_WithMaximumPageSize_WithSetupUrlResolver)this.WithMaximumPageSize(maximumPageSize);
        }

        IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithSetupUrlResolver IXmlSitemap_SetupFeed_SetupXmlWriter<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithSetupUrlResolver>.WithXmlWriterSettings(Func<IXmlSitemap_SetupXmlWriter_Starter, IXmlSitemap_SetupXmlWriter_Finalizer> expression)
        {
            return (IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithSetupUrlResolver)this.WithXmlWriterSettings(expression);
        }

        IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithSetupUrlResolver IXmlSitemap_SetupFeed_SetupXmlWriter<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithSetupUrlResolver>.WithXmlWriterSettings(XmlWriterSettings xmlWriterSettings)
        {
            return (IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithSetupUrlResolver)this.WithXmlWriterSettings(xmlWriterSettings);
        }



        IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithSetupContent_WithSetupUrlResolver IXmlSitemap_SetupFeed_SetupContent<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithSetupContent_WithSetupUrlResolver>.WithContent(Func<IXmlSitemap_SetupContent_Starter, IXmlSitemap_SetupContent_Finalizer> expression)
        {
            return (IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithSetupContent_WithSetupUrlResolver)this.WithContent(expression);
        }

        IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithOmitRequestCaching_WithSetupUrlResolver IXmlSitemap_SetupFeed_OmitRequestCaching<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithOmitRequestCaching_WithSetupUrlResolver>.OmitRequestCaching()
        {
            return (IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithOmitRequestCaching_WithSetupUrlResolver)this.OmitRequestCaching();
        }

        IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithSetupUrlResolver IXmlSitemap_SetupFeed_MaximumPageSize<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithSetupUrlResolver>.WithMaximumPageSize(int maximumPageSize)
        {
            return (IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithSetupUrlResolver)this.WithMaximumPageSize(maximumPageSize);
        }



        IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithOmitRequestCaching_WithSetupUrlResolver IXmlSitemap_SetupFeed_SetupXmlWriter<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithOmitRequestCaching_WithSetupUrlResolver>.WithXmlWriterSettings(Func<IXmlSitemap_SetupXmlWriter_Starter, IXmlSitemap_SetupXmlWriter_Finalizer> expression)
        {
            return (IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithOmitRequestCaching_WithSetupUrlResolver)this.WithXmlWriterSettings(expression);
        }

        IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithOmitRequestCaching_WithSetupUrlResolver IXmlSitemap_SetupFeed_SetupXmlWriter<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithOmitRequestCaching_WithSetupUrlResolver>.WithXmlWriterSettings(XmlWriterSettings xmlWriterSettings)
        {
            return (IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithOmitRequestCaching_WithSetupUrlResolver)this.WithXmlWriterSettings(xmlWriterSettings);
        }

        IXmlSitemap_SetupFeed_WithMaximumPageSize_WithOmitRequestCaching_WithSetupUrlResolver IXmlSitemap_SetupFeed_MaximumPageSize<IXmlSitemap_SetupFeed_WithMaximumPageSize_WithOmitRequestCaching_WithSetupUrlResolver>.WithMaximumPageSize(int maximumPageSize)
        {
            return (IXmlSitemap_SetupFeed_WithMaximumPageSize_WithOmitRequestCaching_WithSetupUrlResolver)this.WithMaximumPageSize(maximumPageSize);
        }

        IXmlSitemap_SetupFeed_WithOmitRequestCaching_WithSetupContent_WithSetupUrlResolver IXmlSitemap_SetupFeed_SetupContent<IXmlSitemap_SetupFeed_WithOmitRequestCaching_WithSetupContent_WithSetupUrlResolver>.WithContent(Func<IXmlSitemap_SetupContent_Starter, IXmlSitemap_SetupContent_Finalizer> expression)
        {
            return (IXmlSitemap_SetupFeed_WithOmitRequestCaching_WithSetupContent_WithSetupUrlResolver)this.WithContent(expression);
        }



        IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithSetupContent_WithSetupUrlResolver IXmlSitemap_SetupFeed_SetupXmlWriter<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithSetupContent_WithSetupUrlResolver>.WithXmlWriterSettings(Func<IXmlSitemap_SetupXmlWriter_Starter, IXmlSitemap_SetupXmlWriter_Finalizer> expression)
        {
            return (IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithSetupContent_WithSetupUrlResolver)this.WithXmlWriterSettings(expression);
        }

        IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithSetupContent_WithSetupUrlResolver IXmlSitemap_SetupFeed_SetupXmlWriter<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithSetupContent_WithSetupUrlResolver>.WithXmlWriterSettings(XmlWriterSettings xmlWriterSettings)
        {
            return (IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithSetupContent_WithSetupUrlResolver)this.WithXmlWriterSettings(xmlWriterSettings);
        }

        IXmlSitemap_SetupFeed_WithMaximumPageSize_WithSetupContent_WithSetupUrlResolver IXmlSitemap_SetupFeed_MaximumPageSize<IXmlSitemap_SetupFeed_WithMaximumPageSize_WithSetupContent_WithSetupUrlResolver>.WithMaximumPageSize(int maximumPageSize)
        {
            return (IXmlSitemap_SetupFeed_WithMaximumPageSize_WithSetupContent_WithSetupUrlResolver)this.WithMaximumPageSize(maximumPageSize);
        }

        IXmlSitemap_SetupFeed_WithOmitRequestCaching_WithSetupContent_WithSetupUrlResolver IXmlSitemap_SetupFeed_OmitRequestCaching<IXmlSitemap_SetupFeed_WithOmitRequestCaching_WithSetupContent_WithSetupUrlResolver>.OmitRequestCaching()
        {
            return (IXmlSitemap_SetupFeed_WithOmitRequestCaching_WithSetupContent_WithSetupUrlResolver)this.OmitRequestCaching();
        }



        IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithSetupUrlResolver IXmlSitemap_SetupFeed_SetupXmlWriter<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithSetupUrlResolver>.WithXmlWriterSettings(Func<IXmlSitemap_SetupXmlWriter_Starter, IXmlSitemap_SetupXmlWriter_Finalizer> expression)
        {
            return (IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithSetupUrlResolver)this.WithXmlWriterSettings(expression);
        }

        IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithSetupUrlResolver IXmlSitemap_SetupFeed_SetupXmlWriter<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithSetupUrlResolver>.WithXmlWriterSettings(XmlWriterSettings xmlWriterSettings)
        {
            return (IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithSetupUrlResolver)this.WithXmlWriterSettings(xmlWriterSettings);
        }

        IXmlSitemap_SetupFeed_WithMaximumPageSize_WithOmitRequestCaching_WithSetupUrlResolver IXmlSitemap_SetupFeed_OmitRequestCaching<IXmlSitemap_SetupFeed_WithMaximumPageSize_WithOmitRequestCaching_WithSetupUrlResolver>.OmitRequestCaching()
        {
            return (IXmlSitemap_SetupFeed_WithMaximumPageSize_WithOmitRequestCaching_WithSetupUrlResolver)this.OmitRequestCaching();
        }

        IXmlSitemap_SetupFeed_WithMaximumPageSize_WithSetupContent_WithSetupUrlResolver IXmlSitemap_SetupFeed_SetupContent<IXmlSitemap_SetupFeed_WithMaximumPageSize_WithSetupContent_WithSetupUrlResolver>.WithContent(Func<IXmlSitemap_SetupContent_Starter, IXmlSitemap_SetupContent_Finalizer> expression)
        {
            return (IXmlSitemap_SetupFeed_WithMaximumPageSize_WithSetupContent_WithSetupUrlResolver)this.WithContent(expression);
        }



        IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithSetupContent_WithSetupUrlResolver IXmlSitemap_SetupFeed_SetupContent<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithSetupContent_WithSetupUrlResolver>.WithContent(Func<IXmlSitemap_SetupContent_Starter, IXmlSitemap_SetupContent_Finalizer> expression)
        {
            return (IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithSetupContent_WithSetupUrlResolver)this.WithContent(expression);
        }

        IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithOmitRequestCaching_WithSetupUrlResolver IXmlSitemap_SetupFeed_OmitRequestCaching<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithOmitRequestCaching_WithSetupUrlResolver>.OmitRequestCaching()
        {
            return (IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithOmitRequestCaching_WithSetupUrlResolver)this.OmitRequestCaching();
        }



        IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithOmitRequestCaching_WithSetupContent_WithSetupUrlResolver IXmlSitemap_SetupFeed_SetupContent<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithOmitRequestCaching_WithSetupContent_WithSetupUrlResolver>.WithContent(Func<IXmlSitemap_SetupContent_Starter, IXmlSitemap_SetupContent_Finalizer> expression)
        {
            return (IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithOmitRequestCaching_WithSetupContent_WithSetupUrlResolver)this.WithContent(expression);
        }

        IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithOmitRequestCaching_WithSetupUrlResolver IXmlSitemap_SetupFeed_MaximumPageSize<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithOmitRequestCaching_WithSetupUrlResolver>.WithMaximumPageSize(int maximumPageSize)
        {
            return (IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithOmitRequestCaching_WithSetupUrlResolver)this.WithMaximumPageSize(maximumPageSize);
        }




        IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithOmitRequestCaching_WithSetupContent_WithSetupUrlResolver IXmlSitemap_SetupFeed_OmitRequestCaching<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithOmitRequestCaching_WithSetupContent_WithSetupUrlResolver>.OmitRequestCaching()
        {
            return (IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithOmitRequestCaching_WithSetupContent_WithSetupUrlResolver)this.OmitRequestCaching();
        }

        IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithSetupContent_WithSetupUrlResolver IXmlSitemap_SetupFeed_MaximumPageSize<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithSetupContent_WithSetupUrlResolver>.WithMaximumPageSize(int maximumPageSize)
        {
            return (IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithSetupContent_WithSetupUrlResolver)this.WithMaximumPageSize(maximumPageSize);
        }




        IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithOmitRequestCaching_WithSetupUrlResolver IXmlSitemap_SetupFeed_SetupXmlWriter<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithOmitRequestCaching_WithSetupUrlResolver>.WithXmlWriterSettings(Func<IXmlSitemap_SetupXmlWriter_Starter, IXmlSitemap_SetupXmlWriter_Finalizer> expression)
        {
            return (IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithOmitRequestCaching_WithSetupUrlResolver)this.WithXmlWriterSettings(expression);
        }

        IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithOmitRequestCaching_WithSetupUrlResolver IXmlSitemap_SetupFeed_SetupXmlWriter<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithOmitRequestCaching_WithSetupUrlResolver>.WithXmlWriterSettings(XmlWriterSettings xmlWriterSettings)
        {
            return (IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithOmitRequestCaching_WithSetupUrlResolver)this.WithXmlWriterSettings(xmlWriterSettings);
        }

        IXmlSitemap_SetupFeed_WithMaximumPageSize_WithOmitRequestCaching_WithSetupContent_WithSetupUrlResolver IXmlSitemap_SetupFeed_SetupContent<IXmlSitemap_SetupFeed_WithMaximumPageSize_WithOmitRequestCaching_WithSetupContent_WithSetupUrlResolver>.WithContent(Func<IXmlSitemap_SetupContent_Starter, IXmlSitemap_SetupContent_Finalizer> expression)
        {
            return (IXmlSitemap_SetupFeed_WithMaximumPageSize_WithOmitRequestCaching_WithSetupContent_WithSetupUrlResolver)this.WithContent(expression);
        }




        IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithOmitRequestCaching_WithSetupContent_WithSetupUrlResolver IXmlSitemap_SetupFeed_SetupXmlWriter<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithOmitRequestCaching_WithSetupContent_WithSetupUrlResolver>.WithXmlWriterSettings(Func<IXmlSitemap_SetupXmlWriter_Starter, IXmlSitemap_SetupXmlWriter_Finalizer> expression)
        {
            return (IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithOmitRequestCaching_WithSetupContent_WithSetupUrlResolver)this.WithXmlWriterSettings(expression);
        }

        IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithOmitRequestCaching_WithSetupContent_WithSetupUrlResolver IXmlSitemap_SetupFeed_SetupXmlWriter<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithOmitRequestCaching_WithSetupContent_WithSetupUrlResolver>.WithXmlWriterSettings(XmlWriterSettings xmlWriterSettings)
        {
            return (IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithOmitRequestCaching_WithSetupContent_WithSetupUrlResolver)this.WithXmlWriterSettings(xmlWriterSettings);
        }

        IXmlSitemap_SetupFeed_WithMaximumPageSize_WithOmitRequestCaching_WithSetupContent_WithSetupUrlResolver IXmlSitemap_SetupFeed_MaximumPageSize<IXmlSitemap_SetupFeed_WithMaximumPageSize_WithOmitRequestCaching_WithSetupContent_WithSetupUrlResolver>.WithMaximumPageSize(int maximumPageSize)
        {
            return (IXmlSitemap_SetupFeed_WithMaximumPageSize_WithOmitRequestCaching_WithSetupContent_WithSetupUrlResolver)this.WithMaximumPageSize(maximumPageSize);
        }





        IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithSetupContent_WithSetupUrlResolver IXmlSitemap_SetupFeed_SetupXmlWriter<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithSetupContent_WithSetupUrlResolver>.WithXmlWriterSettings(Func<IXmlSitemap_SetupXmlWriter_Starter, IXmlSitemap_SetupXmlWriter_Finalizer> expression)
        {
            return (IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithSetupContent_WithSetupUrlResolver)this.WithXmlWriterSettings(expression);
        }

        IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithSetupContent_WithSetupUrlResolver IXmlSitemap_SetupFeed_SetupXmlWriter<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithSetupContent_WithSetupUrlResolver>.WithXmlWriterSettings(XmlWriterSettings xmlWriterSettings)
        {
            return (IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithSetupContent_WithSetupUrlResolver)this.WithXmlWriterSettings(xmlWriterSettings);
        }

        IXmlSitemap_SetupFeed_WithMaximumPageSize_WithOmitRequestCaching_WithSetupContent_WithSetupUrlResolver IXmlSitemap_SetupFeed_OmitRequestCaching<IXmlSitemap_SetupFeed_WithMaximumPageSize_WithOmitRequestCaching_WithSetupContent_WithSetupUrlResolver>.OmitRequestCaching()
        {
            return (IXmlSitemap_SetupFeed_WithMaximumPageSize_WithOmitRequestCaching_WithSetupContent_WithSetupUrlResolver)this.OmitRequestCaching();
        }



        IXmlSitemap_SetupFeed_Finalizer IXmlSitemap_SetupFeed_OmitRequestCaching<IXmlSitemap_SetupFeed_Finalizer>.OmitRequestCaching()
        {
            return this.OmitRequestCaching();
        }


        IXmlSitemap_SetupFeed_Finalizer IXmlSitemap_SetupFeed_SetupContent<IXmlSitemap_SetupFeed_Finalizer>.WithContent(Func<IXmlSitemap_SetupContent_Starter, IXmlSitemap_SetupContent_Finalizer> expression)
        {
            return this.WithContent(expression);
        }




        IXmlSitemap_SetupFeed_Finalizer IXmlSitemap_SetupFeed_MaximumPageSize<IXmlSitemap_SetupFeed_Finalizer>.WithMaximumPageSize(int maximumPageSize)
        {
            return this.WithMaximumPageSize(maximumPageSize);
        }

        IXmlSitemap_SetupFeed_Finalizer IXmlSitemap_SetupUrlResolver<IXmlSitemap_SetupFeed_Finalizer>.SetupUrlResolver(Func<IXmlSitemap_SetupUrlResolver_Starter, IXmlSitemap_SetupUrlResolver_Finalizer> expression)
        {
            return this.SetupUrlResolver(expression);
        }

        IXmlSitemap_SetupFeed_Finalizer IXmlSitemap_SetupUrlResolver<IXmlSitemap_SetupFeed_Finalizer>.SetupUrlResolver(IXmlSitemapUrlResolverFactory xmlSitemapUrlResolverFactory)
        {
            return this.SetupUrlResolver(xmlSitemapUrlResolverFactory);
        }
    }

    public abstract class XmlSitemap_SetupFeed_WithSetupContent : XmlSitemap_SetupFeed_WithOmitRequestCaching,
        IXmlSitemap_SetupFeed_WithSetupContent,
        IXmlSitemap_SetupFeed_WithMaximumPageSize_WithSetupContent,
        IXmlSitemap_SetupFeed_WithOmitRequestCaching_WithSetupContent,
        IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithSetupContent,
        IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithOmitRequestCaching_WithSetupContent,
        IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithSetupContent,
        IXmlSitemap_SetupFeed_WithMaximumPageSize_WithOmitRequestCaching_WithSetupContent,
        IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithOmitRequestCaching_WithSetupContent
    {

        IXmlSitemap_SetupFeed_WithSetupContent_WithSetupUrlResolver IXmlSitemap_SetupUrlResolver<IXmlSitemap_SetupFeed_WithSetupContent_WithSetupUrlResolver>.SetupUrlResolver(Func<IXmlSitemap_SetupUrlResolver_Starter, IXmlSitemap_SetupUrlResolver_Finalizer> expression)
        {
            return (IXmlSitemap_SetupFeed_WithSetupContent_WithSetupUrlResolver)this.SetupUrlResolver(expression);
        }

        IXmlSitemap_SetupFeed_WithSetupContent_WithSetupUrlResolver IXmlSitemap_SetupUrlResolver<IXmlSitemap_SetupFeed_WithSetupContent_WithSetupUrlResolver>.SetupUrlResolver(IXmlSitemapUrlResolverFactory xmlSitemapUrlResolverFactory)
        {
            return (IXmlSitemap_SetupFeed_WithSetupContent_WithSetupUrlResolver)this.SetupUrlResolver(xmlSitemapUrlResolverFactory);
        }

        IXmlSitemap_SetupFeed_WithOmitRequestCaching_WithSetupContent IXmlSitemap_SetupFeed_OmitRequestCaching<IXmlSitemap_SetupFeed_WithOmitRequestCaching_WithSetupContent>.OmitRequestCaching()
        {
            return (IXmlSitemap_SetupFeed_WithOmitRequestCaching_WithSetupContent)this.OmitRequestCaching();
        }

        IXmlSitemap_SetupFeed_WithMaximumPageSize_WithSetupContent IXmlSitemap_SetupFeed_MaximumPageSize<IXmlSitemap_SetupFeed_WithMaximumPageSize_WithSetupContent>.WithMaximumPageSize(int maximumPageSize)
        {
            return (IXmlSitemap_SetupFeed_WithMaximumPageSize_WithSetupContent)this.WithMaximumPageSize(maximumPageSize);
        }

        IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithSetupContent IXmlSitemap_SetupFeed_SetupXmlWriter<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithSetupContent>.WithXmlWriterSettings(Func<IXmlSitemap_SetupXmlWriter_Starter, IXmlSitemap_SetupXmlWriter_Finalizer> expression)
        {
            return (IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithSetupContent)this.WithXmlWriterSettings(expression);
        }

        IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithSetupContent IXmlSitemap_SetupFeed_SetupXmlWriter<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithSetupContent>.WithXmlWriterSettings(XmlWriterSettings xmlWriterSettings)
        {
            return (IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithSetupContent)this.WithXmlWriterSettings(xmlWriterSettings);
        }






        IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithSetupContent IXmlSitemap_SetupFeed_SetupXmlWriter<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithSetupContent>.WithXmlWriterSettings(Func<IXmlSitemap_SetupXmlWriter_Starter, IXmlSitemap_SetupXmlWriter_Finalizer> expression)
        {
            return (IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithSetupContent)this.WithXmlWriterSettings(expression);
        }

        IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithSetupContent IXmlSitemap_SetupFeed_SetupXmlWriter<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithSetupContent>.WithXmlWriterSettings(XmlWriterSettings xmlWriterSettings)
        {
            return (IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithSetupContent)this.WithXmlWriterSettings(xmlWriterSettings);
        }

        IXmlSitemap_SetupFeed_WithMaximumPageSize_WithOmitRequestCaching_WithSetupContent IXmlSitemap_SetupFeed_OmitRequestCaching<IXmlSitemap_SetupFeed_WithMaximumPageSize_WithOmitRequestCaching_WithSetupContent>.OmitRequestCaching()
        {
            return (IXmlSitemap_SetupFeed_WithMaximumPageSize_WithOmitRequestCaching_WithSetupContent)this.OmitRequestCaching();
        }

        IXmlSitemap_SetupFeed_WithMaximumPageSize_WithSetupContent_WithSetupUrlResolver IXmlSitemap_SetupUrlResolver<IXmlSitemap_SetupFeed_WithMaximumPageSize_WithSetupContent_WithSetupUrlResolver>.SetupUrlResolver(Func<IXmlSitemap_SetupUrlResolver_Starter, IXmlSitemap_SetupUrlResolver_Finalizer> expression)
        {
            return (IXmlSitemap_SetupFeed_WithMaximumPageSize_WithSetupContent_WithSetupUrlResolver)this.SetupUrlResolver(expression);
        }

        IXmlSitemap_SetupFeed_WithMaximumPageSize_WithSetupContent_WithSetupUrlResolver IXmlSitemap_SetupUrlResolver<IXmlSitemap_SetupFeed_WithMaximumPageSize_WithSetupContent_WithSetupUrlResolver>.SetupUrlResolver(IXmlSitemapUrlResolverFactory xmlSitemapUrlResolverFactory)
        {
            return (IXmlSitemap_SetupFeed_WithMaximumPageSize_WithSetupContent_WithSetupUrlResolver)this.SetupUrlResolver(xmlSitemapUrlResolverFactory);
        }



        IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithOmitRequestCaching_WithSetupContent IXmlSitemap_SetupFeed_SetupXmlWriter<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithOmitRequestCaching_WithSetupContent>.WithXmlWriterSettings(Func<IXmlSitemap_SetupXmlWriter_Starter, IXmlSitemap_SetupXmlWriter_Finalizer> expression)
        {
            return (IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithOmitRequestCaching_WithSetupContent)this.WithXmlWriterSettings(expression);
        }

        IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithOmitRequestCaching_WithSetupContent IXmlSitemap_SetupFeed_SetupXmlWriter<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithOmitRequestCaching_WithSetupContent>.WithXmlWriterSettings(XmlWriterSettings xmlWriterSettings)
        {
            return (IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithOmitRequestCaching_WithSetupContent)this.WithXmlWriterSettings(xmlWriterSettings);
        }

        IXmlSitemap_SetupFeed_WithMaximumPageSize_WithOmitRequestCaching_WithSetupContent IXmlSitemap_SetupFeed_MaximumPageSize<IXmlSitemap_SetupFeed_WithMaximumPageSize_WithOmitRequestCaching_WithSetupContent>.WithMaximumPageSize(int maximumPageSize)
        {
            return (IXmlSitemap_SetupFeed_WithMaximumPageSize_WithOmitRequestCaching_WithSetupContent)this.WithMaximumPageSize(maximumPageSize);
        }

        IXmlSitemap_SetupFeed_WithOmitRequestCaching_WithSetupContent_WithSetupUrlResolver IXmlSitemap_SetupUrlResolver<IXmlSitemap_SetupFeed_WithOmitRequestCaching_WithSetupContent_WithSetupUrlResolver>.SetupUrlResolver(Func<IXmlSitemap_SetupUrlResolver_Starter, IXmlSitemap_SetupUrlResolver_Finalizer> expression)
        {
            return (IXmlSitemap_SetupFeed_WithOmitRequestCaching_WithSetupContent_WithSetupUrlResolver)this.SetupUrlResolver(expression);
        }

        IXmlSitemap_SetupFeed_WithOmitRequestCaching_WithSetupContent_WithSetupUrlResolver IXmlSitemap_SetupUrlResolver<IXmlSitemap_SetupFeed_WithOmitRequestCaching_WithSetupContent_WithSetupUrlResolver>.SetupUrlResolver(IXmlSitemapUrlResolverFactory xmlSitemapUrlResolverFactory)
        {
            return (IXmlSitemap_SetupFeed_WithOmitRequestCaching_WithSetupContent_WithSetupUrlResolver)this.SetupUrlResolver(xmlSitemapUrlResolverFactory);
        }



        IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithSetupContent_WithSetupUrlResolver IXmlSitemap_SetupUrlResolver<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithSetupContent_WithSetupUrlResolver>.SetupUrlResolver(Func<IXmlSitemap_SetupUrlResolver_Starter, IXmlSitemap_SetupUrlResolver_Finalizer> expression)
        {
            return (IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithSetupContent_WithSetupUrlResolver)this.SetupUrlResolver(expression);
        }

        IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithSetupContent_WithSetupUrlResolver IXmlSitemap_SetupUrlResolver<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithSetupContent_WithSetupUrlResolver>.SetupUrlResolver(IXmlSitemapUrlResolverFactory xmlSitemapUrlResolverFactory)
        {
            return (IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithSetupContent_WithSetupUrlResolver)this.SetupUrlResolver(xmlSitemapUrlResolverFactory);
        }

        IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithOmitRequestCaching_WithSetupContent IXmlSitemap_SetupFeed_OmitRequestCaching<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithOmitRequestCaching_WithSetupContent>.OmitRequestCaching()
        {
            return (IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithOmitRequestCaching_WithSetupContent)this.OmitRequestCaching();
        }

        IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithSetupContent IXmlSitemap_SetupFeed_MaximumPageSize<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithSetupContent>.WithMaximumPageSize(int maximumPageSize)
        {
            return (IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithSetupContent)this.WithMaximumPageSize(maximumPageSize);
        }



        IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithOmitRequestCaching_WithSetupContent_WithSetupUrlResolver IXmlSitemap_SetupUrlResolver<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithOmitRequestCaching_WithSetupContent_WithSetupUrlResolver>.SetupUrlResolver(Func<IXmlSitemap_SetupUrlResolver_Starter, IXmlSitemap_SetupUrlResolver_Finalizer> expression)
        {
            return (IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithOmitRequestCaching_WithSetupContent_WithSetupUrlResolver)this.SetupUrlResolver(expression);
        }

        IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithOmitRequestCaching_WithSetupContent_WithSetupUrlResolver IXmlSitemap_SetupUrlResolver<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithOmitRequestCaching_WithSetupContent_WithSetupUrlResolver>.SetupUrlResolver(IXmlSitemapUrlResolverFactory xmlSitemapUrlResolverFactory)
        {
            return (IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithOmitRequestCaching_WithSetupContent_WithSetupUrlResolver)this.SetupUrlResolver(xmlSitemapUrlResolverFactory);
        }

        IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithOmitRequestCaching_WithSetupContent IXmlSitemap_SetupFeed_MaximumPageSize<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithOmitRequestCaching_WithSetupContent>.WithMaximumPageSize(int maximumPageSize)
        {
            return (IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithOmitRequestCaching_WithSetupContent)this.WithMaximumPageSize(maximumPageSize);
        }


        IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithSetupContent_WithSetupUrlResolver IXmlSitemap_SetupUrlResolver<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithSetupContent_WithSetupUrlResolver>.SetupUrlResolver(Func<IXmlSitemap_SetupUrlResolver_Starter, IXmlSitemap_SetupUrlResolver_Finalizer> expression)
        {
            return (IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithSetupContent_WithSetupUrlResolver)this.SetupUrlResolver(expression);
        }

        IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithSetupContent_WithSetupUrlResolver IXmlSitemap_SetupUrlResolver<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithSetupContent_WithSetupUrlResolver>.SetupUrlResolver(IXmlSitemapUrlResolverFactory xmlSitemapUrlResolverFactory)
        {
            return (IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithSetupContent_WithSetupUrlResolver)this.SetupUrlResolver(xmlSitemapUrlResolverFactory);
        }

        IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithOmitRequestCaching_WithSetupContent IXmlSitemap_SetupFeed_OmitRequestCaching<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithOmitRequestCaching_WithSetupContent>.OmitRequestCaching()
        {
            return (IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithOmitRequestCaching_WithSetupContent)this.OmitRequestCaching();
        }



        IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithOmitRequestCaching_WithSetupContent IXmlSitemap_SetupFeed_SetupXmlWriter<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithOmitRequestCaching_WithSetupContent>.WithXmlWriterSettings(Func<IXmlSitemap_SetupXmlWriter_Starter, IXmlSitemap_SetupXmlWriter_Finalizer> expression)
        {
            return (IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithOmitRequestCaching_WithSetupContent)this.WithXmlWriterSettings(expression);
        }

        IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithOmitRequestCaching_WithSetupContent IXmlSitemap_SetupFeed_SetupXmlWriter<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithOmitRequestCaching_WithSetupContent>.WithXmlWriterSettings(XmlWriterSettings xmlWriterSettings)
        {
            return (IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithOmitRequestCaching_WithSetupContent)this.WithXmlWriterSettings(xmlWriterSettings);
        }

        IXmlSitemap_SetupFeed_WithMaximumPageSize_WithOmitRequestCaching_WithSetupContent_WithSetupUrlResolver IXmlSitemap_SetupUrlResolver<IXmlSitemap_SetupFeed_WithMaximumPageSize_WithOmitRequestCaching_WithSetupContent_WithSetupUrlResolver>.SetupUrlResolver(Func<IXmlSitemap_SetupUrlResolver_Starter, IXmlSitemap_SetupUrlResolver_Finalizer> expression)
        {
            return (IXmlSitemap_SetupFeed_WithMaximumPageSize_WithOmitRequestCaching_WithSetupContent_WithSetupUrlResolver)this.SetupUrlResolver(expression);
        }

        IXmlSitemap_SetupFeed_WithMaximumPageSize_WithOmitRequestCaching_WithSetupContent_WithSetupUrlResolver IXmlSitemap_SetupUrlResolver<IXmlSitemap_SetupFeed_WithMaximumPageSize_WithOmitRequestCaching_WithSetupContent_WithSetupUrlResolver>.SetupUrlResolver(IXmlSitemapUrlResolverFactory xmlSitemapUrlResolverFactory)
        {
            return (IXmlSitemap_SetupFeed_WithMaximumPageSize_WithOmitRequestCaching_WithSetupContent_WithSetupUrlResolver)this.SetupUrlResolver(xmlSitemapUrlResolverFactory);
        }



        IXmlSitemap_SetupFeed_Finalizer IXmlSitemap_SetupUrlResolver<IXmlSitemap_SetupFeed_Finalizer>.SetupUrlResolver(Func<IXmlSitemap_SetupUrlResolver_Starter, IXmlSitemap_SetupUrlResolver_Finalizer> expression)
        {
            return this.SetupUrlResolver(expression);
        }

        IXmlSitemap_SetupFeed_Finalizer IXmlSitemap_SetupUrlResolver<IXmlSitemap_SetupFeed_Finalizer>.SetupUrlResolver(IXmlSitemapUrlResolverFactory xmlSitemapUrlResolverFactory)
        {
            return this.SetupUrlResolver(xmlSitemapUrlResolverFactory);
        }

        
    }

    public abstract class XmlSitemap_SetupFeed_WithOmitRequestCaching : XmlSitemap_SetupFeed_WithMaximumPageSize,
        IXmlSitemap_SetupFeed_WithOmitRequestCaching,
        IXmlSitemap_SetupFeed_WithMaximumPageSize_WithOmitRequestCaching,
        IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithOmitRequestCaching,
        IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithOmitRequestCaching
    {

        IXmlSitemap_SetupFeed_WithOmitRequestCaching_WithSetupUrlResolver IXmlSitemap_SetupUrlResolver<IXmlSitemap_SetupFeed_WithOmitRequestCaching_WithSetupUrlResolver>.SetupUrlResolver(Func<IXmlSitemap_SetupUrlResolver_Starter, IXmlSitemap_SetupUrlResolver_Finalizer> expression)
        {
            return (IXmlSitemap_SetupFeed_WithOmitRequestCaching_WithSetupUrlResolver)this.SetupUrlResolver(expression);
        }

        IXmlSitemap_SetupFeed_WithOmitRequestCaching_WithSetupUrlResolver IXmlSitemap_SetupUrlResolver<IXmlSitemap_SetupFeed_WithOmitRequestCaching_WithSetupUrlResolver>.SetupUrlResolver(IXmlSitemapUrlResolverFactory xmlSitemapUrlResolverFactory)
        {
            return (IXmlSitemap_SetupFeed_WithOmitRequestCaching_WithSetupUrlResolver)this.SetupUrlResolver(xmlSitemapUrlResolverFactory);
        }

        IXmlSitemap_SetupFeed_WithOmitRequestCaching_WithSetupContent IXmlSitemap_SetupFeed_SetupContent<IXmlSitemap_SetupFeed_WithOmitRequestCaching_WithSetupContent>.WithContent(Func<IXmlSitemap_SetupContent_Starter, IXmlSitemap_SetupContent_Finalizer> expression)
        {
            return (IXmlSitemap_SetupFeed_WithOmitRequestCaching_WithSetupContent)this.WithContent(expression);
        }

        IXmlSitemap_SetupFeed_WithMaximumPageSize_WithOmitRequestCaching IXmlSitemap_SetupFeed_MaximumPageSize<IXmlSitemap_SetupFeed_WithMaximumPageSize_WithOmitRequestCaching>.WithMaximumPageSize(int maximumPageSize)
        {
            return (IXmlSitemap_SetupFeed_WithMaximumPageSize_WithOmitRequestCaching)this.WithMaximumPageSize(maximumPageSize);
        }

        IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithOmitRequestCaching IXmlSitemap_SetupFeed_SetupXmlWriter<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithOmitRequestCaching>.WithXmlWriterSettings(Func<IXmlSitemap_SetupXmlWriter_Starter, IXmlSitemap_SetupXmlWriter_Finalizer> expression)
        {
            return (IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithOmitRequestCaching)this.WithXmlWriterSettings(expression);
        }

        IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithOmitRequestCaching IXmlSitemap_SetupFeed_SetupXmlWriter<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithOmitRequestCaching>.WithXmlWriterSettings(XmlWriterSettings xmlWriterSettings)
        {
            return (IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithOmitRequestCaching)this.WithXmlWriterSettings(xmlWriterSettings);
        }



        IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithOmitRequestCaching IXmlSitemap_SetupFeed_SetupXmlWriter<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithOmitRequestCaching>.WithXmlWriterSettings(Func<IXmlSitemap_SetupXmlWriter_Starter, IXmlSitemap_SetupXmlWriter_Finalizer> expression)
        {
            return (IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithOmitRequestCaching)this.WithXmlWriterSettings(expression);
        }

        IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithOmitRequestCaching IXmlSitemap_SetupFeed_SetupXmlWriter<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithOmitRequestCaching>.WithXmlWriterSettings(XmlWriterSettings xmlWriterSettings)
        {
            return (IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithOmitRequestCaching)this.WithXmlWriterSettings(xmlWriterSettings);
        }

        IXmlSitemap_SetupFeed_WithMaximumPageSize_WithOmitRequestCaching_WithSetupContent IXmlSitemap_SetupFeed_SetupContent<IXmlSitemap_SetupFeed_WithMaximumPageSize_WithOmitRequestCaching_WithSetupContent>.WithContent(Func<IXmlSitemap_SetupContent_Starter, IXmlSitemap_SetupContent_Finalizer> expression)
        {
            return (IXmlSitemap_SetupFeed_WithMaximumPageSize_WithOmitRequestCaching_WithSetupContent)this.WithContent(expression);
        }

        IXmlSitemap_SetupFeed_WithMaximumPageSize_WithOmitRequestCaching_WithSetupUrlResolver IXmlSitemap_SetupUrlResolver<IXmlSitemap_SetupFeed_WithMaximumPageSize_WithOmitRequestCaching_WithSetupUrlResolver>.SetupUrlResolver(Func<IXmlSitemap_SetupUrlResolver_Starter, IXmlSitemap_SetupUrlResolver_Finalizer> expression)
        {
            return (IXmlSitemap_SetupFeed_WithMaximumPageSize_WithOmitRequestCaching_WithSetupUrlResolver)this.SetupUrlResolver(expression);
        }

        IXmlSitemap_SetupFeed_WithMaximumPageSize_WithOmitRequestCaching_WithSetupUrlResolver IXmlSitemap_SetupUrlResolver<IXmlSitemap_SetupFeed_WithMaximumPageSize_WithOmitRequestCaching_WithSetupUrlResolver>.SetupUrlResolver(IXmlSitemapUrlResolverFactory xmlSitemapUrlResolverFactory)
        {
            return (IXmlSitemap_SetupFeed_WithMaximumPageSize_WithOmitRequestCaching_WithSetupUrlResolver)this.SetupUrlResolver(xmlSitemapUrlResolverFactory);
        }



        IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithOmitRequestCaching_WithSetupUrlResolver IXmlSitemap_SetupUrlResolver<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithOmitRequestCaching_WithSetupUrlResolver>.SetupUrlResolver(Func<IXmlSitemap_SetupUrlResolver_Starter, IXmlSitemap_SetupUrlResolver_Finalizer> expression)
        {
            return (IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithOmitRequestCaching_WithSetupUrlResolver)this.SetupUrlResolver(expression);
        }

        IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithOmitRequestCaching_WithSetupUrlResolver IXmlSitemap_SetupUrlResolver<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithOmitRequestCaching_WithSetupUrlResolver>.SetupUrlResolver(IXmlSitemapUrlResolverFactory xmlSitemapUrlResolverFactory)
        {
            return (IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithOmitRequestCaching_WithSetupUrlResolver)this.SetupUrlResolver(xmlSitemapUrlResolverFactory);
        }

        IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithOmitRequestCaching_WithSetupContent IXmlSitemap_SetupFeed_SetupContent<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithOmitRequestCaching_WithSetupContent>.WithContent(Func<IXmlSitemap_SetupContent_Starter, IXmlSitemap_SetupContent_Finalizer> expression)
        {
            return (IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithOmitRequestCaching_WithSetupContent)this.WithContent(expression);
        }

        IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithOmitRequestCaching IXmlSitemap_SetupFeed_MaximumPageSize<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithOmitRequestCaching>.WithMaximumPageSize(int maximumPageSize)
        {
            return (IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithOmitRequestCaching)this.WithMaximumPageSize(maximumPageSize);
        }



        IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithOmitRequestCaching_WithSetupUrlResolver IXmlSitemap_SetupUrlResolver<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithOmitRequestCaching_WithSetupUrlResolver>.SetupUrlResolver(Func<IXmlSitemap_SetupUrlResolver_Starter, IXmlSitemap_SetupUrlResolver_Finalizer> expression)
        {
            return (IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithOmitRequestCaching_WithSetupUrlResolver)this.SetupUrlResolver(expression);
        }

        IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithOmitRequestCaching_WithSetupUrlResolver IXmlSitemap_SetupUrlResolver<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithOmitRequestCaching_WithSetupUrlResolver>.SetupUrlResolver(IXmlSitemapUrlResolverFactory xmlSitemapUrlResolverFactory)
        {
            return (IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithOmitRequestCaching_WithSetupUrlResolver)this.SetupUrlResolver(xmlSitemapUrlResolverFactory);
        }

        IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithOmitRequestCaching_WithSetupContent IXmlSitemap_SetupFeed_SetupContent<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithOmitRequestCaching_WithSetupContent>.WithContent(Func<IXmlSitemap_SetupContent_Starter, IXmlSitemap_SetupContent_Finalizer> expression)
        {
            return (IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithOmitRequestCaching_WithSetupContent)this.WithContent(expression);
        }


    }

    public abstract class XmlSitemap_SetupFeed_WithMaximumPageSize : XmlSitemap_SetupFeed_WithSetupXmlWriter,
        IXmlSitemap_SetupFeed_WithMaximumPageSize,
        IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize
    {

        IXmlSitemap_SetupFeed_WithMaximumPageSize_WithSetupUrlResolver IXmlSitemap_SetupUrlResolver<IXmlSitemap_SetupFeed_WithMaximumPageSize_WithSetupUrlResolver>.SetupUrlResolver(Func<IXmlSitemap_SetupUrlResolver_Starter, IXmlSitemap_SetupUrlResolver_Finalizer> expression)
        {
            return (IXmlSitemap_SetupFeed_WithMaximumPageSize_WithSetupUrlResolver)this.SetupUrlResolver(expression);
        }

        IXmlSitemap_SetupFeed_WithMaximumPageSize_WithSetupUrlResolver IXmlSitemap_SetupUrlResolver<IXmlSitemap_SetupFeed_WithMaximumPageSize_WithSetupUrlResolver>.SetupUrlResolver(IXmlSitemapUrlResolverFactory xmlSitemapUrlResolverFactory)
        {
            return (IXmlSitemap_SetupFeed_WithMaximumPageSize_WithSetupUrlResolver)this.SetupUrlResolver(xmlSitemapUrlResolverFactory);
        }

        IXmlSitemap_SetupFeed_WithMaximumPageSize_WithSetupContent IXmlSitemap_SetupFeed_SetupContent<IXmlSitemap_SetupFeed_WithMaximumPageSize_WithSetupContent>.WithContent(Func<IXmlSitemap_SetupContent_Starter, IXmlSitemap_SetupContent_Finalizer> expression)
        {
            return (IXmlSitemap_SetupFeed_WithMaximumPageSize_WithSetupContent)this.WithContent(expression);
        }

        IXmlSitemap_SetupFeed_WithMaximumPageSize_WithOmitRequestCaching IXmlSitemap_SetupFeed_OmitRequestCaching<IXmlSitemap_SetupFeed_WithMaximumPageSize_WithOmitRequestCaching>.OmitRequestCaching()
        {
            return (IXmlSitemap_SetupFeed_WithMaximumPageSize_WithOmitRequestCaching)this.OmitRequestCaching();
        }

        IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize IXmlSitemap_SetupFeed_SetupXmlWriter<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize>.WithXmlWriterSettings(Func<IXmlSitemap_SetupXmlWriter_Starter, IXmlSitemap_SetupXmlWriter_Finalizer> expression)
        {
            return (IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize)this.WithXmlWriterSettings(expression);
        }

        IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize IXmlSitemap_SetupFeed_SetupXmlWriter<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize>.WithXmlWriterSettings(XmlWriterSettings xmlWriterSettings)
        {
            return (IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize)this.WithXmlWriterSettings(xmlWriterSettings);
        }



        IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithSetupUrlResolver IXmlSitemap_SetupUrlResolver<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithSetupUrlResolver>.SetupUrlResolver(Func<IXmlSitemap_SetupUrlResolver_Starter, IXmlSitemap_SetupUrlResolver_Finalizer> expression)
        {
            return (IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithSetupUrlResolver)this.SetupUrlResolver(expression);
        }

        IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithSetupUrlResolver IXmlSitemap_SetupUrlResolver<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithSetupUrlResolver>.SetupUrlResolver(IXmlSitemapUrlResolverFactory xmlSitemapUrlResolverFactory)
        {
            return (IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithSetupUrlResolver)this.SetupUrlResolver(xmlSitemapUrlResolverFactory);
        }

        IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithSetupContent IXmlSitemap_SetupFeed_SetupContent<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithSetupContent>.WithContent(Func<IXmlSitemap_SetupContent_Starter, IXmlSitemap_SetupContent_Finalizer> expression)
        {
            return (IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithSetupContent)this.WithContent(expression);
        }

        IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithOmitRequestCaching IXmlSitemap_SetupFeed_OmitRequestCaching<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithOmitRequestCaching>.OmitRequestCaching()
        {
            return (IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithOmitRequestCaching)this.OmitRequestCaching();
        }


    }


    public abstract class XmlSitemap_SetupFeed_WithSetupXmlWriter : XmlSitemap_SetupFeed_BuilderBase,
        IXmlSitemap_SetupFeed_WithSetupXmlWriter
    {
        IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithSetupUrlResolver IXmlSitemap_SetupUrlResolver<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithSetupUrlResolver>.SetupUrlResolver(Func<IXmlSitemap_SetupUrlResolver_Starter, IXmlSitemap_SetupUrlResolver_Finalizer> expression)
        {
            return (IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithSetupUrlResolver)this.SetupUrlResolver(expression);
        }

        IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithSetupUrlResolver IXmlSitemap_SetupUrlResolver<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithSetupUrlResolver>.SetupUrlResolver(IXmlSitemapUrlResolverFactory xmlSitemapUrlResolverFactory)
        {
            return (IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithSetupUrlResolver)this.SetupUrlResolver(xmlSitemapUrlResolverFactory);
        }

        IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithSetupContent IXmlSitemap_SetupFeed_SetupContent<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithSetupContent>.WithContent(Func<IXmlSitemap_SetupContent_Starter, IXmlSitemap_SetupContent_Finalizer> expression)
        {
            return (IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithSetupContent)this.WithContent(expression);
        }

        IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithOmitRequestCaching IXmlSitemap_SetupFeed_OmitRequestCaching<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithOmitRequestCaching>.OmitRequestCaching()
        {
            return (IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithOmitRequestCaching)this.OmitRequestCaching();
        }

        IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize IXmlSitemap_SetupFeed_MaximumPageSize<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize>.WithMaximumPageSize(int maximumPageSize)
        {
            return (IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize)this.WithMaximumPageSize(maximumPageSize);
        }
    }

    public abstract class XmlSitemap_SetupFeed_BuilderBase : IXmlSitemap_SetupFeed_Starter
    {

        public abstract IXmlSitemap_SetupFeed_WithSetupXmlWriter WithXmlWriterSettings(Func<IXmlSitemap_SetupXmlWriter_Starter, IXmlSitemap_SetupXmlWriter_Finalizer> expression);

        public abstract IXmlSitemap_SetupFeed_WithSetupXmlWriter WithXmlWriterSettings(XmlWriterSettings xmlWriterSettings);

        public abstract IXmlSitemap_SetupFeed_WithMaximumPageSize WithMaximumPageSize(int maximumPageSize);

        public abstract IXmlSitemap_SetupFeed_WithOmitRequestCaching OmitRequestCaching();

        public abstract IXmlSitemap_SetupFeed_WithSetupContent WithContent(Func<IXmlSitemap_SetupContent_Starter, IXmlSitemap_SetupContent_Finalizer> expression);

        public abstract IXmlSitemap_SetupFeed_WithSetupUrlResolver SetupUrlResolver(Func<IXmlSitemap_SetupUrlResolver_Starter, IXmlSitemap_SetupUrlResolver_Finalizer> expression);

        public abstract IXmlSitemap_SetupFeed_WithSetupUrlResolver SetupUrlResolver(IXmlSitemapUrlResolverFactory xmlSitemapUrlResolverFactory);

        public abstract IXmlSitemapFeed Create();

        public abstract IEnumerable<ISpecializedContentXmlWriterFactory> SpecializedContentXmlWriterFactories { get; }

        public abstract IXmlSitemapUrlResolverFactory XmlSitemapUrlResolverFactory { get; }

        public abstract IXmlSitemapProviderFactory XmlSitemapProviderFactory { get; }

        public abstract IAssemblyProviderFactory AssemblyProviderFactory { get; }

        public abstract IXmlSitemapFeedPageNameProvider XmlSitemapFeedPageNameProvider { get; }

        public abstract string FeedName { get; }

        public abstract int MaximumPageSize { get; }

        public abstract bool RequestCachingOmitted { get; }

        public abstract bool UrlsWithNonMatchingContentOmitted { get; }

        public abstract XmlWriterSettings XmlWriterSettings { get; }

    }


    //public interface IXmlSitemapFeedBuilder
    //{
    //}

    // Only once
    public interface IXmlSitemap_SetupFeed_SetupXmlWriter<TRemainder>
    {
        TRemainder WithXmlWriterSettings(Func<IXmlSitemap_SetupXmlWriter_Starter, IXmlSitemap_SetupXmlWriter_Finalizer> expression);

        TRemainder WithXmlWriterSettings(XmlWriterSettings xmlWriterSettings);
    }

    // Only once (not important)
    public interface IXmlSitemap_SetupFeed_MaximumPageSize<TRemainder>
    {
        TRemainder WithMaximumPageSize(int maximumPageSize);
    }

    // Only once (not important)
    public interface IXmlSitemap_SetupFeed_OmitRequestCaching<TRemainder>
    {
        TRemainder OmitRequestCaching();
    }

    // Only once
    public interface IXmlSitemap_SetupFeed_SetupContent<TRemainder>
    {
        TRemainder WithContent(Func<IXmlSitemap_SetupContent_Starter, IXmlSitemap_SetupContent_Finalizer> expression);
    }

    // Only once
    public interface IXmlSitemap_SetupFeed_SetupUrlResolver<TRemainder>
        : IXmlSitemap_SetupUrlResolver<TRemainder>
    {
    }

    public interface IXmlSitemap_SetupFeed_Finalizer
        : IFluentInterface
    {

        IXmlSitemapFeed Create();

        // TODO: Should we just implement these properties on the builder, but remove them from the 
        // interface so they don't clutter up the fluent API?
        IEnumerable<ISpecializedContentXmlWriterFactory> SpecializedContentXmlWriterFactories { get; }

        IXmlSitemapUrlResolverFactory XmlSitemapUrlResolverFactory { get; }

        IXmlSitemapProviderFactory XmlSitemapProviderFactory { get; }

        IAssemblyProviderFactory AssemblyProviderFactory { get; }

        IXmlSitemapFeedPageNameProvider XmlSitemapFeedPageNameProvider { get; }

        string FeedName { get; }

        int MaximumPageSize { get; }

        bool RequestCachingOmitted { get; }

        bool UrlsWithNonMatchingContentOmitted { get; }

        XmlWriterSettings XmlWriterSettings { get; }
    }

    //public interface IXmlSitemap_SetupUrlResolver_Starter
    //    : IXmlSitemap_SetupUrlResolver_WithDefaultProtocol<IXmlSitemap_SetupUrlResolver_Finalizer>,
    //    IXmlSitemap_SetupUrlResolver_WithDefaultHostName<IXmlSitemap_SetupUrlResolver_Finalizer>,
    //    IXmlSitemap_SetupUrlResolver_Finalizer
    //{
    //}

    public interface IXmlSitemap_SetupFeed_Starter
        : IXmlSitemap_SetupFeed_SetupXmlWriter<IXmlSitemap_SetupFeed_WithSetupXmlWriter>,
        IXmlSitemap_SetupFeed_MaximumPageSize<IXmlSitemap_SetupFeed_WithMaximumPageSize>,
        IXmlSitemap_SetupFeed_OmitRequestCaching<IXmlSitemap_SetupFeed_WithOmitRequestCaching>,
        IXmlSitemap_SetupFeed_SetupContent<IXmlSitemap_SetupFeed_WithSetupContent>,
        IXmlSitemap_SetupFeed_SetupUrlResolver<IXmlSitemap_SetupFeed_WithSetupUrlResolver>,
        IXmlSitemap_SetupFeed_Finalizer
    {
    }

    // 1 item set

    public interface IXmlSitemap_SetupFeed_WithSetupUrlResolver
        : IXmlSitemap_SetupFeed_SetupContent<IXmlSitemap_SetupFeed_WithSetupContent_WithSetupUrlResolver>,
        IXmlSitemap_SetupFeed_OmitRequestCaching<IXmlSitemap_SetupFeed_WithOmitRequestCaching_WithSetupUrlResolver>,
        IXmlSitemap_SetupFeed_MaximumPageSize<IXmlSitemap_SetupFeed_WithMaximumPageSize_WithSetupUrlResolver>,
        IXmlSitemap_SetupFeed_SetupXmlWriter<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithSetupUrlResolver>,
        IXmlSitemap_SetupFeed_Finalizer
    {
    }

    public interface IXmlSitemap_SetupFeed_WithSetupContent
        : IXmlSitemap_SetupFeed_SetupUrlResolver<IXmlSitemap_SetupFeed_WithSetupContent_WithSetupUrlResolver>,
        IXmlSitemap_SetupFeed_OmitRequestCaching<IXmlSitemap_SetupFeed_WithOmitRequestCaching_WithSetupContent>,
        IXmlSitemap_SetupFeed_MaximumPageSize<IXmlSitemap_SetupFeed_WithMaximumPageSize_WithSetupContent>,
        IXmlSitemap_SetupFeed_SetupXmlWriter<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithSetupContent>,
        IXmlSitemap_SetupFeed_Finalizer
    {
    }

    public interface IXmlSitemap_SetupFeed_WithOmitRequestCaching
        : IXmlSitemap_SetupFeed_SetupUrlResolver<IXmlSitemap_SetupFeed_WithOmitRequestCaching_WithSetupUrlResolver>,
        IXmlSitemap_SetupFeed_SetupContent<IXmlSitemap_SetupFeed_WithOmitRequestCaching_WithSetupContent>,
        IXmlSitemap_SetupFeed_MaximumPageSize<IXmlSitemap_SetupFeed_WithMaximumPageSize_WithOmitRequestCaching>,
        IXmlSitemap_SetupFeed_SetupXmlWriter<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithOmitRequestCaching>,
        IXmlSitemap_SetupFeed_Finalizer
    {
    }

    public interface IXmlSitemap_SetupFeed_WithMaximumPageSize
        : IXmlSitemap_SetupFeed_SetupUrlResolver<IXmlSitemap_SetupFeed_WithMaximumPageSize_WithSetupUrlResolver>,
        IXmlSitemap_SetupFeed_SetupContent<IXmlSitemap_SetupFeed_WithMaximumPageSize_WithSetupContent>,
        IXmlSitemap_SetupFeed_OmitRequestCaching<IXmlSitemap_SetupFeed_WithMaximumPageSize_WithOmitRequestCaching>,
        IXmlSitemap_SetupFeed_SetupXmlWriter<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize>,
        IXmlSitemap_SetupFeed_Finalizer
    {
    }

    public interface IXmlSitemap_SetupFeed_WithSetupXmlWriter
        : IXmlSitemap_SetupFeed_SetupUrlResolver<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithSetupUrlResolver>,
        IXmlSitemap_SetupFeed_SetupContent<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithSetupContent>,
        IXmlSitemap_SetupFeed_OmitRequestCaching<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithOmitRequestCaching>,
        IXmlSitemap_SetupFeed_MaximumPageSize<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize>,
        IXmlSitemap_SetupFeed_Finalizer
    {
    }


    // 2 items set

    public interface IXmlSitemap_SetupFeed_WithSetupContent_WithSetupUrlResolver
        : IXmlSitemap_SetupFeed_SetupXmlWriter<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithSetupContent_WithSetupUrlResolver>,
        IXmlSitemap_SetupFeed_MaximumPageSize<IXmlSitemap_SetupFeed_WithMaximumPageSize_WithSetupContent_WithSetupUrlResolver>,
        IXmlSitemap_SetupFeed_OmitRequestCaching<IXmlSitemap_SetupFeed_WithOmitRequestCaching_WithSetupContent_WithSetupUrlResolver>,
        IXmlSitemap_SetupFeed_Finalizer
    {
    }


    public interface IXmlSitemap_SetupFeed_WithOmitRequestCaching_WithSetupUrlResolver
        : IXmlSitemap_SetupFeed_SetupXmlWriter<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithOmitRequestCaching_WithSetupUrlResolver>,
        IXmlSitemap_SetupFeed_MaximumPageSize<IXmlSitemap_SetupFeed_WithMaximumPageSize_WithOmitRequestCaching_WithSetupUrlResolver>,
        IXmlSitemap_SetupFeed_SetupContent<IXmlSitemap_SetupFeed_WithOmitRequestCaching_WithSetupContent_WithSetupUrlResolver>,
        IXmlSitemap_SetupFeed_Finalizer
    {
    }

    public interface IXmlSitemap_SetupFeed_WithOmitRequestCaching_WithSetupContent
        : IXmlSitemap_SetupFeed_SetupXmlWriter<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithOmitRequestCaching_WithSetupContent>,
        IXmlSitemap_SetupFeed_MaximumPageSize<IXmlSitemap_SetupFeed_WithMaximumPageSize_WithOmitRequestCaching_WithSetupContent>,
        IXmlSitemap_SetupFeed_SetupUrlResolver<IXmlSitemap_SetupFeed_WithOmitRequestCaching_WithSetupContent_WithSetupUrlResolver>,
        IXmlSitemap_SetupFeed_Finalizer
    {
    }


    public interface IXmlSitemap_SetupFeed_WithMaximumPageSize_WithSetupUrlResolver
        : IXmlSitemap_SetupFeed_SetupXmlWriter<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithSetupUrlResolver>,
        IXmlSitemap_SetupFeed_OmitRequestCaching<IXmlSitemap_SetupFeed_WithMaximumPageSize_WithOmitRequestCaching_WithSetupUrlResolver>,
        IXmlSitemap_SetupFeed_SetupContent<IXmlSitemap_SetupFeed_WithMaximumPageSize_WithSetupContent_WithSetupUrlResolver>,
        IXmlSitemap_SetupFeed_Finalizer
    {
    }

    public interface IXmlSitemap_SetupFeed_WithMaximumPageSize_WithSetupContent
        : IXmlSitemap_SetupFeed_SetupXmlWriter<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithSetupContent>,
        IXmlSitemap_SetupFeed_OmitRequestCaching<IXmlSitemap_SetupFeed_WithMaximumPageSize_WithOmitRequestCaching_WithSetupContent>,
        IXmlSitemap_SetupFeed_SetupUrlResolver<IXmlSitemap_SetupFeed_WithMaximumPageSize_WithSetupContent_WithSetupUrlResolver>,
        IXmlSitemap_SetupFeed_Finalizer
    {
    }

    public interface IXmlSitemap_SetupFeed_WithMaximumPageSize_WithOmitRequestCaching
        : IXmlSitemap_SetupFeed_SetupXmlWriter<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithOmitRequestCaching>,
        IXmlSitemap_SetupFeed_SetupContent<IXmlSitemap_SetupFeed_WithMaximumPageSize_WithOmitRequestCaching_WithSetupContent>,
        IXmlSitemap_SetupFeed_SetupUrlResolver<IXmlSitemap_SetupFeed_WithMaximumPageSize_WithOmitRequestCaching_WithSetupUrlResolver>,
        IXmlSitemap_SetupFeed_Finalizer
    {
    }


    public interface IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithSetupUrlResolver
        : IXmlSitemap_SetupFeed_SetupContent<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithSetupContent_WithSetupUrlResolver>,
        IXmlSitemap_SetupFeed_OmitRequestCaching<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithOmitRequestCaching_WithSetupUrlResolver>,
        IXmlSitemap_SetupFeed_MaximumPageSize<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithSetupUrlResolver>,
        IXmlSitemap_SetupFeed_Finalizer
    {
    }

    public interface IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithSetupContent
        : IXmlSitemap_SetupFeed_SetupUrlResolver<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithSetupContent_WithSetupUrlResolver>,
        IXmlSitemap_SetupFeed_OmitRequestCaching<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithOmitRequestCaching_WithSetupContent>,
        IXmlSitemap_SetupFeed_MaximumPageSize<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithSetupContent>,
        IXmlSitemap_SetupFeed_Finalizer
    {
    }

    public interface IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithOmitRequestCaching
        : IXmlSitemap_SetupFeed_SetupUrlResolver<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithOmitRequestCaching_WithSetupUrlResolver>,
        IXmlSitemap_SetupFeed_SetupContent<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithOmitRequestCaching_WithSetupContent>,
        IXmlSitemap_SetupFeed_MaximumPageSize<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithOmitRequestCaching>,
        IXmlSitemap_SetupFeed_Finalizer
    {
    }

    public interface IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize
        : IXmlSitemap_SetupFeed_SetupUrlResolver<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithSetupUrlResolver>,
        IXmlSitemap_SetupFeed_SetupContent<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithSetupContent>,
        IXmlSitemap_SetupFeed_OmitRequestCaching<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithOmitRequestCaching>,
        IXmlSitemap_SetupFeed_Finalizer
    {
    }

    // 3 items set


    public interface IXmlSitemap_SetupFeed_WithOmitRequestCaching_WithSetupContent_WithSetupUrlResolver
        : IXmlSitemap_SetupFeed_SetupXmlWriter<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithOmitRequestCaching_WithSetupContent_WithSetupUrlResolver>,
        IXmlSitemap_SetupFeed_MaximumPageSize<IXmlSitemap_SetupFeed_WithMaximumPageSize_WithOmitRequestCaching_WithSetupContent_WithSetupUrlResolver>,
        IXmlSitemap_SetupFeed_Finalizer
    {
    }


    public interface IXmlSitemap_SetupFeed_WithMaximumPageSize_WithSetupContent_WithSetupUrlResolver
        : IXmlSitemap_SetupFeed_SetupXmlWriter<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithSetupContent_WithSetupUrlResolver>,
        IXmlSitemap_SetupFeed_OmitRequestCaching<IXmlSitemap_SetupFeed_WithMaximumPageSize_WithOmitRequestCaching_WithSetupContent_WithSetupUrlResolver>,
        IXmlSitemap_SetupFeed_Finalizer
    {
    }

    public interface IXmlSitemap_SetupFeed_WithMaximumPageSize_WithOmitRequestCaching_WithSetupUrlResolver
        : IXmlSitemap_SetupFeed_SetupXmlWriter<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithOmitRequestCaching_WithSetupUrlResolver>,
        IXmlSitemap_SetupFeed_SetupContent<IXmlSitemap_SetupFeed_WithMaximumPageSize_WithOmitRequestCaching_WithSetupContent_WithSetupUrlResolver>,
        IXmlSitemap_SetupFeed_Finalizer
    {
    }

    public interface IXmlSitemap_SetupFeed_WithMaximumPageSize_WithOmitRequestCaching_WithSetupContent
        : IXmlSitemap_SetupFeed_SetupXmlWriter<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithOmitRequestCaching_WithSetupContent>,
        IXmlSitemap_SetupFeed_SetupUrlResolver<IXmlSitemap_SetupFeed_WithMaximumPageSize_WithOmitRequestCaching_WithSetupContent_WithSetupUrlResolver>,
        IXmlSitemap_SetupFeed_Finalizer
    {
    }


    public interface IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithSetupContent_WithSetupUrlResolver
        : IXmlSitemap_SetupFeed_OmitRequestCaching<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithOmitRequestCaching_WithSetupContent_WithSetupUrlResolver>,
        IXmlSitemap_SetupFeed_MaximumPageSize<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithSetupContent_WithSetupUrlResolver>,
        IXmlSitemap_SetupFeed_Finalizer
    {
    }

    public interface IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithOmitRequestCaching_WithSetupUrlResolver
        : IXmlSitemap_SetupFeed_SetupContent<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithOmitRequestCaching_WithSetupContent_WithSetupUrlResolver>,
        IXmlSitemap_SetupFeed_MaximumPageSize<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithOmitRequestCaching_WithSetupUrlResolver>,
        IXmlSitemap_SetupFeed_Finalizer
    {
    }

    public interface IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithOmitRequestCaching_WithSetupContent
        : IXmlSitemap_SetupFeed_SetupUrlResolver<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithOmitRequestCaching_WithSetupContent_WithSetupUrlResolver>,
        IXmlSitemap_SetupFeed_MaximumPageSize<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithOmitRequestCaching_WithSetupContent>,
        IXmlSitemap_SetupFeed_Finalizer
    {
    }

    public interface IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithSetupUrlResolver
        : IXmlSitemap_SetupFeed_SetupContent<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithSetupContent_WithSetupUrlResolver>,
        IXmlSitemap_SetupFeed_OmitRequestCaching<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithOmitRequestCaching_WithSetupUrlResolver>,
        IXmlSitemap_SetupFeed_Finalizer
    {
    }

    public interface IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithSetupContent
        : IXmlSitemap_SetupFeed_SetupUrlResolver<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithSetupContent_WithSetupUrlResolver>,
        IXmlSitemap_SetupFeed_OmitRequestCaching<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithOmitRequestCaching_WithSetupContent>,
        IXmlSitemap_SetupFeed_Finalizer
    {
    }

    public interface IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithOmitRequestCaching
        : IXmlSitemap_SetupFeed_SetupUrlResolver<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithOmitRequestCaching_WithSetupUrlResolver>,
        IXmlSitemap_SetupFeed_SetupContent<IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithOmitRequestCaching_WithSetupContent>,
        IXmlSitemap_SetupFeed_Finalizer
    {
    }

    // 4 items set

    public interface IXmlSitemap_SetupFeed_WithMaximumPageSize_WithOmitRequestCaching_WithSetupContent_WithSetupUrlResolver
        : IXmlSitemap_SetupFeed_SetupUrlResolver<IXmlSitemap_SetupFeed_Finalizer>,
        IXmlSitemap_SetupFeed_Finalizer
    {
    }

    public interface IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithOmitRequestCaching_WithSetupContent_WithSetupUrlResolver
        : IXmlSitemap_SetupFeed_MaximumPageSize<IXmlSitemap_SetupFeed_Finalizer>,
        IXmlSitemap_SetupFeed_Finalizer
    {
    }

    public interface IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithSetupContent_WithSetupUrlResolver
        : IXmlSitemap_SetupFeed_OmitRequestCaching<IXmlSitemap_SetupFeed_Finalizer>,
        IXmlSitemap_SetupFeed_Finalizer
    {
    }

    public interface IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithOmitRequestCaching_WithSetupUrlResolver
        : IXmlSitemap_SetupFeed_SetupContent<IXmlSitemap_SetupFeed_Finalizer>,
        IXmlSitemap_SetupFeed_Finalizer
    {
    }

    public interface IXmlSitemap_SetupFeed_WithSetupXmlWriter_WithMaximumPageSize_WithOmitRequestCaching_WithSetupContent
        : IXmlSitemap_SetupFeed_SetupUrlResolver<IXmlSitemap_SetupFeed_Finalizer>,
        IXmlSitemap_SetupFeed_Finalizer
    {
    }






    //public interface IXmlSitemapFeedBuilder
    //    : IFluentInterface
    //{
    //    IXmlSitemapFeedBuilder WithXmlWriterSettings(XmlWriterSettings xmlWriterSettings);

    //    IXmlSitemapFeedBuilder WithXmlWriterFactory(IXmlWriterFactory xmlWriterFactory);

    //    // TODO: Add a method for each pertinent xml writer setting, and use a builder to create an XmlWriterSettings object

    //    IXmlSitemapFeedBuilder WithXmlSitemapPageManager(IXmlSitemapPageManager xmlSitemapPageManager);

    //    IXmlSitemapFeed Create();
    //}
}
