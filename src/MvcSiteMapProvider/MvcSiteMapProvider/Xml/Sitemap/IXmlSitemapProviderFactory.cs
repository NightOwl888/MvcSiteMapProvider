using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    public interface IXmlSitemapProviderFactory
    {
        IXmlSitemapProvider Create(Type providerType);
        void Release(IXmlSitemapProvider xmlSitemapProvider);
    }
}
