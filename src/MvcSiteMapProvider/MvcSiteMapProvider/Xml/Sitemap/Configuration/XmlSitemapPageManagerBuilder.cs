using System;
using MvcSiteMapProvider.Xml.Sitemap.Index;
using MvcSiteMapProvider.Xml.Sitemap.Paging;

namespace MvcSiteMapProvider.Xml.Sitemap.Configuration
{
    public class XmlSitemapPageManagerBuilder
        : IXmlSitemapPageManagerBuilder
    {
        public XmlSitemapPageManagerBuilder()
            : this(new XmlSitemapPagerBuilder().Create(), new XmlSitemapProviderStrategyBuilder().Create(), new XmlSitemapPageWriterBuilder().Create(), new XmlSitemapIndexPageWriterBuilder().Create())
        {
        }

        private XmlSitemapPageManagerBuilder(
            IXmlSitemapPager xmlSitemapPager,
            IXmlSitemapProviderStrategy xmlSitemapProviderStrategy,
            IXmlSitemapPageWriter xmlSitemapPageWriter,
            IXmlSitemapIndexPageWriter xmlSitemapIndexPageWriter
            )
        {
            if (xmlSitemapPager == null)
                throw new ArgumentNullException("xmlSitemapPager");
            if (xmlSitemapProviderStrategy == null)
                throw new ArgumentNullException("xmlSitemapProviderStrategy");
            if (xmlSitemapPageWriter == null)
                throw new ArgumentNullException("xmlSitemapPageWriter");
            if (xmlSitemapIndexPageWriter == null)
                throw new ArgumentNullException("xmlSitemapIndexPageWriter");

            this.xmlSitemapPager = xmlSitemapPager;
            this.xmlSitemapProviderStrategy = xmlSitemapProviderStrategy;
            this.xmlSitemapPageWriter = xmlSitemapPageWriter;
            this.xmlSitemapIndexPageWriter = xmlSitemapIndexPageWriter;
        }
        private readonly IXmlSitemapPager xmlSitemapPager;
        private readonly IXmlSitemapProviderStrategy xmlSitemapProviderStrategy;
        private readonly IXmlSitemapPageWriter xmlSitemapPageWriter;
        private readonly IXmlSitemapIndexPageWriter xmlSitemapIndexPageWriter; 

        public IXmlSitemapPageManagerBuilder WithXmlSitemapPager(IXmlSitemapPager xmlSitemapPager)
        {
            return new XmlSitemapPageManagerBuilder(xmlSitemapPager, this.xmlSitemapProviderStrategy, this.xmlSitemapPageWriter, this.xmlSitemapIndexPageWriter);
        }

        public IXmlSitemapPageManagerBuilder WithXmlSitemapProviderStrategy(IXmlSitemapProviderStrategy xmlSitemapProviderStrategy)
        {
            return new XmlSitemapPageManagerBuilder(this.xmlSitemapPager, xmlSitemapProviderStrategy, this.xmlSitemapPageWriter, this.xmlSitemapIndexPageWriter);
        }

        public IXmlSitemapPageManagerBuilder WithXmlSitemapPageWriter(IXmlSitemapPageWriter xmlSitemapPageWriter)
        {
            return new XmlSitemapPageManagerBuilder(this.xmlSitemapPager, this.xmlSitemapProviderStrategy, xmlSitemapPageWriter, this.xmlSitemapIndexPageWriter);
        }

        public IXmlSitemapPageManagerBuilder WithXmlSitemapIndexPageWriter(IXmlSitemapIndexPageWriter xmlSitemapIndexPageWriter)
        {
            return new XmlSitemapPageManagerBuilder(this.xmlSitemapPager, this.xmlSitemapProviderStrategy, this.xmlSitemapPageWriter, xmlSitemapIndexPageWriter);
        }

        public IXmlSitemapPageManager Create()
        {
            return new XmlSitemapPageManager(this.xmlSitemapPager, this.xmlSitemapProviderStrategy, this.xmlSitemapPageWriter, xmlSitemapIndexPageWriter);
        }
    }
}
