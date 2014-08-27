using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    // this will be injected into XmlSitemapStrategy
    public class XmlSitemapFeed
        : IXmlSitemapFeed
    {
        public XmlSitemapFeed(
            string name,
            XmlWriterSettings settings,
            IXmlSitemapPageManager siteMapPageManager,
            IXmlWriterFactory xmlWriterFactory
            )
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");
            if (settings == null)
                throw new ArgumentNullException("settings");
            if (siteMapPageManager == null)
                throw new ArgumentNullException("siteMapPageManager");
            if (xmlWriterFactory == null)
                throw new ArgumentNullException("xmlWriterFactory");

            this.name = name;
            this.settings = settings;
            this.siteMapPageManager = siteMapPageManager;
            this.xmlWriterFactory = xmlWriterFactory;
        }
        private readonly string name;
        private readonly XmlWriterSettings settings;
        private readonly IXmlSitemapPageManager siteMapPageManager;
        private readonly IXmlWriterFactory xmlWriterFactory;

        public string Name
        {
            get { return this.name; }
        }

        public bool WritePage(int page, Stream output)
        {
            using (var writer = this.xmlWriterFactory.Create(output, this.settings))
            {
                return this.siteMapPageManager.WritePage(writer, this.Name, page);
            }
        }
    }
}
