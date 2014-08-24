using System;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized
{
    public class ImageContent
        : IImageContent
    {
        public ImageContent(
            string location)
        {
            if (string.IsNullOrEmpty(location))
                throw new ArgumentNullException("location");
            this.location = location;
        }
        private readonly string location;

        //public string Location
        //{
        //    get { return this.location; }
        //}

        //public string Caption
        //{
        //    get { return string.Empty; }
        //}

        //public string GeoLocation
        //{
        //    get { return string.Empty; }
        //}

        //public string Title
        //{
        //    get { return string.Empty; }
        //}

        //public string License
        //{
        //    get { return string.Empty; }
        //}

        public string Url
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string HostName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string Protocol
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string Caption
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string GeoLocation
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string Title
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string LicenseUrl
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string LicenseHostName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string LicenseProtocol
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
