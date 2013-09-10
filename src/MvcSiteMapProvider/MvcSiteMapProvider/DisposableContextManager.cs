using System;
using System.Collections.Generic;
using System.Linq;
using MvcSiteMapProvider.Web.Mvc;
using MvcSiteMapProvider.Caching;

namespace MvcSiteMapProvider
{
    public class DisposableContextManager<T>
        : IDisposableContextManager<T>
        where T: IDisposable
    {
        public DisposableContextManager(
            Func<T> createMethod,
            string label,
            //Action openMethod,
            //Action closeMethod,
            //Action saveMethod,
            IRequestCache requestCache
            )
        {
            if (createMethod == null)
                throw new ArgumentNullException("createMethod");
            if (requestCache == null)
                throw new ArgumentNullException("requestCache");

            this.requestCache = requestCache;
            this.contextTypeName = typeof(T).Name;
            this.label = label;

            if (this.State.ReferenceCount == 0)
            {
                this.State.Context = createMethod();
                //if (openMethod != null)
                //    openMethod();

                //this.State.CloseMethod = closeMethod;
                //this.State.SaveMethod = saveMethod;
            }
            this.AddReference();
        }
        protected readonly IRequestCache requestCache;
        protected readonly string contextTypeName;
        protected readonly string label;
        protected readonly object synclock = new object();

        #region IDisposableContextManager<T> Members

        public T Context
        {
            get { return this.State.Context; }
        }

        public int ReferenceCount
        {
            get { return this.State.ReferenceCount; }
            protected set { this.State.ReferenceCount = value; }
        }

        protected void AddReference()
        {
            this.ReferenceCount += 1;
        }

        protected void DeReference()
        {
            lock (synclock)
            {
                this.ReferenceCount -= 1;
                if (this.ReferenceCount == 0)
                {
                    //if (this.State.SaveMethod != null)
                    //    this.State.SaveMethod();
                    //if (this.State.CloseMethod != null)
                    //    this.State.CloseMethod();
                    this.Context.Dispose();
                    this.ClearState();
                }
            }
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            DeReference();
        }

        #endregion


        protected string GetCacheKey(string memberName)
        {
            return "__DISPOSABLE_CONTEXT__" + this.contextTypeName + "_" + this.label + "_" + memberName + "_";
        }

        protected ContextState State
        {
            get
            {
                lock (synclock)
                {
                    var key = this.GetCacheKey("State");
                    var result = this.requestCache.GetValue<ContextState>(key);
                    if (result == null)
                    {
                        result = new ContextState();
                        this.requestCache.SetValue<ContextState>(key, result);
                    }
                    return result;
                }
            }
        }

        protected void ClearState()
        {
            var key = this.GetCacheKey("State");
            this.requestCache.SetValue<ContextState>(key, null);
        }


        protected class ContextState
        {
            public T Context { get; set; }
            //public Action CloseMethod { get; set; }
            //public Action SaveMethod { get; set; }
            public int ReferenceCount { get; set; }
        }
        

    }
}
