using System;
using MvcSiteMapProvider.ComponentModel;
using MvcSiteMapProvider.Xml.Sitemap.Paging;

namespace MvcSiteMapProvider.Xml.Sitemap.Configuration
{
    public interface IXmlSitemapPagerBuilder
        : IFluentInterface
    {
        IXmlSitemapPagerBuilder WithMaximumPageSize(int maximumPageSize);

        IXmlSitemapPagerBuilder WithPagingInstructionFactory(IPagingInstructionFactory pagingInstructionFactory);

        IXmlSitemapPagerBuilder WithXmlSitemapPageInfoFactory(IXmlSitemapPageInfoFactory xmlSitemapPageInfoFactory);

        IXmlSitemapPagerBuilder WithXmlSitemapPageDataFactory(IXmlSitemapPageDataFactory xmlSitemapPageDataFactory);

        IXmlSitemapPager Create();
    }
}
