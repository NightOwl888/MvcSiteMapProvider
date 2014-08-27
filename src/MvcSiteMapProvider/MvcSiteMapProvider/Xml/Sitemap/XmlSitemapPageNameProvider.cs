using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    public class XmlSitemapPageNameProvider
        : IXmlSitemapPageNameProvider
    {
        public XmlSitemapPageNameProvider()
            : this(defaultFeedRootPageName: "sitemap.xml", defaultFeedPageName: "sitemap-{page}.xml", namedFeedRootPageName: "{feedName}-sitemap.xml", namedFeedPageName: "{feedName}-sitemap-{page}.xml")
        {
        }

        public XmlSitemapPageNameProvider(string defaultFeedRootPageName, string defaultFeedPageName)
            : this(defaultFeedRootPageName: defaultFeedRootPageName, defaultFeedPageName: defaultFeedPageName, namedFeedRootPageName: "{feedName}-sitemap.xml", namedFeedPageName: "{feedName}-sitemap-{page}.xml")
        {
        }

        public XmlSitemapPageNameProvider(string defaultFeedRootPageName, string defaultFeedPageName, string namedFeedRootPageName, string namedFeedPageName)
        {
            if (string.IsNullOrEmpty(defaultFeedRootPageName))
                throw new ArgumentNullException("defaultFeedRootPageName");
            if (string.IsNullOrEmpty(defaultFeedPageName))
                throw new ArgumentNullException("defaultFeedPageName");
            if (string.IsNullOrEmpty(namedFeedRootPageName))
                throw new ArgumentNullException("namedFeedRootPageName");
            if (string.IsNullOrEmpty(namedFeedPageName))
                throw new ArgumentNullException("namedFeedPageName");

            this.defaultFeedRootPageName = defaultFeedRootPageName;
            this.defaultFeedPageName = defaultFeedPageName;
            this.namedFeedRootPageName = namedFeedRootPageName;
            this.namedFeedPageName = namedFeedPageName;
        }
        private readonly string defaultFeedRootPageName;
        private readonly string defaultFeedPageName;
        private readonly string namedFeedRootPageName;
        private readonly string namedFeedPageName;

        public string DefaultFeedRootPageName
        {
            get { return this.defaultFeedRootPageName; }
        }

        public string DefaultFeedPageName
        {
            get { return this.defaultFeedPageName; }
        }

        public string NamedFeedRootPageName
        {
            get { return this.namedFeedRootPageName; }
        }

        public string NamedFeedPageName
        {
            get { return this.namedFeedPageName; }
        }

        // TODO: Make this into XmlSitemapPageNameResolver service that takes this class and XmlSiteMapUrlResolver as dependencies
        public string GetPageName(int page, string feedName)
        {
            string result;
            bool isRootPage = (page == 0);
            if (feedName == "default")
            {
                result = isRootPage ? this.DefaultFeedRootPageName : this.DefaultFeedPageName;
            }
            else
            {
                result = isRootPage ? this.NamedFeedRootPageName : this.NamedFeedPageName;
            }
            result = result.Replace("{page}", page.ToString()).Replace("{feedName}", feedName);

            return result;
        }
    }
}
