using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using MvcSiteMapProvider.Globalization;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized
{
    public class SpecializedContentWriter
        : ISpecializedContentWriter
    {
        public SpecializedContentWriter(
            ISpecializedContentXmlWriterFactoryStrategy specializedContentXmlWriterFactoryStrategy,
            IXmlSitemapUrlResolverFactory xmlSitemapUrlResolverFactory,
            ICultureContextFactory cultureContextFactory
            )
        {
            if (specializedContentXmlWriterFactoryStrategy == null)
                throw new ArgumentNullException("specializedContentXmlWriterFactoryStrategy");
            if (xmlSitemapUrlResolverFactory == null)
                throw new ArgumentNullException("xmlSitemapUrlResolverFactory");
            if (cultureContextFactory == null)
                throw new ArgumentNullException("cultureContextFactory");

            this.specializedContentXmlWriterFactoryStrategy = specializedContentXmlWriterFactoryStrategy;
            this.xmlSitemapUrlResolverFactory = xmlSitemapUrlResolverFactory;
            this.cultureContextFactory = cultureContextFactory;
        }
        private readonly ISpecializedContentXmlWriterFactoryStrategy specializedContentXmlWriterFactoryStrategy;
        private readonly IXmlSitemapUrlResolverFactory xmlSitemapUrlResolverFactory;
        private readonly ICultureContextFactory cultureContextFactory;

        public virtual void WriteNamespaces(XmlWriter writer)
        {
            // call upon registered child services to get namespaces
            var contentTypes = this.specializedContentXmlWriterFactoryStrategy.GetRegisteredContentTypes();

            foreach (var contentType in contentTypes)
            {
                var xmlWriterFactory = this.specializedContentXmlWriterFactoryStrategy.GetFactory(contentType);
                if (xmlWriterFactory != null)
                {
                    var xmlWriter = xmlWriterFactory.Create(writer);
                    try
                    {
                        xmlWriter.WriteNamespace();
                    }
                    finally
                    {
                        // Free up any unmanaged resources
                        xmlWriterFactory.Release(xmlWriter);
                    }
                }
            }
        }

        public virtual void WriteSpecializedContent(XmlWriter writer, IEnumerable<ISpecializedContent> specializedContent)
        {
            var groupedContent = this.GroupContent(specializedContent);

            // Create a url resolver instance
            var urlResolver = this.xmlSitemapUrlResolverFactory.Create();
            try
            {
                foreach (var group in groupedContent)
                {
                    this.WriteContent(writer, group, urlResolver); 
                }
            }
            finally
            {
                // Release unmanaged resources
                this.xmlSitemapUrlResolverFactory.Release(urlResolver);
            }
        }

        public virtual bool ContainsMatchingContentType(IEnumerable<ISpecializedContent> specializedContent)
        {
            var contentTypes = this.specializedContentXmlWriterFactoryStrategy.GetRegisteredContentTypes();

            return (from sc in specializedContent
                    let t = sc.GetType()
                    from i in t.GetInterfaces()
                    select i)
                   .Intersect(contentTypes)
                   .Any();
        }

        protected virtual void WriteContent(XmlWriter writer, ContentTypeToContentListRelation group, IXmlSitemapUrlResolver urlResolver)
        {
            // Strategy pattern to get the correct factory instance based on type.
            var xmlWriterFactory = this.specializedContentXmlWriterFactoryStrategy.GetFactory(group.ContentType);

            if (xmlWriterFactory != null)
            {
                // Create our specialized writer instance
                var xmlWriter = xmlWriterFactory.Create(writer);
                try
                {
                    // Use the invariant context to write the specialized content.
                    using (var cultureContext = this.cultureContextFactory.CreateInvariant())
                    {
                        foreach (var content in group.ContentList)
                        {
                            xmlWriter.WriteContent(content.Instance, urlResolver, cultureContext);
                        }
                    }
                }
                finally
                {
                    // Free up any unmanaged resources
                    xmlWriterFactory.Release(xmlWriter);
                }
            }
        }

        protected virtual IEnumerable<ContentTypeToContentListRelation> GroupContent(IEnumerable<ISpecializedContent> specializedContent)
        {
            var contentTypes = this.specializedContentXmlWriterFactoryStrategy.GetRegisteredContentTypes();

            // Order and group the content type the same way they are ordered in the SpecializedContentXmlWriterFactoryStrategy
            return from ct in contentTypes
                   join contents in
                        (from sc in specializedContent
                        let t = sc.GetType()
                        from i in t.GetInterfaces()
                        select new SpecializedContentToTypeToInterfaceTypeRelation(sc, t, i))
                   on ct equals contents.InterfaceType
                   group contents by ct into g
                   select new ContentTypeToContentListRelation(g.Key, g.Where(x => g.Key == x.InterfaceType));
        }

        protected class ContentTypeToContentListRelation
        {
            public ContentTypeToContentListRelation(
                Type contentType,
                IEnumerable<SpecializedContentToTypeToInterfaceTypeRelation> contentList)
            {
                this.ContentType = contentType;
                this.ContentList = contentList;
            }

            public Type ContentType { get; private set; }
            public IEnumerable<SpecializedContentToTypeToInterfaceTypeRelation> ContentList { get; private set; }
        }

        protected class SpecializedContentToTypeToInterfaceTypeRelation
        {
            public SpecializedContentToTypeToInterfaceTypeRelation(
                ISpecializedContent instance,
                Type specializedContentType,
                Type interfaceType)
            {
                this.Instance = instance;
                this.SpecializedContentType = specializedContentType;
                this.InterfaceType = interfaceType;
            }

            public ISpecializedContent Instance { get; private set; }
            public Type SpecializedContentType { get; private set; }
            public Type InterfaceType { get; private set; }
        }
    }
}
