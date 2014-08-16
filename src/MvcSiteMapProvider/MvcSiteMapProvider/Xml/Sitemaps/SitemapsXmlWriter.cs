using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using MvcSiteMapProvider.Xml.Sitemaps.Specialized;

namespace MvcSiteMapProvider.Xml.Sitemaps
{
    public class SitemapsXmlWriter
        : ISitemapsXmlWriter
    {
        public SitemapsXmlWriter(
            XmlWriter writer,
            ISpecializedContentXmlWriterFactoryStrategy specializedContentXmlWriterFactoryStrategy)
        {
            if (writer == null)
                throw new ArgumentNullException("writer");
            if (specializedContentXmlWriterFactoryStrategy == null)
                throw new ArgumentNullException("specializedContentXmlWriterFactoryStrategy");
            this.writer = writer;
            this.specializedContentXmlWriterFactoryStrategy = specializedContentXmlWriterFactoryStrategy;
        }
        private readonly XmlWriter writer;
        private readonly ISpecializedContentXmlWriterFactoryStrategy specializedContentXmlWriterFactoryStrategy;
        
        public virtual void WriteStartDocument()
        {
            writer.WriteStartDocument();
            writer.WriteStartElement("urlset", "http://www.sitemaps.org/schemas/sitemap/0.9");
 
            // call upon registered child services to get namespaces
            var contentTypes = this.specializedContentXmlWriterFactoryStrategy.GetRegisteredContentTypes();

            foreach (var contentType in contentTypes)
            {
                var xmlWriterFactory = this.specializedContentXmlWriterFactoryStrategy.GetFactory(contentType);
                if (xmlWriterFactory != null)
                {
                    var xmlWriter = xmlWriterFactory.Create(this.writer);
                    try
                    {
                        xmlWriter.WriteNamespace();
                    }
                    finally
                    {
                        // Free up any unmanaged resources
                        xmlWriterFactory.Release(xmlWriter);
                    }
                }
            }
        }

        public virtual void WriteEndDocument()
        {
            writer.WriteEndElement(); // urlset
            writer.WriteEndDocument();
        }

        public virtual void WriteUrlEntry(IUrlEntry urlEntry)
        {
            writer.WriteStartElement("url");

            writer.WriteElementString("loc", urlEntry.Location);

            if (!string.IsNullOrEmpty(urlEntry.LastModified))
            {
                writer.WriteElementString("lastmod", urlEntry.LastModified);
            }

            if (!string.IsNullOrEmpty(urlEntry.ChangeFrequency))
            {
                writer.WriteElementString("changefreq", urlEntry.ChangeFrequency);
            }

            if (!string.IsNullOrEmpty(urlEntry.Priority))
            {
                writer.WriteElementString("priority", urlEntry.Priority);
            }

            if (urlEntry.SpecializedContents != null)
            {
                this.WriteSpecializedContents(urlEntry.SpecializedContents);
            }

            writer.WriteEndElement(); // url
        }

        // NOTE: This implementation does not support using multiple ISpecializedContentType inherited interfaces on the same type.
        // This limitation should probably be overcome.

        protected virtual void WriteSpecializedContents(IEnumerable<ISpecializedContent> specializedContents)
        {
            var contentTypes = this.specializedContentXmlWriterFactoryStrategy.GetRegisteredContentTypes();

            // Order and group the content type the same way they are ordered in the SpecializedContentXmlWriterFactoryStrategy
            var groupedContent = from ct in contentTypes
                                 join sc in specializedContents
                                    on ct equals sc.GetType().GetInterfaces().Where(x => contentTypes.Contains(x)).FirstOrDefault()
                                 group sc by sc.GetType().GetInterfaces().Where(x => contentTypes.Contains(x)).FirstOrDefault() into g
                                 select new
                                 {
                                     ContentType = g.Key,
                                     Contents = g.Where(x => g.Key.IsAssignableFrom(x.GetType()))
                                 };

            foreach (var group in groupedContent)
            {
                // Strategy pattern to get the correct factory instance based on type.
                var xmlWriterFactory = this.specializedContentXmlWriterFactoryStrategy.GetFactory(group.ContentType);

                if (xmlWriterFactory != null)
                {
                    // Create our specialized writer instance
                    var xmlWriter = xmlWriterFactory.Create(this.writer);
                    try
                    {
                        foreach (var content in group.Contents)
                        {
                            xmlWriter.WriteContent(content);
                        }
                    }
                    finally
                    {
                        // Free up any unmanaged resources
                        xmlWriterFactory.Release(xmlWriter);
                    }
                }
            }
        }
    }
}
