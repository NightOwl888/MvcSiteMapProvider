using System;
using System.Collections.Generic;
using System.Linq;
using MvcSiteMapProvider.Xml.Sitemap.Index;

namespace MvcSiteMapProvider.Xml.Sitemap.Configuration
{
    public class XmlSitemapIndexPageWriterBuilder
        : IXmlSitemapIndexPageWriterBuilder
    {
        public XmlSitemapIndexPageWriterBuilder()
            : this(
                xmlSitemapIndexWriterFactory: new XmlSitemapIndexWriterFactory(), 
                xmlSitemapFeedUrlResolver: new XmlSitemapFeedUrlResolverBuilder().Create(), 
                sitemapEntryFactory: new SitemapEntryFactory())
        {
        }

        private XmlSitemapIndexPageWriterBuilder(
            IXmlSitemapIndexWriterFactory xmlSitemapIndexWriterFactory,
            IXmlSitemapFeedUrlResolver xmlSitemapFeedUrlResolver,
            ISitemapEntryFactory sitemapEntryFactory
            )
        {
            if (xmlSitemapIndexWriterFactory == null)
                throw new ArgumentNullException("xmlSitemapIndexWriterFactory");
            if (xmlSitemapFeedUrlResolver == null)
                throw new ArgumentNullException("xmlSitemapFeedUrlResolver");
            if (sitemapEntryFactory == null)
                throw new ArgumentNullException("sitemapEntryFactory");

            this.xmlSitemapIndexWriterFactory = xmlSitemapIndexWriterFactory;
            this.xmlSitemapFeedUrlResolver = xmlSitemapFeedUrlResolver;
            this.sitemapEntryFactory = sitemapEntryFactory;
        }
        private readonly IXmlSitemapIndexWriterFactory xmlSitemapIndexWriterFactory;
        private readonly IXmlSitemapFeedUrlResolver xmlSitemapFeedUrlResolver;
        private readonly ISitemapEntryFactory sitemapEntryFactory;

        public IXmlSitemapIndexPageWriterBuilder WithXmlSitemapIndexWriterFactory(IXmlSitemapIndexWriterFactory xmlSitemapIndexWriterFactory)
        {
            return new XmlSitemapIndexPageWriterBuilder(xmlSitemapIndexWriterFactory, this.xmlSitemapFeedUrlResolver, this.sitemapEntryFactory);
        }

        public IXmlSitemapIndexPageWriterBuilder WithXmlSitemapFeedUrlResolver(IXmlSitemapFeedUrlResolver xmlSitemapFeedUrlResolver)
        {
            return new XmlSitemapIndexPageWriterBuilder(this.xmlSitemapIndexWriterFactory, xmlSitemapFeedUrlResolver, this.sitemapEntryFactory);
        }

        public IXmlSitemapIndexPageWriterBuilder WithSitemapEntryFactory(ISitemapEntryFactory sitemapEntryFactory)
        {
            return new XmlSitemapIndexPageWriterBuilder(this.xmlSitemapIndexWriterFactory, this.xmlSitemapFeedUrlResolver, sitemapEntryFactory);
        }

        public IXmlSitemapIndexPageWriter Create()
        {
            return new XmlSitemapIndexPageWriter(this.xmlSitemapIndexWriterFactory, this.xmlSitemapFeedUrlResolver, this.sitemapEntryFactory);
        }
    }
}
