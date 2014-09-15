using System;
using MvcSiteMapProvider.Xml.Sitemap.Specialized;

namespace MvcSiteMapProvider.Xml.Sitemap.Configuration
{
    public class XmlSitemapWriterFactoryBuilder
        : IXmlSitemapWriterFactoryBuilder
    {
        public XmlSitemapWriterFactoryBuilder()
            : this(
                omitUrlsWithoutMatchingContent: false,
                specializedContentXmlWriterFactoryStrategy: new SpecializedContentXmlWriterFactoryStrategyBuilder().Create(), 
                preparedUrlEntryFactory: new PreparedUrlEntryFactoryBuilder().Create())
        {
        }

        private XmlSitemapWriterFactoryBuilder(
            bool omitUrlsWithoutMatchingContent,
            ISpecializedContentXmlWriterFactoryStrategy specializedContentXmlWriterFactoryStrategy,
            IPreparedUrlEntryFactory preparedUrlEntryFactory
            )
        {
            if (specializedContentXmlWriterFactoryStrategy == null)
                throw new ArgumentNullException("specializedContentXmlWriterFactoryStrategy");
            if (preparedUrlEntryFactory == null)
                throw new ArgumentNullException("preparedUrlEntryFactory");

            this.omitUrlsWithoutMatchingContent = omitUrlsWithoutMatchingContent;
            this.specializedContentXmlWriterFactoryStrategy = specializedContentXmlWriterFactoryStrategy;
            this.preparedUrlEntryFactory = preparedUrlEntryFactory;
        }
        private readonly bool omitUrlsWithoutMatchingContent;
        private readonly ISpecializedContentXmlWriterFactoryStrategy specializedContentXmlWriterFactoryStrategy;
        private readonly IPreparedUrlEntryFactory preparedUrlEntryFactory;

        public IXmlSitemapWriterFactoryBuilder OmitUrlsWithoutMatchingContent()
        {
            var omitUrlsWithoutMatchingContent = true;
            return new XmlSitemapWriterFactoryBuilder(omitUrlsWithoutMatchingContent, this.specializedContentXmlWriterFactoryStrategy, this.preparedUrlEntryFactory);
        }

        public IXmlSitemapWriterFactoryBuilder WithSpecializedContentXmlWriterFactoryStrategy(ISpecializedContentXmlWriterFactoryStrategy specializedContentXmlWriterFactoryStrategy)
        {
            return new XmlSitemapWriterFactoryBuilder(this.omitUrlsWithoutMatchingContent, specializedContentXmlWriterFactoryStrategy, this.preparedUrlEntryFactory);
        }

        public IXmlSitemapWriterFactoryBuilder WithPreparedUrlEntryFactory(IPreparedUrlEntryFactory preparedUrlEntryFactory)
        {
            return new XmlSitemapWriterFactoryBuilder(this.omitUrlsWithoutMatchingContent, this.specializedContentXmlWriterFactoryStrategy, preparedUrlEntryFactory);
        }

        public IXmlSitemapWriterFactory Create()
        {
            return new XmlSitemapWriterFactory(this.omitUrlsWithoutMatchingContent, this.specializedContentXmlWriterFactoryStrategy, this.preparedUrlEntryFactory);
        }
    }
}
