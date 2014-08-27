using System;
using System.Web.Routing;
using MvcSiteMapProvider.Xml.Sitemap;

namespace MvcSiteMapProvider.Web.Routing
{
    public interface IXmlSitemapFeedRouteRegistrar
    {
        void RegisterRoutes(RouteCollection routes);
        void RegisterRoutes(RouteCollection routes, string controllerName);
        void RegisterRoutes(RouteCollection routes, string controllerName, string actionName);
    }
}
