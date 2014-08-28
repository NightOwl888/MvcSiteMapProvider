using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    public interface IXmlSitemapPageInfo
    {
        int Page { get; }
        DateTime LastModifiedDate { get; }
    }
}
