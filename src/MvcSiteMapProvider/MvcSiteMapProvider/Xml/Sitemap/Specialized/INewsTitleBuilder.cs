using System;
using MvcSiteMapProvider.ComponentModel;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized
{
    public interface INewsTitleBuilder
        : IFluentInterface
    {
        INewsOptionalValueBuilder WithTitle(string title);
    }
}
