using System;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized
{
    public interface INewsContentBuilder
        : INewsPublicationNameBuilder, INewsPublicationDateBuilder, INewsPublicationLanguageBuilder, INewsTitleBuilder, INewsOptionalValueBuilder
    {
    }
}
