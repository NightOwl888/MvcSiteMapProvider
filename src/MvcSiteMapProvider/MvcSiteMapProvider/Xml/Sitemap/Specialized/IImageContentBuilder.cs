using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized
{
    public interface IImageContentBuilder
    {
        IImageContentBuilder WithProtocol(string protocol);
        IImageContentBuilder WithHostName(string hostName);
        IImageContentBuilder WithCaption(string caption);
        IImageContentBuilder WithGeoLocation(string geoLocation);
        IImageContentBuilder WithTitle(string title);
        IImageContentBuilder WithLicenseUrl(string licenseUrl);
        IImageContentBuilder WithLicenseProtocol(string licenseProtocol);
        IImageContentBuilder WithLicenseHostName(string licenseHostName);
        IImageContent Create();
    }
}
