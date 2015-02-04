#if MVC2
using System;
using System.Net;
using System.Web.Mvc;

namespace MvcSiteMapProvider.Web.Mvc
{
    internal class HttpNotFoundResult : ActionResult
    {
        public override void ExecuteResult(ControllerContext context)
        {
            if (context == null)
            {
                throw new ArgumentNullException("context");
            }

            // 404 is the HTTP status code for resource not found
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
        }
    }
}
#endif