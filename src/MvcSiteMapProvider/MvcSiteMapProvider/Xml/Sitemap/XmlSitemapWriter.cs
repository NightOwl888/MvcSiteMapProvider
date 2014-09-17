using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using MvcSiteMapProvider.Xml.Sitemap.Specialized;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    public class XmlSitemapWriter
        : IXmlSitemapWriter
    {
        public XmlSitemapWriter(
            bool omitUrlEntriesWithoutMatchingContent,
            XmlWriter writer,
            ISpecializedContentWriter specializedContentWriter,
            IPreparedUrlEntryFactory preparedUrlEntryFactory
            )
        {
            if (writer == null)
                throw new ArgumentNullException("writer");
            if (specializedContentWriter == null)
                throw new ArgumentNullException("specializedContentWriter");
            if (preparedUrlEntryFactory == null)
                throw new ArgumentNullException("preparedUrlEntryFactory");

            this.omitUrlEntriesWithoutMatchingContent = omitUrlEntriesWithoutMatchingContent;
            this.writer = writer;
            this.specializedContentWriter = specializedContentWriter;
            this.preparedUrlEntryFactory = preparedUrlEntryFactory;
        }
        private readonly bool omitUrlEntriesWithoutMatchingContent;
        private readonly XmlWriter writer;
        private readonly ISpecializedContentWriter specializedContentWriter;
        private readonly IPreparedUrlEntryFactory preparedUrlEntryFactory;


        public virtual void WriteStartDocument()
        {
            writer.WriteStartDocument();
            writer.WriteStartElement("urlset", "http://www.sitemaps.org/schemas/sitemap/0.9");

            // call upon registered child services to get namespaces
            this.specializedContentWriter.WriteNamespaces(writer);
        }

        public virtual void WriteEndDocument()
        {
            writer.WriteEndElement(); // urlset
            writer.WriteEndDocument();
        }

        public virtual void WriteEntry(IUrlEntry urlEntry)
        {
            if (this.omitUrlEntriesWithoutMatchingContent && !this.specializedContentWriter.ContainsMatchingContentType(urlEntry.SpecializedContent))
            {
                return;
            }

            // Run any business logic that needs to be executed to prepare
            // the data for writing.
            var prepared = this.preparedUrlEntryFactory.Create(urlEntry);

            writer.WriteStartElement("url");

            writer.WriteElementString("loc", prepared.Location);

            if (!string.IsNullOrEmpty(prepared.LastModified))
            {
                writer.WriteElementString("lastmod", prepared.LastModified);
            }

            if (!string.IsNullOrEmpty(prepared.ChangeFrequency))
            {
                writer.WriteElementString("changefreq", prepared.ChangeFrequency);
            }

            if (!string.IsNullOrEmpty(prepared.Priority))
            {
                writer.WriteElementString("priority", prepared.Priority);
            }

            if (urlEntry.SpecializedContent != null)
            {
                this.specializedContentWriter.WriteSpecializedContent(writer, urlEntry.SpecializedContent);
            }

            writer.WriteEndElement(); // url
        }
    }
}