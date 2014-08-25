using System;
using System.Collections.Generic;
using MvcSiteMapProvider.Xml.Sitemap.Specialized;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    public class UrlEntry
        : IUrlEntry
    {
        public UrlEntry(string url)
        {
            this.Url = url;
            this.UpdatePriority = UpdatePriority.Undefined;
            this.ChangeFrequency = ChangeFrequency.Undefined;
            this.LastModifiedDate = DateTime.MinValue;
            this.specializedContent = new List<ISpecializedContent>();
        }
        private readonly IList<ISpecializedContent> specializedContent;


        public string Protocol { get; set; }

        public string HostName { get; set; }

        public string Url { get; set; }

        public DateTime LastModifiedDate { get; set; }

        public ChangeFrequency ChangeFrequency { get; set; }

        public UpdatePriority UpdatePriority { get; set; }

        public IList<ISpecializedContent> SpecializedContents
        {
            get { return this.specializedContent; }
        }
    }
}
