using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    // This will be injected into an xmlsitemap actionresult class and a class for dealing with file generation
    public class XmlSitemapFeedStrategy
        : IXmlSitemapFeedStrategy
    {
        public XmlSitemapFeedStrategy(
            IXmlSitemapFeed[] xmlSitemapFeeds
            )
        {
            if (xmlSitemapFeeds == null)
                throw new ArgumentNullException("xmlSitemapFeeds");
            this.xmlSitemapFeeds = xmlSitemapFeeds;
        }
        private readonly IXmlSitemapFeed[] xmlSitemapFeeds;

        public IEnumerable<IXmlSitemapFeed> XmlSitemapFeeds { get { return this.xmlSitemapFeeds; } }

        public IXmlSitemapFeed GetFeed(string feedName)
        {
            // NOTE: We are intentionally returning null in the case where the name is not valid.
            var xmlSitemapFeed = this.xmlSitemapFeeds.Where(feed => feed.Name == feedName).FirstOrDefault();
            if (xmlSitemapFeed == null)
            {
                // TODO: Throw exception that the feed was not registered. Make sure this exception is a specialized type
                // that can be caught to return 404 by the controller.
            }
            return xmlSitemapFeed;
        }
    }
}
