using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    public interface IXmlSitemapRecordInfoHelper
    {
        string FeedName { get; }
        IXmlSitemapRecordInfo CreateRecordInfo(int totalRecordCount);
        IXmlSitemapRecordInfo CreateRecordInfo(int totalRecordCount, DateTime lastModifiedDate);
    }
}
