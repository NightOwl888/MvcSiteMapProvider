using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Xml.Sitemap.Paging
{
    public class PagingInstructionFactory
        : IPagingInstructionFactory
    {
        public IPagingInstruction Create(int skip, int take, IUrlEntryProvider urlEntryProvider)
        {
            return new PagingInstruction(skip, take, urlEntryProvider);
        }

        public IPagingInstruction Create(IUrlEntryProvider urlEntryProvider)
        {
            return this.Create(0, int.MaxValue, urlEntryProvider);
        }
    }
}
