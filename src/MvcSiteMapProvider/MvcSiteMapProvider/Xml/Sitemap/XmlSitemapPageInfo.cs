using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    public class XmlSitemapPageInfo
        : IXmlSitemapPageInfo
    {
        public XmlSitemapPageInfo(
            int page,
            DateTime lastModifiedDate
            )
        {
            if (page < 0)
                throw new ArgumentOutOfRangeException("page");

            this.page = page;
            this.lastModifiedDate = lastModifiedDate;
        }
        private readonly int page;
        private readonly DateTime lastModifiedDate;

        public int Page
        {
            get { return this.page; }
        }

        public DateTime LastModifiedDate
        {
            get { return this.lastModifiedDate; }
        }
    }
}
