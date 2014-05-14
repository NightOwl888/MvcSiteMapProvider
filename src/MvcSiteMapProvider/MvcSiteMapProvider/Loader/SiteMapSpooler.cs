using System;
using System.Collections.Generic;
using MvcSiteMapProvider.Caching;
using MvcSiteMapProvider.Web.Mvc;

namespace MvcSiteMapProvider.Loader
{
    /// <summary>
    /// Maintains references to <see cref="T:MvcSiteMapProvider.ISiteMap"/> instances for the current request to ensure 
    /// they don't go out of scope until the request has completed. This ensures a given request will always use the
    /// same <see cref="T:MvcSiteMapProvider.ISiteMap"/> instance(s) regardless if they have already been ejected from
    /// the shared cache.
    /// </summary>
    public class SiteMapSpooler
        : ISiteMapSpooler
    {
        public SiteMapSpooler(
            IRequestCache requestCache
            )
        {
            if (requestCache == null)
                throw new ArgumentNullException("requestCache");

            this.requestCache = requestCache;
        }
        private readonly IRequestCache requestCache;
        private const string SiteMapSpoolerKey = "MvcSiteMapProvider_SiteMapSpooler";

        #region ISiteMapSpooler Members

        public virtual ISiteMap GetOrAdd(string siteMapCacheKey, Func<ISiteMap> loadFunction)
        {
            var siteMaps = this.GetSiteMapDictionary();
            ISiteMap result = null;

            if (!siteMaps.TryGetValue(siteMapCacheKey, out result))
            {
                result = loadFunction();
                siteMaps.Add(siteMapCacheKey, result);
            }
            return result;
        }

        public virtual IDictionary<string, ISiteMap> SiteMaps
        {
            get { return this.GetSiteMapDictionary(); }
        }

        #endregion

        protected IDictionary<string, ISiteMap> GetSiteMapDictionary()
        {
            var key = SiteMapSpoolerKey;
            var result = this.requestCache.GetValue<IDictionary<string, ISiteMap>>(key);
            if (result == null)
            {
                result = new Dictionary<string, ISiteMap>();
                requestCache.SetValue<IDictionary<string, ISiteMap>>(key, result);
            }
            return result;
        }
    }
}
