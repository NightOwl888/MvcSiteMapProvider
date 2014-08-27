using System;
using System.Xml;

namespace MvcSiteMapProvider.Xml.Sitemap.Index
{
    public class XmlSitemapIndexWriterFactory
        : IXmlSitemapIndexWriterFactory
    {
        public IXmlSitemapIndexWriter Create(XmlWriter writer)
        {
            return new XmlSitemapIndexWriter(writer);
        }

        public void Release(IXmlSitemapIndexWriter xmlSitemapWriter)
        {
            var disposable = xmlSitemapWriter as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }
    }
}
