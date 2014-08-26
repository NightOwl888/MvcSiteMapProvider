using System;
using System.Collections.Generic;
using MvcSiteMapProvider.Xml.Sitemap.Specialized;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    public interface IUrlEntry
    {
        string Url { get; set; }
        string Protocol { get; set; }
        string HostName { get; set; }
        DateTime LastModifiedDate { get; set; }
        ChangeFrequency ChangeFrequency { get; set; }
        UpdatePriority UpdatePriority { get; set; }
        IList<ISpecializedContent> SpecializedContent { get; }
    }
}
