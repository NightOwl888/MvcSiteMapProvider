using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized
{
    public static class ISpecializedContentStarterExtensions
    {
        // Image content

        public static IImageContentBuilder Image(this ISpecializedContentStarter starter, string url)
        {
            return new ImageContentBuilder(url);
        }

        public static IImageContentBuilder Image(this ISpecializedContentStarter starter, string url, string protocol)
        {
            return new ImageContentBuilder(url, protocol);
        }

        public static IImageContentBuilder Image(this ISpecializedContentStarter starter, string url, string protocol, string hostName)
        {
            return new ImageContentBuilder(url, protocol, hostName);
        }

        // Mobile content

        public static IMobileContentBuilder Mobile(this ISpecializedContentStarter starter)
        {
            return new MobileContentBuilder();
        }

        // News content

        public static INewsPublicationNameBuilder News(this ISpecializedContentStarter starter)
        {
            return new NewsContentBuilder();
        }

        // Video content

        public static IVideoThumbnailLocationBuilder Video(this ISpecializedContentStarter starter)
        {
            return new VideoContentBuilder();
        }

    }
}
