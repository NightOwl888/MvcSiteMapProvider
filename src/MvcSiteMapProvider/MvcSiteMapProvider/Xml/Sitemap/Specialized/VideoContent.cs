using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized
{
    public class VideoContent
        : IVideoContent
    {
        public VideoContent(
            string url)
        {
            if (string.IsNullOrEmpty(url))
                throw new ArgumentNullException("url");
            this.url = url;
        }
        private string url;

        public string Url 
        {
            get { return this.url; }
            set { this.url = value; }
        }

        public string HostName { get; set; }

        public string Protocol { get; set; }

        public string Video { get; set; }

        public string ThumbnailUrl { get; set; }

        public string ThumbnailHostName { get; set; }

        public string ThumbnailProtocol { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }
    }
}
