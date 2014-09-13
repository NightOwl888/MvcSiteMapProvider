using System;
using MvcSiteMapProvider.ComponentModel;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized
{
    public interface IVideoThumbnailLocationBuilder
        : IFluentInterface
    {
        IVideoTitleBuilder WithThumbnailLocation(string url);

        IVideoTitleBuilder WithThumbnailLocation(string url, string protocol);

        IVideoTitleBuilder WithThumbnailLocation(string url, string protocol, string hostName);
    }
}
