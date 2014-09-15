using System;
using System.Collections.Generic;
using System.Linq;
using MvcSiteMapProvider.ComponentModel;
using MvcSiteMapProvider.Web;
using MvcSiteMapProvider.Web.Mvc;

namespace MvcSiteMapProvider.Xml.Sitemap.Configuration
{
    public interface IUrlPathBuilder
        : IFluentInterface
    {
        IUrlPathBuilder WithMvcContextFactory(IMvcContextFactory mvcContextFactory);

        IUrlPathBuilder WithBindingProvider(IBindingProvider bindingProvider);

        IUrlPath Create();
    }
}
