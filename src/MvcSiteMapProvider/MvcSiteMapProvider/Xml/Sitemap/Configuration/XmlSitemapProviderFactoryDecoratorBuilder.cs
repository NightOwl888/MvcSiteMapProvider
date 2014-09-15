using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Xml.Sitemap.Configuration
{
    public class XmlSitemapProviderFactoryDecoratorBuilder
        : IXmlSitemapProviderFactoryDecoratorBuilder
    {
        public XmlSitemapProviderFactoryDecoratorBuilder()
            : this(xmlSitemapProviderDecoratorFactory: new RequestCachingXmlSitemapProviderDecoratorFactoryBuilder().Create())
        {
        }

        private XmlSitemapProviderFactoryDecoratorBuilder(
            IXmlSitemapProviderDecoratorFactory xmlSitemapProviderDecoratorFactory
            )
        {
            if (xmlSitemapProviderDecoratorFactory == null)
                throw new ArgumentNullException("xmlSitemapProviderDecoratorFactory");

            this.xmlSitemapProviderDecoratorFactory = xmlSitemapProviderDecoratorFactory;
        }
        private readonly IXmlSitemapProviderDecoratorFactory xmlSitemapProviderDecoratorFactory;

        public IXmlSitemapProviderFactoryDecoratorBuilder WithXmlSitemapProviderDecoratorFactory(IXmlSitemapProviderDecoratorFactory xmlSitemapProviderDecoratorFactory)
        {
            return new XmlSitemapProviderFactoryDecoratorBuilder(xmlSitemapProviderDecoratorFactory);
        }

        public IXmlSitemapProviderFactory Create(IXmlSitemapProviderFactory xmlSitemapProviderFactory)
        {
            return new XmlSitemapProviderFactoryDecorator(xmlSitemapProviderFactory, this.xmlSitemapProviderDecoratorFactory);
        }
    }
}
