using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MvcSiteMapProvider.Web.Mvc
{
    public interface IXmlSitemapFeedResultFactory
    {
        ActionResult Create(int page);
        ActionResult Create(int page, string feedName);
    }
}
