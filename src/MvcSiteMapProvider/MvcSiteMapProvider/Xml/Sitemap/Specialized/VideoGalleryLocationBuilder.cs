using System;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized
{
    public class VideoGalleryLocationBuilder
        : IVideoGalleryLocationStarter, IVideoGalleryLocationOptionalValueBuilder
    {
        public VideoGalleryLocationBuilder(string url)
            : this(url: url, protocol: string.Empty, hostName: string.Empty, title: string.Empty)
        {
        }

        public VideoGalleryLocationBuilder(string url, string protocol)
            : this(url: url, protocol: protocol, hostName: string.Empty, title: string.Empty)
        {
        }

        public VideoGalleryLocationBuilder(string url, string protocol, string hostName)
            : this(url: url, protocol: protocol, hostName: hostName, title: string.Empty)
        {
        }

        private VideoGalleryLocationBuilder(
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

        public IVideoGalleryLocationOptionalValueBuilder WithUrl(string url)
        {
            return new VideoGalleryLocationBuilder(url, this.protocol, this.hostName, this.title);
        }

        public IVideoGalleryLocationOptionalValueBuilder WithUrl(string url, string protocol)
        {
            return new VideoGalleryLocationBuilder(url, protocol, this.hostName, this.title);
        }

        public IVideoGalleryLocationOptionalValueBuilder WithUrl(string url, string protocol, string hostName)
        {
            return new VideoGalleryLocationBuilder(url, protocol, hostName, this.title);
        }

        public IVideoGalleryLocationOptionalValueBuilder WithTitle(string title)
        {
            return new VideoGalleryLocationBuilder(this.url, this.protocol, this.hostName, title);
        }

        public IVideoContentGalleryLocation Create()
        {
            return new VideoContentGalleryLocation(this.url, this.protocol, this.hostName, this.title);
        }
    }
}
