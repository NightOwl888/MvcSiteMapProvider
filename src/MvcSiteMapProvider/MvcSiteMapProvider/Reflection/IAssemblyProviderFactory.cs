using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Reflection
{
    public interface IAssemblyProviderFactory
    {
        IAssemblyProvider Create();
        void Release(IAssemblyProvider assemblyProvider);
    }
}
