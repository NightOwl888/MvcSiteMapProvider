﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace MvcSiteMapProvider.Xml.Sitemap.Index
{
    public interface IXmlSitemapIndexPageWriter
    {
        void WritePage(XmlWriter writer, string feedName, IEnumerable<IXmlSitemapPageInfo> pageInfo);
    }
}
