using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using MvcSiteMapProvider.ComponentModel;
using MvcSiteMapProvider.Xml.Sitemap.Specialized;

namespace MvcSiteMapProvider.Xml.Sitemap.Configuration
{
    public interface IXmlSitemapFeedBuilderFacade
        : IFluentInterface
    {
        // TODO: Accept name (required) through the constructor?

        // From XmlWriterSettings

        IXmlSitemapFeedBuilderFacade WithEncoding(Encoding encoding);

        IXmlSitemapFeedBuilderFacade WithIndentation();

        IXmlSitemapFeedBuilderFacade OmitIndentation();

        IXmlSitemapFeedBuilderFacade WithIndentationCharacters(string indentChars); 

        IXmlSitemapFeedBuilderFacade WithNewLineHandling(NewLineHandling newLineHandling); 

        IXmlSitemapFeedBuilderFacade WithNewLineCharacters(string newLineChars);

        IXmlSitemapFeedBuilderFacade WithXmlDeclaration();

        IXmlSitemapFeedBuilderFacade OmitXmlDeclaration();

        // From XmlWriterFactory

        //IXmlSitemapFeedBuilderFacade WithXmlWriterFactory(IXmlWriterFactory xmlWriterFactory);

        // From XmlSitemapPager

        IXmlSitemapFeedBuilderFacade WithMaximumPageSize(int maximumPageSize);

        // From XmlSitemapProviderStrategy

        // TODO: Ensure the factory is wrapped with the request caching decorator.
        //IXmlSitemapFeedBuilderFacade WithXmlSitemapProviderFactory(IXmlSitemapProviderFactory xmlSitemapProviderFactory);

        //// From AttributeAssemblyProvider
        //IXmlSitemapFeedBuilderFacade AddAssemblyForXmlSitemapProviderScan(string assemblyName);

        //IXmlSitemapFeedBuilderFacade RemoveAssemblyFromXmlSitemapProviderScan(string assemblyName);

        //IEnumerable<string> AssembliesToScanForXmlSitemapProvider { get; }

        // From ISpecializedContentXmlWriterFactoryStrategy
        IXmlSitemapFeedBuilderFacade AddNewsContent();

        IXmlSitemapFeedBuilderFacade AddMobileContent();

        IXmlSitemapFeedBuilderFacade AddImageContent();

        IXmlSitemapFeedBuilderFacade AddVideoContent();

        IXmlSitemapFeedBuilderFacade AddSpecializedContentXmlWriterFactory(ISpecializedContentXmlWriterFactory specializedContentXmlWriterFactory);

        IXmlSitemapFeedBuilderFacade RemoveNewsContent();

        IXmlSitemapFeedBuilderFacade RemoveMobileContent();

        IXmlSitemapFeedBuilderFacade RemoveImageContent();

        IXmlSitemapFeedBuilderFacade RemoveVideoContent();

        IXmlSitemapFeedBuilderFacade RemoveSpecializedContentXmlWriterFactory(Type specializedContentType);

        IXmlSitemapFeedBuilderFacade ClearSpecializedContent();

        IEnumerable<ISpecializedContentXmlWriterFactory> SpecializedContentXmlWriterFactories { get; }

        IXmlSitemapFeed Create();
    }

    //private class foo
    //{
    //    void whatever()
    //    {
    //        var x = new 
    //    }
    //}
}
