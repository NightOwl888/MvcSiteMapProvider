//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Xml;

//namespace MvcSiteMapProvider.Xml.Sitemap.Configuration
//{
//    public class XmlSitemapFeedBuilder
//        : IXmlSitemapFeedNameBuilder, IXmlSitemapFeedBuilder
//    {
//        public XmlSitemapFeedBuilder()
//            : this(
//                feedName: string.Empty,
//                xmlWriterSettings: new XmlWriterSettings() { Encoding = new System.Text.UTF8Encoding(false) }, 
//                xmlWriterFactory: new XmlWriterFactory(), 
//                xmlSitemapPageManager: new XmlSitemapPageManagerBuilder().Create())
//        {
//        }

//        private XmlSitemapFeedBuilder(
//            string feedName,
//            XmlWriterSettings xmlWriterSettings,
//            IXmlWriterFactory xmlWriterFactory,
//            IXmlSitemapPageManager xmlSitemapPageManager
//            )
//        {
//            if (xmlWriterSettings == null)
//                throw new ArgumentNullException("xmlWriterSettings");
//            if (xmlWriterFactory == null)
//                throw new ArgumentNullException("xmlWriterFactory");
//            if (xmlSitemapPageManager == null)
//                throw new ArgumentNullException("xmlSitemapPageManager");

//            this.feedName = feedName;
//            this.xmlWriterSettings = xmlWriterSettings;
//            this.xmlWriterFactory = xmlWriterFactory;
//            this.xmlSitemapPageManager = xmlSitemapPageManager;
//        }
//        private readonly string feedName;
//        private readonly XmlWriterSettings xmlWriterSettings;
//        private readonly IXmlWriterFactory xmlWriterFactory;
//        private readonly IXmlSitemapPageManager xmlSitemapPageManager;
        
//        public IXmlSitemapFeedBuilder WithName(string feedName)
//        {
//            return new XmlSitemapFeedBuilder(feedName, this.xmlWriterSettings, this.xmlWriterFactory, this.xmlSitemapPageManager);
//        }

//        public IXmlSitemapFeedBuilder WithXmlWriterSettings(XmlWriterSettings xmlWriterSettings)
//        {
//            return new XmlSitemapFeedBuilder(this.feedName, xmlWriterSettings, this.xmlWriterFactory, this.xmlSitemapPageManager);
//        }

//        public IXmlSitemapFeedBuilder WithXmlWriterFactory(IXmlWriterFactory xmlWriterFactory)
//        {
//            return new XmlSitemapFeedBuilder(this.feedName, this.xmlWriterSettings, xmlWriterFactory, this.xmlSitemapPageManager);
//        }

//        public IXmlSitemapFeedBuilder WithXmlSitemapPageManager(IXmlSitemapPageManager xmlSitemapPageManager)
//        {
//            return new XmlSitemapFeedBuilder(this.feedName, this.xmlWriterSettings, this.xmlWriterFactory, xmlSitemapPageManager);
//        }

//        public IXmlSitemapFeed Create()
//        {
//            return new XmlSitemapFeed(this.feedName, this.xmlWriterSettings, this.xmlWriterFactory, this.xmlSitemapPageManager);
//        }
//    }
//}
