using System;
using System.Collections.Generic;
using System.Linq;
using MvcSiteMapProvider.ComponentModel;
using MvcSiteMapProvider.Xml.Sitemap.Index;

namespace MvcSiteMapProvider.Xml.Sitemap.Configuration
{
    public interface IXmlSitemapIndexPageWriterBuilder
        : IFluentInterface
    {
        IXmlSitemapIndexPageWriterBuilder WithXmlSitemapIndexWriterFactory(IXmlSitemapIndexWriterFactory xmlSitemapIndexWriterFactory);

        IXmlSitemapIndexPageWriterBuilder WithXmlSitemapFeedUrlResolver(IXmlSitemapFeedUrlResolver xmlSitemapFeedUrlResolver);

        IXmlSitemapIndexPageWriterBuilder WithSitemapEntryFactory(ISitemapEntryFactory sitemapEntryFactory);

        IXmlSitemapIndexPageWriter Create();
    }
}
