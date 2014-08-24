using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    public class SitemapPageNameProvider
        : ISitemapPageNameProvider
    {
        public SitemapPageNameProvider()
        {
            // Set the page name templates
            this.FirstPageNameTemplate = "sitemap.xml";
            this.PageNameTemplate = "sitemap-{page}.xml";
        }

        public string FirstPageNameTemplate { get; set; }

        public string PageNameTemplate { get; set; }
    }
}
