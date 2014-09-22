using System;
using System.Collections.Generic;
using System.Reflection;

namespace MvcSiteMapProvider.Reflection
{
    public class AssemblyProviderFactoryBuilder
        : IAssemblyProviderFactoryBuilder
    {
        public AssemblyProviderFactoryBuilder()
            : this(
            includeAssemblies: new List<Assembly>(), 
            excludeAssemblies: new List<string>(), 
            attributeAssemblyProviderFactory: new AttributeAssemblyProviderFactory())
        {
        }

        private AssemblyProviderFactoryBuilder(
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
        }
        private readonly IEnumerable<Assembly> includeAssemblies;
        private readonly IEnumerable<string> excludeAssemblies;
        private readonly IAttributeAssemblyProviderFactory attributeAssemblyProviderFactory;

        public IAssemblyProviderFactoryBuilder WithIncludeAssemblies(IEnumerable<Assembly> includeAssemblies)
        {
            return new AssemblyProviderFactoryBuilder(includeAssemblies, this.excludeAssemblies, this.attributeAssemblyProviderFactory);
        }

        public IAssemblyProviderFactoryBuilder WithExcludeAssemblies(IEnumerable<string> excludeAssemblies)
        {
            return new AssemblyProviderFactoryBuilder(this.includeAssemblies, excludeAssemblies, this.attributeAssemblyProviderFactory);
        }

        public IAssemblyProviderFactoryBuilder WithAttributeAssemblyProviderFactory(IAttributeAssemblyProviderFactory attributeAssemblyProviderFactory)
        {
            return new AssemblyProviderFactoryBuilder(this.includeAssemblies, this.excludeAssemblies, attributeAssemblyProviderFactory);
        }

        public IAssemblyProviderFactory Create()
        {
            return new AssemblyProviderFactory(this.includeAssemblies, this.excludeAssemblies, this.attributeAssemblyProviderFactory);
        }
    }
}
