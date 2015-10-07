//using System;
//using System.IO;
//#if MVC6
//using Microsoft.AspNet.Http;
//using Microsoft.AspNet.Mvc;
//using Microsoft.AspNet.Routing;
//#else
//using System.Web;
//using System.Web.Mvc;
//using System.Web.Routing;
//#endif
//using MvcSiteMapProvider.Caching;

//namespace MvcSiteMapProvider.Web.Mvc
//{
//    /// <summary>
//    /// Contract for an abstract factory that provides context-related instances at runtime.
//    /// </summary>
//    public interface IMvcContextFactory
//    {
//#if MVC6
//        HttpContext CreateHttpContext();
//        HttpContext CreateHttpContext(ISiteMapNode node, Uri uri, TextWriter writer);
//#else
//        HttpContextBase CreateHttpContext();
//        HttpContextBase CreateHttpContext(ISiteMapNode node, Uri uri, TextWriter writer);
//        RequestContext CreateRequestContext(ISiteMapNode node, RouteData routeData);
//        RequestContext CreateRequestContext();
//        RequestContext CreateRequestContext(HttpContextBase httpContext);
//        RequestContext CreateRequestContext(HttpContextBase httpContext, RouteData routeData);
//        ControllerContext CreateControllerContext(RequestContext requestContext, ControllerBase controller);
//#endif
//        IRequestCache GetRequestCache();
//        RouteCollection GetRoutes();
//        IUrlHelper CreateUrlHelper(RequestContext requestContext);
//        IUrlHelper CreateUrlHelper();
//        AuthorizationContext CreateAuthorizationContext(ControllerContext controllerContext, ActionDescriptor actionDescriptor);
//    }
//}
