using System;
using MvcSiteMapProvider.ComponentModel;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized
{
    public interface ISpecializedContentBuilder
        : IFluentInterface
    {
        ISpecializedContent Create();
    }
}
