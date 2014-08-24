using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized
{
    public class PreparedImageContent
        : IPreparedImageContent
    {
        public PreparedImageContent(
            string location,
            string caption,
            string geoLocation,
            string title,
            string license)
        {
            if (string.IsNullOrEmpty(location))
                throw new ArgumentNullException("location");
            if (string.IsNullOrEmpty(caption))
                throw new ArgumentNullException("caption");
            if (string.IsNullOrEmpty(geoLocation))
                throw new ArgumentNullException("geoLocation");
            if (string.IsNullOrEmpty(title))
                throw new ArgumentNullException("title");
            if (string.IsNullOrEmpty(license))
                throw new ArgumentNullException("license");

            this.location = location;
            this.caption = caption;
            this.geoLocation = geoLocation;
            this.title = title;
            this.license = license;
        }
        private readonly string location;
        private readonly string caption;
        private readonly string geoLocation;
        private readonly string title;
        private readonly string license;

        public string Location
        {
            get { return this.location; }
        }

        public string Caption
        {
            get { return this.caption; }
        }

        public string GeoLocation
        {
            get { return this.geoLocation; }
        }

        public string Title
        {
            get { return this.title; }
        }

        public string License
        {
            get { return this.license; }
        }
    }
}
