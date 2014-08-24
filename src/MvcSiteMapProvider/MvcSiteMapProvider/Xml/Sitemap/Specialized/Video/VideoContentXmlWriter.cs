using System;
using System.Xml;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized.Video
{
    public class VideoContentXmlWriter
        : ISpecializedContentXmlWriter
    {
        public VideoContentXmlWriter(
            XmlWriter writer)
        {
            if (writer == null)
                throw new ArgumentNullException("writer");
            this.writer = writer;
        }
        private readonly XmlWriter writer;

        public void WriteNamespace()
        {
            this.writer.WriteAttributeString("xmlns", "video", null, "http://www.google.com/schemas/sitemap-video/1.1");
        }

        public void WriteContent(IPreparedSpecializedContent content)
        {
            var videoContent = content as IPreparedVideoContent;
            if (videoContent != null)
            {
                this.WriteVideo(videoContent);
            }
        }

        private void WriteVideo(IPreparedVideoContent content)
        {
            string prefix = "video";
            string ns = null;

            this.writer.WriteStartElement(prefix, "video", ns);

            this.writer.WriteElementString(prefix, "loc", ns, content.Location);

            if (!string.IsNullOrEmpty(content.Video))
            {
                this.writer.WriteElementString(prefix, "video", ns, content.Video);
            }

            if (!string.IsNullOrEmpty(content.ThumbnailLocation))
            {
                this.writer.WriteElementString(prefix, "thumbnail_loc", ns, content.ThumbnailLocation);
            }

            if (!string.IsNullOrEmpty(content.Title))
            {
                this.writer.WriteElementString(prefix, "title", ns, content.Title);
            }

            if (!string.IsNullOrEmpty(content.Description))
            {
                this.writer.WriteElementString(prefix, "description", ns, content.Description);
            }

            // TODO: Finish other video elements

            this.writer.WriteEndElement();
        }
    }
}
