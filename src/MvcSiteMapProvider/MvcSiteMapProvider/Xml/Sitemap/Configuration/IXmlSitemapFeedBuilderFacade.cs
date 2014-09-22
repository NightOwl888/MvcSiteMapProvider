using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using MvcSiteMapProvider.ComponentModel;
using MvcSiteMapProvider.Reflection;
using MvcSiteMapProvider.Xml.Sitemap.Specialized;

namespace MvcSiteMapProvider.Xml.Sitemap.Configuration
{
    public interface IXmlSitemapFeedBuilderFacade
        : IFluentInterface
    {
        // From XmlWriterSettings

        IXmlSitemapFeedBuilderFacade WithEncoding(Encoding encoding);

        IXmlSitemapFeedBuilderFacade WithIndentation();

        IXmlSitemapFeedBuilderFacade WithIndentationCharacters(string indentChars); 

        IXmlSitemapFeedBuilderFacade WithNewLineHandling(NewLineHandling newLineHandling); 

        IXmlSitemapFeedBuilderFacade WithNewLineCharacters(string newLineChars);

        IXmlSitemapFeedBuilderFacade OmitXmlDeclaration();

        // From XmlSitemapPager

        IXmlSitemapFeedBuilderFacade WithMaximumPageSize(int maximumPageSize);

        IXmlSitemapFeedBuilderFacade OmitRequestCaching();

        // From XmlSitemapWriterFactory

        IXmlSitemapFeedBuilderFacade OmitUrlsWithoutMatchingContent();

        // From ISpecializedContentXmlWriterFactoryStrategy
        IXmlSitemapFeedBuilderFacade WithNewsContent();

        IXmlSitemapFeedBuilderFacade WithMobileContent();

        IXmlSitemapFeedBuilderFacade WithImageContent();

        IXmlSitemapFeedBuilderFacade WithVideoContent();

        IXmlSitemapFeedBuilderFacade WithCustomContent(ISpecializedContentXmlWriterFactory specializedContentXmlWriterFactory);

        IEnumerable<ISpecializedContentXmlWriterFactory> SpecializedContentXmlWriterFactories { get; }


        // TODO: Add this
        //IXmlSitemapFeedBuilderFacade WithContent(Func<IXmlSitemap_SetupContent_Starter, IXmlSitemap_SetupContent_Finalizer> expression);



        IXmlSitemapFeedBuilderFacade WithXmlSitemapUrlResolverFactory(IXmlSitemapUrlResolverFactory xmlSitemapUrlResolverFactory);
        //IXmlSitemapFeedBuilderFacade SetupXmlSitemapUrlResolverFactory(IXmlSitemapUrlResolverFactory xmlSitemapUrlResolverFactory);

        IXmlSitemapUrlResolverFactory XmlSitemapUrlResolverFactory { get; }

        IXmlSitemapFeedBuilderFacade WithXmlSitemapProviderFactory(IXmlSitemapProviderFactory xmlSitemapProviderFactory);

        IXmlSitemapProviderFactory XmlSitemapProviderFactory { get; }

        IXmlSitemapFeedBuilderFacade WithAssemblyProviderFactory(IAssemblyProviderFactory assemblyProviderFactory);

        IAssemblyProviderFactory AssemblyProviderFactory { get; }

        //IXmlSitemapFeedBuilderFacade WithXmlSitemapFeedPageNameProvider(IXmlSitemapFeedPageNameProvider xmlSitemapFeedPageNameProvider);

        IXmlSitemapFeedPageNameProvider XmlSitemapFeedPageNameProvider { get; }

        string FeedName { get; }

        int MaximumPageSize { get; }

        bool RequestCachingOmitted { get; }

        bool UrlsWithNonMatchingContentOmitted { get; }

        XmlWriterSettings XmlWriterSettings { get; }


        IXmlSitemapFeed Create();
    }
}
