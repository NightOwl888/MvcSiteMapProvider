using System;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized
{
    public interface IVideoGalleryLocationOptionalValueBuilder
    {
        IVideoGalleryLocationOptionalValueBuilder WithTitle(string title);

        IVideoContentGalleryLocation Create();
    }
}
