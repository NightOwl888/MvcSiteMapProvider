using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MvcSiteMapProvider.Reflection;

namespace MvcSiteMapProvider.Xml.Sitemap.Configuration
{

    // Top level builder
    public class XmlSitemap_SetupXmlSitemapProviderScan_Builder
        : IXmlSitemap_SetupXmlSitemapProviderScan_Starter
    {
        public XmlSitemap_SetupXmlSitemapProviderScan_Builder()
            : this(
            includeAssembliesForScan: new Dictionary<Assembly, string>(), 
            excludeAssembliesForScan: new Dictionary<Assembly, string>(),
            assemblyProviderFactories: new List<IAssemblyProviderFactory>(),
            xmlSitemapProviderFactory: new XmlSitemapProviderFactory(),
            appDomainFactory: new AppDomainFactory()
            )
        {
        }

        private XmlSitemap_SetupXmlSitemapProviderScan_Builder(
            IDictionary<Assembly, string> includeAssembliesForScan,
            IDictionary<Assembly, string> excludeAssembliesForScan,
            IList<IAssemblyProviderFactory> assemblyProviderFactories,
            IXmlSitemapProviderFactory xmlSitemapProviderFactory,
            IAppDomainFactory appDomainFactory
            )
        {
            if (includeAssembliesForScan == null)
                throw new ArgumentNullException("includeAssembliesForScan");
            if (excludeAssembliesForScan == null)
                throw new ArgumentNullException("excludeAssembliesForScan");
            if (assemblyProviderFactories == null)
                throw new ArgumentNullException("assemblyProviderFactories");
            if (xmlSitemapProviderFactory == null)
                throw new ArgumentNullException("xmlSitemapProviderFactory");
            if (appDomainFactory == null)
                throw new ArgumentNullException("appDomainFactory");

            this.includeAssembliesForScan = includeAssembliesForScan;
            this.excludeAssembliesForScan = excludeAssembliesForScan;
            this.assemblyProviderFactories = assemblyProviderFactories;
            this.xmlSitemapProviderFactory = xmlSitemapProviderFactory;
            this.appDomainFactory = appDomainFactory;
        }
        protected readonly IDictionary<Assembly, string> includeAssembliesForScan;
        protected readonly IDictionary<Assembly, string> excludeAssembliesForScan;
        protected readonly IList<IAssemblyProviderFactory> assemblyProviderFactories;
        protected readonly IXmlSitemapProviderFactory xmlSitemapProviderFactory;
        protected readonly IAppDomainFactory appDomainFactory;

        public IXmlSitemap_SetupXmlSitemapProviderScan_WithIncludeAssemblies IncludeAssembly(string assemblyName)
        {
            this.AddAssembly(assemblyName);
            return new XmlSitemap_SetupXmlSitemapProviderScan_Builder(this.includeAssembliesForScan, this.excludeAssembliesForScan, this.assemblyProviderFactories, this.xmlSitemapProviderFactory, this.appDomainFactory);
        }

        public IXmlSitemap_SetupXmlSitemapProviderScan_WithIncludeAssemblies IncludeAssembly(Assembly assembly)
        {
            this.AddAssembly(assembly);
            return new XmlSitemap_SetupXmlSitemapProviderScan_Builder(this.includeAssembliesForScan, this.excludeAssembliesForScan, this.assemblyProviderFactories, this.xmlSitemapProviderFactory, this.appDomainFactory);
        }

        public IXmlSitemap_SetupXmlSitemapProviderScan_WithIncludeAssemblies IncludeAssemblies(IEnumerable<string> assemblyNames)
        {
            foreach (var assemblyName in assemblyNames)
            {
                this.AddAssembly(assemblyName);
            }
            return new XmlSitemap_SetupXmlSitemapProviderScan_Builder(this.includeAssembliesForScan, this.excludeAssembliesForScan, this.assemblyProviderFactories, this.xmlSitemapProviderFactory, this.appDomainFactory);
        }

        public IXmlSitemap_SetupXmlSitemapProviderScan_WithIncludeAssemblies IncludeAssemblies(IEnumerable<Assembly> assemblies)
        {
            foreach (var assembly in assemblies)
            {
                this.AddAssembly(assembly);
            }
            return new XmlSitemap_SetupXmlSitemapProviderScan_Builder(this.includeAssembliesForScan, this.excludeAssembliesForScan, this.assemblyProviderFactories, this.xmlSitemapProviderFactory, this.appDomainFactory);
        }

        public IXmlSitemap_SetupXmlSitemapProviderScan_WithIncludeAssemblies IncludeAssembliesFrom(IAssemblyProviderFactory assemblyProviderFactory)
        {
            if (assemblyProviderFactory != null && !this.assemblyProviderFactories.Contains(assemblyProviderFactory))
            {
                this.assemblyProviderFactories.Add(assemblyProviderFactory);
            }
            return new XmlSitemap_SetupXmlSitemapProviderScan_Builder(this.includeAssembliesForScan, this.excludeAssembliesForScan, this.assemblyProviderFactories, this.xmlSitemapProviderFactory, this.appDomainFactory);
        }

        public IXmlSitemap_SetupXmlSitemapProviderScan_Finalizer InstantiateUsing(IXmlSitemapProviderFactory xmlSitemapProviderFactory)
        {
            return new XmlSitemap_SetupXmlSitemapProviderScan_Builder(this.includeAssembliesForScan, this.excludeAssembliesForScan, this.assemblyProviderFactories, xmlSitemapProviderFactory, this.appDomainFactory);
        }

        public IXmlSitemap_SetupXmlSitemapProviderScan_WithExcludeAssemblies ExcludeAssembly(string assemblyName)
        {
            this.RemoveAssembly(assemblyName);
            return new XmlSitemap_SetupXmlSitemapProviderScan_Builder(this.includeAssembliesForScan, this.excludeAssembliesForScan, this.assemblyProviderFactories, this.xmlSitemapProviderFactory, this.appDomainFactory);
        }

        public IXmlSitemap_SetupXmlSitemapProviderScan_WithExcludeAssemblies ExcludeAssembly(Assembly assembly)
        {
            this.RemoveAssembly(assembly);
            return new XmlSitemap_SetupXmlSitemapProviderScan_Builder(this.includeAssembliesForScan, this.excludeAssembliesForScan, this.assemblyProviderFactories, this.xmlSitemapProviderFactory, this.appDomainFactory);
        }

        public IXmlSitemap_SetupXmlSitemapProviderScan_WithExcludeAssemblies ExcludeAssemblies(IEnumerable<string> assemblyNames)
        {
            foreach (var assemblyName in assemblyNames)
            {
                this.RemoveAssembly(assemblyName);
            }
            return new XmlSitemap_SetupXmlSitemapProviderScan_Builder(this.includeAssembliesForScan, this.excludeAssembliesForScan, this.assemblyProviderFactories, this.xmlSitemapProviderFactory, this.appDomainFactory);
        }

        public IXmlSitemap_SetupXmlSitemapProviderScan_WithExcludeAssemblies ExcludeAssemblies(IEnumerable<Assembly> assemblies)
        {
            foreach (var assembly in assemblies)
            {
                this.RemoveAssembly(assembly);
            }
            return new XmlSitemap_SetupXmlSitemapProviderScan_Builder(this.includeAssembliesForScan, this.excludeAssembliesForScan, this.assemblyProviderFactories, this.xmlSitemapProviderFactory, this.appDomainFactory);
        }

        public IAssemblyProviderFactory Create()
        {
            var assemblyProviderFactory = new AssemblyProviderFactoryBuilder()
                .WithIncludeAssemblies(this.includeAssembliesForScan.Keys)
                .WithExcludeAssemblies(this.excludeAssembliesForScan.Values)
                .Create();

            this.assemblyProviderFactories.Add(assemblyProviderFactory);
            return new CompositeAssemblyProviderFactory(this.assemblyProviderFactories.ToArray());
        }

        public IXmlSitemapProviderFactory XmlSitemapProviderFactory
        {
            get { return this.xmlSitemapProviderFactory; }
        }

        #region private methods

        private void AddAssembly(string assemblyName)
        {
            var assembly = this.GetAssemblyNamed(assemblyName);
            this.AddAssembly(assembly);
        }

        private void AddAssembly(Assembly assembly)
        {
            if (assembly != null)
            {
                var assemblyName = this.GetAssemblyName(assembly);
                this.includeAssembliesForScan.Add(assembly, assemblyName);
                if (this.excludeAssembliesForScan.ContainsKey(assembly))
                {
                    this.excludeAssembliesForScan.Remove(assembly);
                }
            }
        }

        private void RemoveAssembly(string assemblyName)
        {
            var assembly = this.GetAssemblyNamed(assemblyName);
            this.RemoveAssembly(assembly);
        }

        private void RemoveAssembly(Assembly assembly)
        {
            if (assembly != null)
            {
                if (this.includeAssembliesForScan.ContainsKey(assembly))
                {
                    this.includeAssembliesForScan.Remove(assembly);
                }
                if (!this.excludeAssembliesForScan.ContainsKey(assembly))
                {
                    var assemblyName = this.GetAssemblyName(assembly);
                    this.excludeAssembliesForScan.Add(assembly, assemblyName);
                }
            }
        }

        private string GetAssemblyName(Assembly assembly)
        {
            return new AssemblyName(assembly.FullName).Name;
        }

        private Assembly GetAssemblyNamed(string assemblyName)
        {
            var appDomain = this.appDomainFactory.Create();
            try
            {
                return appDomain.GetAssemblies().Where(a => this.GetAssemblyName(a).Equals(assemblyName)).FirstOrDefault();
            }
            finally
            {
                this.appDomainFactory.Release(appDomain);
            }
        }

        #endregion
    }


