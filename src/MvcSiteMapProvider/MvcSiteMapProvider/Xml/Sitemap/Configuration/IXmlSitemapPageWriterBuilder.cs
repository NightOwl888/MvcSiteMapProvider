using System;
using MvcSiteMapProvider.ComponentModel;

namespace MvcSiteMapProvider.Xml.Sitemap.Configuration
{
    public interface IXmlSitemapPageWriterBuilder
        : IFluentInterface
    {
        IXmlSitemapPageWriterBuilder WithUrlEntryHelperFactory(IUrlEntryHelperFactory urlEntryHelperFactory);

        IXmlSitemapPageWriterBuilder WithXmlSitemapWriterFactory(IXmlSitemapWriterFactory xmlSitemapWriterFactory);

        IXmlSitemapPageWriter Create();
    }
}
