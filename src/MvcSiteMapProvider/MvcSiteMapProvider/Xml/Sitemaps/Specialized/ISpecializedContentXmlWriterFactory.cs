﻿using System;
using System.Xml;

namespace MvcSiteMapProvider.Xml.Sitemaps.Specialized
{
    public interface ISpecializedContentXmlWriterFactory
    {
        ISpecializedContentXmlWriter Create(XmlWriter writer);
        void Release(ISpecializedContentXmlWriter specializedContentXmlWriter);
        Type ContentType { get; }
    }
}
