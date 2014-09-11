using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    public interface IXmlSitemapProvider
    {
        // Total record count of all potential pages
        int GetTotalRecordCount(string feedName);
        DateTime GetLastModifiedDate(string feedName, int skip, int take);
        //IXmlSitemapRecordInfo GetRecordInfo(IXmlSitemapRecordInfoHelper helper);
        void GetUrlEntries(IUrlEntryHelper helper);
        // TODO: Do we need to return the feedback to the provider or can some messaging system be implemented?
    }
}
