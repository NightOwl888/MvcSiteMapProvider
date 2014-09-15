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
            : this(xmlSitemapUrlResolver: new XmlSitemapUrlResolverBuilder().Create(), xmlSitemapFeedPageNameProvider: new XmlSitemapFeedPageNameProvider())
        {
        }

        private XmlSitemapFeedUrlResolverBuilder(
            IXmlSitemapUrlResolver xmlSitemapUrlResolver,
            IXmlSitemapFeedPageNameProvider xmlSitemapFeedPageNameProvider
            )
        {
            if (xmlSitemapUrlResolver == null)
                throw new ArgumentNullException("xmlSitemapUrlResolver");
            if (xmlSitemapFeedPageNameProvider == null)
                throw new ArgumentNullException("xmlSitemapFeedPageNameProvider");

            this.xmlSitemapUrlResolver = xmlSitemapUrlResolver;
            this.xmlSitemapFeedPageNameProvider = xmlSitemapFeedPageNameProvider;
        }
        private readonly IXmlSitemapUrlResolver xmlSitemapUrlResolver;
        private readonly IXmlSitemapFeedPageNameProvider xmlSitemapFeedPageNameProvider;

        public IXmlSitemapFeedUrlResolverBuilder WithXmlSitemapUrlResolver(IXmlSitemapUrlResolver xmlSitemapUrlResolver)
        {
            return new XmlSitemapFeedUrlResolverBuilder(xmlSitemapUrlResolver, this.xmlSitemapFeedPageNameProvider);
        }

        public IXmlSitemapFeedUrlResolverBuilder WithXmlSitemapFeedPageNameProvider(IXmlSitemapFeedPageNameProvider xmlSitemapFeedPageNameProvider)
        {
            return new XmlSitemapFeedUrlResolverBuilder(this.xmlSitemapUrlResolver, xmlSitemapFeedPageNameProvider);
        }

        public IXmlSitemapFeedUrlResolver Create()
        {
            return new XmlSitemapFeedUrlResolver(this.xmlSitemapUrlResolver, this.xmlSitemapFeedPageNameProvider);
        }
    }
}
