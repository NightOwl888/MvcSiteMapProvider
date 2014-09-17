using System;
using MvcSiteMapProvider.Globalization;
using MvcSiteMapProvider.Xml.Sitemap.Specialized;

namespace MvcSiteMapProvider.Xml.Sitemap.Configuration
{
    public interface ISpecializedContentWriterBuilder
    {
        ISpecializedContentWriterBuilder WithSpecializedContentXmlWriterFactoryStrategy(ISpecializedContentXmlWriterFactoryStrategy specializedContentXmlWriterFactoryStrategy);

        ISpecializedContentWriterBuilder WithXmlSitemapUrlResolverFactory(IXmlSitemapUrlResolverFactory xmlSitemapUrlResolverFactory);

        ISpecializedContentWriterBuilder WithCultureContextFactory(ICultureContextFactory cultureContextFactory);

        ISpecializedContentWriter Create();
    }
}
