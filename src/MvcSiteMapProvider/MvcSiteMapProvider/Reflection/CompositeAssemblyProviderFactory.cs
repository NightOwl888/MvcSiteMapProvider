using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MvcSiteMapProvider.Reflection
{
    public class CompositeAssemblyProviderFactory
        : IAssemblyProviderFactory
    {
        public CompositeAssemblyProviderFactory(IAssemblyProviderFactory[] assemblyProividerFactories)
        {
            if (assemblyProividerFactories == null)
                throw new ArgumentNullException("assemblyProividerFactories");

            this.assemblyProividerFactories = assemblyProividerFactories;
            this.syncLock = new object();
        }
        private readonly IAssemblyProviderFactory[] assemblyProividerFactories;
        private readonly object syncLock;

        public IAssemblyProvider Create()
        {
            var assemblies = new List<Assembly>();
            foreach (var assemblyProviderFactory in this.assemblyProividerFactories)
            {
                var provider = assemblyProviderFactory.Create();
                try
                {
                    assemblies.AddRange(provider.GetAssemblies());
                }
                finally
                {
                    assemblyProviderFactory.Release(provider);
                }
            }

            return new AssemblyProvider(assemblies);
        }

        public void Release(IAssemblyProvider assemblyProvider)
        {
            var disposable = assemblyProvider as IDisposable;
            if (disposable != null)
            {
                lock (syncLock)
                {
                    disposable.Dispose();
                }
            }
        }
    }
}
