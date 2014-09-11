using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    public interface IXmlSitemapProviderStrategy
    {
        IEnumerable<IXmlSitemapProvider> GetProviders(string feedName);
        void ReleaseProviders(IEnumerable<IXmlSitemapProvider> providers);
    }
}
