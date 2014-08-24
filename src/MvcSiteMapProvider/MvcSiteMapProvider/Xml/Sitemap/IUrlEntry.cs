using System;
using System.Collections.Generic;
using MvcSiteMapProvider.Xml.Sitemap.Specialized;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    public interface IUrlEntry
    {
        string Protocol { get; set; }
        string HostName { get; set; }
        string Url { get; set; }
        DateTime LastModifiedDate { get; set; }
        ChangeFrequency ChangeFrequency { get; set; }
        UpdatePriority UpdatePriority { get; set; }

        //string Location { get; }
        //string LastModified { get; }
        //string ChangeFrequency { get; }
        //string Priority { get; }

        IList<ISpecializedContent> SpecializedContents { get; }
    }
}
