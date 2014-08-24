using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvcSiteMapProvider.Globalization;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized.Video
{
    public class PreparedVideoContentFactory
        : IPreparedSpecializedContentFactory
    {
        // TODO: Finish implementation with additional properties

        public IPreparedSpecializedContent Create(ISpecializedContent specializedContent, ISitemapUrlResolver urlResolver, ICultureContext cultureContext)
        {
            var videoContent = specializedContent as IVideoContent;
            if (videoContent != null)
            {
                string location = urlResolver.ResolveUrlToAbsolute(videoContent.Url, videoContent.Protocol, videoContent.HostName);
                string video = videoContent.Video;
                string thumbnail = urlResolver.ResolveUrlToAbsolute(videoContent.ThumbnailUrl, videoContent.ThumbnailProtocol, videoContent.ThumbnailHostName);
                string title = videoContent.Title;
                string description = videoContent.Description;

                return new PreparedVideoContent(location, video, thumbnail, title, description);
            }

            return null;
        }

        public void Release(IPreparedSpecializedContent preparedSpecializedContent)
        {
            var disposable = preparedSpecializedContent as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }

        public Type ContentType
        {
            get { return typeof(IVideoContent); }
        }
    }
}
