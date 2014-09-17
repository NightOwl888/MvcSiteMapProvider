using System;
using MvcSiteMapProvider.Globalization;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    public interface IPreparedUrlEntryFactory
    {
        IPreparedUrlEntry Create(IUrlEntry urlEntry);
    }
}
