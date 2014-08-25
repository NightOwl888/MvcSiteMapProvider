using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized.Video
{
    public interface IPreparedVideoContent
        : IPreparedSpecializedContent
    {
        string ThumbnailLocation { get; } // Required
        string Title { get; } // Required
        string Description { get; } // Required
        string ContentLocation { get; } // Required if PlayerLocation is not supplied
        string PlayerLocation { get; } // Required if Contentlocation is not supplied
        string PlayerLocationAllowEmbed { get; } // Optional attribute
        string PlayerLocationAutoPlay { get; } // Optional attribute
        string Duration { get; } // Optional (recommended)
        string ExpirationDate { get; } // Optional
        string Rating { get; } // Optional
        string ViewCount { get; } // Optional
        string PublicationDate { get; } // Optional
        string FamilyFriendly { get; } // Optional
        IEnumerable<string> Tags { get; } // Optional, maximum 32 permitted
        IEnumerable<string> Categories { get; } // Optional
        string Restriction { get; } // Optional (space delimited list)
        string RestrictionRelationship { get; } // Required attribute
        string GalleryLocation { get; } // Optional
        string GalleryLocationTitle { get; } // Optional (attribute)
        IEnumerable<IPreparedVideoContentPrice> Prices { get; } // Optional
        string RequiresSubscription { get; } // Optional 
        string Uploader { get; } // Optional
        string UploaderInfo { get; } // Optional attribute
        string Platform { get; } // Optional (space delimited list)
        string PlatformRelationship { get; } // Required attribute
        string Live { get; } // Optional

        // Documentation: https://support.google.com/webmasters/answer/80472?hl=en
    }
}
