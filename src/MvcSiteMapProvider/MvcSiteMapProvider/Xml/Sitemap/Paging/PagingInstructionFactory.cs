using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Xml.Sitemap.Paging
{
    public class PagingInstructionFactory
        : IPagingInstructionFactory
    {
        public IPagingInstruction Create(int skip, int take, IXmlSitemapProvider xmlSitemapProvider)
        {
            return new PagingInstruction(skip, take, xmlSitemapProvider);
        }

        public IPagingInstruction Create(IXmlSitemapProvider xmlSitemapProvider)
        {
            return this.Create(0, int.MaxValue, xmlSitemapProvider);
        }
    }
}
