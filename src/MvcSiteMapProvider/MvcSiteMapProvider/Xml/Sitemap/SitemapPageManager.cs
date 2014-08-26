using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using MvcSiteMapProvider.Xml.Sitemap.Index;
using MvcSiteMapProvider.Xml.Sitemap.Paging;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    public class SitemapPageManager
        : ISitemapPageManager
    {
        public SitemapPageManager(
            ISitemapPagingStrategy sitemapPagingStrategy,
            ISitemapPageWriter sitemapPageWriter,
            ISitemapIndexPageWriter sitemapIndexPageWriter
            )
        {
            if (sitemapPagingStrategy == null)
                throw new ArgumentNullException("sitemapPagingStrategy");
            if (sitemapPageWriter == null)
                throw new ArgumentNullException("sitemapPageWriter");
            if (sitemapIndexPageWriter == null)
                throw new ArgumentNullException("sitemapIndexPageWriter");

            this.sitemapPagingStrategy = sitemapPagingStrategy;
            this.sitemapPageWriter = sitemapPageWriter;
            this.sitemapIndexPageWriter = sitemapIndexPageWriter;
        }
        private readonly ISitemapPagingStrategy sitemapPagingStrategy;
        private readonly ISitemapPageWriter sitemapPageWriter;
        private readonly ISitemapIndexPageWriter sitemapIndexPageWriter; 


        // TODO: We should request cache the page counts so this doesn't 
        // result in multiple database calls, multiple times per request.
        public IEnumerable<int> GetPageNumbers()
        {
            return this.sitemapPagingStrategy.GetPageNumbers();
        }

        public bool WritePage(int page, XmlWriter writer)
        {
            IEnumerable<int> pageNumbers = this.GetPageNumbers();
            bool isPageIndexRequest = page == 0 && pageNumbers.Count() > 1;

            if (isPageIndexRequest)
            {
                this.sitemapIndexPageWriter.WritePage(writer, pageNumbers);
            }
            else
            {
                var indexCorrectedPage = (page == 0) ? 1 : page;

                if (!pageNumbers.Contains(indexCorrectedPage))
                {
                    // Request was for an invalid page.
                    // We return false so it can be processed as a 404 not found as appropriate.
                    return false;
                }
                var pagingInstructions = this.sitemapPagingStrategy.GetPagingInstructions(indexCorrectedPage);
                this.sitemapPageWriter.WritePage(writer, pagingInstructions);
            }

            // Request was for a valid page
            return true;
        }
    }
}
