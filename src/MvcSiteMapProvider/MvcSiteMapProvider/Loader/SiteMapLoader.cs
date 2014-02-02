using System;
using MvcSiteMapProvider.Caching;
using MvcSiteMapProvider.Builder;
using MvcSiteMapProvider.Web.Mvc;

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

            // Attach an event to the cache so when the SiteMap is removed, the Clear() method can be called on it to ensure
            // we don't have any circular references that aren't GC'd.
            siteMapCache.ItemRemoved += new EventHandler<MicroCacheItemRemovedEventArgs<ISiteMap>>(siteMapCache_ItemRemoved);

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
            if (String.IsNullOrEmpty(siteMapCacheKey))
            {
                siteMapCacheKey = siteMapCacheKeyGenerator.GenerateKey();
            }
            // Request cache the siteMap object so we always get the same
            // reference within the scope of the request
            return this.siteMapSpooler.GetOrAdd(
                siteMapCacheKey,
                // Get or add the SiteMap to/from the shared cache
                () => this.siteMapCache.GetOrAdd(
                        siteMapCacheKey,
                        () => siteMapCreator.CreateSiteMap(siteMapCacheKey),
                        () => siteMapCreator.GetCacheDetails(siteMapCacheKey))
                );
                

            //// Request cache the siteMap object so we always get the same
            //// reference within the scope of the request
            //var requestCache = this.mvcContextFactory.GetRequestCache();
            //var siteMap = requestCache.GetValue<ISiteMap>(siteMapCacheKey);
            //if (siteMap == null)
            //{
            //    siteMap = siteMapCache.GetOrAdd(
            //        siteMapCacheKey,
            //        () => siteMapCreator.CreateSiteMap(siteMapCacheKey),
            //        () => siteMapCreator.GetCacheDetails(siteMapCacheKey));
            //    requestCache.SetValue<ISiteMap>(siteMapCacheKey, siteMap);
            //}

            //return siteMap;
        }

        public virtual void ReleaseSiteMap()
        {
            ReleaseSiteMap(null);
        }

        public virtual void ReleaseSiteMap(string siteMapCacheKey)
        {
            if (String.IsNullOrEmpty(siteMapCacheKey))
            {
                siteMapCacheKey = siteMapCacheKeyGenerator.GenerateKey();
            }
            siteMapCache.Remove(siteMapCacheKey);
        }

        #endregion

        protected virtual void siteMapCache_ItemRemoved(object sender, MicroCacheItemRemovedEventArgs<ISiteMap> e)
        {
#if !MVC2
            e.Item.MarkForDestruction();
#else
            // Call clear to remove ISiteMapNode object references from internal collections. This
            // will release the circular references and free the memory.
            e.Item.Clear();
#endif
        }
    }
}
