﻿using System;
using MvcSiteMapProvider.Globalization;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized.Image
{
    public class PreparedImageContentFactory
        : IPreparedImageContentFactory
    {
        public IPreparedImageContent Create(IImageContent imageContent, IXmlSitemapUrlResolver urlResolver, ICultureContext cultureContext)
        {
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

        public void Release(IPreparedImageContent preparedImageContent)
        {
            var disposable = preparedImageContent as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }
        }
    }
}
