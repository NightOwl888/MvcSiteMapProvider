using System;
using System.Collections.Generic;
using MvcSiteMapProvider.Reflection;

namespace MvcSiteMapProvider
{
    public abstract class SiteMapNodeDynamicTitleProviderBase
        : ISiteMapNodeDynamicTitleProvider
    {
        #region ISiteMapNodeDynamicTitleProvider Members

        public abstract string GetTitle(ISiteMapNode node, IDictionary<string, object> sourceMetadata);

        public bool AppliesTo(string providerName)
        {
            return this.GetType().ShortAssemblyQualifiedName().Equals(providerName, StringComparison.InvariantCulture);
        }

        #endregion
    }
}
