#if MVC6
using Microsoft.AspNet.Routing;
#else
using System.Web.Routing;
#endif

namespace MvcSiteMapProvider.Web.Mvc
{
    /// <summary>
    /// Contract for abstract factory that can provide instances of <see cref="T:MvcSiteMapProvder.Web.Mvc.IControllerTypeResolver"/>
    /// at runtime.
    /// </summary>
    public interface IControllerTypeResolverFactory
    {
        IControllerTypeResolver Create(RouteCollection routes);
    }
}
