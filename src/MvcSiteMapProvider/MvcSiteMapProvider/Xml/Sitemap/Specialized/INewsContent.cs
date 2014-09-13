using System;
using System.Collections.Generic;
using MvcSiteMapProvider.Xml.Sitemap.Specialized.News;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized
{
    public interface INewsContent
        : ISpecializedContent
    {
        string PublicationName { get; }
        string PublicationLanguage { get; }
        NewsAccess Access { get; set; }
        NewsGenre Genres { get; set; }
        DateTime PublicationDate { get; }
        string Title { get; }
        IList<string> Keywords { get; }
        IList<string> StockTickers { get; }

        // Documentation: https://support.google.com/news/publisher/answer/74288?hl=en
    }
}
