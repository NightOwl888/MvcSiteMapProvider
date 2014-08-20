using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Xml.Sitemaps.Paging
{
    public class AutomaticSitemapsPagingStrategy
        : ISitemapsPagingStrategy
    {
        public AutomaticSitemapsPagingStrategy(
            IUrlEntryProvider[] urlEntryProviders,
            IUrlEntryProviderPagingInstructionFactory urlEntryProviderPagingInstructionFactory
            )
        {
            if (urlEntryProviders == null)
                throw new ArgumentNullException("urlEntryProviders");
            if (urlEntryProviderPagingInstructionFactory == null)
                throw new ArgumentNullException("urlEntryProviderPagingInstructionFactory");

            this.urlEntryProviders = urlEntryProviders;
            this.urlEntryProviderPagingInstructionFactory = urlEntryProviderPagingInstructionFactory;

            // This number should be 50,000 in theory, however because the Sitemaps protocol
            // states that the maximum file size is 50 MB and there is no reasonable way to
            // calculate the size during streaming, an average cap of 35,000 has been chosen.
            this.MaximumPageSize = 35000;
        }
        private readonly IUrlEntryProvider[] urlEntryProviders;
        private readonly IUrlEntryProviderPagingInstructionFactory urlEntryProviderPagingInstructionFactory;

        public int MaximumPageSize { get; set; }

        public IEnumerable<int> GetIndexPageNumbers()
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

        public IEnumerable<IUrlEntryProviderPagingInstruction> GetPagingInstructions(int page)
        {
            var result = new List<IUrlEntryProviderPagingInstruction>();

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





        //public IEnumerable<IUrlEntryProviderPagingInstruction> GetPagingInstructions(int page)
        //{
        //    //int minimumRequiredRecords = (page * this.MaximumPageSize);

        //    //// If the page is 0 (the default), assume this is the first page
        //    //if (minimumRequiredRecords == 0)
        //    //{
        //    //    minimumRequiredRecords = this.MaximumPageSize;
        //    //}

        //    //var providerToRecordCountMap = new Dictionary<IUrlEntryProvider, int>();
        //    var result = new List<IUrlEntryProviderPagingInstruction>();

        //    int startAfter = this.GetStartAfterCount(page, this.MaximumPageSize);

        //    // Preview what the record counts will be up to and including the current page
        //    int totalRecordCount = 0;
        //    int totalTake = 0;
        //    foreach (var provider in this.urlEntryProviders)
        //    {
        //        int currentRecordCount = provider.GetTotalRecordCount();

        //        //providerToRecordCountMap.Add(provider, currentRecordCount);

        //        // increment the total record count
        //        totalRecordCount += currentRecordCount;

        //        if (totalRecordCount > startAfter)
        //        {
        //            int skip = this.GetSkipCount(totalRecordCount, currentRecordCount, startAfter);
        //            int take = this.GetTakeCount(currentRecordCount, this.MaximumPageSize, skip, totalTake);

        //            // increment the total take
        //            totalTake += take;

        //            // TODO: inject a factory for paging instructions
        //            result.Add(new UrlEntryProviderPagingInstruction(skip, take, provider));
        //        }

        //        // Break early if we have reached the maximum number of records needed
        //        if (totalTake >= this.MaximumPageSize)
        //        {
        //            break;
        //        }


        //        //// Break early if we have reached the maximum number of records needed
        //        //if (totalRecordCount > minimumRequiredRecords)
        //        //{
        //        //    break;
        //        //}
        //    }

        //    ////int lowerBound = page == 0 ? 0 : ((page + 1) * this.MaximumPageSize) - MaximumPageSize;
        //    ////int upperBound = page == 0 ? this.MaximumPageSize : 

        //    //int globalSkip = pageIndex == 0 ? 0 : ((pageIndex + 1) * this.MaximumPageSize) - this.MaximumPageSize;

        //    ////int globalTake = pageIndex == 0 ? this.MaximumPageSize : (pageIndex + 1) * this.MaximumPageSize;
        //    //var result = new List<IUrlEntryProviderPagingInstruction>();

        //    //totalRecordCount = 0;

        //    //// Now loop through again and create instructions for the current page
        //    //foreach (var providerMap in providerToRecordCountMap)
        //    //{
        //    //    // TODO: inject a factory for paging instructions

        //    //    totalRecordCount += providerMap.Value;

        //    //    //int skip = totalRecordCount -

        //    //    // Find the providers we need to call based on counts and current page.
        //    //    if (totalRecordCount > globalSkip + 1)
        //    //    {
        //    //        int skip = globalSkip; // TODO: work out what the skip value has to be here
        //    //        int take = this.MaximumPageSize;
        //    //        if (providerMap.Value > this.MaximumPageSize)
        //    //        {
        //    //            // we now know there will only need to be 

        //    //            result.Add(new UrlEntryProviderPagingInstruction(skip, take, providerMap.Key));
        //    //            break;
        //    //        }
        //    //        else
        //    //        {

        //    //        }

        //    //        //if (totalRecordCount + 
        //    //    }

        //    //    //bool isPartialPageMatch = providerMap.Value > this.MaximumPageSize;
        //    //    //bool isCompletePageMatch = 


        //    //}

        //    return result;
        //}




        //public IEnumerable<IUrlEntryProviderPagingInstruction> GetPagingInstructions(int pageIndex)
        //{
        //    int minimumRequiredRecords = (pageIndex * this.MaximumPageSize);

        //    // If the page is 0 (the default), assume this is the first page or index
        //    if (minimumRequiredRecords == 0)
        //    {
        //        minimumRequiredRecords = this.MaximumPageSize;
        //    }

        //    var providerToRecordCountMap = new Dictionary<IUrlEntryProvider, int>();

        //    // Preview what the record counts will be up to and including the current page
        //    int totalRecordCount = 0;
        //    for (int i = 0; i < this.urlEntryProviders.Length; i++)
        //    {
        //        var provider = this.urlEntryProviders[i];
        //        int currentRecordCount = provider.GetTotalRecordCount();

        //        providerToRecordCountMap.Add(provider, currentRecordCount);

        //        // increment the total record count
        //        totalRecordCount += currentRecordCount;

        //        // Break early if we have reached the maximum number of records needed
        //        // only if this isn't the very first provider and we are requesting the 
        //        // first page (page 0). In that case, we want to get all of the counts to determine
        //        // how many pages to put in the index.
        //        if (totalRecordCount > minimumRequiredRecords)
        //        {
        //            if (!(i == 0 && pageIndex == 0)) 
        //            {
        //                break;
        //            }
        //        }
        //    }

        //    // If page 0 was requested, and we have more records than the maximum page size, 
        //    // switch to using an index page.
        //    if (pageIndex == 0 && providerToRecordCountMap.Count > 0 && providerToRecordCountMap.Values.ElementAt(0) > this.MaximumPageSize)
        //    {
        //        // TODO: work out how to pass the page information for the index back to the sitemaps service.
        //        // NOTE: maybe there should be a class that wraps this one to determine the page vs index logic because that part won't generally change
        //    }

        //    // If we made it this far, we aren't generating the index page.

        //    //int lowerBound = page == 0 ? 0 : ((page + 1) * this.MaximumPageSize) - MaximumPageSize;
        //    //int upperBound = page == 0 ? this.MaximumPageSize : 

        //    int globalSkip = pageIndex == 0 ? 0 : ((pageIndex + 1) * this.MaximumPageSize) - (MaximumPageSize + 1);
        //    if (globalSkip < 0)
        //    {
        //        globalSkip = 0;
        //    }
        //    //int globalTake = pageIndex == 0 ? this.MaximumPageSize : (pageIndex + 1) * this.MaximumPageSize;
        //    var result = new List<IUrlEntryProviderPagingInstruction>();

        //    totalRecordCount = 0;

        //    // Now loop through again and create instructions for the current page
        //    foreach (var providerMap in providerToRecordCountMap)
        //    {
        //        // TODO: inject a factory for paging instructions

        //        totalRecordCount += providerMap.Value;

        //        //int skip = totalRecordCount -

        //        // Find the providers we need to call based on counts and current page.
        //        if (totalRecordCount > globalSkip + 1)
        //        {
        //            int skip = globalSkip; // TODO: work out what the skip value has to be here
        //            int take = this.MaximumPageSize;
        //            if (providerMap.Value > this.MaximumPageSize)
        //            {
        //                // we now know there will only need to be 

        //                result.Add(new UrlEntryProviderPagingInstruction(skip, take, providerMap.Key));
        //                break;
        //            }
        //            else
        //            {

        //            }

        //            //if (totalRecordCount + 
        //        }

        //        //bool isPartialPageMatch = providerMap.Value > this.MaximumPageSize;
        //        //bool isCompletePageMatch = 


        //    }

        //    return result;
        //}

        //private void CalculatePages(int maximumPageSize, 


        //private class ProviderRecordCountMap
        //{
        //    public ProviderRecordCountMap(
        //        IUrlEntryProvider provider,
        //        int 
        //        )
        //    {

        //    }

        //    public IUrlEntryProvider Provider { return this.Provider; }
        //}



    }
}
