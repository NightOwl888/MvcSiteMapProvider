using System;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized
{
    public interface IVideoGalleryLocationStarter
    {
        IVideoGalleryLocationOptionalValueBuilder WithUrl(string url);

        IVideoGalleryLocationOptionalValueBuilder WithUrl(string url, string protocol);

        IVideoGalleryLocationOptionalValueBuilder WithUrl(string url, string protocol, string hostName);
    }
}
