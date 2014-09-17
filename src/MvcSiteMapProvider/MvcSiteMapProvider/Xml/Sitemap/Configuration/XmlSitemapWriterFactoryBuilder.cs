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
                specializedContentWriter: new SpecializedContentWriterBuilder().Create(), 
                preparedUrlEntryFactory: new PreparedUrlEntryFactoryBuilder().Create())
        {
        }

        private XmlSitemapWriterFactoryBuilder(
            bool omitUrlsWithoutMatchingContent,
            ISpecializedContentWriter specializedContentWriter,
            IPreparedUrlEntryFactory preparedUrlEntryFactory
            )
        {
            if (specializedContentWriter == null)
                throw new ArgumentNullException("specializedContentWriter");
            if (preparedUrlEntryFactory == null)
                throw new ArgumentNullException("preparedUrlEntryFactory");

            this.omitUrlsWithoutMatchingContent = omitUrlsWithoutMatchingContent;
            this.specializedContentWriter = specializedContentWriter;
            this.preparedUrlEntryFactory = preparedUrlEntryFactory;
        }
        private readonly bool omitUrlsWithoutMatchingContent;
        private readonly ISpecializedContentWriter specializedContentWriter;
        private readonly IPreparedUrlEntryFactory preparedUrlEntryFactory;

        public IXmlSitemapWriterFactoryBuilder OmitUrlsWithoutMatchingContent()
        {
            var omitUrlsWithoutMatchingContent = true;
            return new XmlSitemapWriterFactoryBuilder(omitUrlsWithoutMatchingContent, this.specializedContentWriter, this.preparedUrlEntryFactory);
        }

        public IXmlSitemapWriterFactoryBuilder WithSpecializedContentXmlWriterFactoryStrategy(ISpecializedContentXmlWriterFactoryStrategy specializedContentXmlWriterFactoryStrategy)
        {
            return new XmlSitemapWriterFactoryBuilder(this.omitUrlsWithoutMatchingContent, specializedContentWriter, this.preparedUrlEntryFactory);
        }

        public IXmlSitemapWriterFactoryBuilder WithPreparedUrlEntryFactory(IPreparedUrlEntryFactory preparedUrlEntryFactory)
        {
            return new XmlSitemapWriterFactoryBuilder(this.omitUrlsWithoutMatchingContent, this.specializedContentWriter, preparedUrlEntryFactory);
        }

        public IXmlSitemapWriterFactory Create()
        {
            return new XmlSitemapWriterFactory(this.omitUrlsWithoutMatchingContent, this.specializedContentWriter, this.preparedUrlEntryFactory);
        }
    }
}
