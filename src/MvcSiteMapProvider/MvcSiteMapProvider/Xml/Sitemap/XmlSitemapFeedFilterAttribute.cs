using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    public class XmlSitemapFeedFilterAttribute
        : Attribute
    {
        public XmlSitemapFeedFilterAttribute(string feedName)
        {
            if (string.IsNullOrEmpty(feedName))
                throw new ArgumentNullException("feedName");
            this.feedName = feedName;
        }
        private readonly string feedName;

        public string FilterString { get { return this.feedName; } }
    }
}
