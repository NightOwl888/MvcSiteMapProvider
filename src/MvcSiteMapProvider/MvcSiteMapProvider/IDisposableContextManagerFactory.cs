using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider
{
    public interface IDisposableContextManagerFactory
    {
        IDisposableContextManager<T> Get<T>(Func<T> createMethod) where T : IDisposable;
        IDisposableContextManager<T> Get<T>(string label, Func<T> createMethod) where T : IDisposable;
    }
}
