using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized
{
    public interface IPreparedImageContent
        : IPreparedSpecializedContent
    {
        string Location { get; } // TODO: Work out how to make this resolve (may need extra properties for routing, etc.) // Required
        string Caption { get; } // Required
        string GeoLocation { get; } // Optional
        string Title { get; } // Optional
        string License { get; } // TODO: Work out how to make this resolve (may need extra properties for routing, etc.) // Optional

        // Documentation: https://support.google.com/webmasters/answer/178636?hl=en
    }
}
