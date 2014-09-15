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
            ISpecializedContentXmlWriterFactoryStrategy specializedContentXmlWriterFactoryStrategy,
            IPreparedUrlEntryFactory preparedUrlEntryFactory
            )
        {
            if (writer == null)
                throw new ArgumentNullException("writer");
            if (specializedContentXmlWriterFactoryStrategy == null)
                throw new ArgumentNullException("specializedContentXmlWriterFactoryStrategy");
            if (preparedUrlEntryFactory == null)
                throw new ArgumentNullException("preparedUrlEntryFactory");

            this.omitUrlEntriesWithoutMatchingContent = omitUrlEntriesWithoutMatchingContent;
            this.writer = writer;
            this.specializedContentXmlWriterFactoryStrategy = specializedContentXmlWriterFactoryStrategy;
            this.preparedUrlEntryFactory = preparedUrlEntryFactory;
        }
        private readonly bool omitUrlEntriesWithoutMatchingContent;
        private readonly XmlWriter writer;
        private readonly ISpecializedContentXmlWriterFactoryStrategy specializedContentXmlWriterFactoryStrategy;
        private readonly IPreparedUrlEntryFactory preparedUrlEntryFactory;


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
            if (this.omitUrlEntriesWithoutMatchingContent && !this.ContainsMatchingContentType(urlEntry))
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
                this.WriteSpecializedContents(urlEntry.SpecializedContent);
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
                        // Use the invariant context to write the specialized content.
                        using (var cultureContext = this.preparedUrlEntryFactory.CultureContextFactory.CreateInvariant())
                        {
                            foreach (var content in group.Contents)
                            {
                                xmlWriter.WriteContent(content.Instance, this.preparedUrlEntryFactory.UrlResolver, cultureContext);
                            }
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

        protected virtual bool ContainsMatchingContentType(IUrlEntry urlEntry)
        {
            var contentTypes = this.specializedContentXmlWriterFactoryStrategy.GetRegisteredContentTypes();

            return (from sc in urlEntry.SpecializedContent
                   let t = sc.GetType()
                   from i in t.GetInterfaces()
                   select i)
                   .Intersect(contentTypes)
                   .Any();
        }

        
    }
}
