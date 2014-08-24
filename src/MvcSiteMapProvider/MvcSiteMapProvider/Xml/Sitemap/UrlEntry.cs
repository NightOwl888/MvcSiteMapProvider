using System;
using System.Collections.Generic;
using MvcSiteMapProvider.Xml.Sitemap.Specialized;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    public class UrlEntry
        : IUrlEntry
    {
        public UrlEntry()
        {
            this.UpdatePriority = UpdatePriority.Undefined;
            this.ChangeFrequency = ChangeFrequency.Undefined;
            this.LastModifiedDate = DateTime.MinValue;
        }

        //public UrlEntry(
        //    string location,
        //    IEnumerable<ISpecializedContent> specializedContents
        //    )
        //{
        //    if (string.IsNullOrEmpty(location))
        //        throw new ArgumentNullException("location");
        //    if (specializedContents == null)
        //        throw new ArgumentNullException("specializedContents");
        //    this.location = location;
        //    this.specializedContents = specializedContents;
        //}
        //private readonly string location;
        //private readonly IEnumerable<ISpecializedContent> specializedContents;

        //public string Location
        //{
        //    get { return this.location; }
        //}

        //public string LastModified
        //{
        //    get { return string.Empty; }
        //}

        //public string ChangeFrequency
        //{
        //    get { return string.Empty; }
        //}

        //public string Priority
        //{
        //    get { return string.Empty; }
        //}

        //public IEnumerable<ISpecializedContent> SpecializedContents
        //{
        //    get { return this.specializedContents; }
        //}
        public string Protocol
        {
            get;
            set;
        }

        public string HostName
        {
            get;
            set;
        }

        public string Url
        {
            get;
            set;
        }

        public DateTime LastModifiedDate
        {
            get;
            set;
        }

        public ChangeFrequency ChangeFrequency
        {
            get;
            set;
        }

        public UpdatePriority UpdatePriority
        {
            get;
            set;
        }

        public IList<ISpecializedContent> SpecializedContents
        {
            get { return new List<ISpecializedContent>(); }
        }
    }
}
