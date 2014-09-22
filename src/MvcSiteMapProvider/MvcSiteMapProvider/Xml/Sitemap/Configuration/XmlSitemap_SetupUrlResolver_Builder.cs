using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Xml.Sitemap.Configuration
{
    public class XmlSitemap_SetupUrlResolver_Builder
        : IXmlSitemap_SetupUrlResolver_Starter
    {
        public XmlSitemap_SetupUrlResolver_Builder()
            : this(defaultProtocol: Uri.UriSchemeHttp, defaultHostName: string.Empty)
        {
        }

        private XmlSitemap_SetupUrlResolver_Builder(
            string defaultProtocol,
            string defaultHostName
            )
        {
            this.defaultProtocol = defaultProtocol;
            this.defaultHostName = defaultHostName;
        }
        private readonly string defaultProtocol;
        private readonly string defaultHostName;

        public IXmlSitemap_SetupUrlResolver_Finalizer WithDefaultProtocol(string defaultProtocol)
        {
            return new XmlSitemap_SetupUrlResolver_Builder(defaultProtocol, this.defaultHostName);
        }

        public IXmlSitemap_SetupUrlResolver_Finalizer WithDefaultHostName(string defaultHostName)
        {
            return new XmlSitemap_SetupUrlResolver_Builder(defaultProtocol, this.defaultHostName);
        }

        public IXmlSitemapUrlResolverFactory Create()
        {
            return new XmlSitemapUrlResolverFactoryBuilder()
                .WithDefaultProtocol(this.defaultProtocol)
                .WithDefaultHostName(this.defaultHostName)
                .Create();
        }



        //IXmlSitemap_SetupUrlResolver_WithDefaultProtocol IXmlSitemap_SetupUrlResolver_Starter.UseDefaultProtocol(string defaultProtocol)
        //{
        //    return new XmlSitemap_SetupUrlResolver_Builder(defaultProtocol, this.defaultHostName, this.xmlSitemapUrlResolverFactory);
        //}

        //IXmlSitemap_SetupUrlResolver_WithDefaultHostName IXmlSitemap_SetupUrlResolver_Starter.UseDefaultHostName(string defaultHostName)
        //{
        //    return new XmlSitemap_SetupUrlResolver_Builder(this.defaultProtocol, defaultHostName, this.xmlSitemapUrlResolverFactory);
        //}

        //IXmlSitemap_SetupUrlResolver_Finalizer IXmlSitemap_SetupUrlResolver_Starter.UseUrlResolverFactory(IXmlSitemapUrlResolverFactory xmlSitemapUrlResolverFactory)
        //{
        //    return new XmlSitemap_SetupUrlResolver_Builder(this.defaultProtocol, this.defaultHostName, xmlSitemapUrlResolverFactory);
        //}

        //IXmlSitemap_SetupUrlResolver_Finalizer IXmlSitemap_SetupUrlResolver_WithDefaultProtocol.WithDefaultHostName(string defaultHostName)
        //{
        //    return new XmlSitemap_SetupUrlResolver_Builder(this.defaultProtocol, defaultHostName, this.xmlSitemapUrlResolverFactory);
        //}

        //IXmlSitemap_SetupUrlResolver_Finalizer IXmlSitemap_SetupUrlResolver_WithDefaultHostName.WithDefaultProtocol(string defaultProtocol)
        //{
        //    return new XmlSitemap_SetupUrlResolver_Builder(defaultProtocol, this.defaultHostName, this.xmlSitemapUrlResolverFactory);
        //}

        //IXmlSitemapUrlResolverFactory IXmlSitemap_SetupUrlResolver_Finalizer.Create()
        //{
        //    // If the user supplied a URL resolver factory, use it; otherwise build a new one.
        //    if (this.xmlSitemapUrlResolverFactory == null)
        //    {
        //        return new XmlSitemapUrlResolverFactoryBuilder().WithDefaultProtocol(this.defaultProtocol).WithDefaultHostName(this.defaultHostName).Create();
        //    }
        //    else
        //    {
        //        return this.xmlSitemapUrlResolverFactory;
        //    }
        //}


    }
}
