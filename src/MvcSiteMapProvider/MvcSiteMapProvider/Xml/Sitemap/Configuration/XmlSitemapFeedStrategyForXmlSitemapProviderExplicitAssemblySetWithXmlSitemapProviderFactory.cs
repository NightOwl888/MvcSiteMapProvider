//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//using MvcSiteMapProvider.Reflection;

//namespace MvcSiteMapProvider.Xml.Sitemap.Configuration
//{
//    public class XmlSitemapFeedStrategyForXmlSitemapProviderExplicitAssemblySetWithXmlSitemapProviderFactory
//        : IXmlSitemapFeedStrategyForXmlSitemapProviderExplicitAssemblySetWithXmlSitemapProviderFactory
//    {
//        public XmlSitemapFeedStrategyForXmlSitemapProviderExplicitAssemblySetWithXmlSitemapProviderFactory(
//            IDictionary<Assembly, string> includeAssembliesForScan,
//            IDictionary<Assembly, string> excludeAssembliesForScan,
//            IAssemblyProviderFactory assemblyProviderFactory,
//            IXmlSitemapProviderFactory xmlSitemapProviderFactory,
//            IAppDomainFactory appDomainFactory
//            )
//        {
//            if (includeAssembliesForScan == null)
//                throw new ArgumentNullException("includeAssembliesForScan");
//            if (excludeAssembliesForScan == null)
//                throw new ArgumentNullException("excludeAssembliesForScan");
//            if (assemblyProviderFactory == null)
//                throw new ArgumentNullException("assemblyProviderFactory");
//            if (xmlSitemapProviderFactory == null)
//                throw new ArgumentNullException("xmlSitemapProviderFactory");
//            if (appDomainFactory == null)
//                throw new ArgumentNullException("appDomainFactory");

//            this.includeAssembliesForScan = includeAssembliesForScan;
//            this.excludeAssembliesForScan = excludeAssembliesForScan;
//            this.assemblyProviderFactory = assemblyProviderFactory;
//            this.appDomainFactory = appDomainFactory;
//            this.xmlSitemapProviderFactory = xmlSitemapProviderFactory;
//        }
//        private readonly IDictionary<Assembly, string> includeAssembliesForScan;
//        private readonly IDictionary<Assembly, string> excludeAssembliesForScan;
//        private readonly IAssemblyProviderFactory assemblyProviderFactory;
//        private readonly IXmlSitemapProviderFactory xmlSitemapProviderFactory;
//        private readonly IAppDomainFactory appDomainFactory;

//        public IXmlSitemapFeedStrategyForXmlSitemapProviderExplicitAssemblySetWithXmlSitemapProviderFactory UseXmlSitemapProviderFactory(IXmlSitemapProviderFactory xmlSitemapProviderFactory)
//        {
//            return new XmlSitemapFeedStrategyForXmlSitemapProviderExplicitAssemblySetWithXmlSitemapProviderFactory(
//                this.includeAssembliesForScan, this.excludeAssembliesForScan, this.assemblyProviderFactory, 
//                xmlSitemapProviderFactory, this.appDomainFactory);
//        }

//        public IXmlSitemapFeedStrategyForXmlSitemapProviderExplicitAssemblySetWithXmlSitemapProviderFactory ScanAssembly(string assemblyName)
//        {
//            this.AddAssembly(assemblyName);
//            return new XmlSitemapFeedStrategyForXmlSitemapProviderExplicitAssemblySetWithXmlSitemapProviderFactory(
//                this.includeAssembliesForScan, this.excludeAssembliesForScan, this.assemblyProviderFactory,
//                this.xmlSitemapProviderFactory, this.appDomainFactory);
//        }

//        public IXmlSitemapFeedStrategyForXmlSitemapProviderExplicitAssemblySetWithXmlSitemapProviderFactory ScanAssemblies(IEnumerable<string> assemblyNames)
//        {
//            foreach (var assemblyName in assemblyNames)
//            {
//                this.AddAssembly(assemblyName);
//            }
//            return new XmlSitemapFeedStrategyForXmlSitemapProviderExplicitAssemblySetWithXmlSitemapProviderFactory(
//                this.includeAssembliesForScan, this.excludeAssembliesForScan, this.assemblyProviderFactory,
//                this.xmlSitemapProviderFactory, this.appDomainFactory);
//        }

//        public IXmlSitemapFeedStrategyForXmlSitemapProviderExplicitAssemblySetWithXmlSitemapProviderFactory ScanAssembly(Assembly assembly)
//        {
//            this.AddAssembly(assembly);
//            return new XmlSitemapFeedStrategyForXmlSitemapProviderExplicitAssemblySetWithXmlSitemapProviderFactory(
//                this.includeAssembliesForScan, this.excludeAssembliesForScan, this.assemblyProviderFactory,
//                this.xmlSitemapProviderFactory, this.appDomainFactory);
//        }

