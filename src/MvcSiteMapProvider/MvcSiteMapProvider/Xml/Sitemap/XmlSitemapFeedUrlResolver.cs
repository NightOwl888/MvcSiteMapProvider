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
            IXmlSitemapUrlResolver urlResolver,
            IXmlSitemapFeedPageNameProvider pageNameProvider
            )
        {
            if (urlResolver == null)
                throw new ArgumentNullException("urlResolver");
            if (pageNameProvider == null)
                throw new ArgumentNullException("pageNameProvider");

            this.urlResolver = urlResolver;
            this.pageNameProvider = pageNameProvider;
        }
        private readonly IXmlSitemapUrlResolver urlResolver;
        private readonly IXmlSitemapFeedPageNameProvider pageNameProvider;

        public string ResolveFeedUrl(string feedName, int page)
        {
            return this.urlResolver.ResolveUrlToAbsolute("~/" + this.pageNameProvider.GetPageName(feedName, page));
        }
    }
}
