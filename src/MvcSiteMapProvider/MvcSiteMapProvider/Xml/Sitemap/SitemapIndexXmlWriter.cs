using System;
using System.Xml;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    public class SitemapIndexXmlWriter
        : ISitemapIndexXmlWriter
    {
        public SitemapIndexXmlWriter(
            XmlWriter writer)
        {
            if (writer == null)
                throw new ArgumentNullException("writer");
            this.writer = writer;
        }
        private readonly XmlWriter writer;
        
        public virtual void WriteStartDocument()
        {
            writer.WriteStartDocument();
            writer.WriteStartElement("sitemapindex", "http://www.sitemaps.org/schemas/sitemap/0.9");
        }

        public virtual void WriteEndDocument()
        {
            writer.WriteEndElement(); // sitemapindex
            writer.WriteEndDocument();
        }

        public virtual void WriteEntry(IIndexEntry indexEntry)
        {
            writer.WriteStartElement("sitemap");

            writer.WriteElementString("loc", indexEntry.Location);

            if (!string.IsNullOrEmpty(indexEntry.LastModified))
            {
                writer.WriteElementString("lastmod", indexEntry.LastModified);
            }

            writer.WriteEndElement(); // sitemap
        }
    }
}
