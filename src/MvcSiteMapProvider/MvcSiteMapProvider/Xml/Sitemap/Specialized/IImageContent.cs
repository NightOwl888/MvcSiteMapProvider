using System;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized
{
    public interface IImageContent
        : ISpecializedContent
    {
        string Url { get; set; }
        string Protocol { get; set; }
        string HostName { get; set; }
        string Caption { get; set; }
        string GeoLocation { get; set; }
        string Title { get; set; }
        string LicenseUrl { get; set; }
        string LicenseProtocol { get; set; }
        string LicenseHostName { get; set; }
        

        //string Location { get; } // TODO: Work out how to make this resolve (may need extra properties for routing, etc.) // Required
        //string Caption { get; } // Required
        //string GeoLocation { get; } // Optional
        //string Title { get; } // Optional
        //string License { get; } // TODO: Work out how to make this resolve (may need extra properties for routing, etc.) // Optional

        // Documentation: https://support.google.com/webmasters/answer/178636?hl=en
    }
}
