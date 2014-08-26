using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized
{
    public class SpecializedContentStarter
        : ISpecializedContentStarter
    {
        public IImageContentBuilder Image(string url)
        {
            return new ImageContentBuilder(url);
        }

        public IImageContentBuilder Image(string url, string protocol)
        {
            return new ImageContentBuilder(url, protocol);
        }

        public IImageContentBuilder Image(string url, string protocol, string hostName)
        {
            return new ImageContentBuilder(url, protocol, hostName);
        }
    }
}
