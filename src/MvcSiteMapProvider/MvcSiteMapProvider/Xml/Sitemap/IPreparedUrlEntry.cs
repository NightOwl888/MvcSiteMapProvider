using System;
using System.Collections.Generic;
using MvcSiteMapProvider.Xml.Sitemap.Specialized;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    public interface IPreparedUrlEntry
    {
        string Location { get; }
        string LastModified { get; }
        string ChangeFrequency { get; }
        string Priority { get; }
    }
}
