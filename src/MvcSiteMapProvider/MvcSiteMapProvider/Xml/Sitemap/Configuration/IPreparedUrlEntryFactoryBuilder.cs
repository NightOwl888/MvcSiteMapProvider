using System;
using MvcSiteMapProvider.ComponentModel;
using MvcSiteMapProvider.Globalization;

namespace MvcSiteMapProvider.Xml.Sitemap.Configuration
{
    public interface IPreparedUrlEntryFactoryBuilder
        : IFluentInterface
    {
        IPreparedUrlEntryFactoryBuilder WithXmlSitemapUrlResolverFactory(IXmlSitemapUrlResolverFactory xmlSitemapUrlResolverFactory);

        IPreparedUrlEntryFactoryBuilder WithCultureContextFactory(ICultureContextFactory cultureContextFactory);

        IPreparedUrlEntryFactory Create();
    }
}
