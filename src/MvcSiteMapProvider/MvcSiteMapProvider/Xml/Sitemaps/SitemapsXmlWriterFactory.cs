using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using MvcSiteMapProvider.Xml.Sitemaps.Specialized;

namespace MvcSiteMapProvider.Xml.Sitemaps
{
    public class SitemapsXmlWriterFactory
        : ISitemapsXmlWriterFactory
    {
        public SitemapsXmlWriterFactory(
            ISpecializedContentXmlWriterFactoryStrategy specializedContentXmlWriterFactoryStrategy)
        {
            if (specializedContentXmlWriterFactoryStrategy == null)
                throw new ArgumentNullException("specializedContentXmlWriterFactoryStrategy");
            this.specializedContentXmlWriterFactoryStrategy = specializedContentXmlWriterFactoryStrategy;
        }
        private readonly ISpecializedContentXmlWriterFactoryStrategy specializedContentXmlWriterFactoryStrategy;

        public ISitemapsXmlWriter Create(XmlWriter writer)
        {
            return new SitemapsXmlWriter(writer, this.specializedContentXmlWriterFactoryStrategy);
        }

        public void Release(ISitemapsXmlWriter sitemapsXmlWriter)
        {
            var disposable = sitemapsXmlWriter as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }
    }
}
