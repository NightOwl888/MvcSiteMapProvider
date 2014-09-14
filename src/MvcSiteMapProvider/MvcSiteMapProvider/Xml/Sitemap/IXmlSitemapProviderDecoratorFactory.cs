using System;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    public interface IXmlSitemapProviderDecoratorFactory
    {
        IXmlSitemapProvider Create(IXmlSitemapProvider xmlSiteMapProvider);
    }
}
