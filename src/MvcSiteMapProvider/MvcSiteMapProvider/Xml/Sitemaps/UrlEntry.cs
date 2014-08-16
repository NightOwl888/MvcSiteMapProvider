using System;
using System.Collections.Generic;
using MvcSiteMapProvider.Xml.Sitemaps.Specialized;

namespace MvcSiteMapProvider.Xml.Sitemaps
{
    public class UrlEntry
        : IUrlEntry
    {
        public UrlEntry(
            string location,
            IEnumerable<ISpecializedContent> specializedContents
            )
        {
            if (string.IsNullOrEmpty(location))
                throw new ArgumentNullException("location");
            if (specializedContents == null)
                throw new ArgumentNullException("specializedContents");
            this.location = location;
            this.specializedContents = specializedContents;
        }
        private readonly string location;
        private readonly IEnumerable<ISpecializedContent> specializedContents;

        public string Location
        {
            get { return this.location; }
        }

        public string LastModified
        {
            get { return string.Empty; }
        }

        public string ChangeFrequency
        {
            get { return string.Empty; }
        }

        public string Priority
        {
            get { return string.Empty; }
        }

        public IEnumerable<ISpecializedContent> SpecializedContents
        {
            get { return this.specializedContents; }
        }
    }
}
