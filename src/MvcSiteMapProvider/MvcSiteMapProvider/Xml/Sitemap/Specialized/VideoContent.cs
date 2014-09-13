using System;
using System.Collections.Generic;
using MvcSiteMapProvider.Xml.Sitemap.Specialized.Video;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized
{
    public class VideoContent
        : IVideoContent
    {
        public VideoContent(
            string thumbnailLocation,
            string title,
            string description,
            string contentLocation,
            string playerLocation
            )
            : this(thumbnailLocation: thumbnailLocation,
                title: title,
                description: description,
                contentLocation: contentLocation,
                playerLocation: playerLocation, 
                tags: new List<string>(),
                categories: new List<string>(), 
                countriesAllowed: new List<string>(), 
                countriesNotAllowed: new List<string>(), 
                prices: new List<IVideoContentPrice>())
        {
        }

        internal VideoContent(
            string thumbnailLocation,
            string title,
            string description,
            string contentLocation,
            string playerLocation,
            IList<string> tags,
            IList<string> categories,
            IList<string> countriesAllowed,
            IList<string> countriesNotAllowed,
            IList<IVideoContentPrice> prices
            )
        {
            if (string.IsNullOrEmpty(thumbnailLocation))
                throw new ArgumentNullException("thumbnailLocation");
            if (string.IsNullOrEmpty(title))
                throw new ArgumentNullException("title");
            if (string.IsNullOrEmpty(description))
                throw new ArgumentNullException("description");
            if (string.IsNullOrEmpty(contentLocation) && string.IsNullOrEmpty(playerLocation))
                throw new ArgumentNullException("Either contentLocation or playerLocation must be supplied");

            this.thumbnailLocation = thumbnailLocation;
            this.title = title;
            this.description = description;
            this.contentLocation = contentLocation;
            this.playerLocation = playerLocation;
            this.tags = tags;
            this.categories = categories;
            this.countriesAllowed = countriesAllowed;
            this.countriesNotAllowed = countriesNotAllowed;
            this.prices = prices;

            // set defaults
            this.ExpirationDate = DateTime.MinValue;
            this.PublicationDate = DateTime.MinValue;
            this.IsFamilyFriendly = true;
        }

        private readonly string thumbnailLocation;
        private readonly string title;
        private readonly string description;
        private readonly string contentLocation;
        private readonly string playerLocation;

        private readonly IList<string> tags;
        private readonly IList<string> categories;
        private readonly IList<string> countriesAllowed;
        private readonly IList<string> countriesNotAllowed;
        private readonly IList<IVideoContentPrice> prices;

        public string ThumbnailLocation
        {
            get { return this.thumbnailLocation; }
        }

        public string ThumbnailLocationProtocol { get; set; }

        public string ThumbnailLocationHostName { get; set; }

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

        public string ContentLocationProtocol { get; set; }

        public string ContentLocationHostName { get; set; }

        public string PlayerLocation 
        { 
            get { return this.playerLocation; } 
        }

        public string PlayerLocationProtocol { get; set; }

        public string PlayerLocationHostName { get; set; }

        public bool? PlayerLocationAllowEmbed { get; set; }

        public string PlayerLocationAutoPlay { get; set; }

        public int Duration { get; set; }

        public DateTime ExpirationDate { get; set; }

        public double Rating { get; set; }

        public int ViewCount { get; set; }

        public DateTime PublicationDate { get; set; }

        public bool IsFamilyFriendly { get; set; }

        public IList<string> Tags
        {
            get { return this.tags; }
        }

        public IList<string> Categories
        {
            get { return this.categories; }
        }

        public IList<string> CountriesAllowed
        {
            get { return this.countriesAllowed; }
        }

        public IList<string> CountriesNotAllowed
        {
            get { return this.countriesNotAllowed; }
        }

        public string GalleryLocation { get; set; }

        public string GalleryLocationProtocol { get; set; }

        public string GalleryLocationHostName { get; set; }

        public string GalleryLocationTitle { get; set; }

        public IList<IVideoContentPrice> Prices
        {
            get { return this.prices; }
        }

        public bool RequiresSubscription { get; set; }

        public string Uploader { get; set; }

        public string UploaderInfo { get; set; }

        public string UploaderInfoProtocol { get; set; }

        public string UploaderInfoHostName { get; set; }

        public VideoPlatform PlatformsAllowed { get; set; }

        public VideoPlatform PlatformsNotAllowed { get; set; }

        public bool Live { get; set; }
    }
}
