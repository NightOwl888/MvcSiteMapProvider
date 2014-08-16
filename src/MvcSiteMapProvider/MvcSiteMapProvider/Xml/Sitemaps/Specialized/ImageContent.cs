using System;

namespace MvcSiteMapProvider.Xml.Sitemaps.Specialized
{
    public class ImageContent
        : IImageContent
    {
        public ImageContent(
            string location)
        {
            if (string.IsNullOrEmpty(location))
                throw new ArgumentNullException("location");
            this.location = location;
        }
        private readonly string location;

        public string Location
        {
            get { return this.location; }
        }

        public string Caption
        {
            get { return string.Empty; }
        }

        public string GeoLocation
        {
            get { return string.Empty; }
        }

        public string Title
        {
            get { return string.Empty; }
        }

        public string License
        {
            get { return string.Empty; }
        }
    }
}
