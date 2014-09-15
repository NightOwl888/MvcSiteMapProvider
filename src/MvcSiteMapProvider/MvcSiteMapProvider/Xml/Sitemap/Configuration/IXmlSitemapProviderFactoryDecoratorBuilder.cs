using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Xml.Sitemap.Configuration
{
    public interface IXmlSitemapProviderFactoryDecoratorBuilder
    {
        IXmlSitemapProviderFactoryDecoratorBuilder WithXmlSitemapProviderDecoratorFactory(IXmlSitemapProviderDecoratorFactory xmlSitemapProviderDecoratorFactory);

        IXmlSitemapProviderFactory Create(IXmlSitemapProviderFactory xmlSitemapProviderFactory);
    }
}
