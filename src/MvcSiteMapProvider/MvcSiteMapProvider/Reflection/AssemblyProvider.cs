using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MvcSiteMapProvider.Reflection
{
    public class AssemblyProvider
        : IAssemblyProvider
    {
        public AssemblyProvider(
            IEnumerable<Assembly> assemblies
            )
        {
            if (assemblies == null)
                throw new ArgumentNullException("assemblies");

            this.assemblies = assemblies;
        }
        private readonly IEnumerable<Assembly> assemblies;

        public IEnumerable<Assembly> GetAssemblies()
        {
            return this.assemblies;
        }
    }
}
