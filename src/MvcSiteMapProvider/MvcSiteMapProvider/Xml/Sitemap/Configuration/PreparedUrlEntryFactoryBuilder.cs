using System;
using System.Collections.Generic;
using System.Linq;
using MvcSiteMapProvider.Globalization;

namespace MvcSiteMapProvider.Xml.Sitemap.Configuration
{
    public class PreparedUrlEntryFactoryBuilder
        : IPreparedUrlEntryFactoryBuilder
    {
        public PreparedUrlEntryFactoryBuilder()
            : this(xmlSitemapUrlResolverFactory: new XmlSitemapUrlResolverFactoryBuilder().Create(), cultureContextFactory: new CultureContextFactory())
        {
        }

        private PreparedUrlEntryFactoryBuilder(
            IXmlSitemapUrlResolverFactory xmlSitemapUrlResolverFactory,
            ICultureContextFactory cultureContextFactory
            )
        {
            if (xmlSitemapUrlResolverFactory == null)
                throw new ArgumentNullException("xmlSitemapUrlResolverFactory");
            if (cultureContextFactory == null)
                throw new ArgumentNullException("cultureContextFactory");

            this.xmlSitemapUrlResolverFactory = xmlSitemapUrlResolverFactory;
            this.cultureContextFactory = cultureContextFactory;
        }
        private readonly IXmlSitemapUrlResolverFactory xmlSitemapUrlResolverFactory;
        private readonly ICultureContextFactory cultureContextFactory;

        public IPreparedUrlEntryFactoryBuilder WithXmlSitemapUrlResolverFactory(IXmlSitemapUrlResolverFactory xmlSitemapUrlResolverFactory)
        {
            return new PreparedUrlEntryFactoryBuilder(xmlSitemapUrlResolverFactory, this.cultureContextFactory);
        }

        public IPreparedUrlEntryFactoryBuilder WithCultureContextFactory(ICultureContextFactory cultureContextFactory)
        {
            return new PreparedUrlEntryFactoryBuilder(this.xmlSitemapUrlResolverFactory, cultureContextFactory);
        }

        public IPreparedUrlEntryFactory Create()
        {
            return new PreparedUrlEntryFactory(this.xmlSitemapUrlResolverFactory, this.cultureContextFactory);
        }
    }
}
