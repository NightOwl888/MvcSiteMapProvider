using System;
using System.Collections.Generic;
using System.Linq;
using MvcSiteMapProvider.ComponentModel;

namespace MvcSiteMapProvider.Xml.Sitemap.Configuration
{
    //interface IXmlSitemap_WithUrlResolver
    //{
    //}



    public interface IXmlSitemap_SetupUrlResolver_WithDefaultProtocol<TRemainder>
    {
        TRemainder WithDefaultProtocol(string defaultProtocol);
    }

    public interface IXmlSitemap_SetupUrlResolver_WithDefaultHostName<TRemainder>
    {
        TRemainder WithDefaultHostName(string defaultHostName);
    }

    public interface IXmlSitemap_SetupUrlResolver_Finalizer
        : IFluentInterface
    {
        IXmlSitemapUrlResolverFactory Create();
    }

    public interface IXmlSitemap_SetupUrlResolver_Starter
        : IXmlSitemap_SetupUrlResolver_WithDefaultProtocol<IXmlSitemap_SetupUrlResolver_Finalizer>,
        IXmlSitemap_SetupUrlResolver_WithDefaultHostName<IXmlSitemap_SetupUrlResolver_Finalizer>,
        IXmlSitemap_SetupUrlResolver_Finalizer
    {
    }
}
