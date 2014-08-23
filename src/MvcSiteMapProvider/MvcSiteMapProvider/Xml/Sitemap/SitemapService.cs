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
            ISitemapPageWriter sitemapPageWriter,
            IStreamFactory streamFactory
            )
        {
            if (sitemapPageWriter == null)
                throw new ArgumentNullException("sitemapPageWriter");
            if (streamFactory == null)
                throw new ArgumentNullException("streamFactory");

            this.sitemapPageWriter = sitemapPageWriter;
            this.streamFactory = streamFactory;
        }
        private readonly ISitemapPageWriter sitemapPageWriter;
        private readonly IStreamFactory streamFactory;

        public void Execute(int page)
        {
            var stream = this.streamFactory.Create();
            try
            {
                // TODO: make XmlWriterFactory to inject here instead of the static method
                using (var writer = XmlWriter.Create(stream))
                {
                    this.sitemapPageWriter.WritePage(page, writer);
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
            //var firstPageFilePath = @"F:\sitemap.xml";
            //var pageTemplate = @"F:\sitemap-{page}.xml";
            var currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            var firstPageTemplate = Path.Combine(currentDirectory, this.sitemapPageWriter.FirstPageNameTemplate);
            var pageNameTemplate = Path.Combine(currentDirectory, this.sitemapPageWriter.PageNameTemplate);
            var pageNumbers = this.sitemapPageWriter.GetPageNumbers();

            if (pageNumbers.Count() > 1)
            {
                // write the index
                using (var stream = new FileStream(firstPageTemplate.Replace("{page}", "0"), FileMode.Create))
                {
                    using (var writer = XmlWriter.Create(stream))
                    {
                        this.sitemapPageWriter.WritePage(0, writer);
                    }
                }

                foreach (var page in pageNumbers)
                {
                    using (var stream = new FileStream(pageNameTemplate.Replace("{page}", page.ToString()), FileMode.Create))
                    {
                        using (var writer = XmlWriter.Create(stream))
                        {
                            this.sitemapPageWriter.WritePage(page, writer);
                        }
                    }
                }
            }
            else
            {
                using (var stream = new FileStream(firstPageTemplate.Replace("{page}", "0"), FileMode.Create))
                {
                    using (var writer = XmlWriter.Create(stream))
                    {
                        this.sitemapPageWriter.WritePage(0, writer);
                    }
                }
            }

            
        }

        

    }
}
