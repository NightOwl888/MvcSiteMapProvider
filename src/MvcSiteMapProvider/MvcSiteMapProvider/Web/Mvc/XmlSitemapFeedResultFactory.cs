using System;
using System.Web.Mvc;
using MvcSiteMapProvider.IO;
using MvcSiteMapProvider.Xml.Sitemap;

namespace MvcSiteMapProvider.Web.Mvc
{
    public class XmlSitemapFeedResultFactory
        : IXmlSitemapFeedResultFactory
    {
        public XmlSitemapFeedResultFactory(
            IXmlSitemapFeedStrategy xmlSitemapFeedStrategy,
            IHttpResponseStreamCompressor responseStreamCompressor
            )
        {
            if (xmlSitemapFeedStrategy == null)
                throw new ArgumentNullException("xmlSitemapFeedStrategy");
            if (responseStreamCompressor == null)
                throw new ArgumentNullException("responseStreamCompressor");

            this.xmlSitemapFeedStrategy = xmlSitemapFeedStrategy;
            this.responseStreamCompressor = responseStreamCompressor;
        }
        private readonly IXmlSitemapFeedStrategy xmlSitemapFeedStrategy;
        private readonly IHttpResponseStreamCompressor responseStreamCompressor;

        public ActionResult Create(int page)
        {
            return this.Create(page, "default");
        }

        public ActionResult Create(int page, string feedName)
        {
            // TODO: Add a debug mode when debugging is enabled on the server that doesn't return 404
            // Need to add a try catch here and return 404 if not in debug mode

            var xmlSitemapFeed = this.xmlSitemapFeedStrategy.GetFeed(feedName);
            if (xmlSitemapFeed != null)
            {
                return new XmlSitemapFeedResult(page, xmlSitemapFeed, this.responseStreamCompressor);
            }

            // If the sitemap name is not valid, return 404.
            return new HttpNotFoundResult();
        }
    }
}
