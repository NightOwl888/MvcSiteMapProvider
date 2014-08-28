using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    public interface IUrlEntryHelper
    {
        string FeedName { get; }
        int Skip { get; }
        int Take { get; }
        void SendUrlEntry(string url);
        void SendUrlEntry(string url, string protocol);
        void SendUrlEntry(string url, string protocol, string hostName);
        void SendUrlEntry(IUrlEntry urlEntry);
        void SendUrlEntry(IUrlEntryBuilder builder);
        void SendUrlEntry(Func<IUrlEntry, IUrlEntryBuilder> expression);

        IUrlEntryBuilder BuildUrlEntry(string url);
        IUrlEntryBuilder BuildUrlEntry(string url, string protocol);
        IUrlEntryBuilder BuildUrlEntry(string url, string protocol, string hostName);
    }
}
