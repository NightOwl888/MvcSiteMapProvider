using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using MvcSiteMapProvider.Web;
using MvcSiteMapProvider.Xml.Sitemap.Index;
using MvcSiteMapProvider.Xml.Sitemap.Paging;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    public class SitemapPageWriter
        : ISitemapPageWriter
    {
        public SitemapPageWriter(
            IUrlEntryHelperFactory urlEntryHelperFactory,
            ISitemapXmlWriterFactory sitemapXmlWriterFactory,
            IPreparedUrlEntryFactory preparedUrlEntryFactory
            )
        {
            if (urlEntryHelperFactory == null)
                throw new ArgumentNullException("urlEntryHelperFactory");
            if (sitemapXmlWriterFactory == null)
                throw new ArgumentNullException("sitemapXmlWriterFactory");
            if (preparedUrlEntryFactory == null)
                throw new ArgumentNullException("preparedUrlEntryFactory");

            this.urlEntryHelperFactory = urlEntryHelperFactory;
            this.sitemapXmlWriterFactory = sitemapXmlWriterFactory;
            this.preparedUrlEntryFactory = preparedUrlEntryFactory;
        }
        private readonly IUrlEntryHelperFactory urlEntryHelperFactory;
        private readonly ISitemapXmlWriterFactory sitemapXmlWriterFactory;
        private readonly IPreparedUrlEntryFactory preparedUrlEntryFactory;

        public virtual void WritePage(XmlWriter writer, IEnumerable<IPagingInstruction> pagingInstructions)
        {
            var sitemapXmlWriter = this.sitemapXmlWriterFactory.Create(writer);
            try
            {
                sitemapXmlWriter.WriteStartDocument();

                foreach (var instruction in pagingInstructions)
                {
                    instruction.UrlEntryProvider.GetEntries(
                        this.urlEntryHelperFactory.Create(instruction.Skip, instruction.Take,

                        // Wire up an anonymous callback from the helper class to this one
                        // so we can get the entries one by one.
                        (urlEntry) =>
                        {
                            sitemapXmlWriter.WriteEntry(this.preparedUrlEntryFactory.Create(urlEntry));
                        })
                    );
                }

                sitemapXmlWriter.WriteEndDocument();
            }
            finally
            {
                this.sitemapXmlWriterFactory.Release(sitemapXmlWriter);
            }
        }
    }
}
