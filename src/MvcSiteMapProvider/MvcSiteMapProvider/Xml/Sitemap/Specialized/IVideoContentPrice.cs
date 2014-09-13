using System;
using MvcSiteMapProvider.Xml.Sitemap.Specialized.Video;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized
{
    public interface IVideoContentPrice
    {
        decimal Price { get; }
        string Currency { get; }
        VideoPriceType Type { get; set; }
        VideoResolution Resolution { get; set; }
    }
}
