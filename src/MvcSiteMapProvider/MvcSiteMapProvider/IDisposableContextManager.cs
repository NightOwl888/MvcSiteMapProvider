using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider
{
    public interface IDisposableContextManager<T>
        : IDisposable
        where T : IDisposable
    {
        T Context { get; }
        int ReferenceCount { get; }
    }
}
