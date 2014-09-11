using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Xml.Sitemap.Paging
{
    public class ManualPagingInstruction
        : IManualPagingInstruction
    {
        public ManualPagingInstruction(
            int page,
            int skip,
            int take,
            IXmlSitemapProvider xmlSitemapProvider
            )
        {
            if (xmlSitemapProvider == null)
                throw new ArgumentNullException("xmlSitemapProvider");

            this.page = page;
            this.skip = skip;
            this.take = take;
            this.xmlSitemapProvider = xmlSitemapProvider;
        }
        private readonly int page;
        private readonly int skip;
        private readonly int take;
        private readonly IXmlSitemapProvider xmlSitemapProvider;

        public int Page
        {
            get { return this.page; }
        }

        public int Skip
        {
            get { return this.skip; }
        }

        public int Take
        {
            get { return this.take; }
        }

        public IXmlSitemapProvider UrlEntryProvider
        {
            get { return this.xmlSitemapProvider; }
        }
    }
}
