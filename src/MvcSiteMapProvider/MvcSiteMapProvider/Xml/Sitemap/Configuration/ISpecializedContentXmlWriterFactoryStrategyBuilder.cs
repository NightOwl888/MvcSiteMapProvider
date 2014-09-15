using System;
using System.Collections.Generic;
using MvcSiteMapProvider.Xml.Sitemap.Specialized;
using MvcSiteMapProvider.ComponentModel;

namespace MvcSiteMapProvider.Xml.Sitemap.Configuration
{
    public interface ISpecializedContentXmlWriterFactoryStrategyBuilder
        : IFluentInterface
    {
        ISpecializedContentXmlWriterFactoryStrategyBuilder AddSpecializedContentXmlWriterFactory(ISpecializedContentXmlWriterFactory specializedContentXmlWriterFactory);

        ISpecializedContentXmlWriterFactoryStrategyBuilder AddSpecializedContentXmlWriterFactories(IEnumerable<ISpecializedContentXmlWriterFactory> specializedContentXmlWriterFactories);

        ISpecializedContentXmlWriterFactoryStrategyBuilder RemoveSpecializedContentXmlWriterFactory(Type contentType);

        ISpecializedContentXmlWriterFactoryStrategyBuilder ClearSpecializedContentXmlWriterFactories();

        ISpecializedContentXmlWriterFactoryStrategy Create();
    }
}
