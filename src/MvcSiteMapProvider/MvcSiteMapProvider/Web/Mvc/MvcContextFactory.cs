using System;
using System.IO;
#if MVC6
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Routing;
using MvcSiteMapProvider.Web.Routing;
#else
using System.Web;
using System.Web.UI;
using System.Web.Mvc;
using System.Web.Routing;
#endif
using MvcSiteMapProvider.Caching;

namespace MvcSiteMapProvider.Web.Mvc
{
    /// <summary>
    /// An abstract factory that can be used to create new instances of MVC
    /// context-related instances at runtime.
    /// </summary>
    public class MvcContextFactory
        : IMvcContextFactory
    {
#if MVC6
        private readonly Func<ActionContext> getActionContextMethod;

        public MvcContextFactory(Func<ActionContext> getActionContextMethod)
        {
            if (getActionContextMethod == null)
                throw new ArgumentNullException("getActionContextMethod");

            this.getActionContextMethod = getActionContextMethod;
        }
#endif

        #region IMvcContextFactory Members

#if MVC6

        public HttpContextBase CreateHttpContext()
        {
            var actionContext = getActionContextMethod();
            return new SiteMapHttpContext(actionContext, null);
        }

        public HttpContextBase CreateHttpContext(ISiteMapNode node, System.Uri uri, TextWriter writer)
        {
            throw new NotImplementedException();
        }

        public RequestContext CreateRequestContext()
        {
            throw new NotImplementedException();
        }

        public RequestContext CreateRequestContext(HttpContextBase httpContext)
        {
            throw new NotImplementedException();
        }

        public RequestContext CreateRequestContext(HttpContextBase httpContext, RouteData routeData)
        {
            throw new NotImplementedException();
        }

        public RequestContext CreateRequestContext(ISiteMapNode node, RouteData routeData)
        {
            throw new NotImplementedException();
        }

        public IUrlHelper CreateUrlHelper()
        {
            throw new NotImplementedException();
        }

        public IUrlHelper CreateUrlHelper(RequestContext requestContext)
        {
            throw new NotImplementedException();
        }

        public IRequestCache GetRequestCache()
        {
            throw new NotImplementedException();
        }

        public RouteCollection GetRoutes()
        {
            throw new NotImplementedException();
        }

#else

        public virtual HttpContextBase CreateHttpContext()
        {
            return CreateHttpContext(null);
        }

        protected virtual HttpContextBase CreateHttpContext(ISiteMapNode node)
        {
            return new SiteMapHttpContext(HttpContext.Current, node);
        }

        public virtual HttpContextBase CreateHttpContext(ISiteMapNode node, System.Uri uri, TextWriter writer)
        {
            if (uri == null)
                throw new ArgumentNullException("uri");
            if (writer == null)
                throw new ArgumentNullException("writer");

            var request = new HttpRequest(string.Empty, uri.ToString(), uri.Query);
            var response = new HttpResponse(writer);
            var httpContext = new HttpContext(request, response);
            return new SiteMapHttpContext(httpContext, node);
        }

        public virtual RequestContext CreateRequestContext(ISiteMapNode node, RouteData routeData)
        {
            var httpContext = this.CreateHttpContext(node);
            return new RequestContext(httpContext, routeData);
        }

        public virtual RequestContext CreateRequestContext()
        {
            var httpContext = this.CreateHttpContext();
            if (httpContext.Handler is MvcHandler)
                return ((MvcHandler)httpContext.Handler).RequestContext;
#if !NET35
            else if (httpContext.Handler is Page) // Fixes #15 for interop with ASP.NET Webforms
                return new RequestContext(httpContext, ((Page)HttpContext.Current.Handler).RouteData);
#endif
            else
                return new RequestContext(httpContext, new RouteData());
        }

        public virtual RequestContext CreateRequestContext(HttpContextBase httpContext)
        {
            return new RequestContext(httpContext, new RouteData());
        }

        public virtual RequestContext CreateRequestContext(HttpContextBase httpContext, RouteData routeData)
        {
            return new RequestContext(httpContext, routeData);
        }

        public virtual ControllerContext CreateControllerContext(RequestContext requestContext, ControllerBase controller)
        {
            if (requestContext == null)
                throw new ArgumentNullException("requestContext");
            if (controller == null)
                throw new ArgumentNullException("controller");

            var result = new ControllerContext(requestContext, controller);

            // Fixes #271 - set controller's ControllerContext property for MVC
            result.Controller.ControllerContext = result;

            return result;
        }

        public virtual IRequestCache GetRequestCache()
        {
            return new RequestCache(this);
        }

        public virtual RouteCollection GetRoutes()
        {
            return RouteTable.Routes;
        }

        public virtual IUrlHelper CreateUrlHelper(RequestContext requestContext)
        {
            return new UrlHelperAdapter(requestContext, this.GetRoutes());
        }

        public virtual IUrlHelper CreateUrlHelper()
        {
            var requestContext = this.CreateRequestContext();
            return new UrlHelperAdapter(requestContext, this.GetRoutes());
        }

        public virtual AuthorizationContext CreateAuthorizationContext(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            if (controllerContext == null)
                throw new ArgumentNullException("controllerContext");
            if (actionDescriptor == null)
                throw new ArgumentNullException("actionDescriptor");

            return new AuthorizationContext(controllerContext, actionDescriptor);
        }
#endif

        #endregion
    }
}
