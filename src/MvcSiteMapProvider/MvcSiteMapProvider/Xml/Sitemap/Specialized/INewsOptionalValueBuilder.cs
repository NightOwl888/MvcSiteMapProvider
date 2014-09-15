using System;
using System.Collections.Generic;
using MvcSiteMapProvider.Xml.Sitemap.Specialized.News;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized
{
    public interface INewsOptionalValueBuilder
        : ISpecializedContentBuilder
    {
        INewsOptionalValueBuilder WithAccess(NewsAccess access);

        INewsOptionalValueBuilder WithGenre(NewsGenre genre);

        INewsOptionalValueBuilder WithGenre(string genre);

        INewsOptionalValueBuilder WithGenres(string genres);

        INewsOptionalValueBuilder WithKeyword(string keyword);

        INewsOptionalValueBuilder WithKeywords(string keywords);

        INewsOptionalValueBuilder WithKeywords(IEnumerable<string> keywords);

        INewsOptionalValueBuilder WithStockTicker(string stockTicker);

        INewsOptionalValueBuilder WithStockTickers(string stockTickers);

        INewsOptionalValueBuilder WithStockTickers(IEnumerable<string> stockTickers);

        // TODO: Make a way to omit stock tickers and keywords
    }
}
