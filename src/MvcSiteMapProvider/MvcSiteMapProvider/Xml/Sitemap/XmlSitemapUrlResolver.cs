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
            IUrlPath urlPath
            )
        {
            if (urlPath == null)
                throw new ArgumentNullException("urlPath");

            this.urlPath = urlPath;

            // Set the default protocol
            this.DefaultProtocol = Uri.UriSchemeHttp;
        }
        private readonly IUrlPath urlPath;
        private string defaultHostName;

        public string DefaultProtocol { get; set; }

        public string DefaultHostName
        {
            get
            {
                return string.IsNullOrEmpty(defaultHostName) ?
                    this.urlPath.GetPublicFacingUrl().Host :
                    this.defaultHostName;
            }
            set
            {
                this.defaultHostName = value;
            }
        }


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
            return this.urlPath.ResolveUrl(url, this.DefaultProtocol, this.DefaultHostName);
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
            protocol = string.IsNullOrEmpty(protocol) ? this.DefaultProtocol : protocol;

            return this.urlPath.ResolveUrl(url, protocol, this.DefaultHostName);
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
            protocol = string.IsNullOrEmpty(protocol) ? this.DefaultProtocol : protocol;
            hostName = string.IsNullOrEmpty(hostName) ? this.DefaultHostName : hostName;

            return this.urlPath.ResolveUrl(url, protocol, hostName);
        }
    }
}
