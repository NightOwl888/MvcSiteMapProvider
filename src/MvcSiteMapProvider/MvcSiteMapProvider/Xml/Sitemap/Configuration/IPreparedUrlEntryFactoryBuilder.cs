using System;
using MvcSiteMapProvider.ComponentModel;
using MvcSiteMapProvider.Globalization;

namespace MvcSiteMapProvider.Xml.Sitemap.Configuration
{
    public interface IPreparedUrlEntryFactoryBuilder
        : IFluentInterface
    {
        IPreparedUrlEntryFactoryBuilder WithXmlSitemapUrlResolver(IXmlSitemapUrlResolver xmlSitemapUrlResolver);

        IPreparedUrlEntryFactoryBuilder WithCultureContextFactory(ICultureContextFactory cultureContextFactory);

        IPreparedUrlEntryFactory Create();
    }
}
