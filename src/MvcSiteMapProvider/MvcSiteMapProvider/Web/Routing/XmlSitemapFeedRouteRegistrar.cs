using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace MvcSiteMapProvider.Web.Routing
{
    public class XmlSitemapFeedRouteRegistrar
        : IXmlSitemapFeedRouteRegistrar
    {
        public void RegisterRoutes(RouteCollection routes)
        {
            this.RegisterRoutes(routes, "XmlSitemapFeed");
        }

        public void RegisterRoutes(RouteCollection routes, string controllerName)
        {
            this.RegisterRoutes(routes, controllerName, "Index");
        }

        public void RegisterRoutes(RouteCollection routes, string controllerName, string actionName)
        {
            var defaultRouteValues = new RouteValueDictionary(new { controller = controllerName, action = actionName, page = 0, feedName = "default" });
            var handler = new MvcRouteHandler();

            var routesToAdd = new List<RouteBase>()
            {
                new Route("sitemap.xml", defaultRouteValues, handler),
                new Route("sitemap-{page}.xml", defaultRouteValues, handler),
                new Route("{feedName}-sitemap.xml", defaultRouteValues, handler),
                new Route("{feedName}-sitemap-{page}.xml", defaultRouteValues, handler)
            };

            if (routes.Count > 0)
            {
                routes.InsertRange(routes.Count - 1, routesToAdd);
            }
            else
            {
                routes.AddRange(routesToAdd);
            }

            //List<RouteBase> routes = new List<RouteBase> {
            //    new Route("sitemap.xml",
            //        new RouteValueDictionary(
            //            new { controller = "XmlSiteMap", action = "Index", page = 0 }),
            //                new MvcRouteHandler()),

            //    new Route("sitemap-{page}.xml",
            //        new RouteValueDictionary(
            //            new { controller = "XmlSiteMap", action = "Index", page = 0 }),
            //                new MvcRouteHandler())
            //};

            //if (routeCollection.Count == 0)
            //{
            //    foreach (var route in routes)
            //    {
            //        routeCollection.Add(route);
            //    }
            //}
            //else
            //{
            //    foreach (var route in routes)
            //    {
            //        routeCollection.Insert(routeCollection.Count - 1, route);
            //    }
            //}
        }
    }
}
