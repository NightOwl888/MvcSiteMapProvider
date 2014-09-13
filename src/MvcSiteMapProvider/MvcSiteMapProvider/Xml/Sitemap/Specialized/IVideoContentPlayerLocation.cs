using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized
{
    public interface IVideoContentPlayerLocation
    {
        string Url { get; }
        string Protocol { get; }
        string HostName { get; }
        bool? AllowEmbed { get; }
        string AutoPlay { get; }
    }
}
