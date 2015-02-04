using System;
using System.Collections.Generic;
using MvcSiteMapProvider.Xml.Sitemap.Specialized.News;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized
{
    public class NewsContentBuilder
        : INewsContentBuilder
    {
        public NewsContentBuilder()
            : this(
                publicationName: string.Empty,
                publicationDate: DateTime.MinValue,
                publicationLanguage: string.Empty,
                title: string.Empty,
                access: NewsAccess.Undefined,
                genres: NewsGenre.Undefined,
                keywords: new List<string>(),
                stockTickers: new List<string>()
            )
        {
        }

        private NewsContentBuilder(
            string publicationName,
            DateTime publicationDate,
            string publicationLanguage,
            string title,
            NewsAccess access,
            NewsGenre genres,
            IList<string> keywords,
            IList<string> stockTickers)
        {
            this.publicationName = publicationName;
            this.publicationDate = publicationDate;
            this.publicationLanguage = publicationLanguage;
            this.title = title;
            this.access = access;
            this.genres = genres;
            this.keywords = keywords;
            this.stockTickers = stockTickers;
        }

        private readonly string publicationName;
        private readonly DateTime publicationDate;
        private readonly string publicationLanguage;
        private readonly string title;
        private readonly NewsAccess access;
        private readonly NewsGenre genres;
        private readonly IList<string> keywords;
        private readonly IList<string> stockTickers;

        public INewsPublicationLanguageBuilder WithPublicationName(string publicationName)
        {
            return new NewsContentBuilder(publicationName, this.publicationDate, this.publicationLanguage, this.title, this.access, this.genres, this.keywords, this.stockTickers);
        }

        public INewsTitleBuilder WithPublicationDate(DateTime publicationDate)
        {
            return new NewsContentBuilder(this.publicationName, publicationDate, this.publicationLanguage, this.title, this.access, this.genres, this.keywords, this.stockTickers);
        }

        public INewsPublicationDateBuilder WithPublicationLanguage(string publicationLanguage)
        {
            return new NewsContentBuilder(this.publicationName, this.publicationDate, publicationLanguage, this.title, this.access, this.genres, this.keywords, this.stockTickers);
        }

        public INewsOptionalValueBuilder WithTitle(string title)
        {
            return new NewsContentBuilder(this.publicationName, this.publicationDate, this.publicationLanguage, title, this.access, this.genres, this.keywords, this.stockTickers);
        }

        public INewsOptionalValueBuilder WithAccess(NewsAccess access)
        {
            return new NewsContentBuilder(this.publicationName, this.publicationDate, this.publicationLanguage, this.title, access, this.genres, this.keywords, this.stockTickers);
        }

        public INewsOptionalValueBuilder WithGenre(NewsGenre genre)
        {
            var genres = this.genres | genre;
            return new NewsContentBuilder(this.publicationName, this.publicationDate, this.publicationLanguage, this.title, this.access, genres, this.keywords, this.stockTickers);
        }

        public INewsOptionalValueBuilder WithGenre(string genre)
        {
            NewsGenre genres = this.genres;
            NewsGenre parsed = NewsGenre.Undefined;
#if !NET35
            if (Enum.TryParse<NewsGenre>(genre, true, out parsed))
            {
                genres |= parsed;
            }
#else
            try
            {
                parsed = (NewsGenre)Enum.Parse(typeof(NewsGenre), genre, true);
                genres |= parsed;
            }
            catch
            {
                // Do nothing
            }
#endif
            return new NewsContentBuilder(this.publicationName, this.publicationDate, this.publicationLanguage, this.title, this.access, genres, this.keywords, this.stockTickers);
        }

        public INewsOptionalValueBuilder WithGenres(string genres)
        {
            var genreArray = genres.Split(new char[] { ',' });
            NewsGenre g = this.genres;
            foreach (var genre in genreArray)
            {
                if (!string.IsNullOrEmpty(genre))
                {
                    NewsGenre parsed = NewsGenre.Undefined;
#if !NET35
                    if (Enum.TryParse<NewsGenre>(genre, true, out parsed))
                    {
                        g |= parsed;
                    }
#else
                    try
                    {
                        parsed = (NewsGenre)Enum.Parse(typeof(NewsGenre), genre, true);
                        g |= parsed;
                    }
                    catch
                    {
                        // Do nothing
                    }
#endif
                }
            }
            return new NewsContentBuilder(this.publicationName, this.publicationDate, this.publicationLanguage, this.title, this.access, g, this.keywords, this.stockTickers);
        }

        public INewsOptionalValueBuilder WithKeyword(string keyword)
        {
            this.keywords.Add(keyword);
            return new NewsContentBuilder(this.publicationName, this.publicationDate, this.publicationLanguage, this.title, this.access, this.genres, this.keywords, this.stockTickers);
        }

        public INewsOptionalValueBuilder WithKeywords(string keywords)
        {
            var keywordArray = keywords.Split(new char[] { ',' });
            foreach (var keyword in keywordArray)
            {
                if (!string.IsNullOrEmpty(keyword))
                {
                    this.keywords.Add(keyword.Trim());
                }
            }
            return new NewsContentBuilder(this.publicationName, this.publicationDate, this.publicationLanguage, this.title, this.access, this.genres, this.keywords, this.stockTickers);
        }

        public INewsOptionalValueBuilder WithKeywords(IEnumerable<string> keywords)
        {
            foreach (var keyword in keywords)
            {
                if (!string.IsNullOrEmpty(keyword))
                {
                    this.keywords.Add(keyword);
                }
            }
            return new NewsContentBuilder(this.publicationName, this.publicationDate, this.publicationLanguage, this.title, this.access, this.genres, this.keywords, this.stockTickers);
        }

        public INewsOptionalValueBuilder WithStockTicker(string stockTicker)
        {
            this.stockTickers.Add(stockTicker);
            return new NewsContentBuilder(this.publicationName, this.publicationDate, this.publicationLanguage, this.title, this.access, this.genres, this.keywords, this.stockTickers);
        }

        public INewsOptionalValueBuilder WithStockTickers(string stockTickers)
        {
            var stockTickerArray = stockTickers.Split(new char[] { ',' });
            foreach (var stockTicker in stockTickerArray)
            {
                if (!string.IsNullOrEmpty(stockTicker))
                {
                    this.stockTickers.Add(stockTicker.Trim());
                }
            }
            return new NewsContentBuilder(this.publicationName, this.publicationDate, this.publicationLanguage, this.title, this.access, this.genres, this.keywords, this.stockTickers);
        }

        public INewsOptionalValueBuilder WithStockTickers(IEnumerable<string> stockTickers)
        {
            foreach (var stockTicker in stockTickers)
            {
                if (!string.IsNullOrEmpty(stockTicker))
                {
                    this.stockTickers.Add(stockTicker);
                }
            }
            return new NewsContentBuilder(this.publicationName, this.publicationDate, this.publicationLanguage, this.title, this.access, this.genres, this.keywords, this.stockTickers);
        }

        public ISpecializedContent Create()
        {
            return new NewsContent(
                publicationName: this.publicationName,
                publicationLanguage: this.publicationLanguage,
                publicationDate: this.publicationDate,
                title: this.title,
                access: this.access,
                genres: this.genres,
                keywords: this.keywords,
                stockTickers: this.stockTickers);
        }
    }
}