    //public abstract class XmlSitemapFeedStrategy_SetupXmlSitemapProviderScan_WithXmlSitemapProviderFactory_WithScanAssemblies
    //    : XmlSitemap_SetupXmlSitemapProviderScan_BuilderBase,
    //    IXmlSitemapFeedStrategy_SetupXmlSitemapProviderScan_WithXmlSitemapProviderFactory_WithScanAssemblies
    //{
    //    protected XmlSitemapFeedStrategy_SetupXmlSitemapProviderScan_WithXmlSitemapProviderFactory_WithScanAssemblies(
    //        IDictionary<Assembly, string> includeAssembliesForScan,
    //        IDictionary<Assembly, string> excludeAssembliesForScan,
    //        IEnumerable<IAssemblyProviderFactory> assemblyProviderFactories,
    //        IXmlSitemapProviderFactory xmlSitemapProviderFactory,
    //        IAppDomainFactory appDomainFactory
    //        )
    //        : base(includeAssembliesForScan, excludeAssembliesForScan, assemblyProviderFactory, xmlSitemapProviderFactory, appDomainFactory)
    //    {
    //    }

    //    public new IXmlSitemapFeedStrategy_SetupXmlSitemapProviderScan_WithXmlSitemapProviderFactory_WithScanAssemblies ScanAssembly(string assemblyName)
    //    {
    //        this.AddAssembly(assemblyName);
    //        return (IXmlSitemapFeedStrategy_SetupXmlSitemapProviderScan_WithXmlSitemapProviderFactory_WithScanAssemblies)new XmlSitemap_SetupXmlSitemapProviderScan_Builder(
    //            this.includeAssembliesForScan, this.excludeAssembliesForScan, this.assemblyProviderFactory, this.xmlSitemapProviderFactory, this.appDomainFactory);
    //    }

    //    public new IXmlSitemapFeedStrategy_SetupXmlSitemapProviderScan_WithXmlSitemapProviderFactory_WithScanAssemblies ScanAssemblies(IEnumerable<string> assemblyNames)
    //    {
    //        foreach (var assemblyName in assemblyNames)
    //        {
    //            this.AddAssembly(assemblyName);
    //        }
    //        return (IXmlSitemapFeedStrategy_SetupXmlSitemapProviderScan_WithXmlSitemapProviderFactory_WithScanAssemblies)new XmlSitemap_SetupXmlSitemapProviderScan_Builder(
    //            this.includeAssembliesForScan, this.excludeAssembliesForScan, this.assemblyProviderFactory, this.xmlSitemapProviderFactory, this.appDomainFactory);
    //    }

