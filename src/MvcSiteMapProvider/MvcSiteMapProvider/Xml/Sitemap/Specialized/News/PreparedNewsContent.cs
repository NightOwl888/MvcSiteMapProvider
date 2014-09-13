using System;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized.News
{
    public class PreparedNewsContent
        : IPreparedNewsContent
    {
        public PreparedNewsContent(
            string publicationName,
            string publicationLanguage,
            string access,
            string genres,
            string publicationDate,
            string title,
            string keywords,
            string stockTickers
            )
        {
            if (string.IsNullOrEmpty(publicationName))
                throw new ArgumentNullException("publicationName");
            if (string.IsNullOrEmpty(publicationLanguage))
                throw new ArgumentNullException("publicationLanguage");
            if (string.IsNullOrEmpty(publicationDate))
                throw new ArgumentNullException("publicationDate");
            if (string.IsNullOrEmpty(title))
                throw new ArgumentNullException("title");

            this.publicationName = publicationName;
            this.publicationLanguage = publicationLanguage;
            this.access = access;
            this.genres = genres;
            this.publicationDate = publicationDate;
            this.title = title;
            this.keywords = keywords;
            this.stockTickers = stockTickers;
        }
        private readonly string publicationName;
        private readonly string publicationLanguage;
        private readonly string access;
        private readonly string genres;
        private readonly string publicationDate;
        private readonly string title;
        private readonly string keywords;
        private readonly string stockTickers;

        public string PublicationName
        {
            get { return this.publicationName; }
        }

        public string PublicationLanguage
        {
            get { return this.publicationLanguage; }
        }

        public string Access
        {
            get { return this.access; }
        }

        public string Genres
        {
            get { return this.genres; }
        }

        public string PublicationDate
        {
            get { return this.publicationDate; }
        }

        public string Title
        {
            get { return this.title; }
        }

        public string Keywords
        {
            get { return this.keywords; }
        }

        public string StockTickers
        {
            get { return this.stockTickers; }
        }
    }
}
