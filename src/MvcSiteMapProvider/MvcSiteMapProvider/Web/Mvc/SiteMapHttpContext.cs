#if MVC6
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Mvc;
#else
using System.Web;
#endif

namespace MvcSiteMapProvider.Web.Mvc
{
    /// <summary>
    /// HttpContext wrapper.
    /// </summary>
    public class SiteMapHttpContext 
        : HttpContextWrapper
    {
        private readonly HttpContext httpContext;
        private readonly ISiteMapNode node;

        /// <summary>
        /// Initializes a new instance of the <see cref="SiteMapHttpContext"/> class.
        /// </summary>
        /// <param name="httpContext">The object that this wrapper class provides access to.</param>
        /// <param name="node">The site map node to fake node access context for or <c>null</c>.</param>
        /// <exception cref="T:System.ArgumentNullException">
        ///     <paramref name="httpContext"/> is null.
        /// </exception>
#if MVC6
        public SiteMapHttpContext(ActionContext actionContext, ISiteMapNode node)
            : base(actionContext)
        {
            this.httpContext = actionContext.HttpContext;
#else
        public SiteMapHttpContext(HttpContext httpContext, ISiteMapNode node)
            : base(httpContext)
        {
            this.httpContext = httpContext;
#endif
            this.node = node;
        }

        /// <summary>
        /// Gets the <see cref="T:System.Web.HttpRequestBase"/> object for the current HTTP request.
        /// </summary>
        /// <returns>The current HTTP request.</returns>
        public override HttpRequestBase Request
        {
            get { return new SiteMapHttpRequest(this.httpContext.Request, this.node); }
        }

        /// <summary>
        /// Gets the <see cref="System.Web.HttpResponseBase"/> object for the current HTTP response.
        /// </summary>
        /// <returns>The current HTTP response.</returns>
        public override HttpResponseBase Response
        {
            get { return new SiteMapHttpResponse(this.httpContext.Response); }
        }
    }
}