//        public IXmlSitemapFeedStrategyForXmlSitemapProviderExplicitAssemblySetWithXmlSitemapProviderFactory ScanAssemblies(IEnumerable<Assembly> assemblies)
//        {
//            foreach (var assembly in assemblies)
//            {
//                this.AddAssembly(assembly);
//            }
//            return new XmlSitemapFeedStrategyForXmlSitemapProviderExplicitAssemblySetWithXmlSitemapProviderFactory(
//                this.includeAssembliesForScan, this.excludeAssembliesForScan, this.assemblyProviderFactory,
//                this.xmlSitemapProviderFactory, this.appDomainFactory);
//        }

//        public IXmlSitemapFeedStrategyForXmlSitemapProviderExplicitAssemblySetWithXmlSitemapProviderFactory DontScanAssembly(string assemblyName)
//        {
//            this.RemoveAssembly(assemblyName);
//            return new XmlSitemapFeedStrategyForXmlSitemapProviderExplicitAssemblySetWithXmlSitemapProviderFactory(
//                this.includeAssembliesForScan, this.excludeAssembliesForScan, this.assemblyProviderFactory,
//                this.xmlSitemapProviderFactory, this.appDomainFactory);
//        }

//        public IXmlSitemapFeedStrategyForXmlSitemapProviderExplicitAssemblySetWithXmlSitemapProviderFactory DontScanAssemblies(IEnumerable<string> assemblyNames)
//        {
//            foreach (var assemblyName in assemblyNames)
//            {
//                this.RemoveAssembly(assemblyName);
//            }
//            return new XmlSitemapFeedStrategyForXmlSitemapProviderExplicitAssemblySetWithXmlSitemapProviderFactory(
//                this.includeAssembliesForScan, this.excludeAssembliesForScan, this.assemblyProviderFactory,
//                this.xmlSitemapProviderFactory, this.appDomainFactory);
//        }

//        public IXmlSitemapFeedStrategyForXmlSitemapProviderExplicitAssemblySetWithXmlSitemapProviderFactory DontScanAssembly(Assembly assembly)
//        {
//            this.RemoveAssembly(assembly);
//            return new XmlSitemapFeedStrategyForXmlSitemapProviderExplicitAssemblySetWithXmlSitemapProviderFactory(
//                this.includeAssembliesForScan, this.excludeAssembliesForScan, this.assemblyProviderFactory,
//                this.xmlSitemapProviderFactory, this.appDomainFactory);
//        }

//        public IXmlSitemapFeedStrategyForXmlSitemapProviderExplicitAssemblySetWithXmlSitemapProviderFactory DontScanAssemblies(IEnumerable<Assembly> assemblies)
//        {
//            foreach (var assembly in assemblies)
//            {
//                this.RemoveAssembly(assembly);
//            }
//            return new XmlSitemapFeedStrategyForXmlSitemapProviderExplicitAssemblySetWithXmlSitemapProviderFactory(
//                this.includeAssembliesForScan, this.excludeAssembliesForScan, this.assemblyProviderFactory,
//                this.xmlSitemapProviderFactory, this.appDomainFactory);
//        }

//        public IAssemblyProviderFactory Create()
//        {
//            return new AssemblyProviderFactoryBuilder()
//                .WithIncludeAssemblies(this.includeAssembliesForScan.Keys.ToArray())
//                .WithExcludeAssemblies(this.excludeAssembliesForScan.Values.ToArray())
//                .Create();
//        }

//        public IXmlSitemapProviderFactory XmlSitemapProviderFactory
//        {
//            get { return this.xmlSitemapProviderFactory; }
//        }

//        #region private methods

//        private void AddAssembly(string assemblyName)
//        {
//            var assembly = this.GetAssemblyNamed(assemblyName);
//            this.AddAssembly(assembly);
//        }

//        private void AddAssembly(Assembly assembly)
//        {
//            if (assembly != null)
//            {
//                var assemblyName = this.GetAssemblyName(assembly);
//                this.includeAssembliesForScan.Add(assembly, assemblyName);
//                if (this.excludeAssembliesForScan.ContainsKey(assembly))
//                {
//                    this.excludeAssembliesForScan.Remove(assembly);
//                }
//            }
//        }

//        private void RemoveAssembly(string assemblyName)
//        {
//            var assembly = this.GetAssemblyNamed(assemblyName);
//            this.RemoveAssembly(assembly);
//        }

//        private void RemoveAssembly(Assembly assembly)
//        {
//            if (assembly != null)
//            {
//                if (this.includeAssembliesForScan.ContainsKey(assembly))
//                {
//                    this.includeAssembliesForScan.Remove(assembly);
//                }
//                if (!this.excludeAssembliesForScan.ContainsKey(assembly))
//                {
//                    var assemblyName = this.GetAssemblyName(assembly);
//                    this.excludeAssembliesForScan.Add(assembly, assemblyName);
//                }
//            }
//        }

//        private string GetAssemblyName(Assembly assembly)
//        {
//            return new AssemblyName(assembly.FullName).Name;
//        }

//        private Assembly GetAssemblyNamed(string assemblyName)
//        {
//            var appDomain = this.appDomainFactory.Create();
//            try
//            {
//                return appDomain.GetAssemblies().Where(a => this.GetAssemblyName(a).Equals(assemblyName)).FirstOrDefault();
//            }
//            finally
//            {
//                this.appDomainFactory.Release(appDomain);
//            }
//        } 

//        #endregion

//    }
//}
