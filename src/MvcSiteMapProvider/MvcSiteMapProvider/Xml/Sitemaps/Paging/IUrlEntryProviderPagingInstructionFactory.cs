using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Xml.Sitemaps.Paging
{
    public interface IUrlEntryProviderPagingInstructionFactory
    {
        IUrlEntryProviderPagingInstruction Create(int skip, int take, IUrlEntryProvider urlEntryProvider);
        IUrlEntryProviderPagingInstruction Create(IUrlEntryProvider urlEntryProvider);
    }
}
