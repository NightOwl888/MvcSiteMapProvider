using System;
using System.IO;
#if MVC6
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Routing;
using MvcSiteMapProvider.Web.Routing;
#else
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
#endif
using MvcSiteMapProvider.Caching;

namespace MvcSiteMapProvider.Web.Mvc
{
    /// <summary>
    /// Contract for an abstract factory that provides context-related instances at runtime.
    /// </summary>
    public interface IMvcContextFactory
    {
        HttpContextBase CreateHttpContext();
        HttpContextBase CreateHttpContext(ISiteMapNode node, System.Uri uri, TextWriter writer);
        RequestContext CreateRequestContext(ISiteMapNode node, RouteData routeData);
        RequestContext CreateRequestContext();
        RequestContext CreateRequestContext(HttpContextBase httpContext);
        RequestContext CreateRequestContext(HttpContextBase httpContext, RouteData routeData);
#if !MVC6
        ControllerContext CreateControllerContext(RequestContext requestContext, ControllerBase controller);
#endif
        IRequestCache GetRequestCache();
        RouteCollection GetRoutes();
        IUrlHelper CreateUrlHelper(RequestContext requestContext);
        IUrlHelper CreateUrlHelper();
#if !MVC6
        AuthorizationContext CreateAuthorizationContext(ControllerContext controllerContext, ActionDescriptor actionDescriptor);
#endif
    }
}
