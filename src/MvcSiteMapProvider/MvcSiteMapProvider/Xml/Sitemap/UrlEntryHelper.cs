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
            int skip,
            int take,
            Action<IUrlEntry> submitUrlEntryMethod
            )
        {
            if (submitUrlEntryMethod == null)
                throw new ArgumentNullException("submitUrlEntryMethod");

            this.skip = skip;
            this.take = take;
            this.submitUrlEntryMethod = submitUrlEntryMethod;
        }
        private readonly int skip;
        private readonly int take;
        private readonly Action<IUrlEntry> submitUrlEntryMethod;

        public int Skip
        {
            get { return this.skip; }
        }

        public int Take
        {
            get { return this.take; }
        }

        public void SubmitUrlEntry(IUrlEntry urlEntry)
        {
            this.submitUrlEntryMethod(urlEntry);
        }

        public void SubmitUrlEntry(IUrlEntryBuilder builder)
        {
            this.SubmitUrlEntry(builder.Create());
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
