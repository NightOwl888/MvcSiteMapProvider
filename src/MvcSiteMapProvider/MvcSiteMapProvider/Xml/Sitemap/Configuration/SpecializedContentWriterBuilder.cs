using System;
using System.Collections.Generic;
using MvcSiteMapProvider.Globalization;
using MvcSiteMapProvider.Xml.Sitemap.Specialized;

namespace MvcSiteMapProvider.Xml.Sitemap.Configuration
{
    public class SpecializedContentWriterBuilder
        : ISpecializedContentWriterBuilder
    {
        public SpecializedContentWriterBuilder()
            : this(
                specializedContentXmlWriterFactoryStrategy: new SpecializedContentXmlWriterFactoryStrategyBuilder().Create(), 
                xmlSitemapUrlResolverFactory: new XmlSitemapUrlResolverFactoryBuilder().Create(), 
                cultureContextFactory: new CultureContextFactory())
        {
        }

        private SpecializedContentWriterBuilder(
            ISpecializedContentXmlWriterFactoryStrategy specializedContentXmlWriterFactoryStrategy,
            IXmlSitemapUrlResolverFactory xmlSitemapUrlResolverFactory,
            ICultureContextFactory cultureContextFactory
            )
        {
            if (specializedContentXmlWriterFactoryStrategy == null)
                throw new ArgumentNullException("specializedContentXmlWriterFactoryStrategy");
            if (xmlSitemapUrlResolverFactory == null)
                throw new ArgumentNullException("xmlSitemapUrlResolverFactory");
            if (cultureContextFactory == null)
                throw new ArgumentNullException("cultureContextFactory");

            this.specializedContentXmlWriterFactoryStrategy = specializedContentXmlWriterFactoryStrategy;
            this.xmlSitemapUrlResolverFactory = xmlSitemapUrlResolverFactory;
            this.cultureContextFactory = cultureContextFactory;
        }
        private readonly ISpecializedContentXmlWriterFactoryStrategy specializedContentXmlWriterFactoryStrategy;
        private readonly IXmlSitemapUrlResolverFactory xmlSitemapUrlResolverFactory;
        private readonly ICultureContextFactory cultureContextFactory;

        public ISpecializedContentWriterBuilder WithSpecializedContentXmlWriterFactoryStrategy(ISpecializedContentXmlWriterFactoryStrategy specializedContentXmlWriterFactoryStrategy)
        {
            return new SpecializedContentWriterBuilder(specializedContentXmlWriterFactoryStrategy, this.xmlSitemapUrlResolverFactory, this.cultureContextFactory);
        }

        public ISpecializedContentWriterBuilder WithXmlSitemapUrlResolverFactory(IXmlSitemapUrlResolverFactory xmlSitemapUrlResolverFactory)
        {
            return new SpecializedContentWriterBuilder(this.specializedContentXmlWriterFactoryStrategy, xmlSitemapUrlResolverFactory, this.cultureContextFactory);
        }

        public ISpecializedContentWriterBuilder WithCultureContextFactory(ICultureContextFactory cultureContextFactory)
        {
            return new SpecializedContentWriterBuilder(this.specializedContentXmlWriterFactoryStrategy, this.xmlSitemapUrlResolverFactory, cultureContextFactory);
        }

        public ISpecializedContentWriter Create()
        {
            return new SpecializedContentWriter(this.specializedContentXmlWriterFactoryStrategy, this.xmlSitemapUrlResolverFactory, this.cultureContextFactory);
        }
    }
}
