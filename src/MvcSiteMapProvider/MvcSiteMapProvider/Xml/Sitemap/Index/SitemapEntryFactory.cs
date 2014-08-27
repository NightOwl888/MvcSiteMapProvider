using System;
using System.Globalization;

namespace MvcSiteMapProvider.Xml.Sitemap.Index
{
    public class SitemapEntryFactory
        : ISitemapEntryFactory
    {
        private const string W3CDateFormat = "yyyy-MM-ddTHH:mm:ss.fffffffzzz";

        public ISitemapEntry Create(string location)
        {
            return this.Create(location, DateTime.MinValue);
        }

        public ISitemapEntry Create(string location, DateTime lastModified)
        {
            var lastModifiedString = 
                lastModified > DateTime.MinValue ? 
                lastModified.ToString(W3CDateFormat, CultureInfo.InvariantCulture) : 
                string.Empty;

            return new SitemapEntry(location, lastModifiedString);
        }
    }
}
