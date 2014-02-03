//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web.Mvc;
//using MvcSiteMapProvider.Loader;

//namespace MvcSiteMapProvider.Web.Mvc.Filters
//{
//    public class SiteMapDereferenceAttribute
//        : ActionFilterAttribute
//    {
//        public SiteMapDereferenceAttribute(
//            ISiteMapSpooler siteMapSpooler
//            )
//        {
//            if (siteMapSpooler == null)
//                throw new ArgumentNullException("siteMapSpooler");

//            this.siteMapSpooler = siteMapSpooler;
//        }
//        private readonly ISiteMapSpooler siteMapSpooler;

//        public override void OnActionExecuting(ActionExecutingContext filterContext)
//        {
//            base.OnActionExecuting(filterContext);
//        }

//        public override void OnResultExecuted(ResultExecutedContext filterContext)
//        {
//            base.OnResultExecuted(filterContext);

//            this.siteMapSpooler.Dereference();
//        }
//    }
//}
