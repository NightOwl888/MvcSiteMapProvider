using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    public class XmlSitemapPageInfoFactory
        : IXmlSitemapPageInfoFactory
    {
        public IXmlSitemapPageInfo Create(int page, DateTime lastModifiedDate)
        {
            return new XmlSitemapPageInfo(page, lastModifiedDate);
        }
    }
}
