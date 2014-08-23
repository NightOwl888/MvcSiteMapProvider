using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Xml.Sitemap.Paging
{
    public interface IPagingInstructionFactory
    {
        IPagingInstruction Create(int skip, int take, IUrlEntryProvider urlEntryProvider);
        IPagingInstruction Create(IUrlEntryProvider urlEntryProvider);
    }
}
