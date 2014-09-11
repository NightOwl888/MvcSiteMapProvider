using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Xml.Sitemap.Paging
{
    public interface IXmlSitemapPager
    {
        int MaximumPageSize { get; set; }
        IXmlSitemapPageData GetPageData(IEnumerable<IXmlSitemapProvider> providers, string feedName);
        IEnumerable<IPagingInstruction> GetPagingInstructions(IEnumerable<IXmlSitemapProvider> providers, string feedName, int page);
    }
}
