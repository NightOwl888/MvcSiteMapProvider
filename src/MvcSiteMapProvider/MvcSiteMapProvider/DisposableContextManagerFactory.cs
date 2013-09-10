using System;
using System.Collections.Generic;
using System.Linq;
using MvcSiteMapProvider.Web.Mvc;

namespace MvcSiteMapProvider
{
    public class DisposableContextManagerFactory
        : IDisposableContextManagerFactory
        
    {
        //public DisposableContextManagerFactory(
        //    IMvcContextFactory mvcContextFactory
        //    )
        //{
        //    if (mvcContextFactory == null)
        //        throw new ArgumentNullException("mvcContextFactory");
        //    this.mvcContextFactory = mvcContextFactory;
        //}
        //protected readonly IMvcContextFactory mvcContextFactory;

        #region IDisposableContextManagerFactory Members

        public IDisposableContextManager<T> Get<T>(Func<T> createMethod) where T : IDisposable
        {
            //var requestCache = this.mvcContextFactory.GetRequestCache();
            var requestCache = new TestCache();
            //return new DisposableContextManager<T>(createMethod, null, null, null, requestCache);
            return new DisposableContextManager<T>(createMethod, null, requestCache);
        }

        public IDisposableContextManager<T> Get<T>(string label, Func<T> createMethod) where T : IDisposable
        {
            var requestCache = new TestCache();
            return new DisposableContextManager<T>(createMethod, label, requestCache);
        }

        #endregion
    }
}


public class TestCache
    : MvcSiteMapProvider.Caching.IRequestCache
{

    #region ICache Members

    public T GetValue<T>(string key)
    {
        T result = (T)System.Web.HttpContext.Current.Items[key];
        if (result != null)
        {
            return result;
        }
        return default(T);
    }

    public void SetValue<T>(string key, T value)
    {
        System.Web.HttpContext.Current.Items[key] = value;
    }

    #endregion
}