using System;
using System.Xml;

namespace MvcSiteMapProvider.Xml.Sitemap.Index
{
    public interface ISitemapIndexXmlWriterFactory
    {
        ISitemapIndexXmlWriter Create(XmlWriter writer);
        void Release(ISitemapIndexXmlWriter SitemapXmlWriter);
    }
}
