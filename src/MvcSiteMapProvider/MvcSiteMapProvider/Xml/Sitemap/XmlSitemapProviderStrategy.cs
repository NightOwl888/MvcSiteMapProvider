using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    public class XmlSitemapProviderStrategy
        : IXmlSitemapProviderStrategy
    {
        public XmlSitemapProviderStrategy(
            IXmlSitemapProviderFactory xmlSitemapProviderFactory,
            IXmlSitemapProviderTypeStrategy typeStrategy
            )
        {
            if (xmlSitemapProviderFactory == null)
                throw new ArgumentNullException("xmlSitemapProviderFactory");
            if (typeStrategy == null)
                throw new ArgumentNullException("typeStrategy");

            this.xmlSitemapProviderFactory = xmlSitemapProviderFactory;
            this.typeStrategy = typeStrategy;
        }
        private readonly IXmlSitemapProviderFactory xmlSitemapProviderFactory;
        private readonly IXmlSitemapProviderTypeStrategy typeStrategy;


        public IEnumerable<IXmlSitemapProvider> GetProviders(string feedName)
        {
            var result = new List<IXmlSitemapProvider>();
            var providerTypes = this.typeStrategy.GetTypes(feedName);
            foreach (var providerType in providerTypes)
            {
                result.Add(this.xmlSitemapProviderFactory.Create(providerType));
            }

            return result;
        }

        public void ReleaseProviders(IEnumerable<IXmlSitemapProvider> providers)
        {
            foreach (var provider in providers)
            {
                this.xmlSitemapProviderFactory.Release(provider);
            }
        }
    }
}
