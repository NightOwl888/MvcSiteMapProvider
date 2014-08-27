using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml;
using System.Web;
using MvcSiteMapProvider.IO;
using MvcSiteMapProvider.Xml.Sitemap.Paging;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    public class SitemapService
        : ISitemapService
    {
        public SitemapService(
            IXmlSitemapPageManager xmlSitemapPageManager,
            IXmlSitemapFeedPageNameProvider xmlSitemapPageNameProvider//,
            //IStreamFactory streamFactory
            )
        {
            if (xmlSitemapPageManager == null)
                throw new ArgumentNullException("xmlSitemapPageManager");
            if (xmlSitemapPageNameProvider == null)
                throw new ArgumentNullException("xmlSitemapPageNameProvider");
            //if (streamFactory == null)
            //    throw new ArgumentNullException("streamFactory");

            this.xmlSitemapPageManager = xmlSitemapPageManager;
            this.xmlSitemapPageNameProvider = xmlSitemapPageNameProvider;
            //this.streamFactory = streamFactory;

            // TODO: Ensure this works in integrated mode.

            // Set the default base directory to the one in the app domain.
            this.BaseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        }
        private readonly IXmlSitemapPageManager xmlSitemapPageManager;
        private readonly IXmlSitemapFeedPageNameProvider xmlSitemapPageNameProvider;
        //private readonly IStreamFactory streamFactory;

        public string BaseDirectory { get; set; }

        public void Execute(int page)
        {
            //var stream = this.streamFactory.Create();
            //try
            //{
            //    // TODO: make XmlWriterFactory to inject here instead of the static method
            //    using (var writer = XmlWriter.Create(stream))
            //    {
            //        this.xmlSitemapPageManager.WritePage(page, writer);
            //        writer.Flush();
            //    }
            //}
            //finally
            //{
            //    this.streamFactory.Release(stream);
            //}
        }

        public void GenerateFiles()
        {
            var feedName = "default";
            var rootPageTemplate = Path.Combine(this.BaseDirectory, this.xmlSitemapPageNameProvider.DefaultFeedRootPageName);
            var pageNameTemplate = Path.Combine(this.BaseDirectory, this.xmlSitemapPageNameProvider.DefaultFeedPageName);
            var pageNumbers = this.xmlSitemapPageManager.GetPageNumbers(feedName);

            if (pageNumbers.Count() > 1)
            {
                // TODO: Create factory to inject file stream

                // write the index
                this.GenerateFirstPageFile(rootPageTemplate.Replace("{page}", "0"), feedName);

                foreach (var page in pageNumbers)
                {
                    using (var stream = new FileStream(pageNameTemplate.Replace("{page}", page.ToString()), FileMode.Create))
                    {
                        using (var writer = XmlWriter.Create(stream))
                        {
                            this.xmlSitemapPageManager.WritePage(writer, feedName, page);
                            writer.Flush();
                        }
                    }
                }
            }
            else
            {
                this.GenerateFirstPageFile(rootPageTemplate.Replace("{page}", "0"), feedName);
            }
        }

        protected void GenerateFirstPageFile(string fileName, string feedName)
        {
            using (var stream = new FileStream(fileName, FileMode.Create))
            {
                using (var writer = XmlWriter.Create(stream))
                {
                    this.xmlSitemapPageManager.WritePage(writer, feedName, 0);
                    writer.Flush();
                }
            }
        }



    }
}
