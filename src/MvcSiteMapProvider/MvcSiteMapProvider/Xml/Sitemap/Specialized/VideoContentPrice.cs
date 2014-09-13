using System;
using MvcSiteMapProvider.Xml.Sitemap.Specialized.Video;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized
{
    public class VideoContentPrice
        : IVideoContentPrice
    {
        public VideoContentPrice(
            decimal price,
            string currency)
        {
            if (string.IsNullOrEmpty(currency))
                throw new ArgumentNullException("currency");
            this.price = price;
            this.currency = currency;
        }

        private readonly decimal price;
        private readonly string currency;

        public decimal Price
        {
            get { return this.price; }
        }

        public string Currency
        {
            get { return this.currency; }
        }

        public VideoPriceType Type { get; set; }

        public VideoResolution Resolution { get; set; }
    }
}
