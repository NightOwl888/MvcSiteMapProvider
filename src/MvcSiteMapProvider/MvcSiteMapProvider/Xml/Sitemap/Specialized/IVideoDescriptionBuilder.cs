using System;
using MvcSiteMapProvider.ComponentModel;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized
{
    public interface IVideoDescriptionBuilder
        : IFluentInterface
    {
        IVideoConditionalValueBuilder WithDescription(string description);
    }
}
