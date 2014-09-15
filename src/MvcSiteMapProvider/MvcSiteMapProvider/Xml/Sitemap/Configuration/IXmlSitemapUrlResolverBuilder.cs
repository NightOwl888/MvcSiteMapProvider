using System;
using System.Collections.Generic;
using System.Linq;
using MvcSiteMapProvider.ComponentModel;
using MvcSiteMapProvider.Web;

namespace MvcSiteMapProvider.Xml.Sitemap.Configuration
{
    public interface IXmlSitemapUrlResolverBuilder
        : IFluentInterface
    {
        IXmlSitemapUrlResolverBuilder WithDefaultProtocol(string defaultProtocol);

        IXmlSitemapUrlResolverBuilder WithDefaultHostName(string defaultHostName);

        IXmlSitemapUrlResolverBuilder WithUrlPath(IUrlPath urlPath);

        IXmlSitemapUrlResolver Create();
    }
}
