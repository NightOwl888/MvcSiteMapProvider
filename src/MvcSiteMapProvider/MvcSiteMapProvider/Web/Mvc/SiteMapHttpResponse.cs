using System;
#if MVC6
using Microsoft.AspNet.Http;
#else
using System.Web;
#endif

namespace MvcSiteMapProvider.Web.Mvc
{
    /// <summary>
    /// HttpResponse wrapper.
    /// </summary>
    public class SiteMapHttpResponse
        : HttpResponseWrapper
    {
        public SiteMapHttpResponse(HttpResponse httpResponse)
            : base(httpResponse)
        {
        }
#if !MVC6
        public override HttpCachePolicyBase Cache
        {
            get { return new SiteMapHttpResponseCache(); }
        }
#endif
    }
}
