using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized
{
    public interface IVideoContent
        : ISpecializedContent
    {
        string Url { get; set; }
        string HostName { get; set; }
        string Protocol { get; set; }
        string Video { get; set; }
        string ThumbnailUrl { get; set; }
        string ThumbnailHostName { get; set; }
        string ThumbnailProtocol { get; set; }
        string Title { get; set; }
        string Description { get; set; }

        //string Location { get; } // TODO: Work out how to make this resolve (may need extra properties for routing, etc.) // Required
        //string Video { get; } // Required
        //string ThumbnailLocation { get; } // TODO: Work out how to make this resolve (may need extra properties for routing, etc.) // Required
        //string Title { get; } // Required
        //string Description { get; } // Required

        // TODO: Add additional properties from https://support.google.com/webmasters/answer/80472?hl=en
    }
}
