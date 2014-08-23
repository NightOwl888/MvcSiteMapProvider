using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using MvcSiteMapProvider.Web;
using MvcSiteMapProvider.Xml.Sitemap.Paging;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    public class SitemapPageWriter
        : ISitemapPageWriter
    {
        public SitemapPageWriter(
            ISitemapPagingStrategy sitemapPagingStrategy,
            IUrlEntryHelperFactory urlEntryHelperFactory,
            ISitemapXmlWriterFactory sitemapXmlWriterFactory,
            ISitemapIndexXmlWriterFactory sitemapIndexXmlWriterFactory,
            IUrlPath urlPath
            )
        {
            if (sitemapPagingStrategy == null)
                throw new ArgumentNullException("sitemapPagingStrategy");
            if (urlEntryHelperFactory == null)
                throw new ArgumentNullException("urlEntryHelperFactory");
            if (sitemapXmlWriterFactory == null)
                throw new ArgumentNullException("sitemapXmlWriterFactory");
            if (sitemapIndexXmlWriterFactory == null)
                throw new ArgumentNullException("sitemapIndexXmlWriterFactory");
            if (urlPath == null)
                throw new ArgumentNullException("urlPath");

            this.sitemapPagingStrategy = sitemapPagingStrategy;
            this.urlEntryHelperFactory = urlEntryHelperFactory;
            this.sitemapXmlWriterFactory = sitemapXmlWriterFactory;
            this.sitemapIndexXmlWriterFactory = sitemapIndexXmlWriterFactory;
            this.urlPath = urlPath;

            // Set the page name templates
            this.FirstPageNameTemplate = "sitemap.xml";
            this.PageNameTemplate = "sitemap-{page}.xml";
        }
        private readonly ISitemapPagingStrategy sitemapPagingStrategy;
        private readonly IUrlEntryHelperFactory urlEntryHelperFactory;
        private readonly ISitemapXmlWriterFactory sitemapXmlWriterFactory;
        private readonly ISitemapIndexXmlWriterFactory sitemapIndexXmlWriterFactory;
        private readonly IUrlPath urlPath;

        // Cache the page numbers locally.
        private IEnumerable<int> pageNumbers = null;
        private string baseUrl = string.Empty;

        public string FirstPageNameTemplate { get; set; }

        public string PageNameTemplate { get; set; }

        public string BaseUrl 
        {
            get
            {
                return string.IsNullOrEmpty(baseUrl) ?
                    this.urlPath.ResolveUrl("/", Uri.UriSchemeHttp) :
                    this.baseUrl; 
            }
            set 
            { 
                this.baseUrl = value; 
            }
        }

        public IEnumerable<int> GetPageNumbers()
        {
            if (this.pageNumbers == null)
            {
                this.pageNumbers = this.sitemapPagingStrategy.GetPageNumbers();
            }

            return this.pageNumbers;
        }

        public bool WritePage(int page, XmlWriter writer)
        {
            IEnumerable<int> pageNumbers = this.GetPageNumbers();
            bool isPageIndexRequest = page == 0 && pageNumbers.Count() > 1; ;

            if (isPageIndexRequest)
            {
                this.WriteSitemapIndex(writer, pageNumbers);
            }
            else
            {
                var correctedPage = page;
                if (correctedPage == 0)
                {
                    correctedPage = 1;
                }
                if (!pageNumbers.Contains(correctedPage))
                {
                    // Request was for an invalid page.
                    // We return false so it can be processed as a 404 not found as appropriate.
                    return false;
                }
                this.WriteSitemap(writer, correctedPage);
            }

            // Request was for a valid page
            return true;
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

                foreach (var page in pageNumbers)
                {
                    // TODO: make URL absolute to site (need to think about this in case this
                    // service is being called from a windows app).
                    //var templateUrl = "~/" + this.PageNameTemplate.Replace("{page}", page.ToString());
                    //var url = this.urlPath.ResolveUrl("/", Uri.UriSchemeHttp) + 

                    //var templateUrl = "~/" + siteMapUrlTemplate.Replace("{page}", i.ToString());
                    //var pageUrl = this.urlPath.MakeUrlAbsolute(baseUrl, templateUrl);

                    var templateUrl = "~/" + this.PageNameTemplate.Replace("{page}", page.ToString());
                    var pageUrl = this.urlPath.MakeUrlAbsolute(this.BaseUrl, templateUrl);                    

                    // TODO: make factory to inject this (and a service to handle the formatting).
                    var indexEntry = new IndexEntry(pageUrl);

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
