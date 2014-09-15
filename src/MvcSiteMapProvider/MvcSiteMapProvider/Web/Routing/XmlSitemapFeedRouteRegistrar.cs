using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using MvcSiteMapProvider.Xml.Sitemap;

namespace MvcSiteMapProvider.Web.Routing
{
    public class XmlSitemapFeedRouteRegistrar
        : IXmlSitemapFeedRouteRegistrar
    {
        public XmlSitemapFeedRouteRegistrar()
            : this(pageNameProvider: new XmlSitemapFeedPageNameProvider())
        {
        }

        public XmlSitemapFeedRouteRegistrar(
            IXmlSitemapFeedPageNameProvider pageNameProvider
            )
        {
            if (pageNameProvider == null)
                throw new ArgumentNullException("pageNameProvider");

            this.pageNameProvider = pageNameProvider;
        }
        private readonly IXmlSitemapFeedPageNameProvider pageNameProvider;

        public void RegisterRoutes(RouteCollection routes)
        {
            this.RegisterRoutes(routes, "XmlSitemapFeed");
        }

        public void RegisterRoutes(RouteCollection routes, string controllerName)
        {
            this.RegisterRoutes(routes, controllerName, "Index");
        }

        public void RegisterRoutes(RouteCollection routes, string controllerName, IRouteHandler handler)
        {
            this.RegisterRoutes(routes, controllerName, string.Empty, handler);
        }

        public void RegisterRoutes(RouteCollection routes, string controllerName, string actionName)
        {
            this.RegisterRoutes(routes, controllerName, actionName, new MvcRouteHandler());
        }

        public void RegisterRoutes(RouteCollection routes, string controllerName, string actionName, IRouteHandler handler)
        {
            var defaultRouteValues = new RouteValueDictionary(new { controller = controllerName, action = actionName, page = 0, feedName = "default" });
            var routesToAdd = new List<RouteBase>()
            {
                new Route(this.pageNameProvider.DefaultFeedRootPageName, defaultRouteValues, handler),
                new Route(this.pageNameProvider.DefaultFeedPageName, defaultRouteValues, handler),
                new Route(this.pageNameProvider.NamedFeedRootPageName, defaultRouteValues, handler),
                new Route(this.pageNameProvider.NamedFeedPageName, defaultRouteValues, handler)
            };

            if (routes.Count > 0)
            {
                routes.InsertRange(routes.Count - 1, routesToAdd);
            }
            else
            {
                routes.AddRange(routesToAdd);
            }
        }
    }
}
