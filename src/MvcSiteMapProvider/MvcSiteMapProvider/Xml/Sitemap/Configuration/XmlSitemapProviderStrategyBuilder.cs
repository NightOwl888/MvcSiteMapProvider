using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Xml.Sitemap.Configuration
{
    public class XmlSitemapProviderStrategyBuilder
        : IXmlSitemapProviderStrategyBuilder
    {
        public XmlSitemapProviderStrategyBuilder()
            : this(xmlSitemapProviderFactory: new XmlSitemapProviderFactory(), xmlSitemapProviderTypeStrategy: new XmlSitemapProviderTypeStrategyBuilder().Create())
        {
        }

        private XmlSitemapProviderStrategyBuilder(
            IXmlSitemapProviderFactory xmlSitemapProviderFactory,
            IXmlSitemapProviderTypeStrategy xmlSitemapProviderTypeStrategy
            )
        {
            if (xmlSitemapProviderFactory == null)
                throw new ArgumentNullException("xmlSitemapProviderFactory");
            if (xmlSitemapProviderTypeStrategy == null)
                throw new ArgumentNullException("xmlSitemapProviderTypeStrategy");

            this.xmlSitemapProviderFactory = xmlSitemapProviderFactory;
            this.xmlSitemapProviderTypeStrategy = xmlSitemapProviderTypeStrategy;
        }
        private readonly IXmlSitemapProviderFactory xmlSitemapProviderFactory;
        private readonly IXmlSitemapProviderTypeStrategy xmlSitemapProviderTypeStrategy;

        public IXmlSitemapProviderStrategyBuilder WithXmlSitemapProviderFactory(IXmlSitemapProviderFactory xmlSitemapProviderFactory)
        {
            return new XmlSitemapProviderStrategyBuilder(xmlSitemapProviderFactory, this.xmlSitemapProviderTypeStrategy);
        }

        public IXmlSitemapProviderStrategyBuilder WithXmlSitemapProviderTypeStrategy(IXmlSitemapProviderTypeStrategy xmlSitemapProviderTypeStrategy)
        {
            return new XmlSitemapProviderStrategyBuilder(this.xmlSitemapProviderFactory, xmlSitemapProviderTypeStrategy);
        }

        public IXmlSitemapProviderStrategy Create()
        {
            return new XmlSitemapProviderStrategy(this.xmlSitemapProviderFactory, this.xmlSitemapProviderTypeStrategy);
        }
    }
}
