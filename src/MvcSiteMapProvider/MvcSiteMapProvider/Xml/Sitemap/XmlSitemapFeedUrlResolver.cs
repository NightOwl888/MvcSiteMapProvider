using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    public class XmlSitemapFeedUrlResolver
        : IXmlSitemapFeedUrlResolver
    {
        public XmlSitemapFeedUrlResolver(
            IXmlSitemapUrlResolverFactory urlResolverFactory,
            IXmlSitemapFeedPageNameProvider pageNameProvider
            )
        {
            if (urlResolverFactory == null)
                throw new ArgumentNullException("urlResolverFactory");
            if (pageNameProvider == null)
                throw new ArgumentNullException("pageNameProvider");

            this.urlResolverFactory = urlResolverFactory;
            this.pageNameProvider = pageNameProvider;
        }
        private readonly IXmlSitemapUrlResolverFactory urlResolverFactory;
        private readonly IXmlSitemapFeedPageNameProvider pageNameProvider;

        public string ResolveFeedUrl(string feedName, int page)
        {
            var urlResolver = this.urlResolverFactory.Create();
            try
            {
                return urlResolver.ResolveUrlToAbsolute("~/" + this.pageNameProvider.GetPageName(feedName, page));
            }
            finally
            {
                this.urlResolverFactory.Release(urlResolver);
            }
        }
    }
}
