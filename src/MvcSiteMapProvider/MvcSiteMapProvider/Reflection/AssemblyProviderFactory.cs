using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MvcSiteMapProvider.Reflection
{
    public class AssemblyProviderFactory
        : IAssemblyProviderFactory
    {
        public AssemblyProviderFactory(
            IEnumerable<Assembly> includeAssemblies,
            IEnumerable<string> excludeAssemblies,
            IAttributeAssemblyProviderFactory attributeAssemblyProviderFactory
            )
        {
            if (includeAssemblies == null)
                throw new ArgumentNullException("includeAssemblies");
            if (excludeAssemblies == null)
                throw new ArgumentNullException("excludeAssemblies");
            if (attributeAssemblyProviderFactory == null)
                throw new ArgumentNullException("attributeAssemblyProviderFactory");

            this.includeAssemblies = includeAssemblies;
            this.excludeAssemblies = excludeAssemblies;
            this.attributeAssemblyProviderFactory = attributeAssemblyProviderFactory;
            this.syncLock = new object();
        }
        private readonly IEnumerable<Assembly> includeAssemblies;
        private readonly IEnumerable<string> excludeAssemblies;
        private readonly IAttributeAssemblyProviderFactory attributeAssemblyProviderFactory;
        private readonly object syncLock;

        public IAssemblyProvider Create()
        {
            var assemblies = new List<Assembly>();
            if (includeAssemblies.Any())
            {
                assemblies.AddRange(includeAssemblies);
            }
            else
            {
                var attributeAssemblyProvider = this.attributeAssemblyProviderFactory.Create(new string[0], this.excludeAssemblies);
                assemblies.AddRange(attributeAssemblyProvider.GetAssemblies());
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
