using System;
using System.Collections.Generic;

namespace MvcSiteMapProvider.Caching
{
    public class SessionMicroCache
        : MicroCache<IDictionary<string, object>>, ISessionMicroCache
    {
        public SessionMicroCache(
            ICacheProvider<IDictionary<string, object>> cacheProvider
            ) 
            : base(cacheProvider)
        {
        }
    }
}
