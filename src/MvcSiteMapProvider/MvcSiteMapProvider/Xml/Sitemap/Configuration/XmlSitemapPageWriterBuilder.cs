using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Xml.Sitemap.Configuration
{
    public class XmlSitemapPageWriterBuilder
        : IXmlSitemapPageWriterBuilder
    {
        public XmlSitemapPageWriterBuilder()
            : this(urlEntryHelperFactory: new UrlEntryHelperFactory(), xmlSitemapWriterFactory: new XmlSitemapWriterFactoryBuilder().Create())
        {
        }

        private XmlSitemapPageWriterBuilder(
            IUrlEntryHelperFactory urlEntryHelperFactory,
            IXmlSitemapWriterFactory xmlSitemapWriterFactory
            )
        {
            if (urlEntryHelperFactory == null)
                throw new ArgumentNullException("urlEntryHelperFactory");
            if (xmlSitemapWriterFactory == null)
                throw new ArgumentNullException("xmlSitemapWriterFactory");

            this.urlEntryHelperFactory = urlEntryHelperFactory;
            this.xmlSitemapWriterFactory = xmlSitemapWriterFactory;
        }
        private readonly IUrlEntryHelperFactory urlEntryHelperFactory;
        private readonly IXmlSitemapWriterFactory xmlSitemapWriterFactory;


        public IXmlSitemapPageWriterBuilder WithUrlEntryHelperFactory(IUrlEntryHelperFactory urlEntryHelperFactory)
        {
            return new XmlSitemapPageWriterBuilder(urlEntryHelperFactory, this.xmlSitemapWriterFactory);
        }

        public IXmlSitemapPageWriterBuilder WithXmlSitemapWriterFactory(IXmlSitemapWriterFactory xmlSitemapWriterFactory)
        {
            return new XmlSitemapPageWriterBuilder(this.urlEntryHelperFactory, xmlSitemapWriterFactory);
        }

        public IXmlSitemapPageWriter Create()
        {
            return new XmlSitemapPageWriter(this.urlEntryHelperFactory, this.xmlSitemapWriterFactory);
        }
    }
}
