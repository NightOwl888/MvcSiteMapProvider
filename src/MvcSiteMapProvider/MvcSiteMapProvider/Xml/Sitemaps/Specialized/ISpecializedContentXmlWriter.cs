using System;

namespace MvcSiteMapProvider.Xml.Sitemaps.Specialized
{
    public interface ISpecializedContentXmlWriter 
    {
        void WriteNamespace();
        void WriteContent(ISpecializedContent content);
    }
}
