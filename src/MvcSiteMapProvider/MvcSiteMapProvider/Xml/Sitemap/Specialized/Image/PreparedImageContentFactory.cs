using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvcSiteMapProvider.Globalization;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized.Image
{
    public class PreparedImageContentFactory
        : IPreparedSpecializedContentFactory
    {
        public IPreparedSpecializedContent Create(ISpecializedContent specializedContent, IXmlSitemapUrlResolver urlResolver, ICultureContext cultureContext)
        {
            var imageContent = specializedContent as IImageContent;
            if (imageContent != null)
            {
                string location = urlResolver.ResolveUrlToAbsolute(imageContent.Url, imageContent.Protocol, imageContent.HostName);
                string caption = imageContent.Caption;
                string geoLocation = imageContent.GeoLocation;
                string title = imageContent.Title;
                string license = urlResolver.ResolveUrlToAbsolute(imageContent.LicenseUrl, imageContent.LicenseProtocol, imageContent.LicenseHostName);

                return new PreparedImageContent(location, caption, geoLocation, title, license);
            }

            return null;
        }

        public void Release(IPreparedSpecializedContent preparedSpecializedContent)
        {
            var disposable = preparedSpecializedContent as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }

        public Type ContentType
        {
            get { return typeof(IImageContent); }
        }
    }
}
