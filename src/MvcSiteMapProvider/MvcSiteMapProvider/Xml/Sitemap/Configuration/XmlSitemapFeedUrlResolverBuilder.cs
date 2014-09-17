using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Xml.Sitemap.Configuration
{
    public class XmlSitemapFeedUrlResolverBuilder
        : IXmlSitemapFeedUrlResolverBuilder
    {
        public XmlSitemapFeedUrlResolverBuilder()
            : this(xmlSitemapUrlResolverFactory: new XmlSitemapUrlResolverFactoryBuilder().Create(), xmlSitemapFeedPageNameProvider: new XmlSitemapFeedPageNameProvider())
        {
        }

        private XmlSitemapFeedUrlResolverBuilder(
            IXmlSitemapUrlResolverFactory xmlSitemapUrlResolverFactory,
            IXmlSitemapFeedPageNameProvider xmlSitemapFeedPageNameProvider
            )
        {
            if (xmlSitemapUrlResolverFactory == null)
                throw new ArgumentNullException("xmlSitemapUrlResolverFactory");
            if (xmlSitemapFeedPageNameProvider == null)
                throw new ArgumentNullException("xmlSitemapFeedPageNameProvider");

            this.xmlSitemapUrlResolverFactory = xmlSitemapUrlResolverFactory;
            this.xmlSitemapFeedPageNameProvider = xmlSitemapFeedPageNameProvider;
        }
        private readonly IXmlSitemapUrlResolverFactory xmlSitemapUrlResolverFactory;
        private readonly IXmlSitemapFeedPageNameProvider xmlSitemapFeedPageNameProvider;

        public IXmlSitemapFeedUrlResolverBuilder WithXmlSitemapUrlResolverFactory(IXmlSitemapUrlResolverFactory xmlSitemapUrlResolverFactory)
        {
            return new XmlSitemapFeedUrlResolverBuilder(xmlSitemapUrlResolverFactory, this.xmlSitemapFeedPageNameProvider);
        }

        public IXmlSitemapFeedUrlResolverBuilder WithXmlSitemapFeedPageNameProvider(IXmlSitemapFeedPageNameProvider xmlSitemapFeedPageNameProvider)
        {
            return new XmlSitemapFeedUrlResolverBuilder(this.xmlSitemapUrlResolverFactory, xmlSitemapFeedPageNameProvider);
        }

        public IXmlSitemapFeedUrlResolver Create()
        {
            return new XmlSitemapFeedUrlResolver(this.xmlSitemapUrlResolverFactory, this.xmlSitemapFeedPageNameProvider);
        }
    }
}
