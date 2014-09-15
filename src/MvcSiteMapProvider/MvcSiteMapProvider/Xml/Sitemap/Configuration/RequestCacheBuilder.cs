using System;
using MvcSiteMapProvider.Caching;
using MvcSiteMapProvider.Web.Mvc;

namespace MvcSiteMapProvider.Xml.Sitemap.Configuration
{
    public class RequestCacheBuilder
        : IRequestCacheBuilder
    {
        public RequestCacheBuilder()
            : this(mvcContextFactory: new MvcContextFactory())
        {
        }

        private RequestCacheBuilder(
            IMvcContextFactory mvcContextFactory
            )
        {
            if (mvcContextFactory == null)
                throw new ArgumentNullException("mvcContextFactory");

            this.mvcContextFactory = mvcContextFactory;
        }
        private readonly IMvcContextFactory mvcContextFactory;

        public IRequestCacheBuilder WithMvcContextFactory(IMvcContextFactory mvcContextFactory)
        {
            return new RequestCacheBuilder(mvcContextFactory);
        }

        public IRequestCache Create()
        {
            return new RequestCache(this.mvcContextFactory);
        }
    }
}
