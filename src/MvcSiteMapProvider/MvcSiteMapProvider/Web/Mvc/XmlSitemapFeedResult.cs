using System;
//using System.Net;
using System.Web.Mvc;
using MvcSiteMapProvider.IO;
using MvcSiteMapProvider.Xml.Sitemap;

namespace MvcSiteMapProvider.Web.Mvc
{
    public class XmlSitemapFeedResult
        : ActionResult
        //: HttpNotFoundResult
    {
        public XmlSitemapFeedResult(
            int page,
            IXmlSitemapFeed xmlSitemap,
            IHttpResponseStreamCompressor responseStreamCompressor
            )
        {
            if (xmlSitemap == null)
                throw new ArgumentNullException("xmlSitemap");
            if (responseStreamCompressor == null)
                throw new ArgumentNullException("responseStreamCompressor");

            this.page = page;
            this.xmlSitemap = xmlSitemap;
            this.responseStreamCompressor = responseStreamCompressor;
        }
        private readonly int page;
        private readonly IXmlSitemapFeed xmlSitemap;
        private readonly IHttpResponseStreamCompressor responseStreamCompressor;

        //public override void ExecuteResult(ControllerContext context)
        //{
        //    bool notFound = false;

        //    // Output content type
        //    context.HttpContext.Response.ContentType = "text/xml";

        //    using (var stream = this.responseStreamCompressor.Compress(context.HttpContext))
        //    {
        //        notFound = !this.xmlSitemap.WritePage(this.page, stream);
        //    }

        //    if (notFound)
        //    {
        //        throw new HttpException((int)HttpStatusCode.NotFound, "Not Found");
        //    }
        //}

        public override void ExecuteResult(ControllerContext context)
        {
            // TODO: Add a debug mode when debugging is enabled on the server that doesn't return 404

            // Output content type
            context.HttpContext.Response.ContentType = "text/xml";

            using (var stream = this.responseStreamCompressor.Compress(context.HttpContext))
            {
                if (!this.xmlSitemap.WritePage(this.page, stream))
                {
                    // Return 404 not found
                    //base.ExecuteResult(context);

                    new HttpNotFoundResult().ExecuteResult(context);
                }
            }
        }
    }
}
