﻿//using System;
//#if MVC6
//using Microsoft.AspNet.Http;
//using Microsoft.AspNet.Mvc;
//#else
//using System.Web.Mvc;
//#endif
//using MvcSiteMapProvider.Web.Mvc;

//namespace MvcSiteMapProvider.Caching
//{
//    /// <summary>
//    /// Provides type-safe access to <see cref="P:System.Web.HttpContext.Items"/>.
//    /// </summary>
//    public class RequestCache
//        : IRequestCache
//    {
//        public RequestCache(
//            IMvcContextFactory mvcContextFactory
//            )
//        {
//            if (mvcContextFactory == null)
//                throw new ArgumentNullException("mvcContextFactory");

//            this.mvcContextFactory = mvcContextFactory;
//        }

//        private readonly IMvcContextFactory mvcContextFactory;

//#if MVC6

//#else
//        protected HttpContextBase Context
//        {
//            get
//            {
//                return this.mvcContextFactory.CreateHttpContext();
//            }
//        }
//#endif

//        public virtual T GetValue<T>(string key)
//        {
//            if (this.Context.Items.Contains(key))
//            {
//                return (T)this.Context.Items[key];
//            }
//            return default(T);
//        }

//        public virtual void SetValue<T>(string key, T value)
//        {
//            this.Context.Items[key] = value;
//        }
//    }
//}
