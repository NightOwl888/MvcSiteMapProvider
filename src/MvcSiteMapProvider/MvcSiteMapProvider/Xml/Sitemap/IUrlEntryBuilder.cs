using System;
using System.Collections.Generic;
using System.Linq;
using MvcSiteMapProvider.ComponentModel;
using MvcSiteMapProvider.Xml.Sitemap.Specialized;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    public interface IUrlEntryBuilder
        : IFluentInterface
    {
        //IUrlEntryBuilder WithUrl(string url);
        //IUrlEntryBuilder WithProtocol(string protocol);
        //IUrlEntryBuilder WithHostName(string hostName);
        IUrlEntryBuilder WithLastModifiedDate(DateTime lastModifiedDate);
        IUrlEntryBuilder WithChangeFrequency(ChangeFrequency changeFrequency);
        IUrlEntryBuilder WithUpdatePriority(UpdatePriority updatePriority);

        IUrlEntryBuilder AddContent(Func<ISpecializedContentStarter, ISpecializedContentBuilder> expression);
        IUrlEntryBuilder AddContent(ISpecializedContent content);

        IUrlEntry Create();
    }
}
