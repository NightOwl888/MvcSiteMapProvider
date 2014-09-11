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
            IXmlSitemapWriterFactory xmlSitemapWriterFactory
            )
        {
            if (urlEntryHelperFactory == null)
                throw new ArgumentNullException("urlEntryHelperFactory");
            if (xmlSitemapWriterFactory == null)
                throw new ArgumentNullException("xmlSitemapWriterFactory");
            
            this.urlEntryHelperFactory = urlEntryHelperFactory;
            this.xmlSitemapWriterFactory = xmlSitemapWriterFactory;
        }
        private readonly IUrlEntryHelperFactory urlEntryHelperFactory;
        private readonly IXmlSitemapWriterFactory xmlSitemapWriterFactory;
        
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
                        (urlEntry) => { xmlSitemapWriter.WriteEntry(urlEntry); })
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
