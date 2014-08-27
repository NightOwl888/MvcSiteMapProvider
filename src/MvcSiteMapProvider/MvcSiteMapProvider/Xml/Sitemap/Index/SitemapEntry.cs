using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Xml.Sitemap.Index
{
    public class SitemapEntry
        : ISitemapEntry
    {
        public SitemapEntry(
            string location,
            string lastModified
            )
        {
            if (string.IsNullOrEmpty(location))
                throw new ArgumentNullException("location");

            this.location = location;
            this.lastModified = lastModified;
        }
        private readonly string location;
        private readonly string lastModified;

        public string Location
        {
            get { return this.location; }
        }

        public string LastModified
        {
            get { return this.lastModified; }
        }
    }
}
