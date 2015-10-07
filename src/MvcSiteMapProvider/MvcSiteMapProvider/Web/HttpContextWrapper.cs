#if MVC6
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security.Principal;
//using System.Web.Caching;
//using System.Web.Configuration;
//using System.Web.Instrumentation;
//using System.Web.Profile;
//using System.Web.SessionState;
//using System.Web.WebSockets;
using Microsoft.AspNet.Mvc;
using Microsoft.AspNet.Http;
using System.Security.Claims;

namespace MvcSiteMapProvider.Web
{
    public class HttpContextWrapper : HttpContextBase
    {
        private readonly HttpContext _context;
        private readonly ActionContext actionContext;

        public HttpContextWrapper(ActionContext actionContext)
        {
            if (actionContext == null)
            {
                throw new ArgumentNullException("actionContext");
            }
            this._context = actionContext.HttpContext;
            this.actionContext = actionContext;
        }

        public override ActionContext ActionContext
        {
            get
            {
                return this.actionContext;
            }
        }

        // TODO: Work out how to get this from context
        public override IHttpHandler CurrentHandler
        {
            get
            {
                return null;
            }
        }


        // TODO: Implement
        public override object GetGlobalResourceObject(string classKey, string resourceKey)
        {
            return "This is not yet implemented";
        }

        public override IDictionary<object, object> Items
        {
            get
            {
                return this._context.Items;
            }
        }

        public override HttpRequestBase Request
        {
            get
            {
                return new HttpRequestWrapper(this._context.Request);
            }
        }

        public override HttpResponseBase Response
        {
            get
            {
                return new HttpResponseWrapper(this._context.Response);
            }
        }

        public override ClaimsPrincipal User
        {
            get
            {
                return this._context.User;
            }
            set
            {
                this._context.User = value;
            }
        }





        //public override void AcceptWebSocketRequest(Func<AspNetWebSocketContext, Task> userFunc)
        //{
        //    this._context.AcceptWebSocketRequest(userFunc);
        //}

        //public override void AcceptWebSocketRequest(Func<AspNetWebSocketContext, Task> userFunc, AspNetWebSocketOptions options)
        //{
        //    this._context.AcceptWebSocketRequest(userFunc, options);
        //}

        //public override void AddError(Exception errorInfo)
        //{
        //    this._context.AddError(errorInfo);
        //}

        //public override ISubscriptionToken AddOnRequestCompleted(Action<HttpContextBase> callback)
        //{
        //    return this._context.AddOnRequestCompleted(WrapCallback(callback));
        //}

        //public override void ClearError()
        //{
        //    this._context.ClearError();
        //}

        //public override ISubscriptionToken DisposeOnPipelineCompleted(IDisposable target)
        //{
        //    return this._context.DisposeOnPipelineCompleted(target);
        //}

        //public override object GetGlobalResourceObject(string classKey, string resourceKey)
        //{
        //    return HttpContext.GetGlobalResourceObject(classKey, resourceKey);
        //}

        //public override object GetGlobalResourceObject(string classKey, string resourceKey, CultureInfo culture)
        //{
        //    return HttpContext.GetGlobalResourceObject(classKey, resourceKey, culture);
        //}

        //public override object GetLocalResourceObject(string virtualPath, string resourceKey)
        //{
        //    return HttpContext.GetLocalResourceObject(virtualPath, resourceKey);
        //}

        //public override object GetLocalResourceObject(string virtualPath, string resourceKey, CultureInfo culture)
        //{
        //    return HttpContext.GetLocalResourceObject(virtualPath, resourceKey, culture);
        //}

        //public override object GetSection(string sectionName)
        //{
        //    return this._context.GetSection(sectionName);
        //}

        //public override object GetService(Type serviceType)
        //{
        //    return ((IServiceProvider)this._context).GetService(serviceType);
        //}

        //public override void RemapHandler(IHttpHandler handler)
        //{
        //    this._context.RemapHandler(handler);
        //}

        //public override void RewritePath(string path)
        //{
        //    this._context.RewritePath(path);
        //}

        //public override void RewritePath(string path, bool rebaseClientPath)
        //{
        //    this._context.RewritePath(path, rebaseClientPath);
        //}

        //public override void RewritePath(string filePath, string pathInfo, string queryString)
        //{
        //    this._context.RewritePath(filePath, pathInfo, queryString);
        //}

        //public override void RewritePath(string filePath, string pathInfo, string queryString, bool setClientFilePath)
        //{
        //    this._context.RewritePath(filePath, pathInfo, queryString, setClientFilePath);
        //}

        //public override void SetSessionStateBehavior(SessionStateBehavior sessionStateBehavior)
        //{
        //    this._context.SetSessionStateBehavior(sessionStateBehavior);
        //}

        //internal static Action<HttpContext> WrapCallback(Action<HttpContextBase> callback)
        //{
        //    if (callback != null)
        //    {
        //        return delegate (HttpContext context) {
        //            callback(new HttpContextWrapper(context));
        //        };
        //    }
        //    return null;
        //}

