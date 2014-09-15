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
            : this(xmlSitemapUrlResolver: new XmlSitemapUrlResolverBuilder().Create(), cultureContextFactory: new CultureContextFactory())
        {
        }

        private PreparedUrlEntryFactoryBuilder(
            IXmlSitemapUrlResolver xmlSitemapUrlResolver,
            ICultureContextFactory cultureContextFactory
            )
        {
            if (xmlSitemapUrlResolver == null)
                throw new ArgumentNullException("xmlSitemapUrlResolver");
            if (cultureContextFactory == null)
                throw new ArgumentNullException("cultureContextFactory");

            this.xmlSitemapUrlResolver = xmlSitemapUrlResolver;
            this.cultureContextFactory = cultureContextFactory;
        }
        private readonly IXmlSitemapUrlResolver xmlSitemapUrlResolver;
        private readonly ICultureContextFactory cultureContextFactory;

        public IPreparedUrlEntryFactoryBuilder WithXmlSitemapUrlResolver(IXmlSitemapUrlResolver xmlSitemapUrlResolver)
        {
            return new PreparedUrlEntryFactoryBuilder(xmlSitemapUrlResolver, this.cultureContextFactory);
        }

        public IPreparedUrlEntryFactoryBuilder WithCultureContextFactory(ICultureContextFactory cultureContextFactory)
        {
            return new PreparedUrlEntryFactoryBuilder(this.xmlSitemapUrlResolver, cultureContextFactory);
        }

        public IPreparedUrlEntryFactory Create()
        {
            return new PreparedUrlEntryFactory(this.xmlSitemapUrlResolver, this.cultureContextFactory);
        }
    }
}
