using System;
using System.IO;
using System.IO.Compression;
using System.Web;
using MvcSiteMapProvider.Web.Mvc;

namespace MvcSiteMapProvider.IO
{
    public class CompressibleHttpResponseStreamFactory
        : IStreamFactory
    {
        public CompressibleHttpResponseStreamFactory(
            IMvcContextFactory mvcContextFactory
            )
        {
            if (mvcContextFactory == null)
                throw new ArgumentNullException("mvcContextFactory");
            this.mvcContextFactory = mvcContextFactory;
        }
        protected readonly IMvcContextFactory mvcContextFactory;

        public Stream Create()
        {
            var httpContext = this.mvcContextFactory.CreateHttpContext();
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

        public void Release(Stream stream)
        {
            if (stream != null)
            {
                stream.Dispose();
            }
        }
    }
}
