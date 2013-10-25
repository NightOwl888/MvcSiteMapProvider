using System;
using System.Collections.Generic;
using MvcSiteMapProvider.Web.Mvc;

namespace MvcSiteMapProvider.Loader
{
    public class SiteMapSpooler 
        : ISiteMapSpooler
    {
        public SiteMapSpooler(
            IMvcContextFactory mvcContextFactory
            )
        {
            if (mvcContextFactory == null)
                throw new ArgumentNullException("mvcContextFactory");

            this.mvcContextFactory = mvcContextFactory;
        }
        private readonly IMvcContextFactory mvcContextFactory;
        private const string SiteMapSpoolerKey = "MvcSiteMapProvider_SiteMapSpooler";
        private const string SiteMapSpoolerLastWasIncrementKey = "MvcSiteMapProvider_SiteMapSpooler_LastWasIncrement";

        public ISiteMap GetOrAdd(string siteMapCacheKey, Func<ISiteMap> loadFunction)
        {
            var siteMaps = this.GetSiteMapDictionary();
            ISiteMap result = null;

            if (!siteMaps.TryGetValue(siteMapCacheKey, out result))
            {
                result = loadFunction();

                if (this.LastWasIncrement == null)
                {
                    this.LastWasIncrement = true;

                    // Increment the reference count (this request is new)
                    result.ReferenceCounter.Increment();
                }
                siteMaps.Add(siteMapCacheKey, result);
            }
            return result;
        }

        public IDictionary<string, ISiteMap> SiteMaps
        {
            get { return this.GetSiteMapDictionary(); }
        }

        public void Dereference()
        {
            if (this.LastWasIncrement != null && this.LastWasIncrement == true)
            {
                this.LastWasIncrement = false;
                var siteMaps = this.GetSiteMapDictionary();
                foreach (var siteMap in siteMaps.Values)
                {
                    // Decrement the reference count
                    siteMap.ReferenceCounter.Decrement();
                }
            }
        }

        private bool? LastWasIncrement
        {
            get
            {
                var requestCache = mvcContextFactory.GetRequestCache();
                var key = SiteMapSpoolerLastWasIncrementKey;
                return requestCache.GetValue<bool?>(key);
            }
            set
            {
                var requestCache = mvcContextFactory.GetRequestCache();
                var key = SiteMapSpoolerLastWasIncrementKey;
                requestCache.SetValue<bool?>(key, value);
            }
        }

        private IDictionary<string, ISiteMap> GetSiteMapDictionary()
        {
            var requestCache = mvcContextFactory.GetRequestCache();
            var key = SiteMapSpoolerKey;
            var result = requestCache.GetValue<IDictionary<string, ISiteMap>>(key);
            if (result == null)
            {
                result = new Dictionary<string, ISiteMap>();
                requestCache.SetValue<IDictionary<string, ISiteMap>>(key, result);
            }
            return result;
        }
    }
}
