using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Xml.Sitemap.Paging
{
    public class ManualSitemapPagingStrategy
        : ISitemapPagingStrategy
    {
        public ManualSitemapPagingStrategy(
            IManualPagingInstruction[] manualUrlEntryProviderPagingInstructions
            )
        {
            if (manualUrlEntryProviderPagingInstructions == null)
                throw new ArgumentNullException("manualUrlEntryProviderPagingInstructions");

            this.manualUrlEntryProviderPagingInstructions = manualUrlEntryProviderPagingInstructions;

            // This number should be 50,000 in theory, however because the Sitemap protocol
            // states that the maximum file size is 50 MB and there is no reasonable way to
            // calculate the size during streaming, an average cap of 35,000 has been chosen.
            this.MaximumPageSize = 35000;
        }
        private readonly IManualPagingInstruction[] manualUrlEntryProviderPagingInstructions;

        public int MaximumPageSize{ get; set; }

        public IEnumerable<int> GetIndexPageNumbers()
        {
            // Get a distinct list of page numbers and return them as an array
            return this.manualUrlEntryProviderPagingInstructions
                .Select(x => x.Page)
                .Distinct()
                .ToArray();
        }

        public IEnumerable<IPagingInstruction> GetPagingInstructions(int page)
        {
            return this.manualUrlEntryProviderPagingInstructions
                .Where(x => x.Page == page)
                .ToList();
        }
    }
}
