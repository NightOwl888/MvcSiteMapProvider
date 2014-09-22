using System;
using MvcSiteMapProvider.Reflection;
using MvcSiteMapProvider.ComponentModel;

namespace MvcSiteMapProvider.Xml.Sitemap.Configuration
{
    public interface IXmlSitemapProviderTypeStrategyBuilder
        : IFluentInterface
    {
        IXmlSitemapProviderTypeStrategyBuilder WithAssemblyProviderFactory(IAssemblyProviderFactory assemblyProviderFactory);

        IXmlSitemapProviderTypeStrategy Create();
    }
}
