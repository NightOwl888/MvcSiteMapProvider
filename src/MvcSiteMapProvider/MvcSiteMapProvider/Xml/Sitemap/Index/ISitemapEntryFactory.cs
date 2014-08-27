using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Xml.Sitemap.Index
{
    public interface ISitemapEntryFactory
    {
        ISitemapEntry Create(string location);
        ISitemapEntry Create(string location, DateTime lastModified);
    }
}
