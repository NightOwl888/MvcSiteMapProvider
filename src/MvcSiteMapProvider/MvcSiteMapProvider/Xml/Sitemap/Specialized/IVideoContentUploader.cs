using System;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized
{
    public interface IVideoContentUploader
    {
        string Name { get; }
        string Url { get; }
        string Protocol { get; }
        string HostName { get; }
    }
}
