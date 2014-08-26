using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using MvcSiteMapProvider.Web;

namespace MvcSiteMapProvider.Xml.Sitemap.Index
{
    public class SitemapIndexPageWriter
        : ISitemapIndexPageWriter
    {
        public SitemapIndexPageWriter(
            ISitemapIndexXmlWriterFactory sitemapIndexXmlWriterFactory,
            ISitemapPageNameProvider sitemapPageNameProvider,
            ISitemapUrlResolver sitemapUrlResolver
            )
        {
            if (sitemapIndexXmlWriterFactory == null)
                throw new ArgumentNullException("sitemapIndexXmlWriterFactory");
            if (sitemapPageNameProvider == null)
                throw new ArgumentNullException("sitemapPageNameProvider");
            if (sitemapUrlResolver == null)
                throw new ArgumentNullException("sitemapUrlResolver");

            this.sitemapIndexXmlWriterFactory = sitemapIndexXmlWriterFactory;
            this.sitemapPageNameProvider = sitemapPageNameProvider;
            this.sitemapUrlResolver = sitemapUrlResolver;
        }
        private readonly ISitemapIndexXmlWriterFactory sitemapIndexXmlWriterFactory;
        private readonly ISitemapPageNameProvider sitemapPageNameProvider;
        private readonly ISitemapUrlResolver sitemapUrlResolver;

        public virtual void WritePage(XmlWriter writer, IEnumerable<int> pageNumbers)
        {
            var sitemapIndexXmlWriter = this.sitemapIndexXmlWriterFactory.Create(writer);
            try
            {
                sitemapIndexXmlWriter.WriteStartDocument();

                foreach (var page in pageNumbers)
                {
                    // TODO: make factory to inject this (and a service to handle the formatting).
                    var location = this.sitemapUrlResolver.ResolveUrlToAbsolute("~/" + this.sitemapPageNameProvider.PageNameTemplate.Replace("{page}", page.ToString()));
                    var sitemapEntry = new SitemapEntry(location);

                    sitemapIndexXmlWriter.WriteEntry(sitemapEntry);
                }

                sitemapIndexXmlWriter.WriteEndDocument();
            }
            finally
            {
                this.sitemapIndexXmlWriterFactory.Release(sitemapIndexXmlWriter);
            }
        }
    }
}
