using System;
using System.Collections.Generic;
using System.Linq;
using MvcSiteMapProvider.ComponentModel;
using MvcSiteMapProvider.Web;

namespace MvcSiteMapProvider.Xml.Sitemap.Configuration
{
    public interface IXmlSitemapUrlResolverFactoryBuilder
        : IFluentInterface
    {
        IXmlSitemapUrlResolverFactoryBuilder WithDefaultProtocol(string defaultProtocol);

        IXmlSitemapUrlResolverFactoryBuilder WithDefaultHostName(string defaultHostName);

        IXmlSitemapUrlResolverFactoryBuilder WithUrlPath(IUrlPath urlPath);

        IXmlSitemapUrlResolverFactory Create();
    }
}
