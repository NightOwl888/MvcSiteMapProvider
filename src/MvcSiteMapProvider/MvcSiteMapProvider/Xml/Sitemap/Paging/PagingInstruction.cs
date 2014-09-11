using System;

namespace MvcSiteMapProvider.Xml.Sitemap.Paging
{
    public class PagingInstruction
        : IPagingInstruction
    {
        public PagingInstruction(
            int skip,
            int take,
            IXmlSitemapProvider xmlSitemapProvider
            )
        {
            if (xmlSitemapProvider == null)
                throw new ArgumentNullException("xmlSitemapProvider");

            this.skip = skip;
            this.take = take;
            this.xmlSitemapProvider = xmlSitemapProvider;
        }
        private readonly int skip;
        private readonly int take;
        private readonly IXmlSitemapProvider xmlSitemapProvider;

        public int Skip
        {
            get { return this.skip; }
        }

        public int Take
        {
            get { return this.take; }
        }

        public IXmlSitemapProvider XmlSitemapProvider
        {
            get { return this.xmlSitemapProvider; }
        }
    }
}
