using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Reflection
{
    public interface IAppDomainFactory
    {
        AppDomain Create();
        void Release(AppDomain appDomain);
    }
}
