using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using MvcSiteMapProvider.Xml.Sitemap.Specialized;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    public class XmlSitemapWriterFactory
        : IXmlSitemapWriterFactory
    {
        public XmlSitemapWriterFactory(
            ISpecializedContentXmlWriterFactoryStrategy specializedContentXmlWriterFactoryStrategy)
        {
            if (specializedContentXmlWriterFactoryStrategy == null)
                throw new ArgumentNullException("specializedContentXmlWriterFactoryStrategy");
            this.specializedContentXmlWriterFactoryStrategy = specializedContentXmlWriterFactoryStrategy;
        }
        private readonly ISpecializedContentXmlWriterFactoryStrategy specializedContentXmlWriterFactoryStrategy;

        public IXmlSitemapWriter Create(XmlWriter writer)
        {
            return new XmlSitemapWriter(writer, this.specializedContentXmlWriterFactoryStrategy);
        }

        public void Release(IXmlSitemapWriter xmlSitemapWriter)
        {
            var disposable = xmlSitemapWriter as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }
    }
}