    //    IXmlSitemapFeedStrategy_SetupXmlSitemapProviderScan_WithXmlSitemapProviderFactory_WithScanAssemblies IXmlSitemap_SetupXmlSitemapProviderScan_IncludeAssemblies<IXmlSitemapFeedStrategy_SetupXmlSitemapProviderScan_WithXmlSitemapProviderFactory_WithScanAssemblies>.ScanAssembly(Assembly assembly)
    //    {
    //        this.AddAssembly(assembly);
    //        return (IXmlSitemapFeedStrategy_SetupXmlSitemapProviderScan_WithXmlSitemapProviderFactory_WithScanAssemblies)new XmlSitemap_SetupXmlSitemapProviderScan_Builder(
    //            this.includeAssembliesForScan, this.excludeAssembliesForScan, this.assemblyProviderFactory, this.xmlSitemapProviderFactory, this.appDomainFactory);
    //    }

    //    IXmlSitemapFeedStrategy_SetupXmlSitemapProviderScan_WithXmlSitemapProviderFactory_WithScanAssemblies IXmlSitemap_SetupXmlSitemapProviderScan_IncludeAssemblies<IXmlSitemapFeedStrategy_SetupXmlSitemapProviderScan_WithXmlSitemapProviderFactory_WithScanAssemblies>.ScanAssemblies(IEnumerable<Assembly> assemblies)
    //    {
    //        return (IXmlSitemapFeedStrategy_SetupXmlSitemapProviderScan_WithXmlSitemapProviderFactory_WithScanAssemblies)new XmlSitemap_SetupXmlSitemapProviderScan_Builder(
    //            this.includeAssembliesForScan, this.excludeAssembliesForScan, this.assemblyProviderFactory, this.xmlSitemapProviderFactory, this.appDomainFactory);
    //    }

    //    public new IXmlSitemapFeedStrategy_SetupXmlSitemapProviderScan_WithXmlSitemapProviderFactory_WithScanAssemblies DontScanAssembly(string assemblyName)
    //    {
    //        return (IXmlSitemapFeedStrategy_SetupXmlSitemapProviderScan_WithXmlSitemapProviderFactory_WithScanAssemblies)new XmlSitemap_SetupXmlSitemapProviderScan_Builder(
    //            this.includeAssembliesForScan, this.excludeAssembliesForScan, this.assemblyProviderFactory, this.xmlSitemapProviderFactory, this.appDomainFactory);
    //    }

    //    public new IXmlSitemapFeedStrategy_SetupXmlSitemapProviderScan_WithXmlSitemapProviderFactory_WithScanAssemblies DontScanAssemblies(IEnumerable<string> assemblyNames)
    //    {
    //        return (IXmlSitemapFeedStrategy_SetupXmlSitemapProviderScan_WithXmlSitemapProviderFactory_WithScanAssemblies)new XmlSitemap_SetupXmlSitemapProviderScan_Builder(
    //            this.includeAssembliesForScan, this.excludeAssembliesForScan, this.assemblyProviderFactory, this.xmlSitemapProviderFactory, this.appDomainFactory);
    //    }

    //    IXmlSitemapFeedStrategy_SetupXmlSitemapProviderScan_WithXmlSitemapProviderFactory_WithScanAssemblies IXmlSitemap_SetupXmlSitemapProviderScan_IncludeAssemblies<IXmlSitemapFeedStrategy_SetupXmlSitemapProviderScan_WithXmlSitemapProviderFactory_WithScanAssemblies>.DontScanAssembly(Assembly assembly)
    //    {
    //        return (IXmlSitemapFeedStrategy_SetupXmlSitemapProviderScan_WithXmlSitemapProviderFactory_WithScanAssemblies)new XmlSitemap_SetupXmlSitemapProviderScan_Builder(
    //            this.includeAssembliesForScan, this.excludeAssembliesForScan, this.assemblyProviderFactory, this.xmlSitemapProviderFactory, this.appDomainFactory);
    //    }

    //    IXmlSitemapFeedStrategy_SetupXmlSitemapProviderScan_WithXmlSitemapProviderFactory_WithScanAssemblies IXmlSitemap_SetupXmlSitemapProviderScan_IncludeAssemblies<IXmlSitemapFeedStrategy_SetupXmlSitemapProviderScan_WithXmlSitemapProviderFactory_WithScanAssemblies>.DontScanAssemblies(IEnumerable<Assembly> assemblies)
    //    {
    //        return (IXmlSitemapFeedStrategy_SetupXmlSitemapProviderScan_WithXmlSitemapProviderFactory_WithScanAssemblies)new XmlSitemap_SetupXmlSitemapProviderScan_Builder(
    //            this.includeAssembliesForScan, this.excludeAssembliesForScan, this.assemblyProviderFactory, this.xmlSitemapProviderFactory, this.appDomainFactory);
    //    }

    //    #region private methods

    //    private void AddAssembly(string assemblyName)
    //    {
    //        var assembly = this.GetAssemblyNamed(assemblyName);
    //        this.AddAssembly(assembly);
    //    }

    //    private void AddAssembly(Assembly assembly)
    //    {
    //        if (assembly != null)
    //        {
    //            var assemblyName = this.GetAssemblyName(assembly);
    //            this.includeAssembliesForScan.Add(assembly, assemblyName);
    //            if (this.excludeAssembliesForScan.ContainsKey(assembly))
    //            {
    //                this.excludeAssembliesForScan.Remove(assembly);
    //            }
    //        }
    //    }

    //    private void RemoveAssembly(string assemblyName)
    //    {
    //        var assembly = this.GetAssemblyNamed(assemblyName);
    //        this.RemoveAssembly(assembly);
    //    }

