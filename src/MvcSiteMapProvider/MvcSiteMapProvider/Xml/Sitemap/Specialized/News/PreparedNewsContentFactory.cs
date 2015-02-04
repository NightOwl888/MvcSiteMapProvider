using System;
using System.Linq;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized.News
{
    public class PreparedNewsContentFactory
        : IPreparedNewsContentFactory
    {
        private const string W3CDateFormat = "yyyy-MM-ddTHH:mm:ss.fffffffzzz";

        public IPreparedNewsContent Create(INewsContent newsContent, IXmlSitemapUrlResolver urlResolver, Globalization.ICultureContext cultureContext)
        {
            if (newsContent != null)
            {
                string publicationName = newsContent.PublicationName;
                string publicationLanguage = newsContent.PublicationLanguage;
                string access = string.Empty;
                string genres = string.Empty;
                string publicationDate = string.Empty;
                string title = newsContent.Title;
                string keywords = string.Empty;
                string stocktickers = string.Empty;

                if (newsContent.Access != NewsAccess.Undefined)
                {
                    access = newsContent.Access.ToString();
                }
                if (newsContent.Genres != NewsGenre.Undefined)
                {
                    genres = newsContent.Genres.ToString("F");
                }
                if (newsContent.PublicationDate > DateTime.MinValue)
                {
                    publicationDate = newsContent.PublicationDate.ToString(W3CDateFormat);
                }
                if (newsContent.Keywords.Any())
                {
                    keywords = string.Join(", ", newsContent.Keywords.ToArray());
                }
                if (newsContent.StockTickers.Any())
                {
                    stocktickers = string.Join(", ", newsContent.StockTickers.ToArray());
                }

                return new PreparedNewsContent(
                    publicationName,
                    publicationLanguage,
                    access,
                    genres,
                    publicationDate,
                    title,
                    keywords,
                    stocktickers);
            }

            return null;
        }

        public void Release(IPreparedNewsContent preparedNewsContent)
        {
            var disposable = preparedNewsContent as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }
    }
}
