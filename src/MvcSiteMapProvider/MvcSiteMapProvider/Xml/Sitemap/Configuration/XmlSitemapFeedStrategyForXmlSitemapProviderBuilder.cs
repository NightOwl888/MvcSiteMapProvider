//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Reflection;
//using MvcSiteMapProvider.Reflection;

//namespace MvcSiteMapProvider.Xml.Sitemap.Configuration
//{
//    public class XmlSitemapFeedStrategyForXmlSitemapProviderBuilder
//        : IXmlSitemapFeedStrategyForXmlSitemapProviderStarter
//    {
//        public XmlSitemapFeedStrategyForXmlSitemapProviderBuilder()
//            : this(
//            includeAssembliesForScan: new Dictionary<Assembly, string>(), 
//            excludeAssembliesForScan: new Dictionary<Assembly, string>(), 
//            assemblyProviderFactory: new AssemblyProviderFactoryBuilder().Create(), 
//            xmlSitemapProviderFactory: new XmlSitemapProviderFactory(), 
//            appDomainFactory: new AppDomainFactory())
//        {
//        }

//        private XmlSitemapFeedStrategyForXmlSitemapProviderBuilder(
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





//        public IXmlSitemapFeedStrategyForXmlSitemapProviderExplicitAssemblySet ScanAssembly(string assemblyName)
//        {
//            //this.AddAssembly(assemblyName);
//            //return new XmlSitemapFeedStrategyForXmlSitemapProviderBuilder(this.includeAssembliesForScan, this.excludeAssembliesForScan, 
//            //    this.assemblyProviderFactory, this.xmlSitemapProviderFactory, this.appDomainFactory);

//            return new XmlSitemapFeedStrategyForXmlSitemapProviderExplicitAssemblySetWithXmlSitemapProviderFactory(
//                this.includeAssembliesForScan, this.excludeAssembliesForScan, this.assemblyProviderFactory,
//                xmlSitemapProviderFactory, this.appDomainFactory).ScanAssembly(assemblyName);
//        }

//        public IXmlSitemapFeedStrategyForXmlSitemapProviderExplicitAssemblySet ScanAssemblies(IEnumerable<string> assemblyNames)
//        {
//            //foreach (var assemblyName in assemblyNames)
//            //{
//            //    this.AddAssembly(assemblyName);
//            //}
//            //return new XmlSitemapFeedStrategyForXmlSitemapProviderBuilder(this.includeAssembliesForScan, this.excludeAssembliesForScan,
//            //    this.assemblyProviderFactory, this.xmlSitemapProviderFactory, this.appDomainFactory);

//            return new XmlSitemapFeedStrategyForXmlSitemapProviderExplicitAssemblySetWithXmlSitemapProviderFactory(
//                this.includeAssembliesForScan, this.excludeAssembliesForScan, this.assemblyProviderFactory,
//                this.xmlSitemapProviderFactory, this.appDomainFactory).ScanAssemblies(assemblyNames);
//        }

//        public IXmlSitemapFeedStrategyForXmlSitemapProviderExplicitAssemblySet ScanAssembly(Assembly assembly)
//        {
//            //this.AddAssembly(assembly);
//            //return new XmlSitemapFeedStrategyForXmlSitemapProviderBuilder(this.includeAssembliesForScan, this.excludeAssembliesForScan,
//            //    this.assemblyProviderFactory, this.xmlSitemapProviderFactory, this.appDomainFactory);
//            return new XmlSitemapFeedStrategyForXmlSitemapProviderExplicitAssemblySetWithXmlSitemapProviderFactory(
//                this.includeAssembliesForScan, this.excludeAssembliesForScan, this.assemblyProviderFactory,
//                this.xmlSitemapProviderFactory, this.appDomainFactory).ScanAssembly(assembly);
//        }

//        public IXmlSitemapFeedStrategyForXmlSitemapProviderExplicitAssemblySet ScanAssemblies(IEnumerable<Assembly> assemblies)
//        {
//            //foreach (var assembly in assemblies)
//            //{
//            //    this.AddAssembly(assembly);
//            //}
//            //return new XmlSitemapFeedStrategyForXmlSitemapProviderBuilder(this.includeAssembliesForScan, this.excludeAssembliesForScan,
//            //    this.assemblyProviderFactory, this.xmlSitemapProviderFactory, this.appDomainFactory);
//            return new XmlSitemapFeedStrategyForXmlSitemapProviderExplicitAssemblySetWithXmlSitemapProviderFactory(
//                this.includeAssembliesForScan, this.excludeAssembliesForScan, this.assemblyProviderFactory,
//                this.xmlSitemapProviderFactory, this.appDomainFactory).ScanAssemblies(assemblies);
//        }