    //    private void RemoveAssembly(Assembly assembly)
    //    {
    //        if (assembly != null)
    //        {
    //            if (this.includeAssembliesForScan.ContainsKey(assembly))
    //            {
    //                this.includeAssembliesForScan.Remove(assembly);
    //            }
    //            if (!this.excludeAssembliesForScan.ContainsKey(assembly))
    //            {
    //                var assemblyName = this.GetAssemblyName(assembly);
    //                this.excludeAssembliesForScan.Add(assembly, assemblyName);
    //            }
    //        }
    //    }

    //    private string GetAssemblyName(Assembly assembly)
    //    {
    //        return new AssemblyName(assembly.FullName).Name;
    //    }

    //    private Assembly GetAssemblyNamed(string assemblyName)
    //    {
    //        var appDomain = this.appDomainFactory.Create();
    //        try
    //        {
    //            return appDomain.GetAssemblies().Where(a => this.GetAssemblyName(a).Equals(assemblyName)).FirstOrDefault();
    //        }
    //        finally
    //        {
    //            this.appDomainFactory.Release(appDomain);
    //        }
    //    }

    //    #endregion
    //}

    //public abstract class XmlSitemap_SetupXmlSitemapProviderScan_BuilderBase // Needed?
    //    : IXmlSitemap_SetupXmlSitemapProviderScan_Starter,
    //    IXmlSitemap_SetupXmlSitemapProviderScan_Finalizer
    //{
    //    protected XmlSitemap_SetupXmlSitemapProviderScan_BuilderBase(
    //        IDictionary<Assembly, string> includeAssembliesForScan,
    //        IDictionary<Assembly, string> excludeAssembliesForScan,
    //        IEnumerable<IAssemblyProviderFactory> assemblyProviderFactories,
    //        IXmlSitemapProviderFactory xmlSitemapProviderFactory,
    //        IAppDomainFactory appDomainFactory
    //        )
    //    {
    //        if (includeAssembliesForScan == null)
    //            throw new ArgumentNullException("includeAssembliesForScan");
    //        if (excludeAssembliesForScan == null)
    //            throw new ArgumentNullException("excludeAssembliesForScan");
    //        if (assemblyProviderFactories == null)
    //            throw new ArgumentNullException("assemblyProviderFactories");
    //        if (xmlSitemapProviderFactory == null)
    //            throw new ArgumentNullException("xmlSitemapProviderFactory");
    //        if (appDomainFactory == null)
    //            throw new ArgumentNullException("appDomainFactory");

    //        this.includeAssembliesForScan = includeAssembliesForScan;
    //        this.excludeAssembliesForScan = excludeAssembliesForScan;
    //        this.assemblyProviderFactories = assemblyProviderFactories;
    //        this.xmlSitemapProviderFactory = xmlSitemapProviderFactory;
    //        this.appDomainFactory = appDomainFactory;
    //    }
    //    protected readonly IDictionary<Assembly, string> includeAssembliesForScan;
    //    protected readonly IDictionary<Assembly, string> excludeAssembliesForScan;
    //    protected readonly IEnumerable<IAssemblyProviderFactory> assemblyProviderFactories;
    //    protected readonly IXmlSitemapProviderFactory xmlSitemapProviderFactory;
    //    protected readonly IAppDomainFactory appDomainFactory;

    //    //public abstract IXmlSitemapFeedStrategy_SetupXmlSitemapProviderScan_WithXmlSitemapProviderFactory InstantiateUsing(IXmlSitemapProviderFactory xmlSitemapProviderFactory);

    //    //public abstract IXmlSitemap_SetupXmlSitemapProviderScan_Starter IncludeAssembly(string assemblyName);

    //    //public abstract IXmlSitemap_SetupXmlSitemapProviderScan_Starter IncludeAssembly(Assembly assembly);

    //    //public abstract IXmlSitemap_SetupXmlSitemapProviderScan_Starter IncludeAssemblies(IEnumerable<string> assemblyNames);

    //    //public abstract IXmlSitemap_SetupXmlSitemapProviderScan_Starter IncludeAssemblies(IEnumerable<Assembly> assemblies);

    //    //public abstract IXmlSitemap_SetupXmlSitemapProviderScan_Starter ExcludeAssembly(string assemblyName);

    //    //public abstract IXmlSitemap_SetupXmlSitemapProviderScan_Starter ExcludeAssemblies(IEnumerable<string> assemblyNames);

    //    //public abstract IXmlSitemap_SetupXmlSitemapProviderScan_Starter ExcludeAssembly(Assembly assembly);

    //    //public abstract IXmlSitemap_SetupXmlSitemapProviderScan_Starter ExcludeAssemblies(IEnumerable<Assembly> assemblies);

    //    //public abstract IXmlSitemap_SetupXmlSitemapProviderScan_Starter IncludeAssembliesFrom(IAssemblyProviderFactory assemblyProviderFactory);

    //    public abstract IAssemblyProviderFactory Create();

    //    public abstract IXmlSitemapProviderFactory XmlSitemapProviderFactory { get; }
    //}


    public interface IXmlSitemap_SetupXmlSitemapProviderScan_XmlSitemapProviderFactory<TRemainder> // set only 1 time
    {
        TRemainder InstantiateUsing(IXmlSitemapProviderFactory xmlSitemapProviderFactory);
    }

    public interface IXmlSitemap_SetupXmlSitemapProviderScan_IncludeAssemblies<TRemainder> // Use multiple times, but only if ExcludeAssemblies is not set
    {
        TRemainder IncludeAssembly(string assemblyName);

        TRemainder IncludeAssembly(Assembly assembly);

        TRemainder IncludeAssemblies(IEnumerable<string> assemblyNames);

        TRemainder IncludeAssemblies(IEnumerable<Assembly> assemblies);

        TRemainder IncludeAssembliesFrom(IAssemblyProviderFactory assemblyProviderFactory);
    }

    public interface IXmlSitemap_SetupXmlSitemapProviderScan_ExcludeAssemblies<TRemainder> // Use multiple times, but only if IncludeAssemblies is not set
    {
        TRemainder ExcludeAssembly(string assemblyName);

        TRemainder ExcludeAssembly(Assembly assembly);

