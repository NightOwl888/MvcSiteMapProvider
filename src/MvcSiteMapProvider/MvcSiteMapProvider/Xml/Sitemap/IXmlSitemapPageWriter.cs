using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using MvcSiteMapProvider.Xml.Sitemap.Paging;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    public interface IXmlSitemapPageWriter
    {
        void WritePage(XmlWriter writer, string feedName, IEnumerable<IPagingInstruction> pagingInstructions);
    }
}
