#if MVC6
using System;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Routing;

namespace MvcSiteMapProvider.Web.Routing
{
    public class RequestContext
    {
        public RequestContext(ActionContext actionContext)
        {
            if (actionContext == null)
                throw new ArgumentNullException("actionContext");

            this.ActionContext = actionContext;
            this.HttpContext = new HttpContextWrapper(actionContext.HttpContext);
            this.RouteData = actionContext.RouteData;
        }
        
        public virtual ActionContext ActionContext { get; set; }

        public virtual HttpContextBase HttpContext { get; set; }

        public virtual RouteData RouteData { get; set; }
    }
}
#endif