        TRemainder ExcludeAssemblies(IEnumerable<string> assemblyNames);

        TRemainder ExcludeAssemblies(IEnumerable<Assembly> assemblies);
    }

    //public interface IXmlSitemapFeedStrategy_SetupXmlSitemapProviderScan_ScanAssembliesFrom<TRemainder> // Set only 1 time, but not if ScanAssemblies is set
    //{
    //    TRemainder ScanAssembliesFrom(IAssemblyProviderFactory assemblyProviderFactory);
    //}

    public interface IXmlSitemap_SetupXmlSitemapProviderScan_Finalizer
    {
        IAssemblyProviderFactory Create();

        IXmlSitemapProviderFactory XmlSitemapProviderFactory { get; }
    }

    // Nothing set
    public interface IXmlSitemap_SetupXmlSitemapProviderScan_Starter
        : IXmlSitemap_SetupXmlSitemapProviderScan_WithIncludeAssemblies,
        IXmlSitemap_SetupXmlSitemapProviderScan_WithExcludeAssemblies
    {
    }

    //// ScanAssembliesFrom set
    //public interface IXmlSitemapFeedStrategy_SetupXmlSitemapProviderScan_WithScanAssembliesFrom
    //    : IXmlSitemap_SetupXmlSitemapProviderScan_XmlSitemapProviderFactory<IXmlSitemap_SetupXmlSitemapProviderScan_Finalizer>,
    //    IXmlSitemap_SetupXmlSitemapProviderScan_Finalizer
    //{
    //}

    // at least 1 IncludeAssembly set
    public interface IXmlSitemap_SetupXmlSitemapProviderScan_WithIncludeAssemblies
        : IXmlSitemap_SetupXmlSitemapProviderScan_IncludeAssemblies<IXmlSitemap_SetupXmlSitemapProviderScan_WithIncludeAssemblies>,
        IXmlSitemap_SetupXmlSitemapProviderScan_XmlSitemapProviderFactory<IXmlSitemap_SetupXmlSitemapProviderScan_Finalizer>,
        IXmlSitemap_SetupXmlSitemapProviderScan_Finalizer
    {
    }

    // at least 1 ExcludeAssembly set
    public interface IXmlSitemap_SetupXmlSitemapProviderScan_WithExcludeAssemblies
        : IXmlSitemap_SetupXmlSitemapProviderScan_ExcludeAssemblies<IXmlSitemap_SetupXmlSitemapProviderScan_WithExcludeAssemblies>,
        IXmlSitemap_SetupXmlSitemapProviderScan_XmlSitemapProviderFactory<IXmlSitemap_SetupXmlSitemapProviderScan_Finalizer>,
        IXmlSitemap_SetupXmlSitemapProviderScan_Finalizer
    {
    }

    //// XmlSitemapProviderFactory set
    //public interface IXmlSitemapFeedStrategy_SetupXmlSitemapProviderScan_WithXmlSitemapProviderFactory
    //    : IXmlSitemap_SetupXmlSitemapProviderScan_IncludeAssemblies<IXmlSitemapFeedStrategy_SetupXmlSitemapProviderScan_WithXmlSitemapProviderFactory>,
    //    IXmlSitemap_SetupXmlSitemapProviderScan_Finalizer
    //{
    //}






    //// Top level builder
    //public class XmlSitemapProvider_ForXmlSitemapProvider_Builder
    //    : IXmlSitemapFeedStrategy_ForXmlSitemapProvider_Starter
    //{

    //    public XmlSitemapProvider_ForXmlSitemapProvider_Builder(
    //        IDictionary<Assembly, string> includeAssembliesForScan,
    //        IDictionary<Assembly, string> excludeAssembliesForScan,
    //        IAssemblyProviderFactory assemblyProviderFactory,
    //        IXmlSitemapProviderFactory xmlSitemapProviderFactory,
    //        IAppDomainFactory appDomainFactory
    //        )
    //    //: base(includeAssembliesForScan, excludeAssembliesForScan, assemblyProviderFactory, xmlSitemapProviderFactory, appDomainFactory)
    //    {
    //    }

    //    public IXmlSitemapFeedStrategy_ForXmlSitemapProvider_WithXmlSitemapProviderFactory InjectUsing(IXmlSitemapProviderFactory xmlSitemapProviderFactory)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public IXmlSitemapFeedStrategy_ForXmlSitemapProvider_WithScanAssemblies ScanAssembly(string assemblyName)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public IXmlSitemapFeedStrategy_ForXmlSitemapProvider_WithScanAssemblies ScanAssemblies(IEnumerable<string> assemblyNames)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public IXmlSitemapFeedStrategy_ForXmlSitemapProvider_WithScanAssemblies ScanAssembly(Assembly assembly)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public IXmlSitemapFeedStrategy_ForXmlSitemapProvider_WithScanAssemblies ScanAssemblies(IEnumerable<Assembly> assemblies)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public IXmlSitemapFeedStrategy_ForXmlSitemapProvider_WithScanAssemblies DontScanAssembly(string assemblyName)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public IXmlSitemapFeedStrategy_ForXmlSitemapProvider_WithScanAssemblies DontScanAssemblies(IEnumerable<string> assemblyNames)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public IXmlSitemapFeedStrategy_ForXmlSitemapProvider_WithScanAssemblies DontScanAssembly(Assembly assembly)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public IXmlSitemapFeedStrategy_ForXmlSitemapProvider_WithScanAssemblies DontScanAssemblies(IEnumerable<Assembly> assemblies)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    public IXmlSitemapFeedStrategy_ForXmlSitemapProvider_WithScanAssembliesFrom ScanAssembliesFrom(IAssemblyProviderFactory assemblyProviderFactory)
    //    {
    //        throw new NotImplementedException();
    //    }
    //}


