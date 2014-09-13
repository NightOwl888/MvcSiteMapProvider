using System;
using System.Collections.Generic;
using System.Linq;
using MvcSiteMapProvider.Globalization;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized.Video
{
    public class PreparedVideoContentFactory
        : IPreparedVideoContentFactory
    {
        private const string W3CDateFormat = "yyyy-MM-ddTHH:mm:ss.fffffffzzz";

        public IPreparedVideoContent Create(IVideoContent videoContent, IXmlSitemapUrlResolver urlResolver, ICultureContext cultureContext)
        {
            if (videoContent != null)
            {
                // Required fields
                string thumbnail = urlResolver.ResolveUrlToAbsolute(videoContent.ThumbnailLocation, videoContent.ThumbnailLocationProtocol, videoContent.ThumbnailLocationHostName);
                string title = videoContent.Title;
                string description = videoContent.Description;
                string contentLocation = string.Empty;
                string playerLocation = string.Empty;
                if (!string.IsNullOrEmpty(videoContent.ContentLocation))
                {
                    contentLocation = urlResolver.ResolveUrlToAbsolute(videoContent.ContentLocation, videoContent.ContentLocationProtocol, videoContent.ContentLocationHostName);
                }
                if (!string.IsNullOrEmpty(videoContent.PlayerLocation))
                {
                    playerLocation = urlResolver.ResolveUrlToAbsolute(videoContent.PlayerLocation, videoContent.PlayerLocationProtocol, videoContent.PlayerLocationHostName);
                }

                // Optional fields
                string playerLocationAllowEmbed = string.Empty;
                string duration = string.Empty;
                string expirationDate = string.Empty;
                string rating = string.Empty;
                string viewCount = string.Empty;
                string publicationDate = string.Empty;
                string familyFriendly = string.Empty;
                string restriction = string.Empty;
                string restrictionRelationship = string.Empty;
                string galleryLocation = string.Empty;
                var prices = new List<IPreparedVideoContentPrice>();
                string requiresSubscription = string.Empty;
                string uploaderInfo = string.Empty;
                string platform = string.Empty;
                string platformRelationship = string.Empty;
                string live = string.Empty;

                if (videoContent.PlayerLocationAllowEmbed != null)
                {
                    playerLocationAllowEmbed = (videoContent.PlayerLocationAllowEmbed == true) ? "Yes" : "No";
                }
                if (videoContent.Duration > 0)
                {
                    duration = videoContent.Duration.ToString();
                }
                if (videoContent.ExpirationDate > DateTime.MinValue)
                {
                    expirationDate = videoContent.ExpirationDate.ToString(W3CDateFormat);
                }
                if (videoContent.Rating > 0)
                {
                    rating = videoContent.Rating.ToString("0.0");
                }
                if (videoContent.ViewCount > 0)
                {
                    viewCount = videoContent.ViewCount.ToString();
                }
                if (videoContent.PublicationDate > DateTime.MinValue)
                {
                    publicationDate = videoContent.PublicationDate.ToString(W3CDateFormat);
                }
                if (!videoContent.IsFamilyFriendly)
                {
                    familyFriendly = "No";
                }
                if (videoContent.CountriesAllowed.Any())
                {
                    restriction = string.Join(" ", videoContent.CountriesAllowed.ToArray());
                    restrictionRelationship = "allow";
                }
                else if (videoContent.CountriesNotAllowed.Any())
                {
                    restriction = string.Join(" ", videoContent.CountriesNotAllowed.ToArray());
                    restrictionRelationship = "deny";
                }
                if (!string.IsNullOrEmpty(videoContent.GalleryLocation))
                {
                    galleryLocation = urlResolver.ResolveUrlToAbsolute(videoContent.GalleryLocation, videoContent.GalleryLocationProtocol, videoContent.GalleryLocationHostName);
                }
                if (videoContent.Prices.Any())
                {
                    string currency = string.Empty;
                    string currencyFormatString = string.Empty;
                    string type = string.Empty;
                    string resolution = string.Empty;

                    foreach (var price in videoContent.Prices)
                    {
                        currency = price.Currency.ToUpper();
                        currencyFormatString = this.GetCurrencyFormatString(currency);
                        type = (price.Type == VideoPriceType.Undefined) ? string.Empty : price.Type.ToString().ToLower();
                        resolution = (price.Resolution == VideoResolution.Undefined) ? string.Empty : price.Resolution.ToString().ToUpper();

                        prices.Add(new PreparedVideoContentPrice(price.Price.ToString(currencyFormatString), currency, type, resolution));
                    }
                }
                if (videoContent.RequiresSubscription)
                {
                    requiresSubscription = "yes";
                }
                if (!string.IsNullOrEmpty(videoContent.UploaderInfo))
                {
                    uploaderInfo = urlResolver.ResolveUrlToAbsolute(videoContent.UploaderInfo, videoContent.UploaderInfoProtocol, videoContent.UploaderInfoHostName);
                }
                if (videoContent.PlatformsAllowed != VideoPlatform.Undefined)
                {
                    platform = videoContent.PlatformsAllowed.ToString("F").Replace(", ", " ").ToLower();
                    platformRelationship = "allow";
                }
                else if (videoContent.PlatformsNotAllowed != VideoPlatform.Undefined)
                {
                    platform = videoContent.PlatformsNotAllowed.ToString("F").Replace(", ", " ").ToLower();
                    platformRelationship = "deny";
                }
                if (videoContent.Live)
                {
                    live = "yes";
                }

                return new PreparedVideoContent(
                    thumbnail, 
                    title, 
                    description, 
                    contentLocation, 
                    playerLocation,
                    playerLocationAllowEmbed,
                    videoContent.PlayerLocationAutoPlay,
                    duration, 
                    expirationDate, 
                    rating, 
                    viewCount, 
                    publicationDate, 
                    familyFriendly, 
                    videoContent.Tags, 
                    videoContent.Categories, 
                    restriction, 
                    restrictionRelationship, 
                    galleryLocation, 
                    videoContent.GalleryLocationTitle, 
                    prices, 
                    requiresSubscription, 
                    videoContent.Uploader,
                    uploaderInfo, 
                    platform, 
                    platformRelationship,
                    live);
            }

            return null;
        }

        public void Release(IPreparedVideoContent preparedVideoContent)
        {
            var disposable = preparedVideoContent as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }

        public Type ContentType
        {
            get { return typeof(IVideoContent); }
        }

        private string GetCurrencyFormatString(string currencyCode)
        {
            var currency = currencyCode.ToUpper();
            switch (currency)
            {
                case "XBT": // 8
                    return "0.00000000";

                case "COU": // 4
                    return "0.0000";

                case "BHD": // 3
                case "IQD":
                case "JOD":
                case "KWD":
                case "LYD":
                case "OMR":
                case "TND":
                    return "0.000";

                case "BIF": // 0
                case "BYR":
                case "CLF":
                case "CLP":
                case "CVE":
                case "DJF":
                case "GNF":
                case "ISK":
                case "JPY":
                case "KMF":
                case "KRW":
                case "PYG":
                case "RWF":
                case "UGX":
                case "UYI":
                case "VND":
                case "VUV":
                case "XAF":
                case "XOF":
                case "XPF":
                    return "0";

                default: // 2
                    return "0.00";
            }
        }
    }
}
