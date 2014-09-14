using System;
using MvcSiteMapProvider.Caching;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    public class RequestCachingXmlSitemapProviderDecorator
        : XmlSitemapProviderBase, IDisposable
    {
        public RequestCachingXmlSitemapProviderDecorator(
            IXmlSitemapProvider xmlSitemapProvider,
            IRequestCache requestCache
            )
        {
            if (xmlSitemapProvider == null)
                throw new ArgumentNullException("xmlSitemapProvider");
            if (requestCache == null)
                throw new ArgumentNullException("requestCache");

            this.innerXmlSitemapProvider = xmlSitemapProvider;
            this.requestCache = requestCache;
            this.syncLock = new object();
        }
        private readonly IXmlSitemapProvider innerXmlSitemapProvider;
        private readonly IRequestCache requestCache;
        private readonly object syncLock;

        public override int GetTotalRecordCount(string feedName)
        {
            var key = this.GetCacheKey("GetTotalRecordCount_" + feedName);
            var result = this.requestCache.GetValue<int?>(key);
            if (result == null)
            {
                result = this.innerXmlSitemapProvider.GetTotalRecordCount(feedName);
                this.requestCache.SetValue<int>(key, (int)result);
            }
            return (int)result;
        }

        public override DateTime GetLastModifiedDate(string feedName, int skip, int take)
        {
            
            var key = this.GetCacheKey("GetLastModifiedDate_" + feedName + "_" + skip.ToString() + "_" + take.ToString());
            var result = this.requestCache.GetValue<DateTime?>(key);
            if (result == null)
            {
                result = this.innerXmlSitemapProvider.GetLastModifiedDate(feedName, skip, take);
                this.requestCache.SetValue<DateTime>(key, (DateTime)result);
            }
            return (DateTime)result;
        }

        public override void GetUrlEntries(IUrlEntryHelper helper)
        {
            this.innerXmlSitemapProvider.GetUrlEntries(helper);
        }

        public void Dispose()
        {
            var disposable = this.innerXmlSitemapProvider as IDisposable;
            if (disposable != null)
            {
                lock (syncLock)
                {
                    disposable.Dispose();
                }
            }
        }

        protected virtual string GetCacheKey(string memberName)
        {
            return "__" + this.innerXmlSitemapProvider.GetType().FullName + "_" + memberName + "_";
        }
    }
}
