using System;
using System.Xml;

namespace MvcSiteMapProvider.Xml.Sitemaps
{
    public interface ISitemapsIndexXmlWriterFactory
    {
        ISitemapsIndexXmlWriter Create(XmlWriter writer);
        void Release(ISitemapsIndexXmlWriter sitemapsXmlWriter);
    }
}
