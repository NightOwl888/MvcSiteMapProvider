using System;
using System.Linq;
using System.Xml;
using MvcSiteMapProvider.Globalization;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized.Video
{
    public class VideoContentXmlWriter
        : ISpecializedContentXmlWriter
    {
        public VideoContentXmlWriter(
            XmlWriter writer,
            IPreparedVideoContentFactory preparedVideoContentFactory
            )
        {
            if (writer == null)
                throw new ArgumentNullException("writer");
            if (preparedVideoContentFactory == null)
                throw new ArgumentNullException("preparedVideoContentFactory");
            
            this.writer = writer;
            this.preparedVideoContentFactory = preparedVideoContentFactory;
        }
        private readonly XmlWriter writer;
        private readonly IPreparedVideoContentFactory preparedVideoContentFactory;

        public void WriteNamespace()
        {
            this.writer.WriteAttributeString("xmlns", "video", null, "http://www.google.com/schemas/sitemap-video/1.1");
        }

        public virtual void WriteContent(ISpecializedContent content, IXmlSitemapUrlResolver urlResolver, ICultureContext cultureContext)
        {
            var videoContent = content as IVideoContent;
            if (videoContent != null)
            {
                var preparedVideoContent = this.preparedVideoContentFactory.Create(videoContent, urlResolver, cultureContext);
                if (preparedVideoContent != null)
                {
                    this.WriteVideo(preparedVideoContent);
                }
            }
        }

        protected virtual void WriteVideo(IPreparedVideoContent content)
        {
            string prefix = "video";
            string ns = null;

            this.writer.WriteStartElement(prefix, "video", ns);

            this.writer.WriteElementString(prefix, "thumbnail_loc", ns, content.ThumbnailLocation);
            this.writer.WriteElementString(prefix, "title", ns, content.Title);
            this.writer.WriteElementString(prefix, "description", ns, content.Description);

            if (!string.IsNullOrEmpty(content.ContentLocation))
            {
                this.writer.WriteElementString(prefix, "content_loc", ns, content.ContentLocation);
            }

            if (!string.IsNullOrEmpty(content.PlayerLocation))
            {
                this.writer.WriteStartElement(prefix, "player_loc", ns);
                if (!string.IsNullOrEmpty(content.PlayerLocationAllowEmbed))
                {
                    this.writer.WriteAttributeString("allow_embed", ns, content.PlayerLocationAllowEmbed);
                }
                if (!string.IsNullOrEmpty(content.PlayerLocationAutoPlay))
                {
                    this.writer.WriteAttributeString("autoplay", ns, content.PlayerLocationAutoPlay);
                }
                this.writer.WriteString(content.PlayerLocation);
                this.writer.WriteEndElement();
            }

            if (!string.IsNullOrEmpty(content.Duration))
            {
                this.writer.WriteElementString(prefix, "duration", ns, content.Duration);
            }

            if (!string.IsNullOrEmpty(content.ExpirationDate))
            {
                this.writer.WriteElementString(prefix, "expiration_date", ns, content.ExpirationDate);
            }

            if (!string.IsNullOrEmpty(content.Rating))
            {
                this.writer.WriteElementString(prefix, "rating", ns, content.Rating);
            }

            if (!string.IsNullOrEmpty(content.ViewCount))
            {
                this.writer.WriteElementString(prefix, "view_count", ns, content.ViewCount);
            }

            if (!string.IsNullOrEmpty(content.PublicationDate))
            {
                this.writer.WriteElementString(prefix, "publication_date", ns, content.PublicationDate);
            }

            if (!string.IsNullOrEmpty(content.FamilyFriendly))
            {
                this.writer.WriteElementString(prefix, "family_friendly", ns, content.FamilyFriendly);
            }

            if (content.Tags.Any())
            {
                foreach (var tag in content.Tags)
                {
                    this.writer.WriteElementString(prefix, "tag", ns, tag);
                }
            }

            if (content.Categories.Any())
            {
                foreach (var category in content.Categories)
                {
                    this.writer.WriteElementString(prefix, "category", ns, category);
                }
            }

            if (!string.IsNullOrEmpty(content.Restriction))
            {
                this.writer.WriteStartElement(prefix, "restriction", ns);
                this.writer.WriteAttributeString("relationship", ns, content.RestrictionRelationship);
                this.writer.WriteString(content.Restriction);
                this.writer.WriteEndElement();
            }

            if (!string.IsNullOrEmpty(content.GalleryLocation))
            {
                this.writer.WriteStartElement(prefix, "gallery_loc", ns);
                if (!string.IsNullOrEmpty(content.GalleryLocationTitle))
                {
                    this.writer.WriteAttributeString("title", ns, content.GalleryLocationTitle);
                }
                this.writer.WriteString(content.GalleryLocation);
                this.writer.WriteEndElement();
            }

            if (content.Prices.Any())
            {
                foreach (var price in content.Prices)
                {
                    this.writer.WriteStartElement(prefix, "price", ns);
                    this.writer.WriteAttributeString("currency", ns, price.Currency);
                    if (!string.IsNullOrEmpty(price.Type))
                    {
                        this.writer.WriteAttributeString("type", ns, price.Type);
                    }
                    if (!string.IsNullOrEmpty(price.Resolution))
                    {
                        this.writer.WriteAttributeString("resolution", ns, price.Resolution);
                    }
                    this.writer.WriteString(price.Price);
                    this.writer.WriteEndElement();
                }
            }

            if (!string.IsNullOrEmpty(content.RequiresSubscription))
            {
                this.writer.WriteElementString(prefix, "requires_subscription", ns, content.RequiresSubscription);
            }

            if (!string.IsNullOrEmpty(content.Uploader))
            {
                this.writer.WriteStartElement(prefix, "uploader", ns);
                if (!string.IsNullOrEmpty(content.UploaderInfo))
                {
                    this.writer.WriteAttributeString("info", ns, content.UploaderInfo);
                }
                this.writer.WriteString(content.Uploader);
                this.writer.WriteEndElement();
            }

            if (!string.IsNullOrEmpty(content.Platform))
            {
                this.writer.WriteStartElement(prefix, "platform", ns);
                this.writer.WriteAttributeString("relationship", ns, content.PlatformRelationship);
                this.writer.WriteString(content.Platform);
                this.writer.WriteEndElement();
            }

            if (!string.IsNullOrEmpty(content.Live))
            {
                this.writer.WriteElementString(prefix, "live", ns, content.Live);
            }

            this.writer.WriteEndElement();
        }
    }
}
