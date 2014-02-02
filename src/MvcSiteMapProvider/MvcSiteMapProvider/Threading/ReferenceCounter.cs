using System;
using System.Threading;

namespace MvcSiteMapProvider.Threading
{
    public sealed class ReferenceCounter 
        : IReferenceCounter
    {
        public ReferenceCounter(
            Action cleanupMethod
            )
        {
            if (cleanupMethod == null)
                throw new ArgumentNullException("cleanupMethod");

            this.cleanupMethod = cleanupMethod;
        }

        private readonly Action cleanupMethod;
        private int count = 0;
        private ReaderWriterLockSlim synclock = new ReaderWriterLockSlim(LockRecursionPolicy.NoRecursion);

        public int Count
        {
            get { return this.count; }
        }

        public void Increment()
        {
            Interlocked.Increment(ref this.count);
        }

        public void Decrement()
        {
            synclock.EnterWriteLock();
            try
            {
                this.count--;
                if (this.count == 0)
                {
                    this.cleanupMethod();
                }
            }
            finally
            {
                synclock.ExitWriteLock();
            }
        }
    }
}
