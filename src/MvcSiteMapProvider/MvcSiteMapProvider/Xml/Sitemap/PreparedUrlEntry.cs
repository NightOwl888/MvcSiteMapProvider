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
            string priority,
            IEnumerable<IPreparedSpecializedContent> specializedContents
            )
        {
            if (string.IsNullOrEmpty(location))
                throw new ArgumentNullException("location");
            //if (string.IsNullOrEmpty(lastModified))
            //    throw new ArgumentNullException("lastModified");
            //if (string.IsNullOrEmpty(changeFrequency))
            //    throw new ArgumentNullException("changeFrequency");
            //if (string.IsNullOrEmpty(priority))
            //    throw new ArgumentNullException("priority");
            //if (specializedContents == null)
            //    throw new ArgumentNullException("specializedContents");

            this.location = location;
            this.lastModified = lastModified;
            this.changeFrequency = changeFrequency;
            this.priority = priority;
            this.specializedContents = specializedContents;
        }
        private readonly string location;
        private readonly string lastModified;
        private readonly string changeFrequency;
        private readonly string priority;
        private readonly IEnumerable<IPreparedSpecializedContent> specializedContents;

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

        public IEnumerable<IPreparedSpecializedContent> SpecializedContents
        {
            get { return this.specializedContents; }
        }
    }
}
