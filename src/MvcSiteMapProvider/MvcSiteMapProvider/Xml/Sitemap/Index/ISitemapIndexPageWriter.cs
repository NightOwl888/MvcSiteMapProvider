using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace MvcSiteMapProvider.Xml.Sitemap.Index
{
    public interface ISitemapIndexPageWriter
    {
        void WritePage(XmlWriter writer, IEnumerable<int> pageNumbers);
    }
}