    //public abstract class XmlSitemapFeedStrategy_ForXmlSitemapProvider_WithXmlSitemapProviderFactory_WithScanAssemblies
    //    : XmlSitemapProvider_ForXmlSitemapProvider_BuilderBase,
    //    IXmlSitemapFeedStrategy_ForXmlSitemapProvider_WithXmlSitemapProviderFactory_WithScanAssemblies
    //{
    //    protected XmlSitemapFeedStrategy_ForXmlSitemapProvider_WithXmlSitemapProviderFactory_WithScanAssemblies(
    //        IDictionary<Assembly, string> includeAssembliesForScan,
    //        IDictionary<Assembly, string> excludeAssembliesForScan,
    //        IAssemblyProviderFactory assemblyProviderFactory,
    //        IXmlSitemapProviderFactory xmlSitemapProviderFactory,
    //        IAppDomainFactory appDomainFactory
    //        )
    //        : base(includeAssembliesForScan, excludeAssembliesForScan, assemblyProviderFactory, xmlSitemapProviderFactory, appDomainFactory)
    //    {
    //    }

    //    public new IXmlSitemapFeedStrategy_ForXmlSitemapProvider_WithXmlSitemapProviderFactory_WithScanAssemblies ScanAssembly(string assemblyName)
    //    {
    //        this.AddAssembly(assemblyName);
    //        return (IXmlSitemapFeedStrategy_ForXmlSitemapProvider_WithXmlSitemapProviderFactory_WithScanAssemblies)new XmlSitemapProvider_ForXmlSitemapProvider_Builder(
    //            this.includeAssembliesForScan, this.excludeAssembliesForScan, this.assemblyProviderFactory, this.xmlSitemapProviderFactory, this.appDomainFactory);
    //    }

    //    public new IXmlSitemapFeedStrategy_ForXmlSitemapProvider_WithXmlSitemapProviderFactory_WithScanAssemblies ScanAssemblies(IEnumerable<string> assemblyNames)
    //    {
    //        foreach (var assemblyName in assemblyNames)
    //        {
    //            this.AddAssembly(assemblyName);
    //        }
    //        return (IXmlSitemapFeedStrategy_ForXmlSitemapProvider_WithXmlSitemapProviderFactory_WithScanAssemblies)new XmlSitemapProvider_ForXmlSitemapProvider_Builder(
    //            this.includeAssembliesForScan, this.excludeAssembliesForScan, this.assemblyProviderFactory, this.xmlSitemapProviderFactory, this.appDomainFactory);
    //    }

    //    IXmlSitemapFeedStrategy_ForXmlSitemapProvider_WithXmlSitemapProviderFactory_WithScanAssemblies IXmlSitemapFeedStrategy_ForXmlSitemapProvider_ScanAssemblies<IXmlSitemapFeedStrategy_ForXmlSitemapProvider_WithXmlSitemapProviderFactory_WithScanAssemblies>.ScanAssembly(Assembly assembly)
    //    {
    //        this.AddAssembly(assembly);
    //        return (IXmlSitemapFeedStrategy_ForXmlSitemapProvider_WithXmlSitemapProviderFactory_WithScanAssemblies)new XmlSitemapProvider_ForXmlSitemapProvider_Builder(
    //            this.includeAssembliesForScan, this.excludeAssembliesForScan, this.assemblyProviderFactory, this.xmlSitemapProviderFactory, this.appDomainFactory);
    //    }

    //    IXmlSitemapFeedStrategy_ForXmlSitemapProvider_WithXmlSitemapProviderFactory_WithScanAssemblies IXmlSitemapFeedStrategy_ForXmlSitemapProvider_ScanAssemblies<IXmlSitemapFeedStrategy_ForXmlSitemapProvider_WithXmlSitemapProviderFactory_WithScanAssemblies>.ScanAssemblies(IEnumerable<Assembly> assemblies)
    //    {
    //        return (IXmlSitemapFeedStrategy_ForXmlSitemapProvider_WithXmlSitemapProviderFactory_WithScanAssemblies)new XmlSitemapProvider_ForXmlSitemapProvider_Builder(
    //            this.includeAssembliesForScan, this.excludeAssembliesForScan, this.assemblyProviderFactory, this.xmlSitemapProviderFactory, this.appDomainFactory);
    //    }

    //    public new IXmlSitemapFeedStrategy_ForXmlSitemapProvider_WithXmlSitemapProviderFactory_WithScanAssemblies DontScanAssembly(string assemblyName)
    //    {
    //        return (IXmlSitemapFeedStrategy_ForXmlSitemapProvider_WithXmlSitemapProviderFactory_WithScanAssemblies)new XmlSitemapProvider_ForXmlSitemapProvider_Builder(
    //            this.includeAssembliesForScan, this.excludeAssembliesForScan, this.assemblyProviderFactory, this.xmlSitemapProviderFactory, this.appDomainFactory);
    //    }

    //    public new IXmlSitemapFeedStrategy_ForXmlSitemapProvider_WithXmlSitemapProviderFactory_WithScanAssemblies DontScanAssemblies(IEnumerable<string> assemblyNames)
    //    {
    //        return (IXmlSitemapFeedStrategy_ForXmlSitemapProvider_WithXmlSitemapProviderFactory_WithScanAssemblies)new XmlSitemapProvider_ForXmlSitemapProvider_Builder(
    //            this.includeAssembliesForScan, this.excludeAssembliesForScan, this.assemblyProviderFactory, this.xmlSitemapProviderFactory, this.appDomainFactory);
    //    }

    //    IXmlSitemapFeedStrategy_ForXmlSitemapProvider_WithXmlSitemapProviderFactory_WithScanAssemblies IXmlSitemapFeedStrategy_ForXmlSitemapProvider_ScanAssemblies<IXmlSitemapFeedStrategy_ForXmlSitemapProvider_WithXmlSitemapProviderFactory_WithScanAssemblies>.DontScanAssembly(Assembly assembly)
    //    {
    //        return (IXmlSitemapFeedStrategy_ForXmlSitemapProvider_WithXmlSitemapProviderFactory_WithScanAssemblies)new XmlSitemapProvider_ForXmlSitemapProvider_Builder(
    //            this.includeAssembliesForScan, this.excludeAssembliesForScan, this.assemblyProviderFactory, this.xmlSitemapProviderFactory, this.appDomainFactory);
    //    }

