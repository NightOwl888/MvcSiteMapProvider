using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider
{
    public interface ISiteMapNodeDynamicTitleProviderStrategy
    {
        ISiteMapNodeDynamicTitleProvider GetProvider(string providerName);
        string GetTitle(string providerName, ISiteMapNode node, IDictionary<string, object> sourceMetadata);
    }
}
