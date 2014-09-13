using System;
using System.Collections.Generic;
using System.Linq;
using MvcSiteMapProvider.Xml.Sitemap.Specialized.Video;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized
{
    public class VideoContentBuilder
        : IVideoContentBuilder
    {
        public VideoContentBuilder()
            : this(
                thumbnailLocation: string.Empty,
                thumbnailLocationProtocol: string.Empty,
                thumbnailLocationHostName: string.Empty,
                title: string.Empty,
                description: string.Empty,
                contentLocation: string.Empty,
                contentLocationProtocol: string.Empty,
                contentLocationHostName: string.Empty,
                playerLocation: string.Empty,
                playerLocationProtocol: string.Empty,
                playerLocationHostName: string.Empty,
                playerLocationAllowEmbed: null,
                playerLocationAutoPlay: string.Empty,
                duration: 0,
                expirationDate: DateTime.MinValue,
                rating: 0,
                viewCount: 0,
                publicationDate: DateTime.MinValue,
                isFamilyFriendly: true,
                tags: new List<string>(),
                categories: new List<string>(),
                countriesAllowed: new List<string>(),
                countriesNotAllowed: new List<string>(),
                galleryLocation: string.Empty,
                galleryLocationProtocol: string.Empty,
                galleryLocationHostName: string.Empty,
                galleryLocationTitle: string.Empty,
                prices: new List<IVideoContentPrice>(),
                requiresSubscription: false,
                uploader: string.Empty,
                uploaderInfo: string.Empty,
                uploaderInfoProtocol: string.Empty,
                uploaderInfoHostName: string.Empty,
                platformsAllowed: VideoPlatform.Undefined,
                platformsNotAllowed: VideoPlatform.Undefined,
                live: false)
        {
        }

        private VideoContentBuilder(
            string thumbnailLocation,
            string thumbnailLocationProtocol,
            string thumbnailLocationHostName,
            string title,
            string description,
            string contentLocation,
            string contentLocationProtocol,
            string contentLocationHostName,
            string playerLocation,
            string playerLocationProtocol,
            string playerLocationHostName,
            bool? playerLocationAllowEmbed,
            string playerLocationAutoPlay,
            int duration,
            DateTime expirationDate,
            double rating,
            int viewCount,
            DateTime publicationDate,
            bool isFamilyFriendly,
            IList<string> tags,
            IList<string> categories,
            IList<string> countriesAllowed,
            IList<string> countriesNotAllowed,
            string galleryLocation,
            string galleryLocationProtocol,
            string galleryLocationHostName,
            string galleryLocationTitle,
            IList<IVideoContentPrice> prices,
            bool requiresSubscription,
            string uploader,
            string uploaderInfo,
            string uploaderInfoProtocol,
            string uploaderInfoHostName,
            VideoPlatform platformsAllowed,
            VideoPlatform platformsNotAllowed,
            bool live)
        {
            this.thumbnailLocation = thumbnailLocation;
            this.thumbnailLocationProtocol = thumbnailLocationProtocol;
            this.thumbnailLocationHostName = thumbnailLocationHostName;
            this.title = title;
            this.description = description;
            this.contentLocation = contentLocation;
            this.contentLocationProtocol = contentLocationProtocol;
            this.contentLocationHostName = contentLocationHostName;
            this.playerLocation = playerLocation;
            this.playerLocationProtocol = playerLocationProtocol;
            this.playerLocationHostName = playerLocationHostName;
            this.playerLocationAllowEmbed = playerLocationAllowEmbed;
            this.playerLocationAutoPlay = playerLocationAutoPlay;
            this.duration = duration;
            this.expirationDate = expirationDate;
            this.rating = rating;
            this.viewCount = viewCount;
            this.publicationDate = publicationDate;
            this.isFamilyFriendly = isFamilyFriendly;
            this.tags = tags;
            this.categories = categories;
            this.countriesAllowed = countriesAllowed;
            this.countriesNotAllowed = countriesNotAllowed;
            this.galleryLocation = galleryLocation;
            this.galleryLocationProtocol = galleryLocationProtocol;
            this.galleryLocationHostName = galleryLocationHostName;
            this.galleryLocationTitle = galleryLocationTitle;
            this.prices = prices;
            this.requiresSubscription = requiresSubscription;
            this.uploader = uploader;
            this.uploaderInfo = uploaderInfo;
            this.uploaderInfoProtocol = uploaderInfoProtocol;
            this.uploaderInfoHostName = uploaderInfoHostName;
            this.platformsAllowed = platformsAllowed;
            this.platformsNotAllowed = platformsNotAllowed;
            this.live = live;
        }

        private readonly string thumbnailLocation;
        private readonly string thumbnailLocationProtocol;
        private readonly string thumbnailLocationHostName;
        private readonly string title;
        private readonly string description;
        private readonly string contentLocation;
        private readonly string contentLocationProtocol;
        private readonly string contentLocationHostName;
        private readonly string playerLocation;
        private readonly string playerLocationProtocol;
        private readonly string playerLocationHostName;
        private readonly bool? playerLocationAllowEmbed;
        private readonly string playerLocationAutoPlay;
        private readonly int duration;
        private readonly DateTime expirationDate;
        private readonly double rating;
        private readonly int viewCount;
        private readonly DateTime publicationDate;
        private readonly bool isFamilyFriendly;
        private readonly IList<string> tags;
        private readonly IList<string> categories;
        private readonly IList<string> countriesAllowed;
        private readonly IList<string> countriesNotAllowed;
        private readonly string galleryLocation;
        private readonly string galleryLocationProtocol;
        private readonly string galleryLocationHostName;
        private readonly string galleryLocationTitle;
        private readonly IList<IVideoContentPrice> prices;
        private readonly bool requiresSubscription;
        private readonly string uploader;
        private readonly string uploaderInfo;
        private readonly string uploaderInfoProtocol;
        private readonly string uploaderInfoHostName;
        private readonly VideoPlatform platformsAllowed;
        private readonly VideoPlatform platformsNotAllowed;
        private readonly bool live;


        public IVideoTitleBuilder WithThumbnailLocation(string url)
        {
            return new VideoContentBuilder(url, this.thumbnailLocationProtocol, this.thumbnailLocationHostName, this.title, this.description, 
                this.contentLocation, this.contentLocationProtocol, this.contentLocationHostName, this.playerLocation, this.playerLocationProtocol, 
                this.playerLocationHostName, this.playerLocationAllowEmbed, this.playerLocationAutoPlay, this.duration, this.expirationDate, 
                this.rating, this.viewCount, this.publicationDate, this.isFamilyFriendly, this.tags, this.categories, this.countriesAllowed, 
                this.countriesNotAllowed, this.galleryLocation, this.galleryLocationProtocol, this.galleryLocationHostName, 
                this.galleryLocationTitle, this.prices, this.requiresSubscription, this.uploader, this.uploaderInfo, 
                this.uploaderInfoProtocol, this.uploaderInfoHostName, this.platformsAllowed, this.platformsNotAllowed, this.live);
        }

        public IVideoTitleBuilder WithThumbnailLocation(string url, string protocol)
        {
            return new VideoContentBuilder(url, protocol, this.thumbnailLocationHostName, this.title, this.description,
                this.contentLocation, this.contentLocationProtocol, this.contentLocationHostName, this.playerLocation, this.playerLocationProtocol,
                this.playerLocationHostName, this.playerLocationAllowEmbed, this.playerLocationAutoPlay, this.duration, this.expirationDate,
                this.rating, this.viewCount, this.publicationDate, this.isFamilyFriendly, this.tags, this.categories, this.countriesAllowed,
                this.countriesNotAllowed, this.galleryLocation, this.galleryLocationProtocol, this.galleryLocationHostName,
                this.galleryLocationTitle, this.prices, this.requiresSubscription, this.uploader, this.uploaderInfo,
                this.uploaderInfoProtocol, this.uploaderInfoHostName, this.platformsAllowed, this.platformsNotAllowed, this.live);
        }

        public IVideoTitleBuilder WithThumbnailLocation(string url, string protocol, string hostName)
        {
            return new VideoContentBuilder(url, protocol, hostName, this.title, this.description,
                this.contentLocation, this.contentLocationProtocol, this.contentLocationHostName, this.playerLocation, this.playerLocationProtocol,
                this.playerLocationHostName, this.playerLocationAllowEmbed, this.playerLocationAutoPlay, this.duration, this.expirationDate,
                this.rating, this.viewCount, this.publicationDate, this.isFamilyFriendly, this.tags, this.categories, this.countriesAllowed,
                this.countriesNotAllowed, this.galleryLocation, this.galleryLocationProtocol, this.galleryLocationHostName,
                this.galleryLocationTitle, this.prices, this.requiresSubscription, this.uploader, this.uploaderInfo,
                this.uploaderInfoProtocol, this.uploaderInfoHostName, this.platformsAllowed, this.platformsNotAllowed, this.live);
        }

        public IVideoDescriptionBuilder WithTitle(string title)
        {
            return new VideoContentBuilder(this.thumbnailLocation, this.thumbnailLocationProtocol, this.thumbnailLocationHostName, title, this.description,
                this.contentLocation, this.contentLocationProtocol, this.contentLocationHostName, this.playerLocation, this.playerLocationProtocol,
                this.playerLocationHostName, this.playerLocationAllowEmbed, this.playerLocationAutoPlay, this.duration, this.expirationDate,
                this.rating, this.viewCount, this.publicationDate, this.isFamilyFriendly, this.tags, this.categories, this.countriesAllowed,
                this.countriesNotAllowed, this.galleryLocation, this.galleryLocationProtocol, this.galleryLocationHostName,
                this.galleryLocationTitle, this.prices, this.requiresSubscription, this.uploader, this.uploaderInfo,
                this.uploaderInfoProtocol, this.uploaderInfoHostName, this.platformsAllowed, this.platformsNotAllowed, this.live);
        }

        public IVideoConditionalValueBuilder WithDescription(string description)
        {
            return new VideoContentBuilder(this.thumbnailLocation, this.thumbnailLocationProtocol, this.thumbnailLocationHostName, this.title, description,
                this.contentLocation, this.contentLocationProtocol, this.contentLocationHostName, this.playerLocation, this.playerLocationProtocol,
                this.playerLocationHostName, this.playerLocationAllowEmbed, this.playerLocationAutoPlay, this.duration, this.expirationDate,
                this.rating, this.viewCount, this.publicationDate, this.isFamilyFriendly, this.tags, this.categories, this.countriesAllowed,
                this.countriesNotAllowed, this.galleryLocation, this.galleryLocationProtocol, this.galleryLocationHostName,
                this.galleryLocationTitle, this.prices, this.requiresSubscription, this.uploader, this.uploaderInfo,
                this.uploaderInfoProtocol, this.uploaderInfoHostName, this.platformsAllowed, this.platformsNotAllowed, this.live);
        }

        public IVideoOptionalValueBuilder WithContentLocation(string url)
        {
            return new VideoContentBuilder(this.thumbnailLocation, this.thumbnailLocationProtocol, this.thumbnailLocationHostName, this.title, this.description,
                url, this.contentLocationProtocol, this.contentLocationHostName, this.playerLocation, this.playerLocationProtocol,
                this.playerLocationHostName, this.playerLocationAllowEmbed, this.playerLocationAutoPlay, this.duration, this.expirationDate,
                this.rating, this.viewCount, this.publicationDate, this.isFamilyFriendly, this.tags, this.categories, this.countriesAllowed,
                this.countriesNotAllowed, this.galleryLocation, this.galleryLocationProtocol, this.galleryLocationHostName,
                this.galleryLocationTitle, this.prices, this.requiresSubscription, this.uploader, this.uploaderInfo,
                this.uploaderInfoProtocol, this.uploaderInfoHostName, this.platformsAllowed, this.platformsNotAllowed, this.live);
        }

        public IVideoOptionalValueBuilder WithContentLocation(string url, string protocol)
        {
            return new VideoContentBuilder(this.thumbnailLocation, this.thumbnailLocationProtocol, this.thumbnailLocationHostName, this.title, this.description,
                url, protocol, this.contentLocationHostName, this.playerLocation, this.playerLocationProtocol,
                this.playerLocationHostName, this.playerLocationAllowEmbed, this.playerLocationAutoPlay, this.duration, this.expirationDate,
                this.rating, this.viewCount, this.publicationDate, this.isFamilyFriendly, this.tags, this.categories, this.countriesAllowed,
                this.countriesNotAllowed, this.galleryLocation, this.galleryLocationProtocol, this.galleryLocationHostName,
                this.galleryLocationTitle, this.prices, this.requiresSubscription, this.uploader, this.uploaderInfo,
                this.uploaderInfoProtocol, this.uploaderInfoHostName, this.platformsAllowed, this.platformsNotAllowed, this.live);
        }

        public IVideoOptionalValueBuilder WithContentLocation(string url, string protocol, string hostName)
        {
            return new VideoContentBuilder(this.thumbnailLocation, this.thumbnailLocationProtocol, this.thumbnailLocationHostName, this.title, this.description,
                url, protocol, hostName, this.playerLocation, this.playerLocationProtocol,
                this.playerLocationHostName, this.playerLocationAllowEmbed, this.playerLocationAutoPlay, this.duration, this.expirationDate,
                this.rating, this.viewCount, this.publicationDate, this.isFamilyFriendly, this.tags, this.categories, this.countriesAllowed,
                this.countriesNotAllowed, this.galleryLocation, this.galleryLocationProtocol, this.galleryLocationHostName,
                this.galleryLocationTitle, this.prices, this.requiresSubscription, this.uploader, this.uploaderInfo,
                this.uploaderInfoProtocol, this.uploaderInfoHostName, this.platformsAllowed, this.platformsNotAllowed, this.live);
        }

        public IVideoOptionalValueBuilder WithPlayerLocation(string url)
        {
            return new VideoContentBuilder(this.thumbnailLocation, this.thumbnailLocationProtocol, this.thumbnailLocationHostName, this.title, this.description,
                this.contentLocation, this.contentLocationProtocol, this.contentLocationHostName, url, this.playerLocationProtocol,
                this.playerLocationHostName, this.playerLocationAllowEmbed, this.playerLocationAutoPlay, this.duration, this.expirationDate,
                this.rating, this.viewCount, this.publicationDate, this.isFamilyFriendly, this.tags, this.categories, this.countriesAllowed,
                this.countriesNotAllowed, this.galleryLocation, this.galleryLocationProtocol, this.galleryLocationHostName,
                this.galleryLocationTitle, this.prices, this.requiresSubscription, this.uploader, this.uploaderInfo,
                this.uploaderInfoProtocol, this.uploaderInfoHostName, this.platformsAllowed, this.platformsNotAllowed, this.live);
        }

        public IVideoOptionalValueBuilder WithPlayerLocation(string url, string protocol)
        {
            return new VideoContentBuilder(this.thumbnailLocation, this.thumbnailLocationProtocol, this.thumbnailLocationHostName, this.title, this.description,
                this.contentLocation, this.contentLocationProtocol, this.contentLocationHostName, url, protocol,
                this.playerLocationHostName, this.playerLocationAllowEmbed, this.playerLocationAutoPlay, this.duration, this.expirationDate,
                this.rating, this.viewCount, this.publicationDate, this.isFamilyFriendly, this.tags, this.categories, this.countriesAllowed,
                this.countriesNotAllowed, this.galleryLocation, this.galleryLocationProtocol, this.galleryLocationHostName,
                this.galleryLocationTitle, this.prices, this.requiresSubscription, this.uploader, this.uploaderInfo,
                this.uploaderInfoProtocol, this.uploaderInfoHostName, this.platformsAllowed, this.platformsNotAllowed, this.live);
        }

        public IVideoOptionalValueBuilder WithPlayerLocation(string url, string protocol, string hostName)
        {
            return new VideoContentBuilder(this.thumbnailLocation, this.thumbnailLocationProtocol, this.thumbnailLocationHostName, this.title, this.description,
                this.contentLocation, this.contentLocationProtocol, this.contentLocationHostName, url, protocol,
                hostName, this.playerLocationAllowEmbed, this.playerLocationAutoPlay, this.duration, this.expirationDate,
                this.rating, this.viewCount, this.publicationDate, this.isFamilyFriendly, this.tags, this.categories, this.countriesAllowed,
                this.countriesNotAllowed, this.galleryLocation, this.galleryLocationProtocol, this.galleryLocationHostName,
                this.galleryLocationTitle, this.prices, this.requiresSubscription, this.uploader, this.uploaderInfo,
                this.uploaderInfoProtocol, this.uploaderInfoHostName, this.platformsAllowed, this.platformsNotAllowed, this.live);
        }

        public IVideoOptionalValueBuilder WithPlayerLocation(Func<IVideoPlayerLocationStarter, IVideoPlayerLocationOptionalValueBuilder> expression)
        {
            var starter = new VideoContentPlayerLocationBuilder(this.playerLocation);
            var builder = expression(starter);
            var playerLocation = builder.Create();

            return new VideoContentBuilder(this.thumbnailLocation, this.thumbnailLocationProtocol, this.thumbnailLocationHostName, this.title, this.description,
                this.contentLocation, this.contentLocationProtocol, this.contentLocationHostName, playerLocation.Url, playerLocation.Protocol,
                playerLocation.HostName, playerLocation.AllowEmbed, playerLocation.AutoPlay, this.duration, this.expirationDate,
                this.rating, this.viewCount, this.publicationDate, this.isFamilyFriendly, this.tags, this.categories, this.countriesAllowed,
                this.countriesNotAllowed, this.galleryLocation, this.galleryLocationProtocol, this.galleryLocationHostName,
                this.galleryLocationTitle, this.prices, this.requiresSubscription, this.uploader, this.uploaderInfo,
                this.uploaderInfoProtocol, this.uploaderInfoHostName, this.platformsAllowed, this.platformsNotAllowed, this.live);
        }

        public IVideoOptionalValueBuilder WithDuration(int duration)
        {
            return new VideoContentBuilder(this.thumbnailLocation, this.thumbnailLocationProtocol, this.thumbnailLocationHostName, this.title, this.description,
                this.contentLocation, this.contentLocationProtocol, this.contentLocationHostName, this.playerLocation, this.playerLocationProtocol,
                this.playerLocationHostName, this.playerLocationAllowEmbed, this.playerLocationAutoPlay, duration, this.expirationDate,
                this.rating, this.viewCount, this.publicationDate, this.isFamilyFriendly, this.tags, this.categories, this.countriesAllowed,
                this.countriesNotAllowed, this.galleryLocation, this.galleryLocationProtocol, this.galleryLocationHostName,
                this.galleryLocationTitle, this.prices, this.requiresSubscription, this.uploader, this.uploaderInfo,
                this.uploaderInfoProtocol, this.uploaderInfoHostName, this.platformsAllowed, this.platformsNotAllowed, this.live);
        }

        public IVideoOptionalValueBuilder WithExpirationDate(DateTime expirationDate)
        {
            return new VideoContentBuilder(this.thumbnailLocation, this.thumbnailLocationProtocol, this.thumbnailLocationHostName, this.title, this.description,
                this.contentLocation, this.contentLocationProtocol, this.contentLocationHostName, this.playerLocation, this.playerLocationProtocol,
                this.playerLocationHostName, this.playerLocationAllowEmbed, this.playerLocationAutoPlay, this.duration, expirationDate,
                this.rating, this.viewCount, this.publicationDate, this.isFamilyFriendly, this.tags, this.categories, this.countriesAllowed,
                this.countriesNotAllowed, this.galleryLocation, this.galleryLocationProtocol, this.galleryLocationHostName,
                this.galleryLocationTitle, this.prices, this.requiresSubscription, this.uploader, this.uploaderInfo,
                this.uploaderInfoProtocol, this.uploaderInfoHostName, this.platformsAllowed, this.platformsNotAllowed, this.live);
        }

        public IVideoOptionalValueBuilder WithRating(double rating)
        {
            return new VideoContentBuilder(this.thumbnailLocation, this.thumbnailLocationProtocol, this.thumbnailLocationHostName, this.title, this.description,
                this.contentLocation, this.contentLocationProtocol, this.contentLocationHostName, this.playerLocation, this.playerLocationProtocol,
                this.playerLocationHostName, this.playerLocationAllowEmbed, this.playerLocationAutoPlay, this.duration, this.expirationDate,
                rating, this.viewCount, this.publicationDate, this.isFamilyFriendly, this.tags, this.categories, this.countriesAllowed,
                this.countriesNotAllowed, this.galleryLocation, this.galleryLocationProtocol, this.galleryLocationHostName,
                this.galleryLocationTitle, this.prices, this.requiresSubscription, this.uploader, this.uploaderInfo,
                this.uploaderInfoProtocol, this.uploaderInfoHostName, this.platformsAllowed, this.platformsNotAllowed, this.live);
        }

        public IVideoOptionalValueBuilder WithViewCount(int viewCount)
        {
            return new VideoContentBuilder(this.thumbnailLocation, this.thumbnailLocationProtocol, this.thumbnailLocationHostName, this.title, this.description,
                this.contentLocation, this.contentLocationProtocol, this.contentLocationHostName, this.playerLocation, this.playerLocationProtocol,
                this.playerLocationHostName, this.playerLocationAllowEmbed, this.playerLocationAutoPlay, this.duration, this.expirationDate,
                this.rating, viewCount, this.publicationDate, this.isFamilyFriendly, this.tags, this.categories, this.countriesAllowed,
                this.countriesNotAllowed, this.galleryLocation, this.galleryLocationProtocol, this.galleryLocationHostName,
                this.galleryLocationTitle, this.prices, this.requiresSubscription, this.uploader, this.uploaderInfo,
                this.uploaderInfoProtocol, this.uploaderInfoHostName, this.platformsAllowed, this.platformsNotAllowed, this.live);
        }

        public IVideoOptionalValueBuilder WithPublicationDate(DateTime publicationDate)
        {
            return new VideoContentBuilder(this.thumbnailLocation, this.thumbnailLocationProtocol, this.thumbnailLocationHostName, this.title, this.description,
                this.contentLocation, this.contentLocationProtocol, this.contentLocationHostName, this.playerLocation, this.playerLocationProtocol,
                this.playerLocationHostName, this.playerLocationAllowEmbed, this.playerLocationAutoPlay, this.duration, this.expirationDate,
                this.rating, this.viewCount, publicationDate, this.isFamilyFriendly, this.tags, this.categories, this.countriesAllowed,
                this.countriesNotAllowed, this.galleryLocation, this.galleryLocationProtocol, this.galleryLocationHostName,
                this.galleryLocationTitle, this.prices, this.requiresSubscription, this.uploader, this.uploaderInfo,
                this.uploaderInfoProtocol, this.uploaderInfoHostName, this.platformsAllowed, this.platformsNotAllowed, this.live);
        }

        public IVideoOptionalValueBuilder IsFamilyFriendly(bool familyFriendly)
        {
            return new VideoContentBuilder(this.thumbnailLocation, this.thumbnailLocationProtocol, this.thumbnailLocationHostName, this.title, this.description,
                this.contentLocation, this.contentLocationProtocol, this.contentLocationHostName, this.playerLocation, this.playerLocationProtocol,
                this.playerLocationHostName, this.playerLocationAllowEmbed, this.playerLocationAutoPlay, this.duration, this.expirationDate,
                this.rating, this.viewCount, this.publicationDate, familyFriendly, this.tags, this.categories, this.countriesAllowed,
                this.countriesNotAllowed, this.galleryLocation, this.galleryLocationProtocol, this.galleryLocationHostName,
                this.galleryLocationTitle, this.prices, this.requiresSubscription, this.uploader, this.uploaderInfo,
                this.uploaderInfoProtocol, this.uploaderInfoHostName, this.platformsAllowed, this.platformsNotAllowed, this.live);
        }

        public IVideoOptionalValueBuilder WithTag(string tag)
        {
            this.tags.Add(tag);
            return new VideoContentBuilder(this.thumbnailLocation, this.thumbnailLocationProtocol, this.thumbnailLocationHostName, this.title, this.description,
                this.contentLocation, this.contentLocationProtocol, this.contentLocationHostName, this.playerLocation, this.playerLocationProtocol,
                this.playerLocationHostName, this.playerLocationAllowEmbed, this.playerLocationAutoPlay, this.duration, this.expirationDate,
                this.rating, this.viewCount, this.publicationDate, this.isFamilyFriendly, this.tags, this.categories, this.countriesAllowed,
                this.countriesNotAllowed, this.galleryLocation, this.galleryLocationProtocol, this.galleryLocationHostName,
                this.galleryLocationTitle, this.prices, this.requiresSubscription, this.uploader, this.uploaderInfo,
                this.uploaderInfoProtocol, this.uploaderInfoHostName, this.platformsAllowed, this.platformsNotAllowed, this.live);
        }

        public IVideoOptionalValueBuilder WithTags(string tags)
        {
            var tagsArray = tags.Split(new char[] { ',' });
            foreach (var tag in tagsArray)
            {
                if (!string.IsNullOrEmpty(tag))
                {
                    this.tags.Add(tag.Trim());
                }
            }
            return new VideoContentBuilder(this.thumbnailLocation, this.thumbnailLocationProtocol, this.thumbnailLocationHostName, this.title, this.description,
                this.contentLocation, this.contentLocationProtocol, this.contentLocationHostName, this.playerLocation, this.playerLocationProtocol,
                this.playerLocationHostName, this.playerLocationAllowEmbed, this.playerLocationAutoPlay, this.duration, this.expirationDate,
                this.rating, this.viewCount, this.publicationDate, this.isFamilyFriendly, this.tags, this.categories, this.countriesAllowed,
                this.countriesNotAllowed, this.galleryLocation, this.galleryLocationProtocol, this.galleryLocationHostName,
                this.galleryLocationTitle, this.prices, this.requiresSubscription, this.uploader, this.uploaderInfo,
                this.uploaderInfoProtocol, this.uploaderInfoHostName, this.platformsAllowed, this.platformsNotAllowed, this.live);
        }

        public IVideoOptionalValueBuilder WithTags(IEnumerable<string> tags)
        {
            foreach (var tag in tags)
            {
                if (!string.IsNullOrEmpty(tag))
                {
                    this.tags.Add(tag);
                }
            }
            return new VideoContentBuilder(this.thumbnailLocation, this.thumbnailLocationProtocol, this.thumbnailLocationHostName, this.title, this.description,
                this.contentLocation, this.contentLocationProtocol, this.contentLocationHostName, this.playerLocation, this.playerLocationProtocol,
                this.playerLocationHostName, this.playerLocationAllowEmbed, this.playerLocationAutoPlay, this.duration, this.expirationDate,
                this.rating, this.viewCount, this.publicationDate, this.isFamilyFriendly, this.tags, this.categories, this.countriesAllowed,
                this.countriesNotAllowed, this.galleryLocation, this.galleryLocationProtocol, this.galleryLocationHostName,
                this.galleryLocationTitle, this.prices, this.requiresSubscription, this.uploader, this.uploaderInfo,
                this.uploaderInfoProtocol, this.uploaderInfoHostName, this.platformsAllowed, this.platformsNotAllowed, this.live);
        }

        public IVideoOptionalValueBuilder WithCategory(string category)
        {
            this.categories.Add(category);
            return new VideoContentBuilder(this.thumbnailLocation, this.thumbnailLocationProtocol, this.thumbnailLocationHostName, this.title, this.description,
                this.contentLocation, this.contentLocationProtocol, this.contentLocationHostName, this.playerLocation, this.playerLocationProtocol,
                this.playerLocationHostName, this.playerLocationAllowEmbed, this.playerLocationAutoPlay, this.duration, this.expirationDate,
                this.rating, this.viewCount, this.publicationDate, this.isFamilyFriendly, this.tags, this.categories, this.countriesAllowed,
                this.countriesNotAllowed, this.galleryLocation, this.galleryLocationProtocol, this.galleryLocationHostName,
                this.galleryLocationTitle, this.prices, this.requiresSubscription, this.uploader, this.uploaderInfo,
                this.uploaderInfoProtocol, this.uploaderInfoHostName, this.platformsAllowed, this.platformsNotAllowed, this.live);
        }

        public IVideoOptionalValueBuilder WithCategories(string categories)
        {
            var categoryArray = categories.Split(new char[] { ',' });
            foreach (var category in categoryArray)
            {
                if (!string.IsNullOrEmpty(category))
                {
                    this.categories.Add(category.Trim());
                }
            }
            return new VideoContentBuilder(this.thumbnailLocation, this.thumbnailLocationProtocol, this.thumbnailLocationHostName, this.title, this.description,
                this.contentLocation, this.contentLocationProtocol, this.contentLocationHostName, this.playerLocation, this.playerLocationProtocol,
                this.playerLocationHostName, this.playerLocationAllowEmbed, this.playerLocationAutoPlay, this.duration, this.expirationDate,
                this.rating, this.viewCount, this.publicationDate, this.isFamilyFriendly, this.tags, this.categories, this.countriesAllowed,
                this.countriesNotAllowed, this.galleryLocation, this.galleryLocationProtocol, this.galleryLocationHostName,
                this.galleryLocationTitle, this.prices, this.requiresSubscription, this.uploader, this.uploaderInfo,
                this.uploaderInfoProtocol, this.uploaderInfoHostName, this.platformsAllowed, this.platformsNotAllowed, this.live);
        }

        public IVideoOptionalValueBuilder WithCategories(IEnumerable<string> categories)
        {
            foreach (var category in categories)
            {
                if (!string.IsNullOrEmpty(category))
                {
                    this.categories.Add(category);
                }
            }
            return new VideoContentBuilder(this.thumbnailLocation, this.thumbnailLocationProtocol, this.thumbnailLocationHostName, this.title, this.description,
                this.contentLocation, this.contentLocationProtocol, this.contentLocationHostName, this.playerLocation, this.playerLocationProtocol,
                this.playerLocationHostName, this.playerLocationAllowEmbed, this.playerLocationAutoPlay, this.duration, this.expirationDate,
                this.rating, this.viewCount, this.publicationDate, this.isFamilyFriendly, this.tags, this.categories, this.countriesAllowed,
                this.countriesNotAllowed, this.galleryLocation, this.galleryLocationProtocol, this.galleryLocationHostName,
                this.galleryLocationTitle, this.prices, this.requiresSubscription, this.uploader, this.uploaderInfo,
                this.uploaderInfoProtocol, this.uploaderInfoHostName, this.platformsAllowed, this.platformsNotAllowed, this.live);
        }

        public IVideoOptionalValueBuilder WithAllowedCountry(string country)
        {
            this.countriesAllowed.Add(country);
            return new VideoContentBuilder(this.thumbnailLocation, this.thumbnailLocationProtocol, this.thumbnailLocationHostName, this.title, this.description,
                this.contentLocation, this.contentLocationProtocol, this.contentLocationHostName, this.playerLocation, this.playerLocationProtocol,
                this.playerLocationHostName, this.playerLocationAllowEmbed, this.playerLocationAutoPlay, this.duration, this.expirationDate,
                this.rating, this.viewCount, this.publicationDate, this.isFamilyFriendly, this.tags, this.categories, this.countriesAllowed,
                this.countriesNotAllowed, this.galleryLocation, this.galleryLocationProtocol, this.galleryLocationHostName,
                this.galleryLocationTitle, this.prices, this.requiresSubscription, this.uploader, this.uploaderInfo,
                this.uploaderInfoProtocol, this.uploaderInfoHostName, this.platformsAllowed, this.platformsNotAllowed, this.live);
        }

        public IVideoOptionalValueBuilder WithAllowedCountries(string countries)
        {
            var countryArray = countries.Split(new char[] { ',' });
            foreach (var country in countryArray)
            {
                if (!string.IsNullOrEmpty(country))
                {
                    this.countriesAllowed.Add(country.Trim());
                }
            }
            return new VideoContentBuilder(this.thumbnailLocation, this.thumbnailLocationProtocol, this.thumbnailLocationHostName, this.title, this.description,
                this.contentLocation, this.contentLocationProtocol, this.contentLocationHostName, this.playerLocation, this.playerLocationProtocol,
                this.playerLocationHostName, this.playerLocationAllowEmbed, this.playerLocationAutoPlay, this.duration, this.expirationDate,
                this.rating, this.viewCount, this.publicationDate, this.isFamilyFriendly, this.tags, this.categories, this.countriesAllowed,
                this.countriesNotAllowed, this.galleryLocation, this.galleryLocationProtocol, this.galleryLocationHostName,
                this.galleryLocationTitle, this.prices, this.requiresSubscription, this.uploader, this.uploaderInfo,
                this.uploaderInfoProtocol, this.uploaderInfoHostName, this.platformsAllowed, this.platformsNotAllowed, this.live);
        }

        public IVideoOptionalValueBuilder WithAllowedCountries(IEnumerable<string> countries)
        {
            foreach (var country in countries)
            {
                if (!string.IsNullOrEmpty(country))
                {
                    this.countriesAllowed.Add(country);
                }
            }
            return new VideoContentBuilder(this.thumbnailLocation, this.thumbnailLocationProtocol, this.thumbnailLocationHostName, this.title, this.description,
                this.contentLocation, this.contentLocationProtocol, this.contentLocationHostName, this.playerLocation, this.playerLocationProtocol,
                this.playerLocationHostName, this.playerLocationAllowEmbed, this.playerLocationAutoPlay, this.duration, this.expirationDate,
                this.rating, this.viewCount, this.publicationDate, this.isFamilyFriendly, this.tags, this.categories, this.countriesAllowed,
                this.countriesNotAllowed, this.galleryLocation, this.galleryLocationProtocol, this.galleryLocationHostName,
                this.galleryLocationTitle, this.prices, this.requiresSubscription, this.uploader, this.uploaderInfo,
                this.uploaderInfoProtocol, this.uploaderInfoHostName, this.platformsAllowed, this.platformsNotAllowed, this.live);
        }

        public IVideoOptionalValueBuilder WithDeniedCountry(string country)
        {
            this.countriesNotAllowed.Add(country);
            return new VideoContentBuilder(this.thumbnailLocation, this.thumbnailLocationProtocol, this.thumbnailLocationHostName, this.title, this.description,
                this.contentLocation, this.contentLocationProtocol, this.contentLocationHostName, this.playerLocation, this.playerLocationProtocol,
                this.playerLocationHostName, this.playerLocationAllowEmbed, this.playerLocationAutoPlay, this.duration, this.expirationDate,
                this.rating, this.viewCount, this.publicationDate, this.isFamilyFriendly, this.tags, this.categories, this.countriesAllowed,
                this.countriesNotAllowed, this.galleryLocation, this.galleryLocationProtocol, this.galleryLocationHostName,
                this.galleryLocationTitle, this.prices, this.requiresSubscription, this.uploader, this.uploaderInfo,
                this.uploaderInfoProtocol, this.uploaderInfoHostName, this.platformsAllowed, this.platformsNotAllowed, this.live);
        }

        public IVideoOptionalValueBuilder WithDeniedCountries(string countries)
        {
            var countryArray = countries.Split(new char[] { ',' });
            foreach (var country in countryArray)
            {
                if (!string.IsNullOrEmpty(country))
                {
                    this.countriesNotAllowed.Add(country.Trim());
                }
            }
            return new VideoContentBuilder(this.thumbnailLocation, this.thumbnailLocationProtocol, this.thumbnailLocationHostName, this.title, this.description,
                this.contentLocation, this.contentLocationProtocol, this.contentLocationHostName, this.playerLocation, this.playerLocationProtocol,
                this.playerLocationHostName, this.playerLocationAllowEmbed, this.playerLocationAutoPlay, this.duration, this.expirationDate,
                this.rating, this.viewCount, this.publicationDate, this.isFamilyFriendly, this.tags, this.categories, this.countriesAllowed,
                this.countriesNotAllowed, this.galleryLocation, this.galleryLocationProtocol, this.galleryLocationHostName,
                this.galleryLocationTitle, this.prices, this.requiresSubscription, this.uploader, this.uploaderInfo,
                this.uploaderInfoProtocol, this.uploaderInfoHostName, this.platformsAllowed, this.platformsNotAllowed, this.live);
        }

        public IVideoOptionalValueBuilder WithDeniedCountries(IEnumerable<string> countries)
        {
            foreach (var country in countries)
            {
                if (!string.IsNullOrEmpty(country))
                {
                    this.countriesNotAllowed.Add(country);
                }
            }
            return new VideoContentBuilder(this.thumbnailLocation, this.thumbnailLocationProtocol, this.thumbnailLocationHostName, this.title, this.description,
                this.contentLocation, this.contentLocationProtocol, this.contentLocationHostName, this.playerLocation, this.playerLocationProtocol,
                this.playerLocationHostName, this.playerLocationAllowEmbed, this.playerLocationAutoPlay, this.duration, this.expirationDate,
                this.rating, this.viewCount, this.publicationDate, this.isFamilyFriendly, this.tags, this.categories, this.countriesAllowed,
                this.countriesNotAllowed, this.galleryLocation, this.galleryLocationProtocol, this.galleryLocationHostName,
                this.galleryLocationTitle, this.prices, this.requiresSubscription, this.uploader, this.uploaderInfo,
                this.uploaderInfoProtocol, this.uploaderInfoHostName, this.platformsAllowed, this.platformsNotAllowed, this.live);
        }

        public IVideoOptionalValueBuilder WithGalleryLocation(string url)
        {
            return new VideoContentBuilder(this.thumbnailLocation, this.thumbnailLocationProtocol, this.thumbnailLocationHostName, this.title, this.description,
                this.contentLocation, this.contentLocationProtocol, this.contentLocationHostName, this.playerLocation, this.playerLocationProtocol,
                this.playerLocationHostName, this.playerLocationAllowEmbed, this.playerLocationAutoPlay, this.duration, this.expirationDate,
                this.rating, this.viewCount, this.publicationDate, this.isFamilyFriendly, this.tags, this.categories, this.countriesAllowed,
                this.countriesNotAllowed, url, this.galleryLocationProtocol, this.galleryLocationHostName,
                this.galleryLocationTitle, this.prices, this.requiresSubscription, this.uploader, this.uploaderInfo,
                this.uploaderInfoProtocol, this.uploaderInfoHostName, this.platformsAllowed, this.platformsNotAllowed, this.live);
        }

        public IVideoOptionalValueBuilder WithGalleryLocation(string url, string protocol)
        {
            return new VideoContentBuilder(this.thumbnailLocation, this.thumbnailLocationProtocol, this.thumbnailLocationHostName, this.title, this.description,
                this.contentLocation, this.contentLocationProtocol, this.contentLocationHostName, this.playerLocation, this.playerLocationProtocol,
                this.playerLocationHostName, this.playerLocationAllowEmbed, this.playerLocationAutoPlay, this.duration, this.expirationDate,
                this.rating, this.viewCount, this.publicationDate, this.isFamilyFriendly, this.tags, this.categories, this.countriesAllowed,
                this.countriesNotAllowed, url, protocol, this.galleryLocationHostName,
                this.galleryLocationTitle, this.prices, this.requiresSubscription, this.uploader, this.uploaderInfo,
                this.uploaderInfoProtocol, this.uploaderInfoHostName, this.platformsAllowed, this.platformsNotAllowed, this.live);
        }

        public IVideoOptionalValueBuilder WithGalleryLocation(string url, string protocol, string hostName)
        {
            return new VideoContentBuilder(this.thumbnailLocation, this.thumbnailLocationProtocol, this.thumbnailLocationHostName, this.title, this.description,
                this.contentLocation, this.contentLocationProtocol, this.contentLocationHostName, this.playerLocation, this.playerLocationProtocol,
                this.playerLocationHostName, this.playerLocationAllowEmbed, this.playerLocationAutoPlay, this.duration, this.expirationDate,
                this.rating, this.viewCount, this.publicationDate, this.isFamilyFriendly, this.tags, this.categories, this.countriesAllowed,
                this.countriesNotAllowed, url, protocol, hostName,
                this.galleryLocationTitle, this.prices, this.requiresSubscription, this.uploader, this.uploaderInfo,
                this.uploaderInfoProtocol, this.uploaderInfoHostName, this.platformsAllowed, this.platformsNotAllowed, this.live);
        }

        public IVideoOptionalValueBuilder WithGalleryLocation(Func<IVideoGalleryLocationStarter, IVideoGalleryLocationOptionalValueBuilder> expression)
        {
            var starter = new VideoGalleryLocationBuilder(this.galleryLocation);
            var builder = expression(starter);
            var galleryLocation = builder.Create();

            return new VideoContentBuilder(this.thumbnailLocation, this.thumbnailLocationProtocol, this.thumbnailLocationHostName, this.title, this.description,
                this.contentLocation, this.contentLocationProtocol, this.contentLocationHostName, this.playerLocation, this.playerLocationProtocol,
                this.playerLocationHostName, this.playerLocationAllowEmbed, this.playerLocationAutoPlay, this.duration, this.expirationDate,
                this.rating, this.viewCount, this.publicationDate, this.isFamilyFriendly, this.tags, this.categories, this.countriesAllowed,
                this.countriesNotAllowed, galleryLocation.Url, galleryLocation.Protocol, galleryLocation.HostName,
                galleryLocation.Title, this.prices, this.requiresSubscription, this.uploader, this.uploaderInfo,
                this.uploaderInfoProtocol, this.uploaderInfoHostName, this.platformsAllowed, this.platformsNotAllowed, this.live);
        }

        public IVideoOptionalValueBuilder AddPrice(Func<IVideoPriceStarter, IVideoPriceOptionalValueBuilder> expression)
        {
            var starter = new VideoPriceBuilder();
            var builder = expression(starter);
            this.prices.Add(builder.Create());

            return new VideoContentBuilder(this.thumbnailLocation, this.thumbnailLocationProtocol, this.thumbnailLocationHostName, this.title, this.description,
                this.contentLocation, this.contentLocationProtocol, this.contentLocationHostName, this.playerLocation, this.playerLocationProtocol,
                this.playerLocationHostName, this.playerLocationAllowEmbed, this.playerLocationAutoPlay, this.duration, this.expirationDate,
                this.rating, this.viewCount, this.publicationDate, this.isFamilyFriendly, this.tags, this.categories, this.countriesAllowed,
                this.countriesNotAllowed, this.galleryLocation, this.galleryLocationProtocol, this.galleryLocationHostName,
                this.galleryLocationTitle, this.prices, this.requiresSubscription, this.uploader, this.uploaderInfo,
                this.uploaderInfoProtocol, this.uploaderInfoHostName, this.platformsAllowed, this.platformsNotAllowed, this.live);
        }

        public IVideoOptionalValueBuilder IsSubscriptionRequired(bool requiresSubscription)
        {
            return new VideoContentBuilder(this.thumbnailLocation, this.thumbnailLocationProtocol, this.thumbnailLocationHostName, this.title, this.description,
                this.contentLocation, this.contentLocationProtocol, this.contentLocationHostName, this.playerLocation, this.playerLocationProtocol,
                this.playerLocationHostName, this.playerLocationAllowEmbed, this.playerLocationAutoPlay, this.duration, this.expirationDate,
                this.rating, this.viewCount, this.publicationDate, this.isFamilyFriendly, this.tags, this.categories, this.countriesAllowed,
                this.countriesNotAllowed, this.galleryLocation, this.galleryLocationProtocol, this.galleryLocationHostName,
                this.galleryLocationTitle, this.prices, requiresSubscription, this.uploader, this.uploaderInfo,
                this.uploaderInfoProtocol, this.uploaderInfoHostName, this.platformsAllowed, this.platformsNotAllowed, this.live);
        }

        public IVideoOptionalValueBuilder WithUploader(Func<IVideoContentUploaderStarter, IVideoContentUploaderBuilder> exprssion)
        {
            var starter = new VideoContentUploaderBuilder(this.uploader);
            var builder = exprssion(starter);
            var uploader = builder.Create();

            return new VideoContentBuilder(this.thumbnailLocation, this.thumbnailLocationProtocol, this.thumbnailLocationHostName, this.title, this.description,
                this.contentLocation, this.contentLocationProtocol, this.contentLocationHostName, this.playerLocation, this.playerLocationProtocol,
                this.playerLocationHostName, this.playerLocationAllowEmbed, this.playerLocationAutoPlay, this.duration, this.expirationDate,
                this.rating, this.viewCount, this.publicationDate, this.isFamilyFriendly, this.tags, this.categories, this.countriesAllowed,
                this.countriesNotAllowed, this.galleryLocation, this.galleryLocationProtocol, this.galleryLocationHostName,
                this.galleryLocationTitle, this.prices, this.requiresSubscription, uploader.Name, uploader.Url,
                uploader.Protocol, uploader.HostName, this.platformsAllowed, this.platformsNotAllowed, this.live);
        }

        public IVideoOptionalValueBuilder WithAllowedPlatform(VideoPlatform platform)
        {
            var platformsAllowed = this.platformsAllowed | platform;
            return new VideoContentBuilder(this.thumbnailLocation, this.thumbnailLocationProtocol, this.thumbnailLocationHostName, this.title, this.description,
                this.contentLocation, this.contentLocationProtocol, this.contentLocationHostName, this.playerLocation, this.playerLocationProtocol,
                this.playerLocationHostName, this.playerLocationAllowEmbed, this.playerLocationAutoPlay, this.duration, this.expirationDate,
                this.rating, this.viewCount, this.publicationDate, this.isFamilyFriendly, this.tags, this.categories, this.countriesAllowed,
                this.countriesNotAllowed, this.galleryLocation, this.galleryLocationProtocol, this.galleryLocationHostName,
                this.galleryLocationTitle, this.prices, this.requiresSubscription, this.uploader, this.uploaderInfo,
                this.uploaderInfoProtocol, this.uploaderInfoHostName, platformsAllowed, this.platformsNotAllowed, this.live);
        }

        public IVideoOptionalValueBuilder WithAllowedPlatforms(string platforms)
        {
            var platformArray = platforms.Split(new char[] { ',' });
            VideoPlatform platformsAllowed = this.platformsAllowed;
            foreach (var platform in platformArray)
            {
                if (!string.IsNullOrEmpty(platform))
                {
                    VideoPlatform parsed = VideoPlatform.Undefined;
                    if (Enum.TryParse<VideoPlatform>(platform, true, out parsed))
                    {
                        platformsAllowed |= parsed;
                    }
                }
            }
            return new VideoContentBuilder(this.thumbnailLocation, this.thumbnailLocationProtocol, this.thumbnailLocationHostName, this.title, this.description,
                this.contentLocation, this.contentLocationProtocol, this.contentLocationHostName, this.playerLocation, this.playerLocationProtocol,
                this.playerLocationHostName, this.playerLocationAllowEmbed, this.playerLocationAutoPlay, this.duration, this.expirationDate,
                this.rating, this.viewCount, this.publicationDate, this.isFamilyFriendly, this.tags, this.categories, this.countriesAllowed,
                this.countriesNotAllowed, this.galleryLocation, this.galleryLocationProtocol, this.galleryLocationHostName,
                this.galleryLocationTitle, this.prices, this.requiresSubscription, this.uploader, this.uploaderInfo,
                this.uploaderInfoProtocol, this.uploaderInfoHostName, platformsAllowed, this.platformsNotAllowed, this.live);
        }

        public IVideoOptionalValueBuilder WithDeniedPlatform(VideoPlatform platform)
        {
            var platformsNotAllowed = this.platformsNotAllowed | platform;
            return new VideoContentBuilder(this.thumbnailLocation, this.thumbnailLocationProtocol, this.thumbnailLocationHostName, this.title, this.description,
                this.contentLocation, this.contentLocationProtocol, this.contentLocationHostName, this.playerLocation, this.playerLocationProtocol,
                this.playerLocationHostName, this.playerLocationAllowEmbed, this.playerLocationAutoPlay, this.duration, this.expirationDate,
                this.rating, this.viewCount, this.publicationDate, this.isFamilyFriendly, this.tags, this.categories, this.countriesAllowed,
                this.countriesNotAllowed, this.galleryLocation, this.galleryLocationProtocol, this.galleryLocationHostName,
                this.galleryLocationTitle, this.prices, this.requiresSubscription, this.uploader, this.uploaderInfo,
                this.uploaderInfoProtocol, this.uploaderInfoHostName, this.platformsAllowed, platformsNotAllowed, this.live);
        }

        public IVideoOptionalValueBuilder WithDeniedPlatforms(string platforms)
        {
            var platformArray = platforms.Split(new char[] { ',' });
            VideoPlatform platformsNotAllowed = this.platformsAllowed;
            foreach (var platform in platformArray)
            {
                if (!string.IsNullOrEmpty(platform))
                {
                    VideoPlatform parsed = VideoPlatform.Undefined;
                    if (Enum.TryParse<VideoPlatform>(platform, true, out parsed))
                    {
                        platformsNotAllowed |= parsed;
                    }
                }
            }
            return new VideoContentBuilder(this.thumbnailLocation, this.thumbnailLocationProtocol, this.thumbnailLocationHostName, this.title, this.description,
                this.contentLocation, this.contentLocationProtocol, this.contentLocationHostName, this.playerLocation, this.playerLocationProtocol,
                this.playerLocationHostName, this.playerLocationAllowEmbed, this.playerLocationAutoPlay, this.duration, this.expirationDate,
                this.rating, this.viewCount, this.publicationDate, this.isFamilyFriendly, this.tags, this.categories, this.countriesAllowed,
                this.countriesNotAllowed, this.galleryLocation, this.galleryLocationProtocol, this.galleryLocationHostName,
                this.galleryLocationTitle, this.prices, this.requiresSubscription, this.uploader, this.uploaderInfo,
                this.uploaderInfoProtocol, this.uploaderInfoHostName, this.platformsAllowed, platformsNotAllowed, this.live);
        }

        public IVideoOptionalValueBuilder IsLive(bool live)
        {
            return new VideoContentBuilder(this.thumbnailLocation, this.thumbnailLocationProtocol, this.thumbnailLocationHostName, this.title, this.description,
                this.contentLocation, this.contentLocationProtocol, this.contentLocationHostName, this.playerLocation, this.playerLocationProtocol,
                this.playerLocationHostName, this.playerLocationAllowEmbed, this.playerLocationAutoPlay, this.duration, this.expirationDate,
                this.rating, this.viewCount, this.publicationDate, this.isFamilyFriendly, this.tags, this.categories, this.countriesAllowed,
                this.countriesNotAllowed, this.galleryLocation, this.galleryLocationProtocol, this.galleryLocationHostName,
                this.galleryLocationTitle, this.prices, this.requiresSubscription, this.uploader, this.uploaderInfo,
                this.uploaderInfoProtocol, this.uploaderInfoHostName, this.platformsAllowed, this.platformsNotAllowed, live);
        }

        public ISpecializedContent Create()
        {
            return new VideoContent(
                this.thumbnailLocation, 
                this.title, 
                this.description, 
                this.contentLocation, 
                this.playerLocation, 
                this.tags, 
                this.categories, 
                this.countriesAllowed, 
                this.countriesNotAllowed, 
                this.prices)
            {
                ThumbnailLocationProtocol = this.thumbnailLocationProtocol,
                ThumbnailLocationHostName = this.thumbnailLocationHostName,
                ContentLocationProtocol = this.contentLocationProtocol,
                ContentLocationHostName = this.contentLocationHostName,
                PlayerLocationProtocol = this.playerLocationProtocol,
                PlayerLocationHostName = this.playerLocationHostName,
                PlayerLocationAllowEmbed = this.playerLocationAllowEmbed,
                PlayerLocationAutoPlay = this.playerLocationAutoPlay,
                Duration = this.duration,
                ExpirationDate = this.expirationDate,
                Rating = this.rating,
                ViewCount = this.viewCount,
                PublicationDate = this.publicationDate,
                IsFamilyFriendly = this.isFamilyFriendly,
                GalleryLocation = this.galleryLocation,
                GalleryLocationProtocol = this.galleryLocationProtocol,
                GalleryLocationHostName = this.galleryLocationHostName,
                GalleryLocationTitle = this.galleryLocationTitle,
                RequiresSubscription = this.requiresSubscription,
                Uploader = this.uploader,
                UploaderInfo = this.uploaderInfo,
                UploaderInfoProtocol = this.uploaderInfoProtocol,
                UploaderInfoHostName = this.uploaderInfoHostName,
                PlatformsAllowed = this.platformsAllowed,
                PlatformsNotAllowed = this.platformsNotAllowed,
                Live = this.live
            };
        }
    }
}
