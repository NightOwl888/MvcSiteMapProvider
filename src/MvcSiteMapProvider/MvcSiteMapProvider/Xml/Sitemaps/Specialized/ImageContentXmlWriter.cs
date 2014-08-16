using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace MvcSiteMapProvider.Xml.Sitemaps.Specialized
{
    public class ImageContentXmlWriter
        : ISpecializedContentXmlWriter
    {
        public ImageContentXmlWriter(
            XmlWriter writer)
        {
            if (writer == null)
                throw new ArgumentNullException("writer");
            this.writer = writer;
        }
        private readonly XmlWriter writer;

        public void WriteNamespace()
        {
            this.writer.WriteAttributeString("xmlns", "image", null, "http://www.google.com/schemas/sitemap-image/1.1");
        }

        public void WriteContent(ISpecializedContent content)
        {
            var imageContent = content as IImageContent;
            if (imageContent != null)
            {
                this.WriteImage(imageContent);
            }
        }

        private void WriteImage(IImageContent content)
        {
            string prefix = "image";
            string ns = null;

            this.writer.WriteStartElement(prefix, "image", ns);

            this.writer.WriteElementString(prefix, "loc", ns, content.Location);

            if (!string.IsNullOrEmpty(content.Caption))
            {
                this.writer.WriteElementString(prefix, "caption", ns, content.Caption);
            }

            if (!string.IsNullOrEmpty(content.GeoLocation))
            {
                this.writer.WriteElementString(prefix, "geo_location", ns, content.GeoLocation);
            }

            if (!string.IsNullOrEmpty(content.Title))
            {
                this.writer.WriteElementString(prefix, "title", ns, content.Title);
            }

            if (!string.IsNullOrEmpty(content.License))
            {
                this.writer.WriteElementString(prefix, "license", ns, content.License);
            }

            this.writer.WriteEndElement();
        }
    }
}
