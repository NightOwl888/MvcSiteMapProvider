using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Xml.Sitemaps.Paging
{
    public interface IUrlEntryProviderPagingInstruction
    {
        int Skip { get; }
        int Take { get; }
        IUrlEntryProvider UrlEntryProvider { get; }
    }
}
