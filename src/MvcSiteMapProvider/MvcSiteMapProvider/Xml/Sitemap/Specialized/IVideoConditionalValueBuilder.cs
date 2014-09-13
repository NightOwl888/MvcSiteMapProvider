using System;
using MvcSiteMapProvider.ComponentModel;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized
{
    public interface IVideoConditionalValueBuilder
        : IFluentInterface
    {
        IVideoOptionalValueBuilder WithContentLocation(string url);

        IVideoOptionalValueBuilder WithContentLocation(string url, string protocol);

        IVideoOptionalValueBuilder WithContentLocation(string url, string protocol, string hostName);

        IVideoOptionalValueBuilder WithPlayerLocation(string url);

        IVideoOptionalValueBuilder WithPlayerLocation(string url, string protocol);

        IVideoOptionalValueBuilder WithPlayerLocation(string url, string protocol, string hostName);

        IVideoOptionalValueBuilder WithPlayerLocation(Func<IVideoPlayerLocationStarter, IVideoPlayerLocationOptionalValueBuilder> expression);
    }
}
