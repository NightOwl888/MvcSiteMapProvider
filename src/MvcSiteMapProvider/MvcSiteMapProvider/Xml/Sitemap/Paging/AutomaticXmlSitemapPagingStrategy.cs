using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Xml.Sitemap.Paging
{
    public class AutomaticXmlSitemapPagingStrategy
        : IXmlSitemapPagingStrategy
    {
        public AutomaticXmlSitemapPagingStrategy(
            IUrlEntryProvider[] urlEntryProviders,
            IPagingInstructionFactory urlEntryProviderPagingInstructionFactory
            )
        {
            if (urlEntryProviders == null)
                throw new ArgumentNullException("urlEntryProviders");
            if (urlEntryProviderPagingInstructionFactory == null)
                throw new ArgumentNullException("urlEntryProviderPagingInstructionFactory");

            this.urlEntryProviders = urlEntryProviders;
            this.urlEntryProviderPagingInstructionFactory = urlEntryProviderPagingInstructionFactory;

            // This number should be 50,000 in theory, however because the Sitemap protocol
            // states that the maximum file size is 50 MB and there is no reasonable way to
            // calculate the size during streaming, an average cap of 35,000 has been chosen.
            this.MaximumPageSize = 35000;
        }
        private readonly IUrlEntryProvider[] urlEntryProviders;
        private readonly IPagingInstructionFactory urlEntryProviderPagingInstructionFactory;

        public int MaximumPageSize { get; set; }

        public IEnumerable<int> GetPageNumbers()
        {
            int totalRecordCount = 0;
            foreach (var provider in this.urlEntryProviders)
            {
                totalRecordCount += provider.GetTotalRecordCount();
            }

            int pageCount = (int)Math.Ceiling((double)totalRecordCount / this.MaximumPageSize);

            // Return an array from 1 to x
            return Enumerable.Range(1, pageCount);
        }

        public IEnumerable<IPagingInstruction> GetPagingInstructions(int page)
        {
            var result = new List<IPagingInstruction>();

            int startAfter = this.GetStartAfterCount(page, this.MaximumPageSize);

            // Preview what the record counts will be up to and including the current page
            int totalRecordCount = 0;
            int totalTake = 0;
            foreach (var provider in this.urlEntryProviders)
            {
                int currentRecordCount = provider.GetTotalRecordCount();

                // increment the total record count
                totalRecordCount += currentRecordCount;

                if (totalRecordCount > startAfter)
                {
                    int skip = this.GetSkipCount(totalRecordCount, currentRecordCount, startAfter);
                    int take = this.GetTakeCount(currentRecordCount, this.MaximumPageSize, skip, totalTake);

                    // increment the total take
                    totalTake += take;

                    // Create an instruction to send to the provider
                    result.Add(this.urlEntryProviderPagingInstructionFactory.Create(skip, take, provider));
                }

                // Break early if we have reached the maximum number of records needed
                if (totalTake >= this.MaximumPageSize)
                {
                    break;
                }
            }

            return result;
        }

        /// <summary>
        /// Locates the starting point of the page
        /// </summary>
        /// <param name="pageNumber">The page number (starting at 1)</param>
        /// <param name="maximumPageSize">The maximum size of a page.</param>
        /// <returns>The number of the last record from the previous page. If the page number is less than 0, returns int.MaxValue.</returns>
        protected virtual int GetStartAfterCount(int pageNumber, int maximumPageSize)
        {
            return pageNumber < 1 ? int.MaxValue : (pageNumber * maximumPageSize) - maximumPageSize;
        }

        /// <summary>
        /// Gets the number of rows to skip for the current provider.
        /// </summary>
        /// <param name="totalRecordCount">The aggregate total number of rows from the start to the current provider.</param>
        /// <param name="currentRecordCount">The number of rows from the current provider.</param>
        /// <param name="startAfter">The starting point of nodes to take based on the total row count.</param>
        /// <returns></returns>
        protected virtual int GetSkipCount(int totalRecordCount, int currentRecordCount, int startAfter)
        {
            // Calculate the current position relative to the starting point
            int rowsSinceStart = (totalRecordCount - startAfter);
            int skip = (rowsSinceStart < currentRecordCount) ? (currentRecordCount - rowsSinceStart) : 0;

            return skip;
        }

        /// <summary>
        /// Gets the number of rows take from the current provider.
        /// </summary>
        /// <param name="currentRecordCount">The number of rows from the current provider.</param>
        /// <param name="maximumPageSize">The maximum size of a page.</param>
        /// <param name="skip">The number of records to skip for the current provider.</param>
        /// <param name="totalTake">The aggregate number of rows taken already by other providers.</param>
        /// <returns></returns>
        protected virtual int GetTakeCount(int currentRecordCount, int maximumPageSize, int skip, int totalTake)
        {
            int take = (maximumPageSize - totalTake);
            if (take > currentRecordCount)
            {
                take = currentRecordCount;
            }
            if (skip > 0)
            {
                take = currentRecordCount - skip;
                if (take > maximumPageSize)
                {
                    take = maximumPageSize;
                }
            }
            
            return take;
        }
    }
}
