using System;
using MvcSiteMapProvider.Globalization;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized.News
{
    public interface IPreparedNewsContentFactory
    {
        IPreparedNewsContent Create(INewsContent newsContent, IXmlSitemapUrlResolver urlResolver, ICultureContext cultureContext);
        void Release(IPreparedNewsContent preparedNewsContent);
    }
}
