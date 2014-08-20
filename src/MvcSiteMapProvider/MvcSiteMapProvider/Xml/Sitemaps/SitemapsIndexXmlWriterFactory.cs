using System;
using System.Xml;

namespace MvcSiteMapProvider.Xml.Sitemaps
{
    public class SitemapsIndexXmlWriterFactory
        : ISitemapsIndexXmlWriterFactory
    {
        public ISitemapsIndexXmlWriter Create(XmlWriter writer)
        {
            return new SitemapsIndexXmlWriter(writer);
        }

        public void Release(ISitemapsIndexXmlWriter sitemapsXmlWriter)
        {
            var disposable = sitemapsXmlWriter as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }
    }
}
