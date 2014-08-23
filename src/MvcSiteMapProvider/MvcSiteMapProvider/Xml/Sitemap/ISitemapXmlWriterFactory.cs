using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    public interface ISitemapXmlWriterFactory
    {
        ISitemapXmlWriter Create(XmlWriter writer);
        void Release(ISitemapXmlWriter sitemapXmlWriter);
    }
}
