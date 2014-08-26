using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized
{
    public interface ISpecializedContentStarter
    {
        IImageContentBuilder Image(string url);
        IImageContentBuilder Image(string url, string protocol);
        IImageContentBuilder Image(string url, string protocol, string hostName);
    }
}
