using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Xml.Sitemaps.Paging
{
    public interface ISitemapsPagingStrategy
    {
        int MaximumPageSize { get; set; }
        IEnumerable<int> GetIndexPageNumbers();
        IEnumerable<IUrlEntryProviderPagingInstruction> GetPagingInstructions(int page);
    }
}
