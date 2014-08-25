using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized.Video
{
    public class PreparedVideoContent
        : IPreparedVideoContent
    {
        public PreparedVideoContent(
            string thumbnailLocation,
            string title,
            string description,
            string contentLocation,
            string playerLocation,
            string playerLocationAllowEmbed,
            string playerLocationAutoPlay,
            string duration,
            string expirationDate,
            string rating,
            string viewCount,
            string publicationDate,
            string familyFriendly,
            IEnumerable<string> tags,
            IEnumerable<string> categories,
            string restriction,
            string restrictionRelationship,
            string galleryLocation,
            string galleryLocationTitle,
            IEnumerable<IPreparedVideoContentPrice> prices,
            string requiresSubscription,
            string uploader,
            string uploaderInfo,
            string platform,
            string platformRelationship,
            string live)
        {
            if (string.IsNullOrEmpty(thumbnailLocation))
                throw new ArgumentNullException("thumbnailLocation");
            if (string.IsNullOrEmpty(title))
                throw new ArgumentNullException("title");
            if (string.IsNullOrEmpty(description))
                throw new ArgumentNullException("description");

            this.thumbnailLocation = thumbnailLocation;
            this.title = title;
            this.description = description;
            this.contentLocation = contentLocation;
            this.playerLocation = playerLocation;
            this.playerLocationAllowEmbed = playerLocationAllowEmbed;
            this.playerLocationAutoPlay = playerLocationAutoPlay;
            this.duration = duration;
            this.expirationDate = expirationDate;
            this.rating = rating;
            this.viewCount = viewCount;
            this.publicationDate= publicationDate;
            this.familyFriendly = familyFriendly;
            this.tags = tags;
            this.categories = categories;
            this.restriction = restriction;
            this.restrictionRelationship = restrictionRelationship;
            this.galleryLocation = galleryLocation;
            this.galleryLocationTitle = galleryLocationTitle;
            this.prices = prices;
            this.requiresSubscription = requiresSubscription;
            this.uploader = uploader;
            this.uploaderInfo = uploaderInfo;
            this.platform = platform;
            this.platformRelationship = platformRelationship;
            this.live = live;
        }
        private readonly string thumbnailLocation;
        private readonly string title;
        private readonly string description;
        private readonly string contentLocation;
        private readonly string playerLocation;
        private readonly string playerLocationAllowEmbed;
        private readonly string playerLocationAutoPlay;
        private readonly string duration;
        private readonly string expirationDate;
        private readonly string rating;
        private readonly string viewCount;
        private readonly string publicationDate;
        private readonly string familyFriendly;
        private readonly IEnumerable<string> tags;
        private readonly IEnumerable<string> categories;
        private readonly string restriction;
        private readonly string restrictionRelationship;
        private readonly string galleryLocation;
        private readonly string galleryLocationTitle;
        private readonly IEnumerable<IPreparedVideoContentPrice> prices;
        private readonly string requiresSubscription;
        private readonly string uploader;
        private readonly string uploaderInfo;
        private readonly string platform;
        private readonly string platformRelationship;
        private readonly string live;

        public string ThumbnailLocation
        {
            get { return this.thumbnailLocation; }
        }

        public string Title
        {
            get { return this.title; }
        }

        public string Description
        {
            get { return this.description; }
        }

        public string ContentLocation
        {
            get { return this.contentLocation; }
        }

        public string PlayerLocation
        {
            get { return this.playerLocation; }
        }

        public string PlayerLocationAllowEmbed
        {
            get { return this.playerLocationAllowEmbed; }
        }

        public string PlayerLocationAutoPlay
        {
            get { return this.playerLocationAutoPlay; }
        }

        public string Duration
        {
            get { return this.duration; }
        }

        public string ExpirationDate
        {
            get { return this.expirationDate; }
        }

        public string Rating
        {
            get { return this.rating; }
        }

        public string ViewCount
        {
            get { return this.viewCount; }
        }

        public string PublicationDate
        {
            get { return this.publicationDate; }
        }

        public string FamilyFriendly
        {
            get { return this.familyFriendly; }
        }

        public IEnumerable<string> Tags
        {
            get { return this.tags; }
        }

        public IEnumerable<string> Categories
        {
            get { return this.categories; }
        }

        public string Restriction
        {
            get { return this.restriction; }
        }

        public string RestrictionRelationship
        {
            get { return this.restrictionRelationship; }
        }

        public string GalleryLocation
        {
            get { return this.galleryLocation; }
        }

        public string GalleryLocationTitle
        {
            get { return this.galleryLocationTitle; }
        }

        public IEnumerable<IPreparedVideoContentPrice> Prices
        {
            get { return this.prices; }
        }

        public string RequiresSubscription
        {
            get { return this.requiresSubscription; }
        }

        public string Uploader
        {
            get { return this.uploader; }
        }

        public string UploaderInfo
        {
            get { return this.uploaderInfo; }
        }

        public string Platform
        {
            get { return this.platform; }
        }

        public string PlatformRelationship
        {
            get { return this.platformRelationship; }
        }

        public string Live
        {
            get { return this.live; }
        }
    }
}
