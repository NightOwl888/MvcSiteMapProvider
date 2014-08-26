using System;
using System.Collections.Generic;
using System.Linq;
using MvcSiteMapProvider.Xml.Sitemap.Specialized;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    public class UrlEntryBuilder
        : IUrlEntryBuilder
    {
        public UrlEntryBuilder(string url)
            : this(
            url: url, 
            protocol: string.Empty, 
            hostName: string.Empty, 
            lastModifiedDate: DateTime.MinValue, 
            changeFrequency: ChangeFrequency.Undefined,
            updatePriority: UpdatePriority.Undefined,
            specializedContent: new List<ISpecializedContent>())
        {
        }

        public UrlEntryBuilder(string url, string protocol)
            : this(
            url: url,
            protocol: protocol,
            hostName: string.Empty,
            lastModifiedDate: DateTime.MinValue,
            changeFrequency: ChangeFrequency.Undefined,
            updatePriority: UpdatePriority.Undefined,
            specializedContent: new List<ISpecializedContent>())
        {
        }

        public UrlEntryBuilder(string url, string protocol, string hostName)
            : this(
            url: url,
            protocol: protocol,
            hostName: hostName,
            lastModifiedDate: DateTime.MinValue,
            changeFrequency: ChangeFrequency.Undefined,
            updatePriority: UpdatePriority.Undefined,
            specializedContent: new List<ISpecializedContent>())
        {
        }

        private UrlEntryBuilder(
            string url, 
            string protocol, 
            string hostName, 
            DateTime lastModifiedDate, 
            ChangeFrequency changeFrequency, 
            UpdatePriority updatePriority,
            IList<ISpecializedContent> specializedContent)
        {
            this.url = url;
            this.protocol = protocol;
            this.hostName = hostName;
            this.lastModifiedDate = lastModifiedDate;
            this.changeFrequency = changeFrequency;
            this.updatePriority = updatePriority;
            this.specializedContent = specializedContent;
        }

        private readonly string url;
        private readonly string protocol;
        private readonly string hostName;
        private readonly DateTime lastModifiedDate;
        private readonly ChangeFrequency changeFrequency;
        private readonly UpdatePriority updatePriority;
        private readonly IList<ISpecializedContent> specializedContent;

        //public IUrlEntryBuilder WithUrl(string url)
        //{
        //    return new UrlEntryBuilder(url, this.protocol, this.hostName, this.lastModifiedDate, this.changeFrequency, this.updatePriority);
        //}

        //public IUrlEntryBuilder WithProtocol(string protocol)
        //{
        //    return new UrlEntryBuilder(this.url, protocol, this.hostName, this.lastModifiedDate, this.changeFrequency, this.updatePriority);
        //}

        //public IUrlEntryBuilder WithHostName(string hostName)
        //{
        //    return new UrlEntryBuilder(this.url, this.protocol, hostName, this.lastModifiedDate, this.changeFrequency, this.updatePriority);
        //}

        public IUrlEntryBuilder WithLastModifiedDate(DateTime lastModifiedDate)
        {
            return new UrlEntryBuilder(this.url, this.protocol, this.hostName, lastModifiedDate, this.changeFrequency, this.updatePriority, this.specializedContent);
        }

        public IUrlEntryBuilder WithChangeFrequency(ChangeFrequency changeFrequency)
        {
            return new UrlEntryBuilder(this.url, this.protocol, this.hostName, this.lastModifiedDate, changeFrequency, this.updatePriority, this.specializedContent);
        }

        public IUrlEntryBuilder WithUpdatePriority(UpdatePriority updatePriority)
        {
            return new UrlEntryBuilder(this.url, this.protocol, this.hostName, this.lastModifiedDate, this.changeFrequency, updatePriority, this.specializedContent);
        }

        public IUrlEntryBuilder AddContent(Func<ISpecializedContentStarter, ISpecializedContentBuilder> expression)
        {
            var starter = new SpecializedContentStarter();
            var builder = expression(starter);
            return this.AddContent(builder.Create());
        }

        public IUrlEntryBuilder AddContent(ISpecializedContent content)
        {
            this.specializedContent.Add(content);
            return new UrlEntryBuilder(this.url, this.protocol, this.hostName, this.lastModifiedDate, this.changeFrequency, updatePriority, this.specializedContent);
        }

        public IUrlEntry Create()
        {
            return new UrlEntry(this.url, this.specializedContent)
            {
                Protocol = this.protocol,
                HostName = this.hostName,
                LastModifiedDate = this.lastModifiedDate,
                ChangeFrequency = this.changeFrequency,
                UpdatePriority = this.updatePriority
            };
        }
    }
}
