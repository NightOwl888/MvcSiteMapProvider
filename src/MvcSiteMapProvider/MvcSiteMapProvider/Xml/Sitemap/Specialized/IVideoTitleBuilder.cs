using System;
using MvcSiteMapProvider.ComponentModel;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized
{
    public interface IVideoTitleBuilder
        : IFluentInterface
    {
        IVideoDescriptionBuilder WithTitle(string title);
    }
}
