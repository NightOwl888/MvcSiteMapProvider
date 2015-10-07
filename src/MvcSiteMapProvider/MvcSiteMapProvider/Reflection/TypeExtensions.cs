using System;
using System.Reflection;

namespace MvcSiteMapProvider.Reflection
{
    
    /// <summary>
    /// Extensions to the Type type.
    /// </summary>
    public static class TypeExtensions
    {
        public static string ShortAssemblyQualifiedName(this Type type)
        {
#if MVC6
            var assemblyName = new AssemblyName(type.GetTypeInfo().Assembly.FullName);
#else
            var assemblyName = new AssemblyName(type.Assembly.FullName);
#endif
            var shortName = type.FullName + ", " + assemblyName.Name;
            return shortName;
        }
    }
}
