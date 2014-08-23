using System;

namespace MvcSiteMapProvider.Xml.Sitemap.Paging
{
    public class PagingInstruction
        : IPagingInstruction
    {
        public PagingInstruction(
            int skip,
            int take,
            IUrlEntryProvider urlEntryProvider
            )
        {
            if (urlEntryProvider == null)
                throw new ArgumentNullException("urlEntryProvider");

            this.skip = skip;
            this.take = take;
            this.urlEntryProvider = urlEntryProvider;
        }
        private readonly int skip;
        private readonly int take;
        private readonly IUrlEntryProvider urlEntryProvider;

        public int Skip
        {
            get { return this.skip; }
        }

        public int Take
        {
            get { return this.take; }
        }

        public IUrlEntryProvider UrlEntryProvider
        {
            get { return this.urlEntryProvider; }
        }
    }
}
