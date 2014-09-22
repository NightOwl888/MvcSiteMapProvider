using System;
using MvcSiteMapProvider.Reflection;

namespace MvcSiteMapProvider.Xml.Sitemap.Configuration
{
    public class XmlSitemapProviderTypeStrategyBuilder
        : IXmlSitemapProviderTypeStrategyBuilder
    {
        public XmlSitemapProviderTypeStrategyBuilder()
            : this(assemblyProviderFactory: new AssemblyProviderFactoryBuilder().Create())
        {
        }

        private XmlSitemapProviderTypeStrategyBuilder(
            IAssemblyProviderFactory assemblyProviderFactory
            )
        {
            if (assemblyProviderFactory == null)
                throw new ArgumentNullException("assemblyProviderFactory");

            this.assemblyProviderFactory = assemblyProviderFactory;
        }
        private readonly IAssemblyProviderFactory assemblyProviderFactory;

        public IXmlSitemapProviderTypeStrategyBuilder WithAssemblyProviderFactory(IAssemblyProviderFactory assemblyProviderFactory)
        {
            return new XmlSitemapProviderTypeStrategyBuilder(assemblyProviderFactory);
        }

        public IXmlSitemapProviderTypeStrategy Create()
        {
            return new XmlSitemapProviderTypeStrategy(this.assemblyProviderFactory);
        }
    }
}
