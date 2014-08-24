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
            ISitemapPageManager sitemapPageManager,
            ISitemapPageNameProvider sitemapPageNameProvider,
            IStreamFactory streamFactory
            )
        {
            if (sitemapPageManager == null)
                throw new ArgumentNullException("sitemapPageManager");
            if (sitemapPageNameProvider == null)
                throw new ArgumentNullException("sitemapPageNameProvider");
            if (streamFactory == null)
                throw new ArgumentNullException("streamFactory");

            this.sitemapPageManager = sitemapPageManager;
            this.sitemapPageNameProvider = sitemapPageNameProvider;
            this.streamFactory = streamFactory;

            // TODO: Ensure this works in integrated mode.

            // Set the default base directory to the one in the app domain.
            this.BaseDirectory = AppDomain.CurrentDomain.BaseDirectory;
        }
        private readonly ISitemapPageManager sitemapPageManager;
        private readonly ISitemapPageNameProvider sitemapPageNameProvider;
        private readonly IStreamFactory streamFactory;

        public string BaseDirectory { get; set; }

        public void Execute(int page)
        {
            var stream = this.streamFactory.Create();
            try
            {
                // TODO: make XmlWriterFactory to inject here instead of the static method
                using (var writer = XmlWriter.Create(stream))
                {
                    this.sitemapPageManager.WritePage(page, writer);
                    writer.Flush();
                }
            }
            finally
            {
                this.streamFactory.Release(stream);
            }
        }

        public void GenerateFiles()
        {
            var firstPageTemplate = Path.Combine(this.BaseDirectory, this.sitemapPageNameProvider.FirstPageNameTemplate);
            var pageNameTemplate = Path.Combine(this.BaseDirectory, this.sitemapPageNameProvider.PageNameTemplate);
            var pageNumbers = this.sitemapPageManager.GetPageNumbers();

            if (pageNumbers.Count() > 1)
            {
                // TODO: Create factory to inject file stream

                // write the index
                this.GenerateFirstPageFile(firstPageTemplate.Replace("{page}", "0"));

                foreach (var page in pageNumbers)
                {
                    using (var stream = new FileStream(pageNameTemplate.Replace("{page}", page.ToString()), FileMode.Create))
                    {
                        using (var writer = XmlWriter.Create(stream))
                        {
                            this.sitemapPageManager.WritePage(page, writer);
                            writer.Flush();
                        }
                    }
                }
            }
            else
            {
                this.GenerateFirstPageFile(firstPageTemplate.Replace("{page}", "0"));
            }
        }

        protected void GenerateFirstPageFile(string fileName)
        {
            using (var stream = new FileStream(fileName, FileMode.Create))
            {
                using (var writer = XmlWriter.Create(stream))
                {
                    this.sitemapPageManager.WritePage(0, writer);
                    writer.Flush();
                }
            }
        }

        

    }
}
