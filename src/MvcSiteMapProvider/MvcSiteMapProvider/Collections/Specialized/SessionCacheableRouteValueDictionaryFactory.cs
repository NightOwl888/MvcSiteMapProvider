using System;
using MvcSiteMapProvider.Caching;

namespace MvcSiteMapProvider.Collections.Specialized
{
    /// <summary>
    /// An abstract factory that can be used to create new instances of 
    /// <see cref="T:MvcSiteMapProvider.Collections.Specialized.RouteValueDictionary"/>
    /// at runtime.
    /// </summary>
    public class SessionCacheableRouteValueDictionaryFactory
        : IRouteValueDictionaryFactory
    {
        public SessionCacheableRouteValueDictionaryFactory(
            ISessionCache sessionCache
            )
        {
            if (sessionCache == null)
                throw new ArgumentNullException("sessionCache");

            this.sessionCache = sessionCache;
        }

        protected readonly ISessionCache sessionCache;

        #region IRouteValueDictionaryFactory Members

        public virtual IRouteValueDictionary Create(ISiteMap siteMap)
        {
            return new RouteValueDictionary(siteMap, this.sessionCache);
        }

        #endregion
    }
}

