using System;
using System.Collections.Generic;
using System.Linq;
using MvcSiteMapProvider.Web;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    public class XmlSitemapUrlResolverFactory
        : IXmlSitemapUrlResolverFactory
    {
        public XmlSitemapUrlResolverFactory(
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
            this.syncLock = new object();
        }
        private readonly string defaultProtocol;
        private readonly string defaultHostName;
        private readonly IUrlPath urlPath;
        private readonly object syncLock;

        public IXmlSitemapUrlResolver Create()
        {
            return new XmlSitemapUrlResolver(this.defaultProtocol, this.defaultHostName, this.urlPath);
        }

        public void Release(IXmlSitemapUrlResolver xmlSitemapUrlResolver)
        {
            var disposable = xmlSitemapUrlResolver as IDisposable;
            if (disposable != null)
            {
                lock (syncLock)
                {
                    disposable.Dispose();
                }
            }
        }
    }
}
