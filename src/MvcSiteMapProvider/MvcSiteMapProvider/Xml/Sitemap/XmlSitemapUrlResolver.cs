using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvcSiteMapProvider.Web;
using MvcSiteMapProvider.Web.Mvc;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    // TODO: Create an offline sitemap URL resolver with fake HTTP context.

    public class XmlSitemapUrlResolver
        : IXmlSitemapUrlResolver
    {
        public XmlSitemapUrlResolver(
            string defaultProtocol,
            string defaultHostName,
            IUrlPath urlPath
            )
        {
            if (string.IsNullOrEmpty(defaultProtocol))
                throw new ArgumentNullException("defaultProtocol");
            if (urlPath == null)
                throw new ArgumentNullException("urlPath");

            this.defaultProtocol = defaultProtocol;
            this.defaultHostName = defaultHostName;
            this.urlPath = urlPath;

            // Set the default protocol
            //this.DefaultProtocol = Uri.UriSchemeHttp;
        }
        private readonly string defaultProtocol;
        private readonly string defaultHostName;
        private readonly IUrlPath urlPath;
        
        /// <summary>
        /// Resolves a URL, similar to how it would on Control.ResolveUrl() in ASP.NET.
        /// If the URL begins with a "/", it will be resolved to the web root. If the 
        /// URL begins with a "~", it will be resolved to the virtual application root.
        /// Absolute URLs will be passed through unchanged.
        /// </summary>
        /// <param name="url">Any Url including those starting with "/", "~", or protocol.</param>
        /// <returns>The resolved URL.</returns>
        public string ResolveUrlToAbsolute(string url)
        {
            return this.urlPath.ResolveUrl(url, this.defaultProtocol, this.GetDefaultHostName());
        }

        /// <summary>
        /// Resolves a URL, similar to how it would on Control.ResolveUrl() in ASP.NET.
        /// If the URL begins with a "/", it will be resolved to the web root. If the 
        /// URL begins with a "~", it will be resolved to the virtual application root.
        /// Absolute URLs will be passed through unchanged.
        /// </summary>
        /// <param name="url">Any Url including those starting with "/", "~", or protocol.</param>
        /// <param name="protocol">The protocol such as http, https, or ftp. Defaults to http 
        /// protocol if null or empty string. To use the protocol of the current request, use *.</param>
        /// <returns>The resolved URL.</returns>
        public string ResolveUrlToAbsolute(string url, string protocol)
        {
            protocol = string.IsNullOrEmpty(protocol) ? this.defaultProtocol : protocol;

            return this.urlPath.ResolveUrl(url, protocol, this.GetDefaultHostName());
        }

        /// <summary>
        /// Resolves a URL, similar to how it would on Control.ResolveUrl() in ASP.NET.
        /// If the URL begins with a "/", it will be resolved to the web root. If the 
        /// URL begins with a "~", it will be resolved to the virtual application root.
        /// Absolute URLs will be passed through unchanged.
        /// </summary>
        /// <param name="url">Any Url including those starting with "/", "~", or protocol.</param>
        /// <param name="protocol">The protocol such as http, https, or ftp. Defaults to http 
        /// protocol if null or empty string. To use the protocol of the current request, use *.</param>
        /// <param name="hostName">The host name such as www.somewhere.com.</param>
        /// <returns>The resolved URL.</returns>
        public string ResolveUrlToAbsolute(string url, string protocol, string hostName)
        {
            protocol = string.IsNullOrEmpty(protocol) ? this.defaultProtocol : protocol;
            hostName = string.IsNullOrEmpty(hostName) ? this.GetDefaultHostName() : hostName;

            return this.urlPath.ResolveUrl(url, protocol, hostName);
        }

        private string GetDefaultHostName()
        {
            return string.IsNullOrEmpty(defaultHostName) ?
                this.urlPath.GetPublicFacingUrl().Host :
                this.defaultHostName;
        }
    }
}
