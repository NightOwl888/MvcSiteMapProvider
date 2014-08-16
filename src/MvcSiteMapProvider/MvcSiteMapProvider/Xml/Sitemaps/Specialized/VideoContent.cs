using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Xml.Sitemaps.Specialized
{
    public class VideoContent
        : IVideoContent
    {
        public VideoContent(
            string location)
        {
            if (string.IsNullOrEmpty(location))
                throw new ArgumentNullException("location");
            this.location = location;
        }
        private readonly string location;

        public string Location
        {
            get { return this.location; }
        }

        public string Video
        {
            get { return string.Empty; }
        }

        public string ThumbnailLocation
        {
            get { return string.Empty; }
        }

        public string Title
        {
            get { return string.Empty; }
        }

        public string Description
        {
            get { return string.Empty; }
        }
    }
}
