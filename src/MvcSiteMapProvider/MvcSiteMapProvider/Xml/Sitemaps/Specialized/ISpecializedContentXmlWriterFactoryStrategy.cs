﻿using System;
using System.Collections.Generic;

namespace MvcSiteMapProvider.Xml.Sitemaps.Specialized
{
    public interface ISpecializedContentXmlWriterFactoryStrategy
    {
        ISpecializedContentXmlWriterFactory GetFactory(Type specializedContentType);
        IEnumerable<Type> GetRegisteredContentTypes();
    }
}
