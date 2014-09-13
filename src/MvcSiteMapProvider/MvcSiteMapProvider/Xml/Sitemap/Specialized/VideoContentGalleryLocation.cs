using System;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized
{
    public class VideoContentGalleryLocation
        : IVideoContentGalleryLocation
    {
        public VideoContentGalleryLocation(
            string url,
            string protocol,
            string hostName,
            string title
            )
        {
            this.url = url;
            this.protocol = protocol;
            this.hostName = hostName;
            this.title = title;
        }

        private readonly string url;
        private readonly string protocol;
        private readonly string hostName;
        private readonly string title;

        public string Url
        {
            get { return this.url; }
        }

        public string Protocol
        {
            get { return this.protocol; }
        }

        public string HostName
        {
            get { return this.hostName; }
        }

        public string Title
        {
            get { return this.title; }
        }
    }
}
