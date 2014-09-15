using System;
using System.IO;
using System.Xml;
using MvcSiteMapProvider.Xml.Sitemap.Paging;

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

        public virtual string Name
        {
            get { return this.name; }
        }

        public virtual bool WritePage(int page, Stream output)
        {
            return this.WritePage(page, output, this.settings);
        }

        public virtual bool WritePage(int page, Stream output, XmlWriterSettings settings)
        {
            var xmlWriterSettings = (settings != null) ? settings : this.settings;
            using (var writer = this.xmlWriterFactory.Create(output, xmlWriterSettings))
            {
                return this.siteMapPageManager.WritePage(writer, this.Name, page);
            }
        }

        public virtual IXmlSitemapPageData GetPageData()
        {
            return this.siteMapPageManager.GetPageData(this.name);
        }
    }
}
