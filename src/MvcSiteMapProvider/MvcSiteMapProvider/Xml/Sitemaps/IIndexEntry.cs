using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Xml.Sitemaps
{
    public interface IIndexEntry
    {
        string Location { get; }
        string LastModified { get; }
    }
}
