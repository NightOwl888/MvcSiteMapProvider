using System;
using System.Xml;

namespace MvcSiteMapProvider.Xml.Sitemap.Index
{
    public interface IXmlSitemapIndexWriterFactory
    {
        IXmlSitemapIndexWriter Create(XmlWriter writer);
        void Release(IXmlSitemapIndexWriter SitemapXmlWriter);
    }
}
