using System;
using MvcSiteMapProvider.Caching;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    public class XmlSitemapProviderFactoryDecorator
        : IXmlSitemapProviderFactory
    {
        public XmlSitemapProviderFactoryDecorator(
            IXmlSitemapProviderFactory xmlSitemapProviderFactory,
            IXmlSitemapProviderDecoratorFactory decoratorFactory
            )
        {
            if (xmlSitemapProviderFactory == null)
                throw new ArgumentNullException("xmlSitemapProviderFactory");
            if (decoratorFactory == null)
                throw new ArgumentNullException("decoratorFactory");

            this.innerXmlSitemapProviderFactory = xmlSitemapProviderFactory;
            this.decoratorFactory = decoratorFactory;
        }
        private readonly IXmlSitemapProviderFactory innerXmlSitemapProviderFactory;
        private readonly IXmlSitemapProviderDecoratorFactory decoratorFactory;

        public IXmlSitemapProvider Create(Type providerType)
        {
            var provider = this.innerXmlSitemapProviderFactory.Create(providerType);
            if (provider != null)
            {
                provider = this.decoratorFactory.Create(provider);
            }
            return provider;
        }

        public void Release(IXmlSitemapProvider xmlSitemapProvider)
        {
            this.innerXmlSitemapProviderFactory.Release(xmlSitemapProvider);
        }
    }
}
