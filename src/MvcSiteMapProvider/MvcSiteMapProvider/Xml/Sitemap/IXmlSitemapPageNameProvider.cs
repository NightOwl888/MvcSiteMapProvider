using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    public interface IXmlSitemapPageNameProvider
    {
        string DefaultFeedRootPageName { get; }
        string DefaultFeedPageName { get; }
        string NamedFeedRootPageName { get; }
        string NamedFeedPageName { get; }
        string GetPageName(int page, string feedName);
    }
}
