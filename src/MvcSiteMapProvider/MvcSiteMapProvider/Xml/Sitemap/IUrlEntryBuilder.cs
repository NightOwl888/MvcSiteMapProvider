using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    public interface IUrlEntryBuilder
    {
        //IUrlEntryBuilder WithUrl(string url);
        IUrlEntryBuilder WithProtocol(string protocol);
        IUrlEntryBuilder WithHostName(string hostName);
        IUrlEntryBuilder WithLastModifiedDate(DateTime lastModifiedDate);
        IUrlEntryBuilder WithChangeFrequency(ChangeFrequency changeFrequency);
        IUrlEntryBuilder WithUpdatePriority(UpdatePriority updatePriority);


        IUrlEntry Create();
    }
}
