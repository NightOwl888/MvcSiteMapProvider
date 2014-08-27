using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    /// <summary>
    /// Contract for class that resolves URLs of the feeds (not the URLs in the feed content).
    /// </summary>
    public interface IXmlSitemapFeedUrlResolver
    {
        string ResolveFeedUrl(string feedName, int page);
    }
}
