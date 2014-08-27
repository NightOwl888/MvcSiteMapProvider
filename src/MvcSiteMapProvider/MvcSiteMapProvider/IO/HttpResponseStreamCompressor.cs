using System;
using System.IO;
using System.IO.Compression;
using System.Web;

namespace MvcSiteMapProvider.IO
{
    public class HttpResponseStreamCompressor
        : IHttpResponseStreamCompressor
    {
        public Stream Compress(HttpContextBase httpContext)
        {
            var result = httpContext.Response.OutputStream;

            var acceptEncoding = httpContext.Request.Headers["Accept-encoding"];
            if (acceptEncoding != null)
            {
                acceptEncoding = acceptEncoding.ToLowerInvariant();
                if (acceptEncoding.Contains("gzip"))
                {
                    httpContext.Response.AppendHeader("Content-encoding", "gzip");
                    result = new GZipStream(result, CompressionMode.Compress);
                }
                else if (acceptEncoding.Contains("deflate"))
                {
                    httpContext.Response.AppendHeader("Content-encoding", "deflate");
                    result = new DeflateStream(result, CompressionMode.Compress);
                }
            }

            return result;
        }
    }
}
