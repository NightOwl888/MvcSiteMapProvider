using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using MvcSiteMapProvider.Web;
using MvcSiteMapProvider.Xml.Sitemap.Paging;

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

        public virtual void WritePage(XmlWriter writer, string feedName, IEnumerable<IXmlSitemapPageInfo> pageInfo)
        {
            var xmlSitemapIndexWriter = this.xmlSitemapIndexWriterFactory.Create(writer);
            try
            {
                xmlSitemapIndexWriter.WriteStartDocument();

                foreach (var info in pageInfo)
                {
                    var location = this.xmlSitemapFeedUrlResolver.ResolveFeedUrl(feedName, info.Page);
                    var sitemapEntry = this.sitemapEntryFactory.Create(location, info.LastModifiedDate);

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
