using System;
using System.Collections.Generic;
using System.Xml;
using MvcSiteMapProvider.ComponentModel;

namespace MvcSiteMapProvider.Xml.Sitemap.Configuration
{
    public interface IXmlSitemapFeedBuilder
        : IFluentInterface
    {
        IXmlSitemapFeedBuilder WithXmlWriterSettings(XmlWriterSettings xmlWriterSettings);

        IXmlSitemapFeedBuilder WithXmlWriterFactory(IXmlWriterFactory xmlWriterFactory);

        // TODO: Add a method for each pertinent xml writer setting, and use a builder to create an XmlWriterSettings object

        IXmlSitemapFeedBuilder WithXmlSitemapPageManager(IXmlSitemapPageManager xmlSitemapPageManager);

        IXmlSitemapFeed Create();
    }
}
