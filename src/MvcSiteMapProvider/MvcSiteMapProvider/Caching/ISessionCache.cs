using System;

namespace MvcSiteMapProvider.Caching
{
    /// <summary>
    /// Contract for a class to provide type-safe access to a user session-level cache.
    /// </summary>
    public interface ISessionCache
        : ICache
    {
    }
}
