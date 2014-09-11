using System;
using MvcSiteMapProvider.Globalization;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    public interface IPreparedUrlEntryFactory
    {
        IXmlSitemapUrlResolver UrlResolver { get; }
        ICultureContextFactory CultureContextFactory { get; }
        IPreparedUrlEntry Create(IUrlEntry urlEntry);
    }
}