        //public override Exception[] AllErrors
        //{
        //    get
        //    {
        //        return this._context.AllErrors;
        //    }
        //}

        //[EditorBrowsable(EditorBrowsableState.Advanced)]
        //public override bool AllowAsyncDuringSyncStages
        //{
        //    get
        //    {
        //        return this._context.AllowAsyncDuringSyncStages;
        //    }
        //    set
        //    {
        //        this._context.AllowAsyncDuringSyncStages = value;
        //    }
        //}

        //public override HttpApplicationStateBase Application
        //{
        //    get
        //    {
        //        return new HttpApplicationStateWrapper(this._context.Application);
        //    }
        //}

        //public override HttpApplication ApplicationInstance
        //{
        //    get
        //    {
        //        return this._context.ApplicationInstance;
        //    }
        //    set
        //    {
        //        this._context.ApplicationInstance = value;
        //    }
        //}

        //public override AsyncPreloadModeFlags AsyncPreloadMode
        //{
        //    get
        //    {
        //        return this._context.AsyncPreloadMode;
        //    }
        //    set
        //    {
        //        this._context.AsyncPreloadMode = value;
        //    }
        //}

        //public override System.Web.Caching.Cache Cache
        //{
        //    get
        //    {
        //        return this._context.Cache;
        //    }
        //}

        //public override IHttpHandler CurrentHandler
        //{
        //    get
        //    {
        //        return this._context.CurrentHandler;
        //    }
        //}

        //public override RequestNotification CurrentNotification
        //{
        //    get
        //    {
        //        return this._context.CurrentNotification;
        //    }
        //}

        //public override Exception Error
        //{
        //    get
        //    {
        //        return this._context.Error;
        //    }
        //}

        //public override IHttpHandler Handler
        //{
        //    get
        //    {
        //        return this._context.Handler;
        //    }
        //    set
        //    {
        //        this._context.Handler = value;
        //    }
        //}

        //public override bool IsCustomErrorEnabled
        //{
        //    get
        //    {
        //        return this._context.IsCustomErrorEnabled;
        //    }
        //}

        //public override bool IsDebuggingEnabled
        //{
        //    get
        //    {
        //        return this._context.IsDebuggingEnabled;
        //    }
        //}

        //public override bool IsPostNotification
        //{
        //    get
        //    {
        //        return this._context.IsPostNotification;
        //    }
        //}

        //public override bool IsWebSocketRequest
        //{
        //    get
        //    {
        //        return this._context.IsWebSocketRequest;
        //    }
        //}

        //public override bool IsWebSocketRequestUpgrading
        //{
        //    get
        //    {
        //        return this._context.IsWebSocketRequestUpgrading;
        //    }
        //}

        //public override IDictionary Items
        //{
        //    get
        //    {
        //        return this._context.Items;
        //    }
        //}

        //public override PageInstrumentationService PageInstrumentation
        //{
        //    get
        //    {
        //        return this._context.PageInstrumentation;
        //    }
        //}

        //public override IHttpHandler PreviousHandler
        //{
        //    get
        //    {
        //        return this._context.PreviousHandler;
        //    }
        //}

        //public override ProfileBase Profile
        //{
        //    get
        //    {
        //        return this._context.Profile;
        //    }
        //}

        

        //public override HttpServerUtilityBase Server
        //{
        //    get
        //    {
        //        return new HttpServerUtilityWrapper(this._context.Server);
        //    }
        //}

        //public override HttpSessionStateBase Session
        //{
        //    get
        //    {
        //        HttpSessionState session = this._context.Session;
        //        if (session == null)
        //        {
        //            return null;
        //        }
        //        return new HttpSessionStateWrapper(session);
        //    }
        //}

        //public override bool SkipAuthorization
        //{
        //    get
        //    {
        //        return this._context.SkipAuthorization;
        //    }
        //    set
        //    {
        //        this._context.SkipAuthorization = value;
        //    }
        //}

        //public override bool ThreadAbortOnTimeout
        //{
        //    get
        //    {
        //        return this._context.ThreadAbortOnTimeout;
        //    }
        //    set
        //    {
        //        this._context.ThreadAbortOnTimeout = value;
        //    }
        //}

        //public override DateTime Timestamp
        //{
        //    get
        //    {
        //        return this._context.Timestamp;
        //    }
        //}

        //public override TraceContext Trace
        //{
        //    get
        //    {
        //        return this._context.Trace;
        //    }
        //}

        

        //public override string WebSocketNegotiatedProtocol
        //{
        //    get
        //    {
        //        return this._context.WebSocketNegotiatedProtocol;
        //    }
        //}

        //public override IList<string> WebSocketRequestedProtocols
        //{
        //    get
        //    {
        //        return this._context.WebSocketRequestedProtocols;
        //    }
        //}
    }
}
#endif