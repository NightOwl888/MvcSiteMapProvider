using System;
using MvcSiteMapProvider.ComponentModel;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized
{
    public interface INewsPublicationNameBuilder
        : IFluentInterface
    {
        INewsPublicationLanguageBuilder WithPublicationName(string publicationName);
    }
}
