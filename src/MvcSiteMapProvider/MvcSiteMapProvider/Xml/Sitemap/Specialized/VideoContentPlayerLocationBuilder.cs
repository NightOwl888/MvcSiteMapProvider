using System;
using MvcSiteMapProvider.Xml.Sitemap.Specialized.Video;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized
{
    public class VideoContentPlayerLocationBuilder
        : IVideoPlayerLocationStarter, IVideoPlayerLocationOptionalValueBuilder
    {
        public VideoContentPlayerLocationBuilder(
            string url
            )
            : this(url: url, protocol: string.Empty, hostName: string.Empty, allowEmbed: null, autoPlay: string.Empty)
        {
        }

        public VideoContentPlayerLocationBuilder(
            string url,
            string protocol
            )
            : this(url: url, protocol: protocol, hostName: string.Empty, allowEmbed: null, autoPlay: string.Empty)
        {
        }

        public VideoContentPlayerLocationBuilder(
            string url,
            string protocol,
            string hostName
            )
            : this(url: url, protocol: protocol, hostName: hostName, allowEmbed: null, autoPlay: string.Empty)
        {
        }

        private VideoContentPlayerLocationBuilder(
            string url,
            string protocol,
            string hostName,
            bool? allowEmbed,
            string autoPlay
            )
        {
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

        public IVideoPlayerLocationOptionalValueBuilder WithUrl(string url)
        {
            return new VideoContentPlayerLocationBuilder(url, this.protocol, this.hostName, this.allowEmbed, this.autoPlay);
        }

        public IVideoPlayerLocationOptionalValueBuilder WithUrl(string url, string protocol)
        {
            return new VideoContentPlayerLocationBuilder(url, protocol, this.hostName, this.allowEmbed, this.autoPlay);
        }

        public IVideoPlayerLocationOptionalValueBuilder WithUrl(string url, string protocol, string hostName)
        {
            return new VideoContentPlayerLocationBuilder(url, protocol, hostName, this.allowEmbed, this.autoPlay);
        }

        public IVideoPlayerLocationOptionalValueBuilder WithAllowEmbed(bool allowEmbed)
        {
            return new VideoContentPlayerLocationBuilder(this.url, this.protocol, this.hostName, allowEmbed, this.autoPlay);
        }

        public IVideoPlayerLocationOptionalValueBuilder WithAutoPlay(string autoPlay)
        {
            return new VideoContentPlayerLocationBuilder(this.url, this.protocol, this.hostName, this.allowEmbed, autoPlay);
        }

        public IVideoContentPlayerLocation Create()
        {
            return new VideoContentPlayerLocation(this.url, this.protocol, this.hostName, this.allowEmbed, this.autoPlay);
        }
    }
}
