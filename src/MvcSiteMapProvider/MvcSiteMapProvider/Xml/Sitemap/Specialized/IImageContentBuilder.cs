using System;
using MvcSiteMapProvider.ComponentModel;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized
{
    public interface IImageContentBuilder
        : ISpecializedContentBuilder, IFluentInterface
    {
        //IImageContentBuilder WithProtocol(string protocol);
        //IImageContentBuilder WithHostName(string hostName);
        IImageContentBuilder WithCaption(string caption);
        IImageContentBuilder WithGeoLocation(string geoLocation);
        IImageContentBuilder WithTitle(string title);
        IImageContentBuilder WithLicenseUrl(string licenceUrl);
        IImageContentBuilder WithLicenseUrl(string licenceUrl, string licenceUrlProtocol);
        IImageContentBuilder WithLicenseUrl(string licenceUrl, string licenceUrlProtocol, string licenceUrlHostName);
        //IImageContentBuilder WithLicenseProtocol(string licenseProtocol);
        //IImageContentBuilder WithLicenseHostName(string licenseHostName);
        //IImageContent Create();
    }
}
