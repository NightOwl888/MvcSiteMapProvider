#if MVC6
using System;
using System.Collections.Specialized;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Principal;
using System.Text;
using System.Threading;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Routing;
using Microsoft.AspNet.WebUtilities;
using MvcSiteMapProvider.Collections.Specialized;


namespace MvcSiteMapProvider.Web
{
    public class HttpRequestWrapper : HttpRequestBase
    {
        private HttpRequest _httpRequest;
        
        public HttpRequestWrapper(HttpRequest httpRequest)
        {
            if (httpRequest == null)
            {
                throw new ArgumentNullException("httpRequest");
            }
            this._httpRequest = httpRequest;
        }


        public override IHeaderDictionary Headers
        {
            get
            {
                return this._httpRequest.Headers;
            }
        }

        public override NameValueCollection ServerVariables
        {
            get
            {
                return new NameValueCollection(this.Headers);
            }
        }

        public override System.Uri Url
        {
            get
            {
                // TODO: Test this
                return new System.Uri(this.RawUrl, UriKind.Absolute);
            }
        }

        public override string RawUrl
        {
            get
            {
                // TODO: Test this
                return this._httpRequest.Protocol + this._httpRequest.Scheme + this._httpRequest.PathBase + this._httpRequest.Path + this._httpRequest.QueryString;
            }
        }

        public override NameValueCollection QueryString
        {
            get
            {
                return new NameValueCollection(this._httpRequest.Query);
            }
        }

        public override string ApplicationPath
        {
            get
            {
                // TODO: Test this
                return this._httpRequest.PathBase;
            }
        }
    }
}
#endif