    //    IXmlSitemapFeedStrategy_ForXmlSitemapProvider_WithXmlSitemapProviderFactory_WithScanAssemblies IXmlSitemapFeedStrategy_ForXmlSitemapProvider_ScanAssemblies<IXmlSitemapFeedStrategy_ForXmlSitemapProvider_WithXmlSitemapProviderFactory_WithScanAssemblies>.DontScanAssemblies(IEnumerable<Assembly> assemblies)
    //    {
    //        return (IXmlSitemapFeedStrategy_ForXmlSitemapProvider_WithXmlSitemapProviderFactory_WithScanAssemblies)new XmlSitemapProvider_ForXmlSitemapProvider_Builder(
    //            this.includeAssembliesForScan, this.excludeAssembliesForScan, this.assemblyProviderFactory, this.xmlSitemapProviderFactory, this.appDomainFactory);
    //    }

    //    #region private methods

    //    private void AddAssembly(string assemblyName)
    //    {
    //        var assembly = this.GetAssemblyNamed(assemblyName);
    //        this.AddAssembly(assembly);
    //    }

    //    private void AddAssembly(Assembly assembly)
    //    {
    //        if (assembly != null)
    //        {
    //            var assemblyName = this.GetAssemblyName(assembly);
    //            this.includeAssembliesForScan.Add(assembly, assemblyName);
    //            if (this.excludeAssembliesForScan.ContainsKey(assembly))
    //            {
    //                this.excludeAssembliesForScan.Remove(assembly);
    //            }
    //        }
    //    }

    //    private void RemoveAssembly(string assemblyName)
    //    {
    //        var assembly = this.GetAssemblyNamed(assemblyName);
    //        this.RemoveAssembly(assembly);
    //    }

    //    private void RemoveAssembly(Assembly assembly)
    //    {
    //        if (assembly != null)
    //        {
    //            if (this.includeAssembliesForScan.ContainsKey(assembly))
    //            {
    //                this.includeAssembliesForScan.Remove(assembly);
    //            }
    //            if (!this.excludeAssembliesForScan.ContainsKey(assembly))
    //            {
    //                var assemblyName = this.GetAssemblyName(assembly);
    //                this.excludeAssembliesForScan.Add(assembly, assemblyName);
    //            }
    //        }
    //    }

    //    private string GetAssemblyName(Assembly assembly)
    //    {
    //        return new AssemblyName(assembly.FullName).Name;
    //    }

    //    private Assembly GetAssemblyNamed(string assemblyName)
    //    {
    //        var appDomain = this.appDomainFactory.Create();
    //        try
    //        {
    //            return appDomain.GetAssemblies().Where(a => this.GetAssemblyName(a).Equals(assemblyName)).FirstOrDefault();
    //        }
    //        finally
    //        {
    //            this.appDomainFactory.Release(appDomain);
    //        }
    //    }

    //    #endregion
    //}

    //public abstract class XmlSitemapProvider_ForXmlSitemapProvider_BuilderBase // Needed?
    //    : IXmlSitemapFeedStrategy_ForXmlSitemapProvider_Starter,
    //    IXmlSitemapFeedStrategy_ForXmlSitemapProvider_Finalizer
    //{
    //    protected XmlSitemapProvider_ForXmlSitemapProvider_BuilderBase(
    //        IDictionary<Assembly, string> includeAssembliesForScan,
    //        IDictionary<Assembly, string> excludeAssembliesForScan,
    //        IAssemblyProviderFactory assemblyProviderFactory,
    //        IXmlSitemapProviderFactory xmlSitemapProviderFactory,
    //        IAppDomainFactory appDomainFactory
    //        )
    //    {
    //        if (includeAssembliesForScan == null)
    //            throw new ArgumentNullException("includeAssembliesForScan");
    //        if (excludeAssembliesForScan == null)
    //            throw new ArgumentNullException("excludeAssembliesForScan");
    //        if (assemblyProviderFactory == null)
    //            throw new ArgumentNullException("assemblyProviderFactory");
    //        if (xmlSitemapProviderFactory == null)
    //            throw new ArgumentNullException("xmlSitemapProviderFactory");
    //        if (appDomainFactory == null)
    //            throw new ArgumentNullException("appDomainFactory");

    //        this.includeAssembliesForScan = includeAssembliesForScan;
    //        this.excludeAssembliesForScan = excludeAssembliesForScan;
    //        this.assemblyProviderFactory = assemblyProviderFactory;
    //        this.xmlSitemapProviderFactory = xmlSitemapProviderFactory;
    //        this.appDomainFactory = appDomainFactory;
    //    }
    //    protected readonly IDictionary<Assembly, string> includeAssembliesForScan;
    //    protected readonly IDictionary<Assembly, string> excludeAssembliesForScan;
    //    protected readonly IAssemblyProviderFactory assemblyProviderFactory;
    //    protected readonly IXmlSitemapProviderFactory xmlSitemapProviderFactory;
    //    protected readonly IAppDomainFactory appDomainFactory;

    //    public abstract IXmlSitemapFeedStrategy_ForXmlSitemapProvider_WithXmlSitemapProviderFactory InjectUsing(IXmlSitemapProviderFactory xmlSitemapProviderFactory);

    //    public abstract IXmlSitemapFeedStrategy_ForXmlSitemapProvider_WithScanAssemblies ScanAssembly(string assemblyName);

    //    public abstract IXmlSitemapFeedStrategy_ForXmlSitemapProvider_WithScanAssemblies ScanAssemblies(IEnumerable<string> assemblyNames);

    //    public abstract IXmlSitemapFeedStrategy_ForXmlSitemapProvider_WithScanAssemblies ScanAssembly(Assembly assembly);

    //    public abstract IXmlSitemapFeedStrategy_ForXmlSitemapProvider_WithScanAssemblies ScanAssemblies(IEnumerable<Assembly> assemblies);

    //    public abstract IXmlSitemapFeedStrategy_ForXmlSitemapProvider_WithScanAssemblies DontScanAssembly(string assemblyName);

