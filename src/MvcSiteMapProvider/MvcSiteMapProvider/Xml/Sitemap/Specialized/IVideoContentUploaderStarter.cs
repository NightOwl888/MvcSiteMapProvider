using System;
using MvcSiteMapProvider.ComponentModel;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized
{
    public interface IVideoContentUploaderStarter
        : IFluentInterface
    {
        IVideoContentUploaderBuilder WithName(string name);
    }
}
