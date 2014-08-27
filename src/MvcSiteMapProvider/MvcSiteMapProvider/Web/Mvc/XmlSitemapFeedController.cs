using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MvcSiteMapProvider.Web.Mvc
{

#if !MVC2
#if !MVC3
    [AllowAnonymous]
#endif
#endif
    public class XmlSitemapFeedController
        : Controller
    {
        public XmlSitemapFeedController(
            IXmlSitemapFeedResultFactory xmlSitemapFeedResultFactory
            )
        {
            if (xmlSitemapFeedResultFactory == null)
                throw new ArgumentNullException("xmlSitemapFeedResultFactory");
            this.xmlSitemapFeedResultFactory = xmlSitemapFeedResultFactory;
        }
        private readonly IXmlSitemapFeedResultFactory xmlSitemapFeedResultFactory;

        public ActionResult Index(int page = 0, string feedName = "")
        {
            var name = string.IsNullOrEmpty(feedName) ? "default" : feedName;
            return this.xmlSitemapFeedResultFactory.Create(page, name);
        }
    }
}