using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
            updatePriority: UpdatePriority.Undefined)
        {
        }

        private UrlEntryBuilder(
            string url, 
            string protocol, 
            string hostName, 
            DateTime lastModifiedDate, 
            ChangeFrequency changeFrequency, 
            UpdatePriority updatePriority)
        {
            this.url = url;
            this.protocol = protocol;
            this.hostName = hostName;
            this.lastModifiedDate = lastModifiedDate;
            this.changeFrequency = changeFrequency;
            this.updatePriority = updatePriority;
        }

        private readonly string url;
        private readonly string protocol;
        private readonly string hostName;
        private readonly DateTime lastModifiedDate;
        private readonly ChangeFrequency changeFrequency;
        private readonly UpdatePriority updatePriority;

        //public IUrlEntryBuilder WithUrl(string url)
        //{
        //    return new UrlEntryBuilder(url, this.protocol, this.hostName, this.lastModifiedDate, this.changeFrequency, this.updatePriority);
        //}

        public IUrlEntryBuilder WithProtocol(string protocol)
        {
            return new UrlEntryBuilder(this.url, protocol, this.hostName, this.lastModifiedDate, this.changeFrequency, this.updatePriority);
        }

        public IUrlEntryBuilder WithHostName(string hostName)
        {
            return new UrlEntryBuilder(this.url, this.protocol, hostName, this.lastModifiedDate, this.changeFrequency, this.updatePriority);
        }

        public IUrlEntryBuilder WithLastModifiedDate(DateTime lastModifiedDate)
        {
            return new UrlEntryBuilder(this.url, this.protocol, this.hostName, lastModifiedDate, this.changeFrequency, this.updatePriority);
        }

        public IUrlEntryBuilder WithChangeFrequency(ChangeFrequency changeFrequency)
        {
            return new UrlEntryBuilder(this.url, this.protocol, this.hostName, this.lastModifiedDate, changeFrequency, this.updatePriority);
        }

        public IUrlEntryBuilder WithUpdatePriority(UpdatePriority updatePriority)
        {
            return new UrlEntryBuilder(this.url, this.protocol, this.hostName, this.lastModifiedDate, this.changeFrequency, updatePriority);
        }

        public IUrlEntry Create()
        {
            return new UrlEntry(this.url)
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
