#if MVC6
using Microsoft.AspNet.Http;
using System;
using System.IO;

namespace MvcSiteMapProvider.Web
{
    public class HttpResponseWrapper : HttpResponseBase
    {
        private HttpResponse _httpResponse;

        public HttpResponseWrapper(HttpResponse httpResponse)
        {
            if (httpResponse == null)
            {
                throw new ArgumentNullException("httpResponse");
            }
            this._httpResponse = httpResponse;
        }

        public override string ContentType
        {
            get
            {
                return this._httpResponse.ContentType;
            }
            set
            {
                this._httpResponse.ContentType = value;
            }
        }

        public override Stream OutputStream
        {
            get
            {
                return this._httpResponse.Body;
            }
        }

        public override void AppendHeader(string name, string value)
        {
            this._httpResponse.Headers.Append(name, value);
        }
    }
}
#endif