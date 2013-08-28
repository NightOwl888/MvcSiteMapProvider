using System;
using System.Collections.Generic;

namespace MvcSiteMapProvider.Caching
{
    public interface ISessionMicroCache
        : IMicroCache<IDictionary<string, object>>
    {
    }
}
