using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml;
using MvcSiteMapProvider.IO;
using MvcSiteMapProvider.Xml.Sitemap.Paging;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    public class SitemapService
        : ISitemapService
    {
        public SitemapService(
            ISitemapPagingStrategy sitemapPagingStrategy,
            IStreamFactory streamFactory,
            IUrlEntryHelperFactory urlEntryHelperFactory,
            ISitemapXmlWriterFactory sitemapXmlWriterFactory,
            ISitemapIndexXmlWriterFactory sitemapIndexXmlWriterFactory
            )
        {
            if (sitemapPagingStrategy == null)
                throw new ArgumentNullException("sitemapPagingStrategy");
            if (streamFactory == null)
                throw new ArgumentNullException("streamFactory");
            if (urlEntryHelperFactory == null)
                throw new ArgumentNullException("urlEntryHelperFactory");
            if (sitemapXmlWriterFactory == null)
                throw new ArgumentNullException("sitemapXmlWriterFactory");
            if (sitemapIndexXmlWriterFactory == null)
                throw new ArgumentNullException("sitemapIndexXmlWriterFactory");

            this.sitemapPagingStrategy = sitemapPagingStrategy;
            this.streamFactory = streamFactory;
            this.urlEntryHelperFactory = urlEntryHelperFactory;
            this.sitemapXmlWriterFactory = sitemapXmlWriterFactory;
            this.sitemapIndexXmlWriterFactory = sitemapIndexXmlWriterFactory;
        }
        private readonly ISitemapPagingStrategy sitemapPagingStrategy;
        private readonly IStreamFactory streamFactory;
        private readonly IUrlEntryHelperFactory urlEntryHelperFactory;
        private readonly ISitemapXmlWriterFactory sitemapXmlWriterFactory;
        private readonly ISitemapIndexXmlWriterFactory sitemapIndexXmlWriterFactory;

        public void Execute(int page)
        {
            IEnumerable<int> indexPageNumbers = new List<int>();
            bool isPageIndexRequest = false;

            if (page == 0)
            {
                // Request is for the sitemap index file
                indexPageNumbers = this.sitemapPagingStrategy.GetIndexPageNumbers();
                isPageIndexRequest = (indexPageNumbers.Count() > 1);
            }

            var stream = this.streamFactory.Create();
            try
            {
                // TODO: make XmlWriterFactory to inject here instead of the static method
                using (var writer = XmlWriter.Create(stream))
                {
                    if (isPageIndexRequest)
                    {
                        this.WriteSitemapIndex(writer, indexPageNumbers);
                    }
                    else
                    {
                        var correctedPage = page;
                        if (correctedPage == 0)
                        {
                            correctedPage = 1;
                        }
                        this.WriteSitemap(writer, correctedPage);
                    }
                    writer.Flush();
                }
            }
            finally
            {
                this.streamFactory.Release(stream);
            }
        }

        protected virtual void WriteSitemap(XmlWriter writer, int page)
        {
            var pagingInstructions = this.sitemapPagingStrategy.GetPagingInstructions(page);

            var sitemapXmlWriter = this.sitemapXmlWriterFactory.Create(writer);
            try
            {
                sitemapXmlWriter.WriteStartDocument();

                foreach (var instruction in pagingInstructions)
                {
                    instruction.UrlEntryProvider.GetEntries(
                        this.urlEntryHelperFactory.Create(instruction.Skip, instruction.Take,

                        // Wire up an anonymous callback from the helper class to this one
                        // so we can get the entries one by one.
                        (urlEntry) =>
                        {
                            sitemapXmlWriter.WriteEntry(urlEntry);
                        })
                    );
                }

                sitemapXmlWriter.WriteEndDocument();
            }
            finally
            {
                this.sitemapXmlWriterFactory.Release(sitemapXmlWriter);
            }
           
        }

        protected virtual void WriteSitemapIndex(XmlWriter writer, IEnumerable<int> pageNumbers)
        {
            var sitemapIndexXmlWriter = this.sitemapIndexXmlWriterFactory.Create(writer);
            try
            {
                sitemapIndexXmlWriter.WriteStartDocument();

                var template = "sitemap-{page}.xml";

                foreach (var page in pageNumbers)
                {
                    // TODO: make URL absolute to site (need to think about this in case this
                    // service is being called from a windows app).
                    var templateUrl = "~/" + template.Replace("{page}", page.ToString());

                    // TODO: make factory to inject this (and a service to handle the formatting).
                    var indexEntry = new IndexEntry(templateUrl);

                    sitemapIndexXmlWriter.WriteEntry(indexEntry);
                }

                sitemapIndexXmlWriter.WriteEndDocument();
            }
            finally
            {
                this.sitemapIndexXmlWriterFactory.Release(sitemapIndexXmlWriter);
            }
        }
    }
}
