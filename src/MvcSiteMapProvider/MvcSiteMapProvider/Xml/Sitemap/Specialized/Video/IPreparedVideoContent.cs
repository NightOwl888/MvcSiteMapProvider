using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized.Video
{
    public interface IPreparedVideoContent
        : IPreparedSpecializedContent
    {
        string Location { get; } // TODO: Work out how to make this resolve (may need extra properties for routing, etc.) // Required
        string Video { get; } // Required
        string ThumbnailLocation { get; } // TODO: Work out how to make this resolve (may need extra properties for routing, etc.) // Required
        string Title { get; } // Required
        string Description { get; } // Required

        // TODO: Add additional properties from https://support.google.com/webmasters/answer/80472?hl=en
    }
}
