using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using MvcSiteMapProvider.Xml.Sitemap.Specialized;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    public class SitemapXmlWriterFactory
        : ISitemapXmlWriterFactory
    {
        public SitemapXmlWriterFactory(
            ISpecializedContentXmlWriterFactoryStrategy specializedContentXmlWriterFactoryStrategy)
        {
            if (specializedContentXmlWriterFactoryStrategy == null)
                throw new ArgumentNullException("specializedContentXmlWriterFactoryStrategy");
            this.specializedContentXmlWriterFactoryStrategy = specializedContentXmlWriterFactoryStrategy;
        }
        private readonly ISpecializedContentXmlWriterFactoryStrategy specializedContentXmlWriterFactoryStrategy;

        public ISitemapXmlWriter Create(XmlWriter writer)
        {
            return new SitemapXmlWriter(writer, this.specializedContentXmlWriterFactoryStrategy);
        }

        public void Release(ISitemapXmlWriter sitemapXmlWriter)
        {
            var disposable = sitemapXmlWriter as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }
    }
}
