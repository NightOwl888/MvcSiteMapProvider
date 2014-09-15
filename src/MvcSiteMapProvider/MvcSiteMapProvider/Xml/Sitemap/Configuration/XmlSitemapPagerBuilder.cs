using System;
using System.Collections.Generic;
using System.Linq;
using MvcSiteMapProvider.Xml.Sitemap.Paging;

namespace MvcSiteMapProvider.Xml.Sitemap.Configuration
{
    public class XmlSitemapPagerBuilder
        : IXmlSitemapPagerBuilder
    {
        public XmlSitemapPagerBuilder()
            : this(
                maximumPageSize: 50000, 
                pagingInstructionFactory: new PagingInstructionFactory(), 
                xmlSitemapPageInfoFactory: new XmlSitemapPageInfoFactory(), 
                xmlSitemapPageDataFactory: new XmlSitemapPageDataFactory())
        {
        }

        private XmlSitemapPagerBuilder(
            int maximumPageSize,
            IPagingInstructionFactory pagingInstructionFactory,
            IXmlSitemapPageInfoFactory xmlSitemapPageInfoFactory,
            IXmlSitemapPageDataFactory xmlSitemapPageDataFactory
            )
        {
            if (pagingInstructionFactory == null)
                throw new ArgumentNullException("pagingInstructionFactory");
            if (xmlSitemapPageInfoFactory == null)
                throw new ArgumentNullException("xmlSitemapPageInfoFactory");
            if (xmlSitemapPageDataFactory == null)
                throw new ArgumentNullException("xmlSitemapPageDataFactory");

            this.maximumPageSize = maximumPageSize;
            this.pagingInstructionFactory = pagingInstructionFactory;
            this.xmlSitemapPageInfoFactory = xmlSitemapPageInfoFactory;
            this.xmlSitemapPageDataFactory = xmlSitemapPageDataFactory;
        }
        private readonly int maximumPageSize;
        private readonly IPagingInstructionFactory pagingInstructionFactory;
        private readonly IXmlSitemapPageInfoFactory xmlSitemapPageInfoFactory;
        private readonly IXmlSitemapPageDataFactory xmlSitemapPageDataFactory;

        public IXmlSitemapPagerBuilder WithMaximumPageSize(int maximumPageSize)
        {
            return new XmlSitemapPagerBuilder(maximumPageSize, this.pagingInstructionFactory, this.xmlSitemapPageInfoFactory, this.xmlSitemapPageDataFactory);
        }

        public IXmlSitemapPagerBuilder WithPagingInstructionFactory(IPagingInstructionFactory pagingInstructionFactory)
        {
            return new XmlSitemapPagerBuilder(this.maximumPageSize, pagingInstructionFactory, this.xmlSitemapPageInfoFactory, this.xmlSitemapPageDataFactory);
        }

        public IXmlSitemapPagerBuilder WithXmlSitemapPageInfoFactory(IXmlSitemapPageInfoFactory xmlSitemapPageInfoFactory)
        {
            return new XmlSitemapPagerBuilder(this.maximumPageSize, this.pagingInstructionFactory, xmlSitemapPageInfoFactory, this.xmlSitemapPageDataFactory);
        }

        public IXmlSitemapPagerBuilder WithXmlSitemapPageDataFactory(IXmlSitemapPageDataFactory xmlSitemapPageDataFactory)
        {
            return new XmlSitemapPagerBuilder(this.maximumPageSize, this.pagingInstructionFactory, this.xmlSitemapPageInfoFactory, xmlSitemapPageDataFactory);
        }

        public IXmlSitemapPager Create()
        {
            return new XmlSitemapPager(
                this.pagingInstructionFactory, 
                this.xmlSitemapPageInfoFactory, 
                this.xmlSitemapPageDataFactory) 
            { 
                MaximumPageSize = this.maximumPageSize 
            };
        }
    }
}
