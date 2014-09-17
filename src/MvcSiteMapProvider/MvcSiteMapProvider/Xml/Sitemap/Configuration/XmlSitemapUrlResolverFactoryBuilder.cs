using System;
using MvcSiteMapProvider.Web;

namespace MvcSiteMapProvider.Xml.Sitemap.Configuration
{
    public class XmlSitemapUrlResolverFactoryBuilder
        : IXmlSitemapUrlResolverFactoryBuilder
    {
        public XmlSitemapUrlResolverFactoryBuilder()
            : this(defaultProtocol: Uri.UriSchemeHttp, defaultHostName: string.Empty, urlPath: new UrlPathBuilder().Create())
        {
        }

        private XmlSitemapUrlResolverFactoryBuilder(
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
        

        public IXmlSitemapUrlResolverFactoryBuilder WithDefaultProtocol(string defaultProtocol)
        {
            return new XmlSitemapUrlResolverFactoryBuilder(defaultProtocol, this.defaultHostName, this.urlPath);
        }

        public IXmlSitemapUrlResolverFactoryBuilder WithDefaultHostName(string defaultHostName)
        {
            return new XmlSitemapUrlResolverFactoryBuilder(this.defaultProtocol, defaultHostName, this.urlPath);
        }

        public IXmlSitemapUrlResolverFactoryBuilder WithUrlPath(IUrlPath urlPath)
        {
            return new XmlSitemapUrlResolverFactoryBuilder(this.defaultProtocol, this.defaultHostName, urlPath);
        }

        public IXmlSitemapUrlResolverFactory Create()
        {
            return new XmlSitemapUrlResolverFactory(this.defaultProtocol, this.defaultHostName, this.urlPath);
        }
    }
}
