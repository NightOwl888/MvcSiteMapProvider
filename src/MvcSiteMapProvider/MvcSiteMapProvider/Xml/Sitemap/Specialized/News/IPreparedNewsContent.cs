using System;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized.News
{
    public interface IPreparedNewsContent
    {
        string PublicationName { get; }
        string PublicationLanguage { get; }
        string Access { get; }
        string Genres { get; }
        string PublicationDate { get; }
        string Title { get; }
        string Keywords { get; }
        string StockTickers { get; }
    }
}
