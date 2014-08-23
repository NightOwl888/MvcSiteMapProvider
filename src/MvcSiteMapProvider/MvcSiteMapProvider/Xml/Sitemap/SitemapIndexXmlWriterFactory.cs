using System;
using System.Xml;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    public class SitemapIndexXmlWriterFactory
        : ISitemapIndexXmlWriterFactory
    {
        public ISitemapIndexXmlWriter Create(XmlWriter writer)
        {
            return new SitemapIndexXmlWriter(writer);
        }

        public void Release(ISitemapIndexXmlWriter sitemapXmlWriter)
        {
            var disposable = sitemapXmlWriter as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }
    }
}
