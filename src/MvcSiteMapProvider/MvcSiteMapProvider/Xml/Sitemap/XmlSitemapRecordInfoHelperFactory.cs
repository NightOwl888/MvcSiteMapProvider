using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    public class XmlSitemapRecordInfoHelperFactory
        : IXmlSitemapRecordInfoHelperFactory
    {
        public XmlSitemapRecordInfoHelperFactory(
            IXmlSitemapRecordInfoFactory xmlSitemapRecordInfoFactory
            )
        {
            if (xmlSitemapRecordInfoFactory == null)
                throw new ArgumentNullException("xmlSitemapRecordInfoFactory");

            this.xmlSitemapRecordInfoFactory = xmlSitemapRecordInfoFactory;
        }
        private readonly IXmlSitemapRecordInfoFactory xmlSitemapRecordInfoFactory;

        public IXmlSitemapRecordInfoHelper Create(string feedName)
        {
            return new XmlSitemapRecordInfoHelper(feedName, this.xmlSitemapRecordInfoFactory);
        }
    }
}
