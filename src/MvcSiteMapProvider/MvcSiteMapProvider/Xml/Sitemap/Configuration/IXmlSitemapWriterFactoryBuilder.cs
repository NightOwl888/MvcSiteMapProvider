using System;
using MvcSiteMapProvider.ComponentModel;
using MvcSiteMapProvider.Xml.Sitemap.Specialized;

namespace MvcSiteMapProvider.Xml.Sitemap.Configuration
{
    public interface IXmlSitemapWriterFactoryBuilder
        : IFluentInterface
    {
        IXmlSitemapWriterFactoryBuilder OmitUrlsWithoutMatchingContent();

        IXmlSitemapWriterFactoryBuilder WithSpecializedContentXmlWriterFactoryStrategy(ISpecializedContentXmlWriterFactoryStrategy specializedContentXmlWriterFactoryStrategy);

        IXmlSitemapWriterFactoryBuilder WithPreparedUrlEntryFactory(IPreparedUrlEntryFactory preparedUrlEntryFactory);

        IXmlSitemapWriterFactory Create();
    }
}
