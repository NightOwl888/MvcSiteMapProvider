using System;
using MvcSiteMapProvider.ComponentModel;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized
{
    public interface INewsPublicationDateBuilder
        : IFluentInterface
    {
        INewsTitleBuilder WithPublicationDate(DateTime publicationDate);
    }
}
