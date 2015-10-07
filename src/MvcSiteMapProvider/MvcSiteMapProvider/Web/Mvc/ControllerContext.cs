#if MVC6
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Routing;
using MvcSiteMapProvider.Web.Routing;

namespace MvcSiteMapProvider.Web.Mvc
{
    public class ControllerContext
    {
        //public ControllerContext(HttpContextBase httpContext, RouteData routeData, Controller controller)
        //    : this(new RequestContext(httpContext, routeData), controller)
        //{
        //}

        public ControllerContext(RequestContext requestContext, Controller controller)
        {
            if (requestContext == null)
            {
                throw new ArgumentNullException("requestContext");
            }
            if (controller == null)
            {
                throw new ArgumentNullException("controller");
            }

            this.RequestContext = requestContext;
            this.RouteData = requestContext.RouteData;
            this.HttpContext = requestContext.HttpContext;
            this.Controller = controller;
        }

        public virtual Controller Controller { get; set; }

        //public IDisplayMode DisplayMode
        //{
        //    get { return DisplayModeProvider.GetDisplayMode(HttpContext); }
        //    set { DisplayModeProvider.SetDisplayMode(HttpContext, value); }
        //}

        public virtual HttpContextBase HttpContext { get; set; }

        public RequestContext RequestContext { get; set; }

        public virtual RouteData RouteData { get; set; }
    }
}
#endif