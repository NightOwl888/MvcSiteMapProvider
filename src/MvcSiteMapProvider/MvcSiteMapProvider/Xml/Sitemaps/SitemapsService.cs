using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml;
using MvcSiteMapProvider.IO;
using MvcSiteMapProvider.Xml.Sitemaps.Paging;

namespace MvcSiteMapProvider.Xml.Sitemaps
{
    public class SitemapsService
        : ISitemapsService
    {
        public SitemapsService(
            ISitemapsPagingStrategy sitemapsPagingStrategy,
            IStreamFactory streamFactory,
            IUrlEntryHelperFactory urlEntryHelperFactory,
            ISitemapsXmlWriterFactory sitemapsXmlWriterFactory,
            ISitemapsIndexXmlWriterFactory sitemapsIndexXmlWriterFactory
            )
        {
            if (sitemapsPagingStrategy == null)
                throw new ArgumentNullException("sitemapsPagingStrategy");
            if (streamFactory == null)
                throw new ArgumentNullException("streamFactory");
            if (urlEntryHelperFactory == null)
                throw new ArgumentNullException("urlEntryHelperFactory");
            if (sitemapsXmlWriterFactory == null)
                throw new ArgumentNullException("sitemapsXmlWriterFactory");
            if (sitemapsIndexXmlWriterFactory == null)
                throw new ArgumentNullException("sitemapsIndexXmlWriterFactory");

            this.sitemapsPagingStrategy = sitemapsPagingStrategy;
            this.streamFactory = streamFactory;
            this.urlEntryHelperFactory = urlEntryHelperFactory;
            this.sitemapsXmlWriterFactory = sitemapsXmlWriterFactory;
            this.sitemapsIndexXmlWriterFactory = sitemapsIndexXmlWriterFactory;
        }
        private readonly ISitemapsPagingStrategy sitemapsPagingStrategy;
        private readonly IStreamFactory streamFactory;
        private readonly IUrlEntryHelperFactory urlEntryHelperFactory;
        private readonly ISitemapsXmlWriterFactory sitemapsXmlWriterFactory;
        private readonly ISitemapsIndexXmlWriterFactory sitemapsIndexXmlWriterFactory;

        public void Execute(int page)
        {
            IEnumerable<int> indexPageNumbers = new List<int>();
            bool isPageIndexRequest = false;

            if (page == 0)
            {
                // Request is for the sitemaps index file
                indexPageNumbers = this.sitemapsPagingStrategy.GetIndexPageNumbers();
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
                        this.WriteSitemapsIndex(writer, indexPageNumbers);
                    }
                    else
                    {
                        var correctedPage = page;
                        if (correctedPage == 0)
                        {
                            correctedPage = 1;
                        }
                        this.WriteSitemaps(writer, correctedPage);
                    }
                    writer.Flush();
                }
            }
            finally
            {
                this.streamFactory.Release(stream);
            }
        }

        protected virtual void WriteSitemaps(XmlWriter writer, int page)
        {
            var pagingInstructions = this.sitemapsPagingStrategy.GetPagingInstructions(page);

            var sitemapsXmlWriter = this.sitemapsXmlWriterFactory.Create(writer);
            try
            {
                sitemapsXmlWriter.WriteStartDocument();

                foreach (var instruction in pagingInstructions)
                {
                    instruction.UrlEntryProvider.GetEntries(
                        this.urlEntryHelperFactory.Create(instruction.Skip, instruction.Take,

                        // Wire up an anonymous callback from the helper class to this one
                        // so we can get the entries one by one.
                        (urlEntry) =>
                        {
                            sitemapsXmlWriter.WriteEntry(urlEntry);
                        })
                    );
                }

                sitemapsXmlWriter.WriteEndDocument();
            }
            finally
            {
                this.sitemapsXmlWriterFactory.Release(sitemapsXmlWriter);
            }
           
        }

        protected virtual void WriteSitemapsIndex(XmlWriter writer, IEnumerable<int> pageNumbers)
        {
            var sitemapsIndexXmlWriter = this.sitemapsIndexXmlWriterFactory.Create(writer);
            try
            {
                sitemapsIndexXmlWriter.WriteStartDocument();

                var template = "sitemap-{page}.xml";

                foreach (var page in pageNumbers)
                {
                    // TODO: make URL absolute to site (need to think about this in case this
                    // service is being called from a windows app).
                    var templateUrl = "~/" + template.Replace("{page}", page.ToString());

                    // TODO: make factory to inject this (and a service to handle the formatting).
                    var indexEntry = new IndexEntry(templateUrl);

                    sitemapsIndexXmlWriter.WriteEntry(indexEntry);
                }

                sitemapsIndexXmlWriter.WriteEndDocument();
            }
            finally
            {
                this.sitemapsIndexXmlWriterFactory.Release(sitemapsIndexXmlWriter);
            }
        }
    }
}
