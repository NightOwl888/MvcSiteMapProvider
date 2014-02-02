using System;
using MvcSiteMapProvider.Builder;
using MvcSiteMapProvider.Web.Mvc;
using MvcSiteMapProvider.Web;
using MvcSiteMapProvider.Threading;

namespace MvcSiteMapProvider
{
    /// <summary>
    /// An abstract factory that can be used to create new instances of <see cref="T:MvcSiteMapProvider.RequestCacheableSiteMap"/>
    /// at runtime.
    /// </summary>
    public class SiteMapFactory
        : ISiteMapFactory
    {
        public SiteMapFactory(
            ISiteMapPluginProviderFactory pluginProviderFactory,
            IMvcResolverFactory mvcResolverFactory,
            IMvcContextFactory mvcContextFactory,
            ISiteMapChildStateFactory siteMapChildStateFactory,
            IUrlPath urlPath,
            IReferenceCounterFactory referenceCounterFactory,
            IControllerTypeResolverFactory controllerTypeResolverFactory,
            IActionMethodParameterResolverFactory actionMethodParameterResolverFactory
            )
        {
            if (pluginProviderFactory == null)
                throw new ArgumentNullException("pluginProviderFactory");
            if (mvcResolverFactory == null)
                throw new ArgumentNullException("mvcResolverFactory");
            if (mvcContextFactory == null)
                throw new ArgumentNullException("mvcContextFactory");
            if (siteMapChildStateFactory == null)
                throw new ArgumentNullException("siteMapChildStateFactory");
            if (urlPath == null)
                throw new ArgumentNullException("urlPath");
            if (referenceCounterFactory == null)
                throw new ArgumentNullException("referenceCounterFactory");
            if (controllerTypeResolverFactory == null)
                throw new ArgumentNullException("controllerTypeResolverFactory");
            if (actionMethodParameterResolverFactory == null)
                throw new ArgumentNullException("actionMethodParameterResolverFactory");
            

            this.pluginProviderFactory = pluginProviderFactory;
            this.mvcResolverFactory = mvcResolverFactory;
            this.mvcContextFactory = mvcContextFactory;
            this.siteMapChildStateFactory = siteMapChildStateFactory;
            this.urlPath = urlPath;
            this.referenceCounterFactory = referenceCounterFactory;
            this.controllerTypeResolverFactory = controllerTypeResolverFactory;
            this.actionMethodParameterResolverFactory = actionMethodParameterResolverFactory;
        }

        protected readonly ISiteMapPluginProviderFactory pluginProviderFactory;
        protected readonly IMvcResolverFactory mvcResolverFactory;
        protected readonly IMvcContextFactory mvcContextFactory;
        protected readonly ISiteMapChildStateFactory siteMapChildStateFactory;
        protected readonly IUrlPath urlPath;
        protected readonly IControllerTypeResolverFactory controllerTypeResolverFactory;
        protected readonly IActionMethodParameterResolverFactory actionMethodParameterResolverFactory;
        protected readonly IReferenceCounterFactory referenceCounterFactory;
        

        #region ISiteMapFactory Members

        public virtual ISiteMap Create(ISiteMapBuilder siteMapBuilder, ISiteMapSettings siteMapSettings)
        {
            var routes = mvcContextFactory.GetRoutes();
            var requestCache = mvcContextFactory.GetRequestCache();

            // IMPORTANT: We need to ensure there is one instance of controllerTypeResolver and 
            // one instance of ActionMethodParameterResolver per SiteMap instance because each of
            // these classes does internal caching.
            var controllerTypeResolver = controllerTypeResolverFactory.Create(routes);
            var actionMethodParameterResolver = actionMethodParameterResolverFactory.Create();
            var mvcResolver = mvcResolverFactory.Create(controllerTypeResolver, actionMethodParameterResolver);
            var pluginProvider = pluginProviderFactory.Create(siteMapBuilder, mvcResolver);

            return new RequestCacheableSiteMap(
                pluginProvider,
                mvcContextFactory,
                siteMapChildStateFactory,
                urlPath,
                referenceCounterFactory,
                siteMapSettings,
                requestCache);
        }

        #endregion
    }
}
