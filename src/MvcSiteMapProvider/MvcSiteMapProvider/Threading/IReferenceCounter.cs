using System;

namespace MvcSiteMapProvider.Threading
{
    public interface IReferenceCounter
    {
        int Count { get; }
        void Decrement();
        void Increment();
    }
}
