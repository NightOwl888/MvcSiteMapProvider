//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace MvcSiteMapProvider.Xml.Sitemap
//{
//    public class XmlSitemapRecordInfoHelper
//        : IXmlSitemapRecordInfoHelper
//    {
//        public XmlSitemapRecordInfoHelper(
//            string feedName,
//            IXmlSitemapRecordInfoFactory xmlSitemapRecordInfoFactory
//            )
//        {
//            if (string.IsNullOrEmpty(feedName))
//                throw new ArgumentNullException("feedName");
//            if (xmlSitemapRecordInfoFactory == null)
//                throw new ArgumentNullException("xmlSitemapRecordInfoFactory");

//            this.feedName = feedName;
//            this.xmlSitemapRecordInfoFactory = xmlSitemapRecordInfoFactory;
//        }
//        private readonly string feedName;
//        private readonly IXmlSitemapRecordInfoFactory xmlSitemapRecordInfoFactory;

//        public string FeedName
//        {
//            get { return this.feedName; }
//        }

//        public IXmlSitemapRecordInfo CreateRecordInfo(int totalRecordCount)
//        {
//            return this.xmlSitemapRecordInfoFactory.Create(totalRecordCount);
//        }

//        public IXmlSitemapRecordInfo CreateRecordInfo(int totalRecordCount, DateTime lastModifiedDate)
//        {
//            return this.xmlSitemapRecordInfoFactory.Create(totalRecordCount, lastModifiedDate);
//        }
//    }
//}
