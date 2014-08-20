using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace MvcSiteMapProvider.Xml.Sitemaps
{
    public interface ISitemapsXmlWriterFactory
    {
        ISitemapsXmlWriter Create(XmlWriter writer);
        void Release(ISitemapsXmlWriter sitemapsXmlWriter);
    }
}
