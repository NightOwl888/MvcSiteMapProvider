using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Xml;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    public interface IXmlSitemapFeed
    {
        string Name { get; }
        bool WritePage(int page, Stream output);
    }
}
