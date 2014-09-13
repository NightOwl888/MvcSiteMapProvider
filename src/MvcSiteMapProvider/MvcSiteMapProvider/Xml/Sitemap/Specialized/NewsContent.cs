using System;
using System.Collections.Generic;
using MvcSiteMapProvider.Xml.Sitemap.Specialized.News;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized
{
    public class NewsContent
        : INewsContent
    {
        public NewsContent(
            string publicationName,
            string publicationLanguage,
            DateTime publicationDate,
            string title
            )
            : this(publicationName, 
            publicationLanguage, 
            publicationDate, 
            title, 
            NewsAccess.Undefined, 
            NewsGenre.Undefined, 
            new List<string>(), 
            new List<string>())
        {
        }

        internal NewsContent(
            string publicationName,
            string publicationLanguage,
            DateTime publicationDate,
            string title,
            NewsAccess access,
            NewsGenre genres,
            IList<string> keywords,
            IList<string> stockTickers
            )
        {
            if (string.IsNullOrEmpty(publicationName))
                throw new ArgumentNullException("publicationName");
            if (string.IsNullOrEmpty(publicationLanguage))
                throw new ArgumentNullException("publicationLanguage");
            if (publicationDate == DateTime.MinValue)
                throw new ArgumentNullException("publicationDate");
            if (string.IsNullOrEmpty(title))
                throw new ArgumentNullException("title");
            if (keywords == null)
                throw new ArgumentNullException("keywords");
            if (stockTickers == null)
                throw new ArgumentNullException("stockTickers");

            this.publicationName = publicationName;
            this.publicationLanguage = publicationLanguage;
            this.publicationDate = publicationDate;
            this.title = title;
            this.Access = access;
            this.Genres = genres;
            this.keywords = keywords;
            this.stockTickers = keywords;
        }

        private readonly string publicationName;
        private readonly string publicationLanguage;
        private readonly DateTime publicationDate;
        private readonly string title;
        private readonly IList<string> keywords;
        private readonly IList<string> stockTickers;

        private string genresString = string.Empty;
        private string keywordsString = string.Empty;
        private string stockTickersString = string.Empty;

        public string PublicationName
        {
            get { return this.publicationName; }
        }

        public string PublicationLanguage
        {
            get { return this.publicationLanguage; }
        }

        public NewsAccess Access { get; set; }

        public NewsGenre Genres { get; set; }

        public DateTime PublicationDate
        {
            get { return this.publicationDate; }
        }

        public string Title
        {
            get { return this.title; }
        }

        public IList<string> Keywords
        {
            get { return this.keywords; }
        }

        public IList<string> StockTickers
        {
            get { return this.stockTickers; }
        }
    }
}
