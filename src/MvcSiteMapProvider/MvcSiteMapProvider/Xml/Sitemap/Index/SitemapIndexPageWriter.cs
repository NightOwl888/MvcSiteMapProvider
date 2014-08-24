using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using MvcSiteMapProvider.Web;

namespace MvcSiteMapProvider.Xml.Sitemap.Index
{
    public class SitemapIndexPageWriter
        : ISitemapIndexPageWriter
    {
        // TODO: Move UrlPath out of this class and make the SitemapEntry class automatically resolve the URL
        // TODO: Make a service to share the default templates and base URL between services.

        public SitemapIndexPageWriter(
            ISitemapIndexXmlWriterFactory sitemapIndexXmlWriterFactory,
            ISitemapPageNameProvider sitemapPageNameProvider,
            ISitemapUrlResolver sitemapUrlResolver
            )
        {
            if (sitemapIndexXmlWriterFactory == null)
                throw new ArgumentNullException("sitemapIndexXmlWriterFactory");
            if (sitemapPageNameProvider == null)
                throw new ArgumentNullException("sitemapPageNameProvider");
            if (sitemapUrlResolver == null)
                throw new ArgumentNullException("sitemapUrlResolver");

            this.sitemapIndexXmlWriterFactory = sitemapIndexXmlWriterFactory;
            this.sitemapPageNameProvider = sitemapPageNameProvider;
            this.sitemapUrlResolver = sitemapUrlResolver;

            //// Set the page name templates
            //this.FirstPageNameTemplate = "sitemap.xml";
            //this.PageNameTemplate = "sitemap-{page}.xml";
        }
        private readonly ISitemapIndexXmlWriterFactory sitemapIndexXmlWriterFactory;
        private readonly ISitemapPageNameProvider sitemapPageNameProvider;
        private readonly ISitemapUrlResolver sitemapUrlResolver;

        //// Cache the page numbers locally.
        //private IEnumerable<int> pageNumbers = null;
        //private string baseUrl = string.Empty;

        //public string FirstPageNameTemplate { get; set; }

        //public string PageNameTemplate { get; set; }

        //public string BaseUrl
        //{
        //    get
        //    {
        //        return string.IsNullOrEmpty(baseUrl) ?
        //            this.urlPath.ResolveUrl("/", Uri.UriSchemeHttp) :
        //            this.baseUrl;
        //    }
        //    set
        //    {
        //        this.baseUrl = value;
        //    }
        //}

        public virtual void WritePage(XmlWriter writer, IEnumerable<int> pageNumbers)
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

                    //var templateUrl = "~/" + this.PageNameTemplate.Replace("{page}", page.ToString());
                    //var pageUrl = this.urlPath.MakeUrlAbsolute(this.BaseUrl, templateUrl);

                    //// TODO: make factory to inject this (and a service to handle the formatting).
                    //var sitemapEntry = new SitemapEntry(pageUrl);

                    // TODO: make factory to inject this (and a service to handle the formatting).
                    var location = this.sitemapUrlResolver.ResolveUrlToAbsolute("~/" + this.sitemapPageNameProvider.PageNameTemplate.Replace("{page}", page.ToString()));
                    var sitemapEntry = new SitemapEntry(location);

                    sitemapIndexXmlWriter.WriteEntry(sitemapEntry);
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
