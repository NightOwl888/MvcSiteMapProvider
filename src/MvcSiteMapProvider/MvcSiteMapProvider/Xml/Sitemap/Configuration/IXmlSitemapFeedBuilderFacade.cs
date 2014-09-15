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

        //IXmlSitemapFeedBuilderFacade OmitIndentation(); // No point

        IXmlSitemapFeedBuilderFacade WithIndentationCharacters(string indentChars); 

        IXmlSitemapFeedBuilderFacade WithNewLineHandling(NewLineHandling newLineHandling); 

        IXmlSitemapFeedBuilderFacade WithNewLineCharacters(string newLineChars);

        //IXmlSitemapFeedBuilderFacade WithXmlDeclaration(); // No point

        IXmlSitemapFeedBuilderFacade OmitXmlDeclaration();

        // From XmlWriterFactory

        //IXmlSitemapFeedBuilderFacade WithXmlWriterFactory(IXmlWriterFactory xmlWriterFactory);

        // From XmlSitemapPager

        IXmlSitemapFeedBuilderFacade WithMaximumPageSize(int maximumPageSize);

        IXmlSitemapFeedBuilderFacade OmitRequestCaching();

        IXmlSitemapFeedBuilderFacade OmitUrlsWithoutMatchingContent();

        // From XmlSitemapProviderStrategy

        // TODO: Ensure the factory is wrapped with the request caching decorator.
        //IXmlSitemapFeedBuilderFacade WithXmlSitemapProviderFactory(IXmlSitemapProviderFactory xmlSitemapProviderFactory);

        //// From AttributeAssemblyProvider
        //IXmlSitemapFeedBuilderFacade AddAssemblyForXmlSitemapProviderScan(string assemblyName);

        //IXmlSitemapFeedBuilderFacade RemoveAssemblyFromXmlSitemapProviderScan(string assemblyName);

        //IEnumerable<string> AssembliesToScanForXmlSitemapProvider { get; }

        // From ISpecializedContentXmlWriterFactoryStrategy
        IXmlSitemapFeedBuilderFacade WithNewsContent();

        IXmlSitemapFeedBuilderFacade WithMobileContent();

        IXmlSitemapFeedBuilderFacade WithImageContent();

        IXmlSitemapFeedBuilderFacade WithVideoContent();

        IXmlSitemapFeedBuilderFacade WithCustomContent(ISpecializedContentXmlWriterFactory specializedContentXmlWriterFactory);

        //IXmlSitemapFeedBuilderFacade RemoveNewsContent();

        //IXmlSitemapFeedBuilderFacade RemoveMobileContent();

        //IXmlSitemapFeedBuilderFacade RemoveImageContent();

        //IXmlSitemapFeedBuilderFacade RemoveVideoContent();

        //IXmlSitemapFeedBuilderFacade RemoveSpecializedContentXmlWriterFactory(Type specializedContentType);

        //IXmlSitemapFeedBuilderFacade ClearSpecializedContent();

        IEnumerable<ISpecializedContentXmlWriterFactory> SpecializedContentXmlWriterFactories { get; }

        IXmlSitemapFeedBuilderFacade WithXmlSitemapProviderFactory(IXmlSitemapProviderFactory xmlSitemapProviderFactory);

        IXmlSitemapProviderFactory XmlSitemapProviderFactory { get; }

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
