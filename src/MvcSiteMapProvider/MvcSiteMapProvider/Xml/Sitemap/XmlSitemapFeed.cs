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
            IXmlWriterFactory xmlWriterFactory,
            IXmlSitemapPageManager siteMapPageManager
            )
        {
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException("name");
            if (settings == null)
                throw new ArgumentNullException("settings");
            if (xmlWriterFactory == null)
                throw new ArgumentNullException("xmlWriterFactory");
            if (siteMapPageManager == null)
                throw new ArgumentNullException("siteMapPageManager");

            this.name = name;
            this.settings = settings;
            this.xmlWriterFactory = xmlWriterFactory;
            this.siteMapPageManager = siteMapPageManager;
        }
        private readonly string name;
        private readonly XmlWriterSettings settings;
        private readonly IXmlWriterFactory xmlWriterFactory;
        private readonly IXmlSitemapPageManager siteMapPageManager;

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
