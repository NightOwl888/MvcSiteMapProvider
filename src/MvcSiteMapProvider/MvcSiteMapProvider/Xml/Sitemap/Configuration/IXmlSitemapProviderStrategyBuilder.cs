using System;
using System.Collections.Generic;
using System.Linq;
using MvcSiteMapProvider.ComponentModel;

namespace MvcSiteMapProvider.Xml.Sitemap.Configuration
{
    public interface IXmlSitemapProviderStrategyBuilder
        : IFluentInterface
    {
        IXmlSitemapProviderStrategyBuilder WithXmlSitemapProviderFactory(IXmlSitemapProviderFactory xmlSitemapProviderFactory);

        IXmlSitemapProviderStrategyBuilder WithXmlSitemapProviderTypeStrategy(IXmlSitemapProviderTypeStrategy xmlSitemapProviderTypeStrategy);

        IXmlSitemapProviderStrategy Create();
    }
}
