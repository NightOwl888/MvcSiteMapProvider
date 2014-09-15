using System;
using MvcSiteMapProvider.Reflection;

namespace MvcSiteMapProvider.Xml.Sitemap.Configuration
{
    public class XmlSitemapProviderTypeStrategyBuilder
        : IXmlSitemapProviderTypeStrategyBuilder
    {
        public XmlSitemapProviderTypeStrategyBuilder()
            : this(assemblyProvider: new AttributeAssemblyProviderBuilder().Create())
        {
        }

        private XmlSitemapProviderTypeStrategyBuilder(
            IAttributeAssemblyProvider assemblyProvider
            )
        {
            if (assemblyProvider == null)
                throw new ArgumentNullException("assemblyProvider");

            this.assemblyProvider = assemblyProvider;
        }
        private readonly IAttributeAssemblyProvider assemblyProvider;

        public IXmlSitemapProviderTypeStrategyBuilder WithAssemblyProvider(IAttributeAssemblyProvider assemblyProvider)
        {
            return new XmlSitemapProviderTypeStrategyBuilder(assemblyProvider);
        }

        public IXmlSitemapProviderTypeStrategy Create()
        {
            return new XmlSitemapProviderTypeStrategy(this.assemblyProvider);
        }
    }
}
