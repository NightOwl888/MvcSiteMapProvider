using System;
using MvcSiteMapProvider.ComponentModel;
using MvcSiteMapProvider.Xml.Sitemap.Paging;
using MvcSiteMapProvider.Xml.Sitemap.Index;

namespace MvcSiteMapProvider.Xml.Sitemap.Configuration
{
    public interface IXmlSitemapPageManagerBuilder
        : IFluentInterface
    {
        IXmlSitemapPageManagerBuilder WithXmlSitemapPager(IXmlSitemapPager xmlSitemapPager);

        IXmlSitemapPageManagerBuilder WithXmlSitemapProviderStrategy(IXmlSitemapProviderStrategy xmlSitemapProviderStrategy);

        IXmlSitemapPageManagerBuilder WithXmlSitemapPageWriter(IXmlSitemapPageWriter xmlSitemapPageWriter);

        IXmlSitemapPageManagerBuilder WithXmlSitemapIndexPageWriter(IXmlSitemapIndexPageWriter xmlSitemapIndexPageWriter);

        IXmlSitemapPageManager Create();
    }
}
