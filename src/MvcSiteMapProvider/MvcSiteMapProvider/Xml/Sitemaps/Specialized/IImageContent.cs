using System;

namespace MvcSiteMapProvider.Xml.Sitemaps.Specialized
{
    public interface IImageContent
        : ISpecializedContent
    {
        string Location { get; } // TODO: Work out how to make this resolve (may need extra properties for routing, etc.) // Required
        string Caption { get; } // Required
        string GeoLocation { get; } // Optional
        string Title { get; } // Optional
        string License { get; } // TODO: Work out how to make this resolve (may need extra properties for routing, etc.) // Optional

        // Documentation: https://support.google.com/webmasters/answer/178636?hl=en
    }
}
