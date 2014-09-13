using System;
using MvcSiteMapProvider.ComponentModel;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized
{
    public interface IVideoContentUploaderBuilder
        : IFluentInterface
    {
        IVideoContentUploaderBuilder WithName(string name);

        IVideoContentUploaderBuilder WithUrl(string url);

        IVideoContentUploaderBuilder WithUrl(string url, string protocol);

        IVideoContentUploaderBuilder WithUrl(string url, string protocol, string hostName);

        IVideoContentUploader Create();
    }
}
