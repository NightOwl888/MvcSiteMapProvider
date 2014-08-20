using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Xml.Sitemaps.Paging
{
    public class ManualUrlEntryProviderPagingInstruction
    {
        public ManualUrlEntryProviderPagingInstruction(
            int page,
            int skip,
            int take,
            IUrlEntryProvider urlEntryProvider
            )
        {
            if (urlEntryProvider == null)
                throw new ArgumentNullException("urlEntryProvider");

            this.page = page;
            this.skip = skip;
            this.take = take;
            this.urlEntryProvider = urlEntryProvider;
        }
        private readonly int page;
        private readonly int skip;
        private readonly int take;
        private readonly IUrlEntryProvider urlEntryProvider;

        public int Page
        {
            get { return this.page; }
        }

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
