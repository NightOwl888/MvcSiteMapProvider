using System;
using System.Collections.Generic;
using System.Linq;
using MvcSiteMapProvider.Xml.Sitemap.Specialized.Video;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized
{
    public class VideoContent
        : IVideoContent
    {
        public VideoContent(
            string thumbnailUrl,
            string title,
            string description,
            string contentLocation,
            string playerLocation)
        {
            if (string.IsNullOrEmpty(thumbnailUrl))
                throw new ArgumentNullException("thumbnailUrl");
            if (string.IsNullOrEmpty(title))
                throw new ArgumentNullException("title");
            if (string.IsNullOrEmpty(description))
                throw new ArgumentNullException("description");
            if (string.IsNullOrEmpty(contentLocation) && string.IsNullOrEmpty(playerLocation))
                throw new ArgumentNullException("Either contentLocation or playerLocation must be supplied");

            this.thumbnailUrl = thumbnailUrl;
            this.title = title;
            this.description = description;
            this.contentLocation = contentLocation;
            this.playerLocation = playerLocation;

            // set defaults
            this.ExpirationDate = DateTime.MinValue;
            this.PublicationDate = DateTime.MinValue;
            this.IsFamilyFriendly = true;
            this.tags = new List<string>();
            this.categories = new List<string>();
            this.countriesAllowed = new List<string>();
            this.countriesNotAllowed = new List<string>();
            this.prices = new List<IVideoContentPrice>();
        }
        private readonly string thumbnailUrl;
        private readonly string title;
        private readonly string description;
        private readonly string contentLocation;
        private readonly string playerLocation;

        private readonly IList<string> tags;
        private readonly IList<string> categories;
        private readonly IList<string> countriesAllowed;
        private readonly IList<string> countriesNotAllowed;
        private readonly IList<IVideoContentPrice> prices;

        private string countriesAllowedString = string.Empty;
        private string countriesNotAllowedString = string.Empty;
        private string platformsAllowedString = string.Empty;
        private string platformsNotAllowedString = string.Empty;

        public string ThumbnailUrl
        {
            get { return this.thumbnailUrl; }
        }

        public string ThumbnailHostName { get; set; }

        public string ThumbnailProtocol { get; set; }

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

        public string CountriesAllowedString
        {
            get 
            {
                if (!string.IsNullOrEmpty(this.countriesAllowedString))
                {
                    return this.countriesAllowedString; 
                }
                if (this.countriesAllowed.Any())
                {
                    return string.Join(" ", this.countriesAllowed.ToArray());
                }
                return string.Empty;
            }
            set
            { 
                this.countriesAllowedString = value; 
            }
        }

        public IList<string> CountriesNotAllowed
        {
            get { return this.countriesNotAllowed; }
        }

        public string CountriesNotAllowedString
        {
            get
            {
                if (!string.IsNullOrEmpty(this.countriesNotAllowedString))
                {
                    return this.countriesNotAllowedString;
                }
                if (this.countriesNotAllowed.Any())
                {
                    return string.Join(" ", this.countriesNotAllowed.ToArray());
                }
                return string.Empty;
            }
            set
            {
                this.countriesNotAllowedString = value;
            }
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

        public string PlatformsAllowedString
        {
            get
            {
                if (!string.IsNullOrEmpty(this.platformsAllowedString))
                {
                    return this.platformsAllowedString;
                }
                else
                {
                    return this.PlatformsAllowed.ToString("F").Replace(", ", " ").ToLower();
                }
            }
            set
            {
                this.platformsAllowedString = value;
            }
        }

        public VideoPlatform PlatformsNotAllowed { get; set; }

        public string PlatformsNotAllowedString
        {
            get
            {
                if (!string.IsNullOrEmpty(this.platformsNotAllowedString))
                {
                    return this.platformsNotAllowedString;
                }
                else
                {
                    return this.PlatformsNotAllowed.ToString("F").Replace(", ", " ").ToLower();
                }
            }
            set
            {
                this.platformsNotAllowedString = value;
            }
        }

        public bool Live { get; set; }
    }
}
