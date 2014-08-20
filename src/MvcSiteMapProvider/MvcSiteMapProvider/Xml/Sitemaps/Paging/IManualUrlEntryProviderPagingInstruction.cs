using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Xml.Sitemaps.Paging
{
    public interface IManualUrlEntryProviderPagingInstruction
        : IUrlEntryProviderPagingInstruction
    {
        int Page { get; }
    }
}
