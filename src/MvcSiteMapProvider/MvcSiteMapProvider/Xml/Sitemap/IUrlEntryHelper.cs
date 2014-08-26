using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    public interface IUrlEntryHelper
    {
        int Skip { get; }
        int Take { get; }
        void SubmitUrlEntry(IUrlEntry urlEntry);
        void SubmitUrlEntry(IUrlEntryBuilder builder);

        IUrlEntryBuilder BuildUrlEntry(string url);
        IUrlEntryBuilder BuildUrlEntry(string url, string protocol);
        IUrlEntryBuilder BuildUrlEntry(string url, string protocol, string hostName);
    }
}
