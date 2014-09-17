using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using MvcSiteMapProvider.Xml.Sitemap.Specialized;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    public class XmlSitemapWriterFactory
        : IXmlSitemapWriterFactory
    {
        public XmlSitemapWriterFactory(
            bool omitUrlEntriesWithoutMatchingContent,
            ISpecializedContentWriter specializedContentWriter,
            IPreparedUrlEntryFactory preparedUrlEntryFactory
            )
        {
            if (specializedContentWriter == null)
                throw new ArgumentNullException("specializedContentWriter");
            if (preparedUrlEntryFactory == null)
                throw new ArgumentNullException("preparedUrlEntryFactory");

            this.omitUrlEntriesWithoutMatchingContent = omitUrlEntriesWithoutMatchingContent;
            this.specializedContentWriter = specializedContentWriter;
            this.preparedUrlEntryFactory = preparedUrlEntryFactory;
        }
        private readonly bool omitUrlEntriesWithoutMatchingContent;
        private readonly ISpecializedContentWriter specializedContentWriter;
        private readonly IPreparedUrlEntryFactory preparedUrlEntryFactory;

        public IXmlSitemapWriter Create(XmlWriter writer)
        {
            return new XmlSitemapWriter(
                this.omitUrlEntriesWithoutMatchingContent, 
                writer,
                this.specializedContentWriter, 
                this.preparedUrlEntryFactory);
        }

        public void Release(IXmlSitemapWriter xmlSitemapWriter)
        {
            var disposable = xmlSitemapWriter as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }
    }
}
