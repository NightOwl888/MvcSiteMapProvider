using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized.Video
{
    public class PreparedVideoContent
        : IPreparedVideoContent
    {
        // TODO: Finish implementation with additional properties
        public PreparedVideoContent(
            string location,
            string video,
            string thumbnailLocation,
            string title,
            string description)
        {
            if (string.IsNullOrEmpty(location))
                throw new ArgumentNullException("location");
            this.location = location;
            this.video = video;
            this.thumbnailLocation = thumbnailLocation;
            this.title = title;
            this.description = description;
        }
        private readonly string location;
        private readonly string video;
        private readonly string thumbnailLocation;
        private readonly string title;
        private readonly string description;

        public string Location
        {
            get { return this.location; }
        }

        public string Video
        {
            get { return this.video; }
        }

        public string ThumbnailLocation
        {
            get { return this.thumbnailLocation; }
        }

        public string Title
        {
            get { return this.title; }
        }

        public string Description
        {
            get { return this.description; }
        }
    }
}
