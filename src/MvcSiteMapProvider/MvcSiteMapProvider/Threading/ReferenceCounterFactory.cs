using System;

namespace MvcSiteMapProvider.Threading
{
    public class ReferenceCounterFactory 
        : IReferenceCounterFactory
    {
        public IReferenceCounter Create(Action cleanupMethod)
        {
            return new ReferenceCounter(cleanupMethod);
        }
    }
}
