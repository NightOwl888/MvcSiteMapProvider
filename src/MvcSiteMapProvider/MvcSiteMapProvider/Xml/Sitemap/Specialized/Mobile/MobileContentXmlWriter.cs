using System;
using System.Xml;
using MvcSiteMapProvider.Globalization;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized.Mobile
{
    public class MobileContentXmlWriter
        : ISpecializedContentXmlWriter
    {
        public MobileContentXmlWriter(
            XmlWriter writer
            )
        {
            if (writer == null)
                throw new ArgumentNullException("writer");

            this.writer = writer;
        }
        private readonly XmlWriter writer;

        public virtual void WriteNamespace()
        {
            this.writer.WriteAttributeString("xmlns", "mobile", null, "http://www.google.com/schemas/sitemap-mobile/1.0");
        }

        public virtual void WriteContent(ISpecializedContent content, IXmlSitemapUrlResolver urlResolver, ICultureContext cultureContext)
        {
            var mobileContent = content as IMobileContent;
            if (mobileContent != null)
            {
                this.WriteMobile();
            }
        }

        protected virtual void WriteMobile()
        {
            string prefix = "mobile";
            string ns = null;

            this.writer.WriteStartElement(prefix, "mobile", ns);
            this.writer.WriteEndElement();
        }
    }
}
