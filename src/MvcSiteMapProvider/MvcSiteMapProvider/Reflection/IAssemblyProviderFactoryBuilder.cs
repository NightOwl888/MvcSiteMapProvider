using System;
using System.Collections.Generic;
using System.Reflection;

namespace MvcSiteMapProvider.Reflection
{
    public interface IAssemblyProviderFactoryBuilder
    {
        IAssemblyProviderFactoryBuilder WithIncludeAssemblies(IEnumerable<Assembly> includeAssemblies);

        IAssemblyProviderFactoryBuilder WithExcludeAssemblies(IEnumerable<string> excludeAssemblies);

        IAssemblyProviderFactoryBuilder WithAttributeAssemblyProviderFactory(IAttributeAssemblyProviderFactory attributeAssemblyProviderFactory);

        IAssemblyProviderFactory Create();
    }
}
