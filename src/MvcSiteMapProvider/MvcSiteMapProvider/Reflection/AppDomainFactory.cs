using System;

namespace MvcSiteMapProvider.Reflection
{
    public class AppDomainFactory
        : IAppDomainFactory
    {
        public AppDomain Create()
        {
            return AppDomain.CurrentDomain;
        }

        public void Release(AppDomain appDomain)
        {
            // We don't want to unload the current domain, so do nothing.
        }
    }
}
