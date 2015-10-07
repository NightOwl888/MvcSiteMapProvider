#if MVC6
using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading;
using Microsoft.AspNet.Http;
using Microsoft.AspNet.Http.Authentication;
using Microsoft.AspNet.Http.Features;
using Microsoft.AspNet.Mvc;

namespace MvcSiteMapProvider.Web
{
    public abstract class HttpContextBase
    {
        public virtual ActionContext ActionContext { get; }

        public virtual IHttpHandler CurrentHandler
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual object GetGlobalResourceObject(string classKey, string resourceKey)
        {
            throw new NotImplementedException();
        }

        public virtual IDictionary<object, object> Items
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual HttpRequestBase Request
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual HttpResponseBase Response
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public virtual ClaimsPrincipal User
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

    }
}
#endif

