using System;
using MvcSiteMapProvider.Globalization;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized
{
    public interface ISpecializedContentXmlWriter 
    {
        void WriteNamespace();
        void WriteContent(ISpecializedContent content, IXmlSitemapUrlResolver urlResolver, ICultureContext cultureContext);
    }
}
