using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    public class UrlEntryHelperFactory
        : IUrlEntryHelperFactory
    {
        public IUrlEntryHelper Create(string feedName, int skip, int take,Action<IUrlEntry> addUrlEntryMethod)
        {
            return new UrlEntryHelper(feedName, skip, take, addUrlEntryMethod);
        }
    }
}
