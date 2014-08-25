using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized.Video
{
    public interface IPreparedVideoContentPrice
    {
        string Price { get; }
        string Currency { get; } // Optional attribute
        string Type { get; } // Optional attribute
        string Resolution { get; } // Optional attribute
    }
}
