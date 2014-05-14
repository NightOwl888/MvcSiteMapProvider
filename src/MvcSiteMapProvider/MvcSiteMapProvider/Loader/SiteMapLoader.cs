using System;
using MvcSiteMapProvider.Caching;
using MvcSiteMapProvider.Builder;

namespace MvcSiteMapProvider.Loader
{
    /// <summary>
    /// <see cref="T:MvcSiteMapProvider.Loader.SiteMapLoader"/> is responsible for loading or unloading
    /// an <see cref="T:MvcSitemapProvider.ISiteMap"/> instance from the cache.
    /// </summary>
    public class SiteMapLoader 
        : ISiteMapLoader
    {
        public SiteMapLoader(
            ISiteMapCache siteMapCache,
            ISiteMapCacheKeyGenerator siteMapCacheKeyGenerator,
            ISiteMapCreator siteMapCreator,
            ISiteMapSpooler siteMapSpooler
            )
        {
            if (siteMapCache == null)
                throw new ArgumentNullException("siteMapCache");
            if (siteMapCacheKeyGenerator == null)
                throw new ArgumentNullException("siteMapCacheKeyGenerator");
            if (siteMapCreator == null)
                throw new ArgumentNullException("siteMapCreator");
            if (siteMapSpooler == null)
                throw new ArgumentNullException("siteMapSpooler");

            this.siteMapCache = siteMapCache;
            this.siteMapCacheKeyGenerator = siteMapCacheKeyGenerator;
            this.siteMapCreator = siteMapCreator;
            this.siteMapSpooler = siteMapSpooler;
        }

        protected readonly ISiteMapCache siteMapCache;
        protected readonly ISiteMapCacheKeyGenerator siteMapCacheKeyGenerator;
        protected readonly ISiteMapCreator siteMapCreator;
        protected readonly ISiteMapSpooler siteMapSpooler;

        #region ISiteMapLoader Members

        public virtual ISiteMap GetSiteMap()
        {
            return GetSiteMap(null);
        }

        public virtual ISiteMap GetSiteMap(string siteMapCacheKey)
        {
            if (string.IsNullOrEmpty(siteMapCacheKey))
            {
                siteMapCacheKey = siteMapCacheKeyGenerator.GenerateKey();
            }

            // Request cache the SiteMap object so we always get the same
            // reference within the scope of the request
            return this.siteMapSpooler.GetOrAdd(
                siteMapCacheKey,

                // Get or add the SiteMap to/from the shared cache
                () => this.siteMapCache.GetOrAdd(
                    siteMapCacheKey,
                    () => siteMapCreator.CreateSiteMap(siteMapCacheKey),
                    () => siteMapCreator.GetCacheDetails(siteMapCacheKey))
                );
        }

        public virtual void ReleaseSiteMap()
        {
            ReleaseSiteMap(null);
        }

        public virtual void ReleaseSiteMap(string siteMapCacheKey)
        {
            if (string.IsNullOrEmpty(siteMapCacheKey))
            {
                siteMapCacheKey = siteMapCacheKeyGenerator.GenerateKey();
            }
            siteMapCache.Remove(siteMapCacheKey);
        }

        #endregion
    }
}
