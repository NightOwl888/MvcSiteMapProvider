using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using MvcSiteMapProvider.Web;

namespace MvcSiteMapProvider.Xml.Sitemap.Index
{
    public class XmlSitemapIndexPageWriter
        : IXmlSitemapIndexPageWriter
    {
        public XmlSitemapIndexPageWriter(
            IXmlSitemapIndexWriterFactory xmlSitemapIndexWriterFactory,
            IXmlSitemapFeedUrlResolver xmlSitemapFeedUrlResolver,
            ISitemapEntryFactory sitemapEntryFactory
            )
        {
            if (xmlSitemapIndexWriterFactory == null)
                throw new ArgumentNullException("xmlSitemapIndexWriterFactory");
            if (xmlSitemapFeedUrlResolver == null)
                throw new ArgumentNullException("xmlSitemapFeedUrlResolver");
            if (sitemapEntryFactory == null)
                throw new ArgumentNullException("sitemapEntryFactory");

            this.xmlSitemapIndexWriterFactory = xmlSitemapIndexWriterFactory;
            this.xmlSitemapFeedUrlResolver = xmlSitemapFeedUrlResolver;
            this.sitemapEntryFactory = sitemapEntryFactory;
        }
        private readonly IXmlSitemapIndexWriterFactory xmlSitemapIndexWriterFactory;
        private readonly IXmlSitemapFeedUrlResolver xmlSitemapFeedUrlResolver;
        private readonly ISitemapEntryFactory sitemapEntryFactory;

        // TODO: instead of an IEnumerable<int>, there should be an interface where the last modified date can be passed through
        public virtual void WritePage(XmlWriter writer, string feedName, IEnumerable<int> pageNumbers)
        {
            var xmlSitemapIndexWriter = this.xmlSitemapIndexWriterFactory.Create(writer);
            try
            {
                xmlSitemapIndexWriter.WriteStartDocument();

                foreach (var page in pageNumbers)
                {
                    var location = this.xmlSitemapFeedUrlResolver.ResolveFeedUrl(feedName, page);
                    var sitemapEntry = this.sitemapEntryFactory.Create(location);

                    xmlSitemapIndexWriter.WriteEntry(sitemapEntry);
                }

                xmlSitemapIndexWriter.WriteEndDocument();
            }
            finally
            {
                this.xmlSitemapIndexWriterFactory.Release(xmlSitemapIndexWriter);
            }
        }
    }
}
