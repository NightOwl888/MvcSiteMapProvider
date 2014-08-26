using System;
using System.Collections.Generic;
using MvcSiteMapProvider.Xml.Sitemap.Specialized;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    public class UrlEntry
        : IUrlEntry
    {
        public UrlEntry(string url)
            : this(url, null, null, new List<ISpecializedContent>())
        {
        }

        public UrlEntry(string url, string protocol)
            : this(url, protocol, null, new List<ISpecializedContent>())
        {
        }

        public UrlEntry(string url, string protocol, string hostName)
            : this(url, protocol, hostName, new List<ISpecializedContent>())
        {
        }

        internal UrlEntry(string url, IList<ISpecializedContent> specializedContent)
            : this(url, null, null, specializedContent)
        {
        }

        private UrlEntry(string url, string protocol, string hostName, IList<ISpecializedContent> specializedContent)
        {
            this.Url = url;
            this.specializedContent = specializedContent;

            // Set defaults
            this.UpdatePriority = UpdatePriority.Undefined;
            this.ChangeFrequency = ChangeFrequency.Undefined;
            this.LastModifiedDate = DateTime.MinValue;
        }
        private readonly IList<ISpecializedContent> specializedContent;


        public string Protocol { get; set; }

        public string HostName { get; set; }

        public string Url { get; set; }

        public DateTime LastModifiedDate { get; set; }

        public ChangeFrequency ChangeFrequency { get; set; }

        public UpdatePriority UpdatePriority { get; set; }

        public IList<ISpecializedContent> SpecializedContent
        {
            get { return this.specializedContent; }
        }
    }
}
