using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using MvcSiteMapProvider.Web;

namespace MvcSiteMapProvider.Xml.Sitemap.Index
{
    public class XmlSitemapIndexPageWriter
        : IXmlSitemapIndexPageWriter
    {
        public XmlSitemapIndexPageWriter(
            IXmlSitemapIndexWriterFactory xmlSitemapIndexWriterFactory,
            IXmlSitemapPageNameProvider xmlSitemapPageNameProvider,
            IXmlSitemapUrlResolver xmlSitemapUrlResolver
            )
        {
            if (xmlSitemapIndexWriterFactory == null)
                throw new ArgumentNullException("xmlSitemapIndexWriterFactory");
            if (xmlSitemapPageNameProvider == null)
                throw new ArgumentNullException("xmlSitemapPageNameProvider");
            if (xmlSitemapUrlResolver == null)
                throw new ArgumentNullException("xmlSitemapUrlResolver");

            this.xmlSitemapIndexWriterFactory = xmlSitemapIndexWriterFactory;
            this.xmlSitemapPageNameProvider = xmlSitemapPageNameProvider;
            this.xmlSitemapUrlResolver = xmlSitemapUrlResolver;
        }
        private readonly IXmlSitemapIndexWriterFactory xmlSitemapIndexWriterFactory;
        private readonly IXmlSitemapPageNameProvider xmlSitemapPageNameProvider;
        private readonly IXmlSitemapUrlResolver xmlSitemapUrlResolver;

        public virtual void WritePage(XmlWriter writer, IEnumerable<int> pageNumbers)
        {
            var xmlSitemapIndexWriter = this.xmlSitemapIndexWriterFactory.Create(writer);
            try
            {
                xmlSitemapIndexWriter.WriteStartDocument();

                foreach (var page in pageNumbers)
                {
                    // TODO: make factory to inject this (and a service to handle the formatting).
                    //var location = this.sitemapUrlResolver.ResolveUrlToAbsolute("~/" + this.sitemapPageNameProvider.DefaultPageNameTemplate.Replace("{page}", page.ToString()));

                    // TODO: Pass the feed name here
                    var location = this.xmlSitemapUrlResolver.ResolveUrlToAbsolute("~/" + this.xmlSitemapPageNameProvider.GetPageName(page, "default"));

                    // TODO: make factory to inject this
                    var sitemapEntry = new SitemapEntry(location);

                    xmlSitemapIndexWriter.WriteEntry(sitemapEntry);
                }

                xmlSitemapIndexWriter.WriteEndDocument();
            }
            finally
            {
                this.xmlSitemapIndexWriterFactory.Release(xmlSitemapIndexWriter);
            }
        }
    }
}
