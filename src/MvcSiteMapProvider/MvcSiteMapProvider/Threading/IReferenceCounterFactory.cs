using System;

namespace MvcSiteMapProvider.Threading
{
    public interface IReferenceCounterFactory
    {
        IReferenceCounter Create(Action cleanupMethod);
    }
}