    //    public abstract IXmlSitemapFeedStrategy_ForXmlSitemapProvider_WithScanAssemblies DontScanAssemblies(IEnumerable<string> assemblyNames);

    //    public abstract IXmlSitemapFeedStrategy_ForXmlSitemapProvider_WithScanAssemblies DontScanAssembly(Assembly assembly);

    //    public abstract IXmlSitemapFeedStrategy_ForXmlSitemapProvider_WithScanAssemblies DontScanAssemblies(IEnumerable<Assembly> assemblies);

    //    public abstract IXmlSitemapFeedStrategy_ForXmlSitemapProvider_WithScanAssembliesFrom ScanAssembliesFrom(IAssemblyProviderFactory assemblyProviderFactory);

    //    public abstract IAssemblyProviderFactory Create();

    //    public abstract IXmlSitemapProviderFactory XmlSitemapProviderFactory { get; }
    //}


    //public interface IXmlSitemapFeedStrategy_ForXmlSitemapProvider_XmlSitemapProviderFactory<TRemainder> // set only 1 time
    //{
    //    TRemainder InjectUsing(IXmlSitemapProviderFactory xmlSitemapProviderFactory);
    //}

    //public interface IXmlSitemapFeedStrategy_ForXmlSitemapProvider_ScanAssemblies<TRemainder> // Use multiple times, but only if ScanAssembliesFrom is not set
    //{
    //    TRemainder ScanAssembly(string assemblyName);

    //    TRemainder ScanAssemblies(IEnumerable<string> assemblyNames);

    //    TRemainder ScanAssembly(Assembly assembly);

    //    TRemainder ScanAssemblies(IEnumerable<Assembly> assemblies);

    //    TRemainder DontScanAssembly(string assemblyName);

    //    TRemainder DontScanAssemblies(IEnumerable<string> assemblyNames);

    //    TRemainder DontScanAssembly(Assembly assembly);

    //    TRemainder DontScanAssemblies(IEnumerable<Assembly> assemblies);
    //}

    //public interface IXmlSitemapFeedStrategy_ForXmlSitemapProvider_ScanAssembliesFrom<TRemainder> // Set only 1 time, but not if ScanAssemblies is set
    //{
    //    TRemainder ScanAssembliesFrom(IAssemblyProviderFactory assemblyProviderFactory);
    //}

    //public interface IXmlSitemapFeedStrategy_ForXmlSitemapProvider_Finalizer
    //{
    //    IAssemblyProviderFactory Create();

    //    IXmlSitemapProviderFactory XmlSitemapProviderFactory { get; }
    //}

    //// Nothing set
    //public interface IXmlSitemapFeedStrategy_ForXmlSitemapProvider_Starter
    //    : IXmlSitemapFeedStrategy_ForXmlSitemapProvider_XmlSitemapProviderFactory<IXmlSitemapFeedStrategy_ForXmlSitemapProvider_WithXmlSitemapProviderFactory>,
    //    IXmlSitemapFeedStrategy_ForXmlSitemapProvider_ScanAssemblies<IXmlSitemapFeedStrategy_ForXmlSitemapProvider_WithScanAssemblies>,
    //    IXmlSitemapFeedStrategy_ForXmlSitemapProvider_ScanAssembliesFrom<IXmlSitemapFeedStrategy_ForXmlSitemapProvider_WithScanAssembliesFrom>
    //{
    //}

    //// ScanAssembliesFrom set
    //public interface IXmlSitemapFeedStrategy_ForXmlSitemapProvider_WithScanAssembliesFrom
    //    : IXmlSitemapFeedStrategy_ForXmlSitemapProvider_XmlSitemapProviderFactory<IXmlSitemapFeedStrategy_ForXmlSitemapProvider_Finalizer>,
    //    IXmlSitemapFeedStrategy_ForXmlSitemapProvider_Finalizer
    //{
    //}

    //// at least 1 ScanAssemblySet
    //public interface IXmlSitemapFeedStrategy_ForXmlSitemapProvider_WithScanAssemblies
    //    : IXmlSitemapFeedStrategy_ForXmlSitemapProvider_ScanAssemblies<IXmlSitemapFeedStrategy_ForXmlSitemapProvider_WithScanAssemblies>,
    //    IXmlSitemapFeedStrategy_ForXmlSitemapProvider_XmlSitemapProviderFactory<IXmlSitemapFeedStrategy_ForXmlSitemapProvider_WithXmlSitemapProviderFactory_WithScanAssemblies>
    //{
    //}

    //// XmlSitemapProviderFactory set
    //public interface IXmlSitemapFeedStrategy_ForXmlSitemapProvider_WithXmlSitemapProviderFactory
    //    : IXmlSitemapFeedStrategy_ForXmlSitemapProvider_ScanAssemblies<IXmlSitemapFeedStrategy_ForXmlSitemapProvider_WithXmlSitemapProviderFactory_WithScanAssemblies>,
    //    IXmlSitemapFeedStrategy_ForXmlSitemapProvider_ScanAssembliesFrom<IXmlSitemapFeedStrategy_ForXmlSitemapProvider_Finalizer>
    //{
    //}


    ////// XmlSitemapProviderFactory set AND ScanAssembliesFrom set (valid?)
    ////public interface IXmlSitemapFeedStrategy_ForXmlSitemapProvider_WithXmlSitemapProviderFactory_WithScanAssembliesFrom
    ////    : IXmlSitemapFeedStrategy_ForXmlSitemapProvider_Finalizer
    ////{
    ////}

    //// XmlSitemapProviderFactory set AND at least 1 ScanAssembly set
    //public interface IXmlSitemapFeedStrategy_ForXmlSitemapProvider_WithXmlSitemapProviderFactory_WithScanAssemblies
    //    : IXmlSitemapFeedStrategy_ForXmlSitemapProvider_ScanAssemblies<IXmlSitemapFeedStrategy_ForXmlSitemapProvider_WithXmlSitemapProviderFactory_WithScanAssemblies>,
    //    IXmlSitemapFeedStrategy_ForXmlSitemapProvider_Finalizer
    //{
    //}


}
