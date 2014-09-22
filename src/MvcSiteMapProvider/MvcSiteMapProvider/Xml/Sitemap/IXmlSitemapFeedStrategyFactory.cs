using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    public interface IXmlSitemapFeedStrategyFactory
    {
        IXmlSitemapFeedStrategy Create();
        void Release(IXmlSitemapFeedStrategy xmlSitemapFeedStrategy);
    }
}
