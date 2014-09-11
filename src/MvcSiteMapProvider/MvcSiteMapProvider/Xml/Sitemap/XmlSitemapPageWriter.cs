using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using MvcSiteMapProvider.Web;
using MvcSiteMapProvider.Xml.Sitemap.Index;
using MvcSiteMapProvider.Xml.Sitemap.Paging;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    public class XmlSitemapPageWriter
        : IXmlSitemapPageWriter
    {
        public XmlSitemapPageWriter(
            IUrlEntryHelperFactory urlEntryHelperFactory,
            IXmlSitemapWriterFactory xmlSitemapWriterFactory,
            IPreparedUrlEntryFactory preparedUrlEntryFactory
            )
        {
            if (urlEntryHelperFactory == null)
                throw new ArgumentNullException("urlEntryHelperFactory");
            if (xmlSitemapWriterFactory == null)
                throw new ArgumentNullException("xmlSitemapWriterFactory");
            if (preparedUrlEntryFactory == null)
                throw new ArgumentNullException("preparedUrlEntryFactory");

            this.urlEntryHelperFactory = urlEntryHelperFactory;
            this.xmlSitemapWriterFactory = xmlSitemapWriterFactory;
            this.preparedUrlEntryFactory = preparedUrlEntryFactory;
        }
        private readonly IUrlEntryHelperFactory urlEntryHelperFactory;
        private readonly IXmlSitemapWriterFactory xmlSitemapWriterFactory;
        private readonly IPreparedUrlEntryFactory preparedUrlEntryFactory;

        public virtual void WritePage(XmlWriter writer, string feedName, IEnumerable<IPagingInstruction> pagingInstructions)
        {
            var xmlSitemapWriter = this.xmlSitemapWriterFactory.Create(writer);
            try
            {
                xmlSitemapWriter.WriteStartDocument();

                foreach (var instruction in pagingInstructions)
                {
                    instruction.XmlSitemapProvider.GetUrlEntries(
                        this.urlEntryHelperFactory.Create(feedName, instruction.Skip, instruction.Take,

                        // Wire up an anonymous callback from the helper class to this one
                        // so we can get the entries one by one.
                        (urlEntry) =>
                        {
                            // Run any business logic that needs to be executed to prepare
                            // the data for writing.
                            var prepared = this.preparedUrlEntryFactory.Create(urlEntry);
                            xmlSitemapWriter.WriteEntry(prepared);
                        })
                    );
                }

                xmlSitemapWriter.WriteEndDocument();
            }
            finally
            {
                this.xmlSitemapWriterFactory.Release(xmlSitemapWriter);
            }
        }
    }
}
