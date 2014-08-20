using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Xml.Sitemaps.Paging
{
    public class UrlEntryProviderPagingInstructionFactory
        : IUrlEntryProviderPagingInstructionFactory
    {
        public IUrlEntryProviderPagingInstruction Create(int skip, int take, IUrlEntryProvider urlEntryProvider)
        {
            return new UrlEntryProviderPagingInstruction(skip, take, urlEntryProvider);
        }

        public IUrlEntryProviderPagingInstruction Create(IUrlEntryProvider urlEntryProvider)
        {
            return this.Create(0, int.MaxValue, urlEntryProvider);
        }
    }
}
