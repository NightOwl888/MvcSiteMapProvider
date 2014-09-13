using System;
using MvcSiteMapProvider.ComponentModel;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized
{
    public interface IVideoPlayerLocationStarter
        : IFluentInterface
    {
        IVideoPlayerLocationOptionalValueBuilder WithUrl(string url);

        IVideoPlayerLocationOptionalValueBuilder WithUrl(string url, string protocol);

        IVideoPlayerLocationOptionalValueBuilder WithUrl(string url, string protocol, string hostName);
    }
}