//        public IXmlSitemapFeedStrategyForXmlSitemapProviderExplicitAssemblySet DontScanAssembly(string assemblyName)
//        {
//            //this.RemoveAssembly(assemblyName);
//            //return new XmlSitemapFeedStrategyForXmlSitemapProviderBuilder(this.includeAssembliesForScan, this.excludeAssembliesForScan,
//            //    this.assemblyProviderFactory, this.xmlSitemapProviderFactory, this.appDomainFactory);
//            return new XmlSitemapFeedStrategyForXmlSitemapProviderExplicitAssemblySetWithXmlSitemapProviderFactory(
//                this.includeAssembliesForScan, this.excludeAssembliesForScan, this.assemblyProviderFactory,
//                this.xmlSitemapProviderFactory, this.appDomainFactory).DontScanAssembly(assemblyName);
//        }

//        public IXmlSitemapFeedStrategyForXmlSitemapProviderExplicitAssemblySet DontScanAssemblies(IEnumerable<string> assemblyNames)
//        {
//            //foreach (var assemblyName in assemblyNames)
//            //{
//            //    this.RemoveAssembly(assemblyName);
//            //}
//            //return new XmlSitemapFeedStrategyForXmlSitemapProviderBuilder(this.includeAssembliesForScan, this.excludeAssembliesForScan,
//            //    this.assemblyProviderFactory, this.xmlSitemapProviderFactory, this.appDomainFactory);
//            return new XmlSitemapFeedStrategyForXmlSitemapProviderExplicitAssemblySetWithXmlSitemapProviderFactory(
//                this.includeAssembliesForScan, this.excludeAssembliesForScan, this.assemblyProviderFactory,
//                this.xmlSitemapProviderFactory, this.appDomainFactory).DontScanAssemblies(assemblyNames);
//        }

//        public IXmlSitemapFeedStrategyForXmlSitemapProviderExplicitAssemblySet DontScanAssembly(Assembly assembly)
//        {
//            //this.RemoveAssembly(assembly);
//            //return new XmlSitemapFeedStrategyForXmlSitemapProviderBuilder(this.includeAssembliesForScan, this.excludeAssembliesForScan,
//            //    this.assemblyProviderFactory, this.xmlSitemapProviderFactory, this.appDomainFactory);
//            return new XmlSitemapFeedStrategyForXmlSitemapProviderExplicitAssemblySetWithXmlSitemapProviderFactory(
//                this.includeAssembliesForScan, this.excludeAssembliesForScan, this.assemblyProviderFactory,
//                this.xmlSitemapProviderFactory, this.appDomainFactory).DontScanAssembly(assembly);
//        }

//        public IXmlSitemapFeedStrategyForXmlSitemapProviderExplicitAssemblySet DontScanAssemblies(IEnumerable<Assembly> assemblies)
//        {
//            //foreach (var assembly in assemblies)
//            //{
//            //    this.RemoveAssembly(assembly);
//            //}
//            //return new XmlSitemapFeedStrategyForXmlSitemapProviderBuilder(this.includeAssembliesForScan, this.excludeAssembliesForScan,
//            //    this.assemblyProviderFactory, this.xmlSitemapProviderFactory, this.appDomainFactory);
//            return new XmlSitemapFeedStrategyForXmlSitemapProviderExplicitAssemblySetWithXmlSitemapProviderFactory(
//                this.includeAssembliesForScan, this.excludeAssembliesForScan, this.assemblyProviderFactory,
//                this.xmlSitemapProviderFactory, this.appDomainFactory).DontScanAssemblies(assemblies);
//        }

//        //public IXmlSitemapFeedStrategyForXmlSitemapProviderExplicitAssemblySet UseXmlSitemapProviderFactory(IXmlSitemapProviderFactory xmlSitemapProviderFactory)
//        //{
//        //    return new XmlSitemapFeedStrategyForXmlSitemapProviderBuilder(this.includeAssembliesForScan, this.excludeAssembliesForScan,
//        //        this.assemblyProviderFactory, xmlSitemapProviderFactory, this.appDomainFactory);
//        //}

//        public IXmlSitemapFeedStrategyForXmlSitemapProviderAssemblyProviderFactorySet ScanAssembliesFrom(IAssemblyProviderFactory assemblyProviderFactory)
//        {
//            return new XmlSitemapFeedStrategyForXmlSitemapProviderAssemblyProviderFactorySetBuilder(
//                assemblyProviderFactory, this.xmlSitemapProviderFactory);
//        }

//        public IXmlSitemapFeedStrategyForXmlSitemapProviderXmlSitemapProviderSet UseXmlSitemapProviderFactory(IXmlSitemapProviderFactory xmlSitemapProviderFactory)
//        {
//            return new XmlSitemapFeedStrategyForXmlSitemapProviderBuilder(this.includeAssembliesForScan, this.excludeAssembliesForScan,
//                this.assemblyProviderFactory, xmlSitemapProviderFactory, this.appDomainFactory);
//        }

//        public IXmlSitemapProviderFactory XmlSitemapProviderFactory
//        {
//            get { return this.xmlSitemapProviderFactory; }
//        }

//        public IAssemblyProviderFactory Create()
//        {
//            return new AssemblyProviderFactoryBuilder()
//                .WithIncludeAssemblies(this.includeAssembliesForScan.Keys.ToArray())
//                .WithExcludeAssemblies(this.excludeAssembliesForScan.Values.ToArray())
//                .Create();
//        }
//    }
//}
