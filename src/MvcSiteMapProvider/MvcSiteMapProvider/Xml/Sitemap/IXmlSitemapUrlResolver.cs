using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    public interface IXmlSitemapUrlResolver
    {
        /// <summary>
        /// Resolves a URL, similar to how it would on Control.ResolveUrl() in ASP.NET.
        /// If the URL begins with a "/", it will be resolved to the web root. If the 
        /// URL begins with a "~", it will be resolved to the virtual application root.
        /// Absolute URLs will be passed through unchanged.
        /// </summary>
        /// <param name="url">Any Url including those starting with "/", "~", or protocol.</param>
        /// <returns>The resolved URL.</returns>
        string ResolveUrlToAbsolute(string url);

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
        string ResolveUrlToAbsolute(string url, string protocol);

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
        string ResolveUrlToAbsolute(string url, string protocol, string hostName);
    }
}
