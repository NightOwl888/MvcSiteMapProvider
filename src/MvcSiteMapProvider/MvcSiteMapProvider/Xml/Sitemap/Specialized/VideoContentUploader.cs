using System;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized
{
    public class VideoContentUploader
        : IVideoContentUploader
    {
        public VideoContentUploader(
            string name,
            string url,
            string protocol,
            string hostName
            )
        {
            this.name = name;
            this.url = url;
            this.protocol = protocol;
            this.hostName = hostName;
        }
        private readonly string name;
        private readonly string url;
        private readonly string protocol;
        private readonly string hostName;

        public string Name
        {
            get { return this.name; }
        }

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
    }
}
