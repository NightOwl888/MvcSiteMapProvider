using System;
using System.Collections.Generic;
using MvcSiteMapProvider.Xml.Sitemaps.Specialized;

namespace MvcSiteMapProvider.Xml.Sitemaps
{
    public interface IUrlEntry
    {
        string Location { get; }
        string LastModified { get; }
        string ChangeFrequency { get; }
        string Priority { get; }

        IEnumerable<ISpecializedContent> SpecializedContents { get; }
    }
}
