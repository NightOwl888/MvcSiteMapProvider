using System;
using System.Collections.Generic;
using System.Linq;
using MvcSiteMapProvider.ComponentModel;

namespace MvcSiteMapProvider.Xml.Sitemap.Configuration
{
    public interface IXmlSitemapFeedNameBuilder
        : IFluentInterface
    {
        IXmlSitemapFeedBuilder WithName(string feedName);
    }
}
