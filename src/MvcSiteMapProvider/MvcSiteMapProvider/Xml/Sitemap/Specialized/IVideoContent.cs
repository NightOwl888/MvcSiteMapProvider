using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvcSiteMapProvider.Xml.Sitemap.Specialized.Video;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized
{
    public interface IVideoContent
        : ISpecializedContent
    {
        string ThumbnailLocation { get; } // Required
        string ThumbnailLocationProtocol { get; set; }
        string ThumbnailLocationHostName { get; set; }
        string Title { get; } // Required
        string Description { get; } // Required

        string ContentLocation { get; } // Required if PlayerLocation is not supplied
        string ContentLocationProtocol { get; set; }
        string ContentLocationHostName { get; set; }
        string PlayerLocation { get; } // Required if ContentLocation is not supplied
        string PlayerLocationProtocol { get; set; }
        string PlayerLocationHostName { get; set; }
        bool? PlayerLocationAllowEmbed { get; set; }
        string PlayerLocationAutoPlay { get; set; }
        int Duration { get; set; } // Optional (recommended)
        DateTime ExpirationDate { get; set; } // Optional
        double Rating { get; set; } // Optional
        int ViewCount { get; set; } // Optional
        DateTime PublicationDate { get; set; } // Optional
        bool IsFamilyFriendly { get; set; } // Optional
        IList<string> Tags { get; } // Optional, maximum 32 permitted
        IList<string> Categories { get; } // Optional
        IList<string> CountriesAllowed { get; } // Restriction
        IList<string> CountriesNotAllowed{ get; }
        string GalleryLocation { get; set; } // Optional
        string GalleryLocationProtocol { get; set; }
        string GalleryLocationHostName { get; set; }
        string GalleryLocationTitle { get; set; } // Optional (attribute)
        IList<IVideoContentPrice> Prices { get; } // Optional
        bool RequiresSubscription { get; set; } // Optional 
        string Uploader { get; set; } // Optional
        string UploaderInfo { get; set; } // Optional attribute
        string UploaderInfoProtocol { get; set; }
        string UploaderInfoHostName { get; set; }
        VideoPlatform PlatformsAllowed { get; set; } // Optional (space delimited list)
        VideoPlatform PlatformsNotAllowed { get; set; }
        bool Live { get; set; } // Optional

        // Documentation: https://support.google.com/webmasters/answer/80472?hl=en
    }
}
