using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    public class XmlSitemapRecordInfoFactory
        : IXmlSitemapRecordInfoFactory
    {
        public IXmlSitemapRecordInfo Create(int totalRecordCount)
        {
            return new XmlSitemapRecordInfo(totalRecordCount);
        }

        public IXmlSitemapRecordInfo Create(int totalRecordCount, DateTime lastModifiedDate)
        {
            return new XmlSitemapRecordInfo(totalRecordCount, lastModifiedDate);
        }
    }
}
