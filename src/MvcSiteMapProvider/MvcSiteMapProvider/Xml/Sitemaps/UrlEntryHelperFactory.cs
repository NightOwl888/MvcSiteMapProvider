using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Xml.Sitemaps
{
    public class UrlEntryHelperFactory
        : IUrlEntryHelperFactory
    {
        public IUrlEntryHelper Create(int skip, int take, Action<IUrlEntry> addUrlEntryMethod)
        {
            return new UrlEntryHelper(skip, take, addUrlEntryMethod);
        }
    }
}
