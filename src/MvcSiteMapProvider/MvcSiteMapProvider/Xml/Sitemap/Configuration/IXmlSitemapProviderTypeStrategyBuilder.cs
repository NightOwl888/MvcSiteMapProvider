using System;
using MvcSiteMapProvider.Reflection;
using MvcSiteMapProvider.ComponentModel;

namespace MvcSiteMapProvider.Xml.Sitemap.Configuration
{
    public interface IXmlSitemapProviderTypeStrategyBuilder
        : IFluentInterface
    {
        IXmlSitemapProviderTypeStrategyBuilder WithAssemblyProvider(IAttributeAssemblyProvider assemblyProvider);

        IXmlSitemapProviderTypeStrategy Create();
    }
}
