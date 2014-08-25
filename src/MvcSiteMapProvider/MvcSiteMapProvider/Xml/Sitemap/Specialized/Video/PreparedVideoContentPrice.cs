using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized.Video
{
    public class PreparedVideoContentPrice
        : IPreparedVideoContentPrice
    {
        public PreparedVideoContentPrice(
            string price, 
            string currency,
            string type,
            string resolution)
        {
            if (string.IsNullOrEmpty(price))
                throw new ArgumentNullException("price");
            if (string.IsNullOrEmpty(currency))
                throw new ArgumentNullException("currency");

            this.price = price;
            this.currency = currency;
            this.type = type;
            this.resolution = resolution;
        }
        private readonly string price;
        private readonly string currency;
        private readonly string type;
        private readonly string resolution;

        public string Price
        {
            get { return this.price; }
        }

        public string Currency
        {
            get { return this.currency; }
        }

        public string Type
        {
            get { return this.type; }
        }

        public string Resolution
        {
            get { return this.resolution; }
        }
    }
}
