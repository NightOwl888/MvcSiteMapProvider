using System;
using MvcSiteMapProvider.Xml.Sitemap.Specialized.Video;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized
{
    public class VideoPriceBuilder
        : IVideoPriceOptionalValueBuilder, IVideoPriceStarter
    {
        public VideoPriceBuilder(decimal price, string currency)
            : this(price: price, currency: currency, type: VideoPriceType.Undefined, resolution: VideoResolution.Undefined)
        {
        }

        internal VideoPriceBuilder()
            : this(price: 0, currency: string.Empty, type: VideoPriceType.Undefined, resolution: VideoResolution.Undefined)
        {
        }

        private VideoPriceBuilder(
            decimal price,
            string currency,
            VideoPriceType type,
            VideoResolution resolution)
        {
            this.price = price;
            this.currency = currency;
            this.type = type;
            this.resolution = resolution;
        }

        private readonly decimal price;
        private readonly string currency;
        private readonly VideoPriceType type;
        private readonly VideoResolution resolution;

        public IVideoPriceOptionalValueBuilder WithAmount(double price, string currencyCode)
        {
            return new VideoPriceBuilder((decimal)price, currencyCode, this.type, this.resolution);
        }

        public IVideoPriceOptionalValueBuilder WithAmount(decimal price, string currencyCode)
        {
            return new VideoPriceBuilder(price, currencyCode, this.type, this.resolution);
        }

        public IVideoPriceOptionalValueBuilder WithType(VideoPriceType type)
        {
            return new VideoPriceBuilder(this.price, this.currency, type, this.resolution);
        }

        public IVideoPriceOptionalValueBuilder WithResolution(VideoResolution resolution)
        {
            return new VideoPriceBuilder(this.price, this.currency, this.type, resolution);
        }

        public IVideoContentPrice Create()
        {
            return new VideoContentPrice(this.price, this.currency) 
            { 
                Type = this.type, 
                Resolution = this.resolution 
            };
        }
    }
}
