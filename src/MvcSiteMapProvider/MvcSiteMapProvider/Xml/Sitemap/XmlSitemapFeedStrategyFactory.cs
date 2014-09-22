using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    public class XmlSitemapFeedStrategyFactory
        : IXmlSitemapFeedStrategyFactory
    {
        public XmlSitemapFeedStrategyFactory(
            IXmlSitemapFeed[] xmlSitemapFeeds
            )
        {
            if (xmlSitemapFeeds == null)
                throw new ArgumentNullException("xmlSitemapFeeds");

            this.xmlSitemapFeeds = xmlSitemapFeeds;
            this.syncRoot = new object();
        }
        private readonly IXmlSitemapFeed[] xmlSitemapFeeds;
        private readonly object syncRoot;

        public IXmlSitemapFeedStrategy Create()
        {
            return new XmlSitemapFeedStrategy(this.xmlSitemapFeeds);
        }

        public void Release(IXmlSitemapFeedStrategy xmlSitemapFeedStrategy)
        {
            var disposable = xmlSitemapFeedStrategy as IDisposable;
            if (disposable != null)
            {
                lock (syncRoot)
                {
                    disposable.Dispose();
                }
            }
        }
    }
}
