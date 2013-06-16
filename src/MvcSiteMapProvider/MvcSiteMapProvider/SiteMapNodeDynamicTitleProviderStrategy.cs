using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider
{
    public class SiteMapNodeDynamicTitleProviderStrategy
        : ISiteMapNodeDynamicTitleProviderStrategy
    {
        public SiteMapNodeDynamicTitleProviderStrategy(ISiteMapNodeDynamicTitleProvider[] siteMapNodeDynamicTitleProviders)
        {
            if (siteMapNodeDynamicTitleProviders == null)
                throw new ArgumentNullException("siteMapNodeDynamicTitleProviders");
            this.siteMapNodeDynamicTitleProviders = siteMapNodeDynamicTitleProviders;
        }

        private readonly ISiteMapNodeDynamicTitleProvider[] siteMapNodeDynamicTitleProviders;



        #region ISiteMapNodeDynamicTitleProviderStrategy Members

        public ISiteMapNodeDynamicTitleProvider GetProvider(string providerName)
        {
            ISiteMapNodeDynamicTitleProvider provider = null;
            if (!string.IsNullOrEmpty(providerName))
            {
                provider = siteMapNodeDynamicTitleProviders.FirstOrDefault(x => x.AppliesTo(providerName));
                if (provider == null)
                {
                    throw new MvcSiteMapException(String.Format(Resources.Messages.NamedSiteMapNodeDynamicTitleProviderNotFound, providerName));
                }
            }
            return provider;
        }

        public string GetTitle(string providerName, ISiteMapNode node, IDictionary<string, object> sourceMetadata)
        {
            var provider = GetProvider(providerName);
            if (provider == null) return string.Empty; // TODO: Try to pull the original title before localization from the node.
            return provider.GetTitle(node, sourceMetadata);
        }

        #endregion
    }
}
