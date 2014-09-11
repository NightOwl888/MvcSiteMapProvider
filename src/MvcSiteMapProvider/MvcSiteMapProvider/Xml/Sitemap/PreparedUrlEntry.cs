using System;
using System.Collections.Generic;
using MvcSiteMapProvider.Xml.Sitemap.Specialized;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    public class PreparedUrlEntry
        : IPreparedUrlEntry
    {
        public PreparedUrlEntry(
            string location,
            string lastModified,
            string changeFrequency,
            string priority
            )
        {
            if (string.IsNullOrEmpty(location))
                throw new ArgumentNullException("location");

            this.location = location;
            this.lastModified = lastModified;
            this.changeFrequency = changeFrequency;
            this.priority = priority;
        }
        private readonly string location;
        private readonly string lastModified;
        private readonly string changeFrequency;
        private readonly string priority;

        public string Location
        {
            get { return this.location; }
        }

        public string LastModified
        {
            get { return this.lastModified; }
        }

        public string ChangeFrequency
        {
            get { return this.changeFrequency; }
        }

        public string Priority
        {
            get { return this.priority; }
        }
    }
}
