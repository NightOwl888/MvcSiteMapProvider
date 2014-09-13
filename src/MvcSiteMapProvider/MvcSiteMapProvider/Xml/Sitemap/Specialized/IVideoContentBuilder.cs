using System;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized
{
    public interface IVideoContentBuilder
        : IVideoThumbnailLocationBuilder, IVideoTitleBuilder, IVideoDescriptionBuilder, IVideoOptionalValueBuilder
    {
    }
}
