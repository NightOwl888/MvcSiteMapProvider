using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Caching
{
    /// <summary>
    /// Provides a user session level cache.
    /// </summary>
    /// <remarks>
    /// This class does not depend on ASP.NET session state. 
    /// </remarks>
    public class SessionCache
        : ISessionCache
    {
        public SessionCache(
            ISessionMicroCache microCache,
            ICacheDetails cacheDetails,
            ISessionCacheKeyGenerator cacheKeyGenerator
            )
        {
            if (microCache == null)
                throw new ArgumentNullException("microCache");
            if (cacheDetails == null)
                throw new ArgumentNullException("cacheDetails");
            if (cacheKeyGenerator == null)
                throw new ArgumentNullException("cacheKeyGenerator");

            this.microCache = microCache;
            this.cacheDetails = cacheDetails;
            this.cacheKeyGenerator = cacheKeyGenerator;
        }
        protected readonly ISessionMicroCache microCache;
        protected readonly ICacheDetails cacheDetails;
        protected readonly ISessionCacheKeyGenerator cacheKeyGenerator;

        /// <summary>
        /// Gets a reference to the session cache for the current user.
        /// </summary>
        protected virtual IDictionary<string, object> Session
        {
            get
            {
                var key = "__Session_" + cacheKeyGenerator.GenerateKey();
                return this.microCache.GetOrAdd(key,
                    () => { return new Dictionary<string, object>(); },
                    () => { return this.cacheDetails; }
                );
            }
        }

        #region ICache Members

        public virtual T GetValue<T>(string key)
        {
            if (this.Session.ContainsKey(key))
            {
                return (T)this.Session[key];
            }
            return default(T);
        }

        public virtual void SetValue<T>(string key, T value)
        {
            this.Session[key] = value;
        }

        #endregion
    }
}
