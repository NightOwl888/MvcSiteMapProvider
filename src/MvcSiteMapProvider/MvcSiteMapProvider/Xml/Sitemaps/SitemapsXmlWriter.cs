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

        public virtual void WriteEntry(IUrlEntry urlEntry)
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

        protected virtual void WriteSpecializedContents(IEnumerable<ISpecializedContent> specializedContents)
        {
            var contentTypes = this.specializedContentXmlWriterFactoryStrategy.GetRegisteredContentTypes();

            // Order and group the content type the same way they are ordered in the SpecializedContentXmlWriterFactoryStrategy
            var groupedContent = from ct in contentTypes
                                 join contents in
                                     (from sc in specializedContents
                                      let t = sc.GetType()
                                      from i in t.GetInterfaces()
                                      select new { Instance = sc, Type = t, Interface = i })
                                 on ct equals contents.Interface
                                 group contents by ct into g
                                 select new
                                 {
                                     ContentType = g.Key,
                                     Contents = g.Where(x => g.Key == x.Interface)
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
                            xmlWriter.WriteContent(content.Instance);
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
