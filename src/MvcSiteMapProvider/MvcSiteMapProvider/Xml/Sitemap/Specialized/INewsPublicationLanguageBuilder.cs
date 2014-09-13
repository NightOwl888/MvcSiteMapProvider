using System;
using MvcSiteMapProvider.ComponentModel;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized
{
    public interface INewsPublicationLanguageBuilder
        : IFluentInterface
    {
        INewsPublicationDateBuilder WithPublicationLanguage(string publicationLanguage);
    }
}
