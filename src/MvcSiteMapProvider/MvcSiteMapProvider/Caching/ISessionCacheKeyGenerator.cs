using System;

namespace MvcSiteMapProvider.Caching
{
    /// <summary>
    /// Contract to provide a user session key for each unique user.
    /// </summary>
    public interface ISessionCacheKeyGenerator
    {
        string GenerateKey();
    }
}
