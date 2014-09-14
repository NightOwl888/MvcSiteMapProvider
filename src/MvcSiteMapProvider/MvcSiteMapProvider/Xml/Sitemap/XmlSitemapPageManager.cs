using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using MvcSiteMapProvider.Xml.Sitemap.Index;
using MvcSiteMapProvider.Xml.Sitemap.Paging;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    public class XmlSitemapPageManager
        : IXmlSitemapPageManager
    {
        public XmlSitemapPageManager(
            IXmlSitemapPager xmlSitemapPager,
            IXmlSitemapProviderStrategy xmlSitemapProviderStrategy,
            IXmlSitemapPageWriter xmlSitemapPageWriter,
            IXmlSitemapIndexPageWriter xmlSitemapIndexPageWriter
            )
        {
            if (xmlSitemapPager == null)
                throw new ArgumentNullException("xmlSitemapPager");
            if (xmlSitemapProviderStrategy == null)
                throw new ArgumentNullException("xmlSitemapProviderStrategy");
            if (xmlSitemapPageWriter == null)
                throw new ArgumentNullException("xmlSitemapPageWriter");
            if (xmlSitemapIndexPageWriter == null)
                throw new ArgumentNullException("xmlSitemapIndexPageWriter");

            this.xmlSitemapPager = xmlSitemapPager;
            this.xmlSitemapProviderStrategy = xmlSitemapProviderStrategy;
            this.xmlSitemapPageWriter = xmlSitemapPageWriter;
            this.xmlSitemapIndexPageWriter = xmlSitemapIndexPageWriter;
        }
        private readonly IXmlSitemapPager xmlSitemapPager;
        private readonly IXmlSitemapProviderStrategy xmlSitemapProviderStrategy;
        private readonly IXmlSitemapPageWriter xmlSitemapPageWriter;
        private readonly IXmlSitemapIndexPageWriter xmlSitemapIndexPageWriter; 

        public IXmlSitemapPageData GetPageData(string feedName)
        {
            IXmlSitemapPageData result = null;
            var providers = this.xmlSitemapProviderStrategy.GetProviders(feedName);
            try
            {
                result = this.xmlSitemapPager.GetPageData(providers, feedName);
            }
            finally
            {
                this.xmlSitemapProviderStrategy.ReleaseProviders(providers);
            }

            return result;
        }

        public bool WritePage(XmlWriter writer, string feedName, int page)
        {
            if (writer == null)
            {
                throw new ArgumentNullException("writer");
            }

            if (page < 0)
            {
                return false;
            }

            var providers = this.xmlSitemapProviderStrategy.GetProviders(feedName);
            try
            {
                var pageData = this.xmlSitemapPager.GetPageData(providers, feedName);

                // Check total page count for all providers
                // if it is 0, return false.
                if (pageData.TotalRecordCount == 0)
                {
                    return false;
                }

                bool isIndexPageRequest = page == 0 && pageData.Pages.Count() > 1;

                if (isIndexPageRequest)
                {
                    this.xmlSitemapIndexPageWriter.WritePage(writer, feedName, pageData.Pages);
                }
                else
                {
                    var indexCorrectedPage = (page == 0) ? 1 : page;

                    if (!pageData.Pages.Select(x => x.Page).Contains(indexCorrectedPage))
                    {
                        // Request was for an invalid page.
                        // We return false so it can be processed as a 404 not found as appropriate.
                        return false;
                    }

                    var pagingInstructions = this.xmlSitemapPager.GetPagingInstructions(providers, feedName, indexCorrectedPage);
                    this.xmlSitemapPageWriter.WritePage(writer, feedName, pagingInstructions);
                }
            }
            finally
            {
                this.xmlSitemapProviderStrategy.ReleaseProviders(providers);
            }

            // Request was for a valid page
            return true;
        }
    }
}
