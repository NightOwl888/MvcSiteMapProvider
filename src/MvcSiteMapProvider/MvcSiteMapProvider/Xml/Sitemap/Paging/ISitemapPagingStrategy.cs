using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Xml.Sitemap.Paging
{
    public interface ISitemapPagingStrategy
    {
        int MaximumPageSize { get; set; }
        IEnumerable<int> GetPageNumbers();
        IEnumerable<IPagingInstruction> GetPagingInstructions(int page);
    }
}
