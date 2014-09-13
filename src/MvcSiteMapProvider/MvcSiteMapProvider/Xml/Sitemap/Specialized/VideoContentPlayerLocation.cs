using System;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized
{
    public class VideoContentPlayerLocation
        : IVideoContentPlayerLocation
    {
        public VideoContentPlayerLocation(
            string url,
            string protocol,
            string hostName,
            bool? allowEmbed,
            string autoPlay
            )
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException("url");

            this.url = url;
            this.protocol = protocol;
            this.hostName = hostName;
            this.allowEmbed = allowEmbed;
            this.autoPlay = autoPlay;
        }

        private readonly string url;
        private readonly string protocol;
        private readonly string hostName;
        private readonly bool? allowEmbed;
        private readonly string autoPlay;

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

        public bool? AllowEmbed
        {
            get { return this.allowEmbed; }
        }

        public string AutoPlay
        {
            get { return this.autoPlay; }
        }
    }
}
