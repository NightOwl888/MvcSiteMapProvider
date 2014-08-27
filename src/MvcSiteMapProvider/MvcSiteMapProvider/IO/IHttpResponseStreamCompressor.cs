using System;
using System.IO;
using System.Web;

namespace MvcSiteMapProvider.IO
{
    public interface IHttpResponseStreamCompressor
    {
        Stream Compress(HttpContextBase httpContext);
    }
}
