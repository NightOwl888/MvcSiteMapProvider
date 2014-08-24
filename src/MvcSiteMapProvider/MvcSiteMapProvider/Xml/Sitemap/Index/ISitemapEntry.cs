using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Xml.Sitemap.Index
{
    public interface ISitemapEntry
    {
        string Location { get; }
        string LastModified { get; }
    }
}
