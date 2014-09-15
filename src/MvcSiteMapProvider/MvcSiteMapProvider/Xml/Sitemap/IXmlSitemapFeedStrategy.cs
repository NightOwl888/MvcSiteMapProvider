using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    public interface IXmlSitemapFeedStrategy
    {
        IEnumerable<IXmlSitemapFeed> XmlSitemapFeeds { get; }
        IXmlSitemapFeed GetFeed(string name);
    }
}
