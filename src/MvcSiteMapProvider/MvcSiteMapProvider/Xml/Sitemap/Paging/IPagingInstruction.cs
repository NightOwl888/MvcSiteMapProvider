using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Xml.Sitemap.Paging
{
    public interface IPagingInstruction
    {
        int Skip { get; }
        int Take { get; }
        IXmlSitemapProvider UrlEntryProvider { get; }
    }
}
