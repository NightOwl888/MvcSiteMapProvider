using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Xml.Sitemaps
{
    public class IndexEntry
        : IIndexEntry
    {
        public IndexEntry(
            string location
            )
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

        public string LastModified
        {
            get { return string.Empty; }
        }
    }
}
