using System;
using MvcSiteMapProvider.Xml.Sitemap.Specialized;

namespace MvcSiteMapProvider.Xml.Sitemap.Configuration
{
    public class XmlSitemapWriterFactoryBuilder
        : IXmlSitemapWriterFactoryBuilder
    {
        public XmlSitemapWriterFactoryBuilder()
            : this(
                specializedContentXmlWriterFactoryStrategy: new SpecializedContentXmlWriterFactoryStrategyBuilder().Create(), 
                preparedUrlEntryFactory: new PreparedUrlEntryFactoryBuilder().Create())
        {
        }

        private XmlSitemapWriterFactoryBuilder(
            ISpecializedContentXmlWriterFactoryStrategy specializedContentXmlWriterFactoryStrategy,
            IPreparedUrlEntryFactory preparedUrlEntryFactory
            )
        {
            if (specializedContentXmlWriterFactoryStrategy == null)
                throw new ArgumentNullException("specializedContentXmlWriterFactoryStrategy");
            if (preparedUrlEntryFactory == null)
                throw new ArgumentNullException("preparedUrlEntryFactory");

            this.specializedContentXmlWriterFactoryStrategy = specializedContentXmlWriterFactoryStrategy;
            this.preparedUrlEntryFactory = preparedUrlEntryFactory;
        }
        private readonly ISpecializedContentXmlWriterFactoryStrategy specializedContentXmlWriterFactoryStrategy;
        private readonly IPreparedUrlEntryFactory preparedUrlEntryFactory;

        public IXmlSitemapWriterFactoryBuilder WithSpecializedContentXmlWriterFactoryStrategy(ISpecializedContentXmlWriterFactoryStrategy specializedContentXmlWriterFactoryStrategy)
        {
            return new XmlSitemapWriterFactoryBuilder(specializedContentXmlWriterFactoryStrategy, this.preparedUrlEntryFactory);
        }

        public IXmlSitemapWriterFactoryBuilder WithPreparedUrlEntryFactory(IPreparedUrlEntryFactory preparedUrlEntryFactory)
        {
            return new XmlSitemapWriterFactoryBuilder(this.specializedContentXmlWriterFactoryStrategy, preparedUrlEntryFactory);
        }

        public IXmlSitemapWriterFactory Create()
        {
            return new XmlSitemapWriterFactory(this.specializedContentXmlWriterFactoryStrategy, this.preparedUrlEntryFactory);
        }
    }
}
