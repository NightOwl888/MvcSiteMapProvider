using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Xml.Sitemaps
{
    public class UrlEntryHelper
        : IUrlEntryHelper
    {
        public UrlEntryHelper(
            int skip,
            int take,
            Action<IUrlEntry> addUrlEntryMethod
            )
        {
            if (addUrlEntryMethod == null)
                throw new ArgumentNullException("addUrlEntryMethod");

            this.skip = skip;
            this.take = take;
            this.addUrlEntryMethod = addUrlEntryMethod;
        }
        private readonly int skip;
        private readonly int take;
        private readonly Action<IUrlEntry> addUrlEntryMethod;

        public int Skip
        {
            get { return this.skip; }
        }

        public int Take
        {
            get { return this.take; }
        }

        public void AddUrlEntry(IUrlEntry urlEntry)
        {
            this.addUrlEntryMethod(urlEntry);
        }
    }
}
