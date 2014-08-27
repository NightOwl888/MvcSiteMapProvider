using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    public interface IXmlSitemapWriterFactory
    {
        IXmlSitemapWriter Create(XmlWriter writer);
        void Release(IXmlSitemapWriter xmlSitemapWriter);
    }
}
