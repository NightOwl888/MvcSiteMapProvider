//using System;
//using System.Reflection;
//using MvcSiteMapProvider.Reflection;

//namespace MvcSiteMapProvider.Xml.Sitemap.Configuration
//{
//    public class XmlSitemapFeedStrategyForXmlSitemapProviderAssemblyProviderFactorySetBuilder
//        : IXmlSitemapFeedStrategyForXmlSitemapProviderAssemblyProviderFactorySetWithXmlSitemapProviderFactory
//    {
//        public XmlSitemapFeedStrategyForXmlSitemapProviderAssemblyProviderFactorySetBuilder()
//            : this(
//            assemblyProviderFactory: new AssemblyProviderFactoryBuilder().Create(), 
//            xmlSitemapProviderFactory: new XmlSitemapProviderFactory()
//            )
//        {
//        }

//        public XmlSitemapFeedStrategyForXmlSitemapProviderAssemblyProviderFactorySetBuilder(
//            IAssemblyProviderFactory assemblyProviderFactory,
//            IXmlSitemapProviderFactory xmlSitemapProviderFactory
//            )
//        {
//            if (assemblyProviderFactory == null)
//                throw new ArgumentNullException("assemblyProviderFactory");
//            if (xmlSitemapProviderFactory == null)
//                throw new ArgumentNullException("xmlSitemapProviderFactory");

//            this.assemblyProviderFactory = assemblyProviderFactory;
//            this.xmlSitemapProviderFactory = xmlSitemapProviderFactory;
//        }
//        private readonly IAssemblyProviderFactory assemblyProviderFactory;
//        private readonly IXmlSitemapProviderFactory xmlSitemapProviderFactory;

//        public IXmlSitemapFeedStrategyForXmlSitemapProviderAssemblyProviderFactorySetWithXmlSitemapProviderFactory ScanAssembliesFrom(IAssemblyProviderFactory assemblyProviderFactory)
//        {
//            return new XmlSitemapFeedStrategyForXmlSitemapProviderAssemblyProviderFactorySetBuilder(assemblyProviderFactory, this.xmlSitemapProviderFactory);
//        }

//        public IXmlSitemapFeedStrategyForXmlSitemapProviderAssemblyProviderFactorySetWithXmlSitemapProviderFactory UseXmlSitemapProviderFactory(IXmlSitemapProviderFactory xmlSitemapProviderFactory)
//        {
//            return new XmlSitemapFeedStrategyForXmlSitemapProviderAssemblyProviderFactorySetBuilder(this.assemblyProviderFactory, xmlSitemapProviderFactory);
//        }

//        public IAssemblyProviderFactory Create()
//        {
//            return this.assemblyProviderFactory;
//        }

//        public IXmlSitemapProviderFactory XmlSitemapProviderFactory
//        {
//            get { return this.xmlSitemapProviderFactory; }
//        }
//    }
//}
