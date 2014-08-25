using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvcSiteMapProvider.Xml.Sitemap.Specialized.Video;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized
{
    public interface IVideoContentPrice
    {
        decimal Price { get; set; }
        string Currency { get; set; }
        PriceType Type { get; set; }
        VideoResolution Resolution { get; set; }
    }
}
