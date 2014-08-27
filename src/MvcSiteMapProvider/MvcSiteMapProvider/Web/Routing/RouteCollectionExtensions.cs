using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;

namespace MvcSiteMapProvider.Web.Routing
{
    /// <summary>
    /// Extension methods for <see cref="T:System.Web.Routing.RouteCollection"/>.
    /// </summary>
    public static class RouteCollectionExtensions
    {
        public static void AddRange(this RouteCollection destination, IEnumerable<RouteBase> source)
        {
            foreach (var item in source)
            {
                destination.Add(item);
            }
        }

        public static void InsertRange(this RouteCollection destination, int index, IEnumerable<RouteBase> source)
        {
            for (int i = 0; i < source.Count(); i++)
            {
                destination.Insert(index + i, source.ElementAt(i));
            }
        }
    }
}
