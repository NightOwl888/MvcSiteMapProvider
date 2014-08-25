using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized
{
    public class ImageContentBuilder
        : IImageContentBuilder
    {
        public ImageContentBuilder(string url)
            : this(
            url: url, 
            protocol: string.Empty, 
            hostName: string.Empty, 
            caption: string.Empty, 
            geoLocation: string.Empty, 
            title: string.Empty, 
            licenseUrl: string.Empty, 
            licenseProtocol: string.Empty, 
            licenseHostName: string.Empty)
        {
        }

        private ImageContentBuilder(
            string url, 
            string protocol, 
            string hostName, 
            string caption, 
            string geoLocation, 
            string title, 
            string licenseUrl, 
            string licenseProtocol, 
            string licenseHostName)
        {
            this.url = url;
            this.protocol = protocol;
            this.hostName = hostName;
            this.caption = caption;
            this.geoLocation = geoLocation;
            this.title = title;
            this.licenseUrl = licenseUrl;
            this.licenseProtocol = licenseProtocol;
            this.licenseHostName = licenseHostName;
        }

        private readonly string url;
        private readonly string protocol;
        private readonly string hostName;
        private readonly string caption;
        private readonly string geoLocation;
        private readonly string title;
        private readonly string licenseUrl;
        private readonly string licenseProtocol;
        private readonly string licenseHostName;
        


        public IImageContentBuilder WithProtocol(string protocol)
        {
            return new ImageContentBuilder(this.url, protocol, this.hostName, this.caption, this.geoLocation, this.title, this.licenseUrl, this.licenseProtocol, this.licenseHostName);
        }

        public IImageContentBuilder WithHostName(string hostName)
        {
            return new ImageContentBuilder(this.url, this.protocol, hostName, this.caption, this.geoLocation, this.title, this.licenseUrl, this.licenseProtocol, this.licenseHostName);
        }

        public IImageContentBuilder WithCaption(string caption)
        {
            return new ImageContentBuilder(this.url, this.protocol, this.hostName, caption, this.geoLocation, this.title, this.licenseUrl, this.licenseProtocol, this.licenseHostName);
        }

        public IImageContentBuilder WithGeoLocation(string geoLocation)
        {
            return new ImageContentBuilder(this.url, this.protocol, this.hostName, this.caption, geoLocation, this.title, this.licenseUrl, this.licenseProtocol, this.licenseHostName);
        }

        public IImageContentBuilder WithTitle(string title)
        {
            return new ImageContentBuilder(this.url, this.protocol, this.hostName, this.caption, this.geoLocation, title, this.licenseUrl, this.licenseProtocol, this.licenseHostName);
        }

        public IImageContentBuilder WithLicenseUrl(string licenseUrl)
        {
            return new ImageContentBuilder(this.url, this.protocol, this.hostName, this.caption, this.geoLocation, this.title, licenseUrl, this.licenseProtocol, this.licenseHostName);
        }

        public IImageContentBuilder WithLicenseProtocol(string licenseProtocol)
        {
            return new ImageContentBuilder(this.url, this.protocol, this.hostName, this.caption, this.geoLocation, this.title, this.licenseUrl, licenseProtocol, this.licenseHostName);
        }

        public IImageContentBuilder WithLicenseHostName(string licenseHostName)
        {
            return new ImageContentBuilder(this.url, this.protocol, this.hostName, this.caption, this.geoLocation, this.title, this.licenseUrl, this.licenseProtocol, licenseHostName);
        }

        public IImageContent Create()
        {
            return new ImageContent(this.url)
            {
                Protocol = this.protocol,
                HostName = this.hostName,
                Caption = this.caption,
                GeoLocation = this.geoLocation,
                Title = this.title,
                LicenseUrl = this.licenseUrl,
                LicenseProtocol = this.licenseProtocol,
                LicenseHostName = this.licenseHostName
            };
        }
    }
}
