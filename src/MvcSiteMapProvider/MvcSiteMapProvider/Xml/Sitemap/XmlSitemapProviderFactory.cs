using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
                result = (IXmlSitemapProvider)Activator.CreateInstance(providerType);
            }
            catch (Exception)
            {
                // TODO: Throw exception indicating to use a custom XmlSiteMapProviderFactory
                // in order to inject dependencies into the provider.
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
