﻿using System;
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
            ISpecializedContentXmlWriterFactoryStrategy specializedContentXmlWriterFactoryStrategy,
            IPreparedUrlEntryFactory preparedUrlEntryFactory
            )
        {
            if (specializedContentXmlWriterFactoryStrategy == null)
                throw new ArgumentNullException("specializedContentXmlWriterFactoryStrategy");
            if (preparedUrlEntryFactory == null)
                throw new ArgumentNullException("preparedUrlEntryFactory");

            this.specializedContentXmlWriterFactoryStrategy = specializedContentXmlWriterFactoryStrategy;
            this.preparedUrlEntryFactory = preparedUrlEntryFactory;
        }
        private readonly ISpecializedContentXmlWriterFactoryStrategy specializedContentXmlWriterFactoryStrategy;
        private readonly IPreparedUrlEntryFactory preparedUrlEntryFactory;

        public IXmlSitemapWriter Create(XmlWriter writer)
        {
            return new XmlSitemapWriter(writer, this.specializedContentXmlWriterFactoryStrategy, this.preparedUrlEntryFactory);
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
