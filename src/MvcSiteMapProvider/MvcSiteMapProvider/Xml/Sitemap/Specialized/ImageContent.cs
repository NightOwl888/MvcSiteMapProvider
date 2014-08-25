using System;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized
{
    public class ImageContent
        : IImageContent
    {
        public ImageContent(
            string url)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException("url");
            this.url = url;
        }
        private string url;

        public string Url 
        {
            get { return this.url; }
            set { this.url = value; }
        }

        public string Protocol { get; set; }

        public string HostName { get; set; }

        public string Caption { get; set; }

        public string GeoLocation { get; set; }

        public string Title { get; set; }

        public string LicenseUrl { get; set; }

        public string LicenseProtocol { get; set; }

        public string LicenseHostName { get; set; }
    }
}
