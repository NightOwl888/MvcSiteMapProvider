using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Xml.Sitemap.Paging
{
    public class XmlSitemapPager
        : IXmlSitemapPager
    {
        public XmlSitemapPager(
            IPagingInstructionFactory pagingInstructionFactory,
            IXmlSitemapPageInfoFactory xmlSitemapPageInfoFactory,
            IXmlSitemapPageDataFactory xmlSitemapPageDataFactory
            )
        {
            if (pagingInstructionFactory == null)
                throw new ArgumentNullException("pagingInstructionFactory");
            if (xmlSitemapPageInfoFactory == null)
                throw new ArgumentNullException("xmlSitemapPageInfoFactory");
            if (xmlSitemapPageDataFactory == null)
                throw new ArgumentNullException("xmlSitemapPageDataFactory");

            this.pagingInstructionFactory = pagingInstructionFactory;
            this.xmlSitemapPageInfoFactory = xmlSitemapPageInfoFactory;
            this.xmlSitemapPageDataFactory = xmlSitemapPageDataFactory;

            // This number should be 50,000 in theory, however because the Sitemap protocol
            // states that the maximum file size is 50 MB and there is no reasonable way to
            // calculate the size during streaming, an average cap of 40,000 has been chosen.
            this.MaximumPageSize = 40000;
        }
        private readonly IPagingInstructionFactory pagingInstructionFactory;
        private readonly IXmlSitemapPageInfoFactory xmlSitemapPageInfoFactory;
        private readonly IXmlSitemapPageDataFactory xmlSitemapPageDataFactory;

        public int MaximumPageSize { get; set; }

        public IXmlSitemapPageData GetPageData(IEnumerable<IXmlSitemapProvider> providers, string feedName)
        {
            int totalRecordCount = 0;

            foreach (var provider in providers)
            {
                totalRecordCount += provider.GetTotalRecordCount(feedName);
            }

            int pageCount = (int)Math.Ceiling((double)totalRecordCount / this.MaximumPageSize);

            // Create an array from 1 to x
            var pageNumbers = Enumerable.Range(1, pageCount);

            var pages = new List<IXmlSitemapPageInfo>();

            foreach (var page in pageNumbers)
            {
                var lastModifiedDate = this.GetLastModifiedDate(providers, feedName, page);
                pages.Add(this.xmlSitemapPageInfoFactory.Create(page, lastModifiedDate));
            }

            var result = this.xmlSitemapPageDataFactory.Create(totalRecordCount, pages);

            return result;
        }

        public IEnumerable<IPagingInstruction> GetPagingInstructions(IEnumerable<IXmlSitemapProvider> providers, string feedName, int page)
        {
            var result = new List<IPagingInstruction>();

            int startAfter = this.GetStartAfterCount(page, this.MaximumPageSize);

            // Preview what the record counts will be up to and including the current page
            int totalRecordCount = 0;
            int totalTake = 0;
            foreach (var provider in providers)
            {
                int currentRecordCount = provider.GetTotalRecordCount(feedName);

                // increment the total record count
                totalRecordCount += currentRecordCount;

                if (totalRecordCount > startAfter)
                {
                    int skip = this.GetSkipCount(totalRecordCount, currentRecordCount, startAfter);
                    int take = this.GetTakeCount(currentRecordCount, this.MaximumPageSize, skip, totalTake);

                    // increment the total take
                    totalTake += take;

                    // Create an instruction to send to the provider
                    result.Add(this.pagingInstructionFactory.Create(skip, take, provider));
                }

                // Break early if we have reached the maximum number of records needed
                if (totalTake >= this.MaximumPageSize)
                {
                    break;
                }
            }

            return result;
        }

        protected virtual DateTime GetLastModifiedDate(IEnumerable<IXmlSitemapProvider> providers, string feedName, int page)
        {
            var result = DateTime.MinValue;

            int startAfter = this.GetStartAfterCount(page, this.MaximumPageSize);

            // Preview what the record counts will be up to and including the current page
            int totalRecordCount = 0;
            int totalTake = 0;
            foreach (var provider in providers)
            {
                int currentRecordCount = provider.GetTotalRecordCount(feedName);

                // increment the total record count
                totalRecordCount += currentRecordCount;

                if (totalRecordCount > startAfter)
                {
                    int skip = this.GetSkipCount(totalRecordCount, currentRecordCount, startAfter);
                    int take = this.GetTakeCount(currentRecordCount, this.MaximumPageSize, skip, totalTake);

                    // increment the total take
                    totalTake += take;

                    var lastModifiedDate = provider.GetLastModifiedDate(feedName, skip, take);
                    // Change the result if the last modified date is newer
                    if (lastModifiedDate > result)
                    {
                        result = lastModifiedDate;
                    }
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
