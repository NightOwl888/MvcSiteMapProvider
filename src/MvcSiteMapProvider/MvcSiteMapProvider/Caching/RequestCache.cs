using System;
#if MVC6
using Microsoft.AspNet.Mvc;
using MvcSiteMapProvider.Web;
#else
using System.Web.Mvc;
#endif
using MvcSiteMapProvider.Web.Mvc;

namespace MvcSiteMapProvider.Caching
{
    /// <summary>
    /// Provides type-safe access to <see cref="P:System.Web.HttpContext.Items"/>.
    /// </summary>
    public class RequestCache 
        : IRequestCache
    {
        public RequestCache(
            IMvcContextFactory mvcContextFactory
            )
        {
            if (mvcContextFactory == null)
                throw new ArgumentNullException("mvcContextFactory");

            this.mvcContextFactory = mvcContextFactory;
        }

        private readonly IMvcContextFactory mvcContextFactory;

        protected HttpContextBase Context
        {
            get 
            { 
                return this.mvcContextFactory.CreateHttpContext();
            }
        }

        public virtual T GetValue<T>(string key)
        {
#if MVC6
            if (this.Context.Items.ContainsKey(key))
#else
            if (this.Context.Items.Contains(key))
#endif
            {
                return (T)this.Context.Items[key];
            }
            return default(T);
        }

        public virtual void SetValue<T>(string key, T value)
        {
            this.Context.Items[key] = value;
        }
    }
}
