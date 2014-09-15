using System;
using MvcSiteMapProvider.Web;
using MvcSiteMapProvider.Web.Mvc;

namespace MvcSiteMapProvider.Xml.Sitemap.Configuration
{
    public class UrlPathBuilder
        : IUrlPathBuilder
    {
        // TODO: Make a builder for the binding provider.
        public UrlPathBuilder()
            : this(mvcContextFactory: new MvcContextFactory(), bindingProvider: new BindingProvider(new BindingFactory(), new MvcContextFactory()))
        {
        }

        private UrlPathBuilder(
            IMvcContextFactory mvcContextFactory,
            IBindingProvider bindingProvider
            )
        {
            if (mvcContextFactory == null)
                throw new ArgumentNullException("mvcContextFactory");
            if (bindingProvider == null)
                throw new ArgumentNullException("bindingProvider");

            this.mvcContextFactory = mvcContextFactory;
            this.bindingProvider = bindingProvider;
        }
        private readonly IMvcContextFactory mvcContextFactory;
        private readonly IBindingProvider bindingProvider;

        public IUrlPathBuilder WithMvcContextFactory(IMvcContextFactory mvcContextFactory)
        {
            return new UrlPathBuilder(mvcContextFactory, this.bindingProvider);
        }

        public IUrlPathBuilder WithBindingProvider(IBindingProvider bindingProvider)
        {
            return new UrlPathBuilder(this.mvcContextFactory, bindingProvider);
        }

        public IUrlPath Create()
        {
            return new UrlPath(this.mvcContextFactory, this.bindingProvider);
        }
    }
}
