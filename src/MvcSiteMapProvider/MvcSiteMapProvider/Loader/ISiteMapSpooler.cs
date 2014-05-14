using System;
using System.Collections.Generic;
using MvcSiteMapProvider;

namespace MvcSiteMapProvider.Loader
{
    /// <summary>
    /// Contract for class that maintains references to <see cref="T:MvcSiteMapProvider.ISiteMap"/> 
    /// instances for the current request to ensure they don't go out of scope until the request has completed.
    /// </summary>
    public interface ISiteMapSpooler
    {
        ISiteMap GetOrAdd(string siteMapCacheKey, Func<ISiteMap> loadFunction);
        IDictionary<string, ISiteMap> SiteMaps { get; }
    }
}
