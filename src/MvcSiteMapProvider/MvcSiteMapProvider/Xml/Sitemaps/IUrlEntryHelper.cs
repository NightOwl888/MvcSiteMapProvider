using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Xml.Sitemaps
{
    public interface IUrlEntryHelper
    {
        int Skip { get; }
        int Take { get; }
        void AddUrlEntry(IUrlEntry urlEntry);
    }
}
