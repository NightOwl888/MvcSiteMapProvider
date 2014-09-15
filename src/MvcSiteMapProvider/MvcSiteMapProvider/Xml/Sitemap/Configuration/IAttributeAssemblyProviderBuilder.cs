using System;
using System.Collections.Generic;
using MvcSiteMapProvider.ComponentModel;
using MvcSiteMapProvider.Reflection;

namespace MvcSiteMapProvider.Xml.Sitemap.Configuration
{
    public interface IAttributeAssemblyProviderBuilder
        : IFluentInterface
    {
        IAttributeAssemblyProviderBuilder WithAssembly(string assemblyName);

        IAttributeAssemblyProviderBuilder WithAssemblies(IEnumerable<string> assemblyNames);

        IAttributeAssemblyProvider Create();
    }
}
