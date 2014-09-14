using System;
using MvcSiteMapProvider.Caching;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    public class XmlSitemapProviderFactoryDecorator
        : IXmlSitemapProviderFactory
    {
        public XmlSitemapProviderFactoryDecorator(
            IXmlSitemapProviderFactory xmlSiteMapProviderFactory,
            IXmlSitemapProviderDecoratorFactory decoratorFactory
            )
        {
            if (xmlSiteMapProviderFactory == null)
                throw new ArgumentNullException("xmlSiteMapProviderFactory");
            if (decoratorFactory == null)
                throw new ArgumentNullException("decoratorFactory");

            this.innerXmlSiteMapProviderFactory = xmlSiteMapProviderFactory;
            this.decoratorFactory = decoratorFactory;
        }
        private readonly IXmlSitemapProviderFactory innerXmlSiteMapProviderFactory;
        private readonly IXmlSitemapProviderDecoratorFactory decoratorFactory;

        public IXmlSitemapProvider Create(Type providerType)
        {
            var provider = this.innerXmlSiteMapProviderFactory.Create(providerType);
            if (provider != null)
            {
                provider = this.decoratorFactory.Create(provider);
            }
            return provider;
        }

        public void Release(IXmlSitemapProvider xmlSitemapProvider)
        {
            this.innerXmlSiteMapProviderFactory.Release(xmlSitemapProvider);
        }
    }
}
