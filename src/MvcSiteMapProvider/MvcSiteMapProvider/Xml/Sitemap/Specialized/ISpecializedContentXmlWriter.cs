using System;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized
{
    public interface ISpecializedContentXmlWriter 
    {
        void WriteNamespace();
        void WriteContent(ISpecializedContent content);
    }
}
