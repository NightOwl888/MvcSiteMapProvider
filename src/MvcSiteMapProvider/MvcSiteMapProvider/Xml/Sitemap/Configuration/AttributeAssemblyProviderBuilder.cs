using System;
using System.Collections.Generic;
using MvcSiteMapProvider.Reflection;

namespace MvcSiteMapProvider.Xml.Sitemap.Configuration
{
    public class AttributeAssemblyProviderBuilder
        : IAttributeAssemblyProviderBuilder
    {

        // TODO: Work out how to make the includeAssemblies default to what is in the configuration
        // file.
        public AttributeAssemblyProviderBuilder()
            : this(includeAssemblies: new List<string>(), excludeAssemblies: new List<string>())
        {
        }

        private AttributeAssemblyProviderBuilder(
            IList<string> includeAssemblies,
            IList<string> excludeAssemblies)
        {
            if (includeAssemblies == null)
                throw new ArgumentNullException("includeAssemblies");
            if (excludeAssemblies == null)
                throw new ArgumentNullException("excludeAssemblies");

            this.includeAssemblies = includeAssemblies;
            this.excludeAssemblies = excludeAssemblies;
        }
        protected readonly IList<string> includeAssemblies;
        protected readonly IList<string> excludeAssemblies;
           

        public IAttributeAssemblyProviderBuilder WithAssembly(string assemblyName)
        {
            if (!string.IsNullOrEmpty(assemblyName))
            {
                this.includeAssemblies.Add(assemblyName);
            }
            return new AttributeAssemblyProviderBuilder(this.includeAssemblies, this.excludeAssemblies);
        }

        public IAttributeAssemblyProviderBuilder WithAssemblies(IEnumerable<string> assemblyNames)
        {
            foreach (var assemblyName in assemblyNames)
            {
                if (!string.IsNullOrEmpty(assemblyName))
                {
                    this.includeAssemblies.Add(assemblyName);
                }
            }
            return new AttributeAssemblyProviderBuilder(this.includeAssemblies, this.excludeAssemblies);
        }

        public IAttributeAssemblyProvider Create()
        {
            return new AttributeAssemblyProvider(this.includeAssemblies, this.excludeAssemblies);
        }
    }
}
