using System;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized
{
    public class MobileContentBuilder
        : IMobileContentBuilder
    {
        public ISpecializedContent Create()
        {
            return new MobileContent();
        }
    }
}
