using System;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized
{
    public interface IVideoContentGalleryLocation
    {
        string Url { get; }
        string Protocol { get; }
        string HostName { get; }
        string Title { get; }
    }
}
