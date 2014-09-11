using System;
using System.Collections.Generic;
using System.Linq;
using MvcSiteMapProvider.Reflection;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    /// <summary>
    /// Gets all of the types that implement <see cref="T:MvcSiteMapProvider.Xml.Sitemap.IXmlSitemapProvider"/>
    /// that either do not implement the <see cref="T:MvcSiteMapProvider.Xml.Sitemap.XmlSitemapFeedFilterAttribute"/> 
    /// or contain one with a matching feed name.
    /// </summary>
    public class XmlSitemapProviderTypeStrategy
        : IXmlSitemapProviderTypeStrategy
    {
        public XmlSitemapProviderTypeStrategy(
            IAttributeAssemblyProvider assemblyProvider
            )
        {
            if (assemblyProvider == null)
                throw new ArgumentNullException("assemblyProvider");

            this.assemblyProvider = assemblyProvider;
        }
        private readonly IAttributeAssemblyProvider assemblyProvider;

        public virtual IEnumerable<Type> GetTypes(string feedName)
        {
            var providerInterfaceType = typeof(IXmlSitemapProvider);

            return this.assemblyProvider.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => providerInterfaceType.IsAssignableFrom(type) && !type.IsInterface)
                .Where(type => this.IncludeInFeed(type, feedName));
        }

        protected virtual bool IncludeInFeed(Type providerType, string feedName)
        {
            var attributeType = typeof(XmlSitemapFeedFilterAttribute);

            // Special case - include in all feeds if no filter attributes
            // are defined on the provider.
            if (!Attribute.IsDefined(providerType, attributeType))
            {
                return true;
            }

            return Attribute.GetCustomAttributes(providerType, attributeType)
                .Select(attribute => ((XmlSitemapFeedFilterAttribute)attribute).FeedName.Equals(feedName))
                .Any();
        }
    }
}
