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
            IAssemblyProviderFactory assemblyProviderFactory
            )
        {
            if (assemblyProviderFactory == null)
                throw new ArgumentNullException("assemblyProviderFactory");

            this.assemblyProviderFactory = assemblyProviderFactory;

            // TODO: Work out how to load the internal providers (including dependencies)
            // and optionally exclude them. They should always be loaded by the XmlSitemapProviderFactory.


            // Load all of the provider types at startup so 
            // reflection is not used at runtime.
            this.providerTypes = this.GetProviderTypes(assemblyProviderFactory);
        }
        private readonly IAssemblyProviderFactory assemblyProviderFactory;
        private readonly IEnumerable<Type> providerTypes;

        public virtual IEnumerable<Type> GetTypes(string feedName)
        {
            return this.providerTypes
                .Where(providerType => this.IncludeInFeed(providerType, feedName))
                .OrderByDescending(providerType => this.GetPriority(providerType, feedName))
                .ThenBy(providerType => providerType.ShortAssemblyQualifiedName());
        }

        protected virtual bool IncludeInFeed(Type providerType, string feedName)
        {
            var attributeType = typeof(IncludeInFeedAttribute);

            // Special case - include in all feeds if no filter attributes
            // are defined on the provider.
            if (!Attribute.IsDefined(providerType, attributeType))
            {
                return true;
            }

            return Attribute.GetCustomAttributes(providerType, attributeType)
                .Where(attribute => ((IncludeInFeedAttribute)attribute).FeedName.Equals(feedName))
                .Any();
        }

        protected virtual int GetPriority(Type providerType, string feedName)
        {
            // Get the attribute that matches the feed (if it exists)
            FeedPriorityAttribute priorityAttribute = (FeedPriorityAttribute)providerType
                .GetCustomAttributes(typeof(IncludeInFeedAttribute), false)
                .Where(attribute => ((IncludeInFeedAttribute)attribute).FeedName.Equals(feedName))
                .FirstOrDefault();

            if (priorityAttribute == null)
            {
                // Try to get the attribute that matches all feeds (if it exists)
                priorityAttribute = (FeedPriorityAttribute)providerType
                    .GetCustomAttributes(typeof(FeedPriorityAttribute), false)
                    .FirstOrDefault();
            }

            if (priorityAttribute != null)
            {
                return priorityAttribute.Priority;
            }

            // Default priority if no attribute defined
            return FeedPriorityAttribute.DefaultPriority;
        }

        protected virtual IEnumerable<Type> GetProviderTypes(IAssemblyProviderFactory assemblyProviderFactory)
        {
            var assemblyProvider = assemblyProviderFactory.Create();
            try
            {
                return assemblyProvider.GetAssemblies()
                    .SelectMany(assembly => assembly.GetTypes())
                    .Where(type => type.GetInterfaces().Contains(typeof(IXmlSitemapProvider)))
                    .Where(type => !type.IsInterface)
                    .Where(type => !type.IsAbstract);
            }
            finally
            {
                assemblyProviderFactory.Release(assemblyProvider);
            }
        }
    }
}
