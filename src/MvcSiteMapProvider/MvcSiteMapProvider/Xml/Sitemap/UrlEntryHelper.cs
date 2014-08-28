using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    public class UrlEntryHelper
        : IUrlEntryHelper
    {
        public UrlEntryHelper(
            string feedName,
            int skip,
            int take,
            Action<IUrlEntry> sendUrlEntryMethod
            )
        {
            if (string.IsNullOrEmpty(feedName))
                throw new ArgumentNullException("feedName");
            if (sendUrlEntryMethod == null)
                throw new ArgumentNullException("sendUrlEntryMethod");

            this.feedName = feedName;
            this.skip = skip;
            this.take = take;
            this.sendUrlEntryMethod = sendUrlEntryMethod;
        }
        private readonly string feedName;
        private readonly int skip;
        private readonly int take;
        private readonly Action<IUrlEntry> sendUrlEntryMethod;

        public string FeedName
        {
            get { return this.feedName; }
        }

        public int Skip
        {
            get { return this.skip; }
        }

        public int Take
        {
            get { return this.take; }
        }

        public void SendUrlEntry(string url)
        {
            this.SendUrlEntry(this.BuildUrlEntry(url).Create());
        }

        public void SendUrlEntry(string url, string protocol)
        {
            this.SendUrlEntry(this.BuildUrlEntry(url, protocol).Create());
        }

        public void SendUrlEntry(string url, string protocol, string hostName)
        {
            this.SendUrlEntry(this.BuildUrlEntry(url, protocol, hostName).Create());
        }

        public void SendUrlEntry(IUrlEntry urlEntry)
        {
            this.sendUrlEntryMethod(urlEntry);
        }

        public void SendUrlEntry(IUrlEntryBuilder builder)
        {
            this.SendUrlEntry(builder.Create());
        }

        public void SendUrlEntry(Func<IUrlEntry, IUrlEntryBuilder> expression)
        {
            //var builder = new UrlEntryBuilder(
            throw new NotImplementedException();
        }


        public IUrlEntryBuilder BuildUrlEntry(string url)
        {
            return new UrlEntryBuilder(url);
        }

        public IUrlEntryBuilder BuildUrlEntry(string url, string protocol)
        {
            return new UrlEntryBuilder(url, protocol);
        }

        public IUrlEntryBuilder BuildUrlEntry(string url, string protocol, string hostName)
        {
            return new UrlEntryBuilder(url, protocol, hostName);
        }


    }
}
