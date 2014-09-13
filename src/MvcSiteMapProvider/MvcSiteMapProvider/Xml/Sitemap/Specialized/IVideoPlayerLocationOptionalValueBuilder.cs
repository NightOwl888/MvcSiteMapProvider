using System;
using MvcSiteMapProvider.ComponentModel;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized
{
    public interface IVideoPlayerLocationOptionalValueBuilder
        : IFluentInterface
    {
        IVideoPlayerLocationOptionalValueBuilder WithAllowEmbed(bool allowEmbed);

        IVideoPlayerLocationOptionalValueBuilder WithAutoPlay(string autoPlay);

        IVideoContentPlayerLocation Create();
    }
}
