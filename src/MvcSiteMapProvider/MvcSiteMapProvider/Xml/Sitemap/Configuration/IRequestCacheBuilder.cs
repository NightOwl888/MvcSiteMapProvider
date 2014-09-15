using System;
using System.Collections.Generic;
using MvcSiteMapProvider.Caching;
using MvcSiteMapProvider.Web.Mvc;

namespace MvcSiteMapProvider.Xml.Sitemap.Configuration
{
    public interface IRequestCacheBuilder
    {
        IRequestCacheBuilder WithMvcContextFactory(IMvcContextFactory mvcContextFactory);

        IRequestCache Create();
    }
}
