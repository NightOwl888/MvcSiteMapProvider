using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml;
using MvcSiteMapProvider.IO;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    public class XmlSitemapFileGenerator 
        : IXmlSitemapFileGenerator
    {
        public XmlSitemapFileGenerator()
            : this(
                streamFactory: new FileStreamFactory(),
                xmlSitemapFeedPageNameProvider: new XmlSitemapFeedPageNameProvider()
            )
        {
        }

        public XmlSitemapFileGenerator(
            IXmlSitemapFeedPageNameProvider xmlSitemapFeedPageNameProvider
            )
            : this(
                streamFactory: new FileStreamFactory(),
                xmlSitemapFeedPageNameProvider: xmlSitemapFeedPageNameProvider
            )
        {
        }

        public XmlSitemapFileGenerator(
            IStreamFactory streamFactory
            ) 
            : this(
                streamFactory: streamFactory, 
                xmlSitemapFeedPageNameProvider: new XmlSitemapFeedPageNameProvider()
            )
        {
        }

        public XmlSitemapFileGenerator(
            IStreamFactory streamFactory,
            IXmlSitemapFeedPageNameProvider xmlSitemapFeedPageNameProvider
            )
        {
            if (streamFactory == null)
                throw new ArgumentNullException("streamFactory");
            if (xmlSitemapFeedPageNameProvider == null)
                throw new ArgumentNullException("xmlSitemapFeedPageNameProvider");

            this.streamFactory = streamFactory;
            this.xmlSitemapFeedPageNameProvider = xmlSitemapFeedPageNameProvider;
        }
        private readonly IStreamFactory streamFactory;
        private readonly IXmlSitemapFeedPageNameProvider xmlSitemapFeedPageNameProvider;

        public virtual void GenerateFiles(IXmlSitemapFeedStrategy feedStrategy)
        {
            this.GenerateFiles(feedStrategy, string.Empty);
        }

        public void GenerateFiles(IXmlSitemapFeedStrategy feedStrategy, XmlWriterSettings settings)
        {
            this.GenerateFiles(feedStrategy, string.Empty, settings);
        }

        public virtual void GenerateFiles(IXmlSitemapFeedStrategy feedStrategy, string feedName)
        {
            this.GenerateFiles(feedStrategy, feedName, (XmlWriterSettings)null);
        }

        public void GenerateFiles(IXmlSitemapFeedStrategy feedStrategy, string feedName, XmlWriterSettings settings)
        {
            var outputDirectory = AppDomain.CurrentDomain.BaseDirectory;
            this.GenerateFiles(feedStrategy, feedName, outputDirectory, settings);
        }

        public virtual void GenerateFiles(IXmlSitemapFeedStrategy feedStrategy, string feedName, string outputDirectory)
        {
            this.GenerateFiles(feedStrategy, feedName, outputDirectory, null);
        }

        public void GenerateFiles(IXmlSitemapFeedStrategy feedStrategy, string feedName, string outputDirectory, XmlWriterSettings settings)
        {
            if (feedStrategy == null)
                throw new ArgumentNullException("feedStrategy");

            if (!string.IsNullOrEmpty(feedName))
            {
                var feed = feedStrategy.GetFeed(feedName);
                this.GenerateFiles(feed, outputDirectory, settings);
            }
            else
            {
                // Generate all feeds if no name is provided.
                foreach (var feed in feedStrategy.XmlSitemapFeeds)
                {
                    this.GenerateFiles(feed, settings);
                }
            }
        }

        public virtual void GenerateFiles(IXmlSitemapFeed feed)
        {
            var outputDirectory = AppDomain.CurrentDomain.BaseDirectory;
            this.GenerateFiles(feed, outputDirectory);
        }

        public void GenerateFiles(IXmlSitemapFeed feed, XmlWriterSettings settings)
        {
            var outputDirectory = AppDomain.CurrentDomain.BaseDirectory;
            this.GenerateFiles(feed, outputDirectory, settings);
        }

        public virtual void GenerateFiles(IXmlSitemapFeed feed, string outputDirectory)
        {
            this.GenerateFiles(feed, outputDirectory, null);
        }

        public void GenerateFiles(IXmlSitemapFeed feed, string outputDirectory, XmlWriterSettings settings)
        {
            if (feed == null)
                throw new ArgumentNullException("feed");
            if (string.IsNullOrEmpty(outputDirectory))
                throw new ArgumentNullException("outputDirectory");

            var pageData = feed.GetPageData();

            if (pageData.Pages.Count() > 1)
            {
                // Write the index
                this.WritePage(feed, 0, outputDirectory, settings);

                // Write the content pages
                foreach (var info in pageData.Pages)
                {
                    this.WritePage(feed, info.Page, outputDirectory, settings);
                }
            }
            else
            {
                this.WritePage(feed, 0, outputDirectory, settings);
            }
        }

        protected virtual void WritePage(IXmlSitemapFeed feed, int page, string outputDirectory, XmlWriterSettings settings)
        {
            var fileName = this.xmlSitemapFeedPageNameProvider.GetPageName(feed.Name, page);
            var filePath = Path.Combine(outputDirectory, fileName);

            using (var stream = this.streamFactory.CreateWriteable(filePath))
            {
                feed.WritePage(page, stream, settings);
            }
        }
    }
}
