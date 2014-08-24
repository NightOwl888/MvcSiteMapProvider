using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using MvcSiteMapProvider.Xml.Sitemap.Paging;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    public interface ISitemapPageWriter
    {
        //string FirstPageNameTemplate { get; set; }
        //string PageNameTemplate { get; set; }
        //string BaseUrl { get; set; }
        //IEnumerable<int> GetPageNumbers();
        //bool WritePage(int page, XmlWriter writer);
        void WritePage(XmlWriter writer, IEnumerable<IPagingInstruction> pagingInstructions);
    }
}
