using System;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized
{
    public class VideoContentUploaderBuilder
        : IVideoContentUploaderStarter, IVideoContentUploaderBuilder
    {
        public VideoContentUploaderBuilder(string name)
            : this(name: name, url: string.Empty, protocol: string.Empty, hostName: string.Empty)
        {
        }

        private VideoContentUploaderBuilder(
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

        public IVideoContentUploaderBuilder WithName(string name)
        {
            return new VideoContentUploaderBuilder(name, this.url, this.protocol, this.hostName);
        }

        public IVideoContentUploaderBuilder WithUrl(string url)
        {
            return new VideoContentUploaderBuilder(this.name, url, this.protocol, this.hostName);
        }

        public IVideoContentUploaderBuilder WithUrl(string url, string protocol)
        {
            return new VideoContentUploaderBuilder(this.name, url, protocol, this.hostName);
        }

        public IVideoContentUploaderBuilder WithUrl(string url, string protocol, string hostName)
        {
            return new VideoContentUploaderBuilder(this.name, url, protocol, hostName);
        }

        public IVideoContentUploader Create()
        {
            return new VideoContentUploader(this.name, this.url, this.protocol, this.hostName);
        }
    }
}
