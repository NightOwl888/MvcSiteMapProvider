using System;
using MvcSiteMapProvider.Web;

namespace MvcSiteMapProvider.Xml.Sitemap.Configuration
{
    public class XmlSitemapUrlResolverBuilder
        : IXmlSitemapUrlResolverBuilder
    {
        public XmlSitemapUrlResolverBuilder()
            : this(defaultProtocol: Uri.UriSchemeHttp, defaultHostName: string.Empty, urlPath: new UrlPathBuilder().Create())
        {
        }

        private XmlSitemapUrlResolverBuilder(
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
        }
        private readonly string defaultProtocol;
        private readonly string defaultHostName;
        private readonly IUrlPath urlPath;
        

        public IXmlSitemapUrlResolverBuilder WithDefaultProtocol(string defaultProtocol)
        {
            return new XmlSitemapUrlResolverBuilder(defaultProtocol, this.defaultHostName, this.urlPath);
        }

        public IXmlSitemapUrlResolverBuilder WithDefaultHostName(string defaultHostName)
        {
            return new XmlSitemapUrlResolverBuilder(this.defaultProtocol, defaultHostName, this.urlPath);
        }

        public IXmlSitemapUrlResolverBuilder WithUrlPath(IUrlPath urlPath)
        {
            return new XmlSitemapUrlResolverBuilder(this.defaultProtocol, this.defaultHostName, urlPath);
        }

        public IXmlSitemapUrlResolver Create()
        {
            return new XmlSitemapUrlResolver(this.urlPath) 
            { 
                DefaultProtocol = this.defaultProtocol, 
                DefaultHostName = this.defaultHostName 
            };
        }
    }
}
