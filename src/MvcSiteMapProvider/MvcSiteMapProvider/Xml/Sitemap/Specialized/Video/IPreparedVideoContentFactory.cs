using System;
using MvcSiteMapProvider.Globalization;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized.Video
{
    public interface IPreparedVideoContentFactory
    {
        IPreparedVideoContent Create(IVideoContent videoContent, IXmlSitemapUrlResolver urlResolver, ICultureContext cultureContext);
        void Release(IPreparedVideoContent preparedSpecializedContent);
    }
}
