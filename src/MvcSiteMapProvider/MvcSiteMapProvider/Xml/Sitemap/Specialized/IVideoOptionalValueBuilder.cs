using System;
using System.Collections.Generic;
using MvcSiteMapProvider.Xml.Sitemap.Specialized.Video;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized
{
    public interface IVideoOptionalValueBuilder
        : ISpecializedContentBuilder, IVideoConditionalValueBuilder
    {
        IVideoOptionalValueBuilder WithDuration(int duration);

        IVideoOptionalValueBuilder WithExpirationDate(DateTime expirationDate);

        IVideoOptionalValueBuilder WithRating(double rating);

        IVideoOptionalValueBuilder WithViewCount(int viewCount);

        IVideoOptionalValueBuilder WithPublicationDate(DateTime publicationDate);

        IVideoOptionalValueBuilder IsFamilyFriendly(bool familyFriendly);

        IVideoOptionalValueBuilder WithTag(string tag);

        IVideoOptionalValueBuilder WithTags(string tags);

        IVideoOptionalValueBuilder WithTags(IEnumerable<string> tags);

        IVideoOptionalValueBuilder WithCategory(string category);

        IVideoOptionalValueBuilder WithCategories(string categories);

        IVideoOptionalValueBuilder WithCategories(IEnumerable<string> categories);

        IVideoOptionalValueBuilder WithAllowedCountry(string country);

        IVideoOptionalValueBuilder WithAllowedCountries(string countries);

        IVideoOptionalValueBuilder WithAllowedCountries(IEnumerable<string> countries);

        IVideoOptionalValueBuilder WithDeniedCountry(string country);

        IVideoOptionalValueBuilder WithDeniedCountries(string countries);

        IVideoOptionalValueBuilder WithDeniedCountries(IEnumerable<string> countries);

        IVideoOptionalValueBuilder WithGalleryLocation(string url);

        IVideoOptionalValueBuilder WithGalleryLocation(string url, string protocol);

        IVideoOptionalValueBuilder WithGalleryLocation(string url, string protocol, string hostName);

        IVideoOptionalValueBuilder WithGalleryLocation(Func<IVideoGalleryLocationStarter, IVideoGalleryLocationOptionalValueBuilder> expression);

        IVideoOptionalValueBuilder AddPrice(Func<IVideoPriceStarter, IVideoPriceOptionalValueBuilder> expression);

        IVideoOptionalValueBuilder IsSubscriptionRequired(bool requiresSubscription);

        IVideoOptionalValueBuilder WithUploader(Func<IVideoContentUploaderStarter, IVideoContentUploaderBuilder> exprssion);
        
        IVideoOptionalValueBuilder WithAllowedPlatform(VideoPlatform platform);

        IVideoOptionalValueBuilder WithAllowedPlatforms(string platforms);

        IVideoOptionalValueBuilder WithDeniedPlatform(VideoPlatform platform);

        IVideoOptionalValueBuilder WithDeniedPlatforms(string platforms);

        IVideoOptionalValueBuilder IsLive(bool live);
    }
}
