
#if MVC6
using Microsoft.AspNet.Mvc;
#else
using System.Web;
using System.Web.Mvc;
#endif

namespace MvcSiteMapProvider.Web.Mvc
{
    /// <summary>
    /// ControllerExtensions
    /// </summary>
    public static class ControllerExtensions
    {
        /// <summary>
        /// Gets the current site map node.
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <returns></returns>
#if MVC6
        public static ISiteMapNode GetCurrentSiteMapNode(this Controller controller)
#else
        public static ISiteMapNode GetCurrentSiteMapNode(this ControllerBase controller)
#endif
        {
            return GetCurrentSiteMapNode(controller, SiteMaps.Current);
        }

        /// <summary>
        /// Gets the current site map node.
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <param name="siteMap">The site map.</param>
        /// <returns></returns>
#if MVC6
        public static ISiteMapNode GetCurrentSiteMapNode(this Controller controller, ISiteMap siteMap)
#else
        public static ISiteMapNode GetCurrentSiteMapNode(this ControllerBase controller, ISiteMap siteMap)
#endif
        {
            return siteMap.CurrentNode;
        }

        /// <summary>
        /// Gets the current site map node for child action.
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <returns></returns>
#if MVC6
        public static ISiteMapNode GetCurrentSiteMapNodeForChildAction(this Controller controller)
#else
        public static ISiteMapNode GetCurrentSiteMapNodeForChildAction(this ControllerBase controller)
#endif
        {
            return GetCurrentSiteMapNodeForChildAction(controller, SiteMaps.Current);
        }

        /// <summary>
        /// Gets the current site map node for child action.
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <param name="siteMap">The SiteMap.</param>
        /// <returns></returns>
#if MVC6
        public static ISiteMapNode GetCurrentSiteMapNodeForChildAction(this Controller controller, ISiteMap siteMap)
        {
            return siteMap.FindSiteMapNode(controller.ActionContext);
        }
#else
        public static ISiteMapNode GetCurrentSiteMapNodeForChildAction(this ControllerBase controller, ISiteMap siteMap)
        {
            return siteMap.FindSiteMapNode(controller.ControllerContext);
        }
#endif

    }
}
