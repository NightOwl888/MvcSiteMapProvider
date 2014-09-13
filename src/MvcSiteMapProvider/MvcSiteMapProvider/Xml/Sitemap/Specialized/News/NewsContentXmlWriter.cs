using System;
using System.Xml;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized.News
{
    public class NewsContentXmlWriter
        : ISpecializedContentXmlWriter
    {
        public NewsContentXmlWriter(
            XmlWriter writer,
            IPreparedNewsContentFactory preparedNewsContentFactory
            )
        {
            if (writer == null)
                throw new ArgumentNullException("writer");
            if (preparedNewsContentFactory == null)
                throw new ArgumentNullException("preparedNewsContentFactory");

            this.writer = writer;
            this.preparedNewsContentFactory = preparedNewsContentFactory;
        }
        private readonly XmlWriter writer;
        private readonly IPreparedNewsContentFactory preparedNewsContentFactory;

        public void WriteNamespace()
        {
            this.writer.WriteAttributeString("xmlns", "news", null, "http://www.google.com/schemas/sitemap-news/0.9");
        }

        public void WriteContent(ISpecializedContent content, IXmlSitemapUrlResolver urlResolver, Globalization.ICultureContext cultureContext)
        {
            var newsContent = content as INewsContent;
            if (newsContent != null)
            {
                var preparedNewsContent = this.preparedNewsContentFactory.Create(newsContent, urlResolver, cultureContext);
                if (preparedNewsContent != null)
                {
                    this.WriteNews(preparedNewsContent);
                }
            }
        }

        protected virtual void WriteNews(IPreparedNewsContent content)
        {
            string prefix = "news";
            string ns = null;

            this.writer.WriteStartElement(prefix, "news", ns);

            if (!string.IsNullOrEmpty(content.PublicationName) || !string.IsNullOrEmpty(content.PublicationLanguage))
            {
                this.writer.WriteStartElement(prefix, "publication", ns);

                if (!string.IsNullOrEmpty(content.PublicationName))
                {
                    this.writer.WriteElementString(prefix, "name", ns, content.PublicationName);
                }

                if (!string.IsNullOrEmpty(content.PublicationLanguage))
                {
                    this.writer.WriteElementString(prefix, "language", ns, content.PublicationLanguage);
                }

                this.writer.WriteEndElement(); // publication
            }

            if (!string.IsNullOrEmpty(content.Access))
            {
                this.writer.WriteElementString(prefix, "access", ns, content.Access);
            }

            if (!string.IsNullOrEmpty(content.Genres))
            {
                this.writer.WriteElementString(prefix, "genres", ns, content.Genres);
            }

            if (!string.IsNullOrEmpty(content.PublicationDate))
            {
                this.writer.WriteElementString(prefix, "publication_date", ns, content.PublicationDate);
            }

            if (!string.IsNullOrEmpty(content.Title))
            {
                this.writer.WriteElementString(prefix, "title", ns, content.Title);
            }

            if (!string.IsNullOrEmpty(content.Keywords))
            {
                this.writer.WriteElementString(prefix, "keywords", ns, content.Keywords);
            }

            if (!string.IsNullOrEmpty(content.StockTickers))
            {
                this.writer.WriteElementString(prefix, "stock_tickers", ns, content.StockTickers);
            }

            this.writer.WriteEndElement(); // news
        }
    }
}
