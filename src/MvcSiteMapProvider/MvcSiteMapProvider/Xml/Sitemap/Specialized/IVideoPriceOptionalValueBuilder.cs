using System;
using MvcSiteMapProvider.Xml.Sitemap.Specialized.Video;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized
{
    public interface IVideoPriceOptionalValueBuilder
    {
        IVideoPriceOptionalValueBuilder WithType(VideoPriceType type);

        IVideoPriceOptionalValueBuilder WithResolution(VideoResolution resolution);

        IVideoContentPrice Create();
    }
}
