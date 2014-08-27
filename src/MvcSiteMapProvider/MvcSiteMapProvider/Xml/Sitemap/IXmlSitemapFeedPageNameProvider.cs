using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    public interface IXmlSitemapFeedPageNameProvider
    {
        string DefaultFeedRootPageName { get; }
        string DefaultFeedPageName { get; }
        string NamedFeedRootPageName { get; }
        string NamedFeedPageName { get; }
        string GetPageName(string feedName, int page);
    }
}
