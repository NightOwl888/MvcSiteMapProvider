using System;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    public class XmlSitemapProviderFactory
        : IXmlSitemapProviderFactory
    {
        public IXmlSitemapProvider Create(Type providerType)
        {
            IXmlSitemapProvider result = null;
            try
            {
                // TODO: Wrap this provider instance in a class that request caches the
                // GetTotalRecordCount and GetLastModifiedDate methods.
                result = (IXmlSitemapProvider)Activator.CreateInstance(providerType);
            }
            catch (Exception ex)
            {
                // TODO: Throw exception indicating to use a custom XmlSiteMapProviderFactory
                // in order to inject dependencies into the provider.

                //throw new InvalidOperationException(
                //        String.Format(
                //            CultureInfo.CurrentCulture,
                //            MvcResources.DefaultControllerFactory_ErrorCreatingController,
                //            controllerType),
                //        ex);
            }

            return result;
        }

        public void Release(IXmlSitemapProvider xmlSitemapProvider)
        {
            var disposable = xmlSitemapProvider as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }
    }
}
