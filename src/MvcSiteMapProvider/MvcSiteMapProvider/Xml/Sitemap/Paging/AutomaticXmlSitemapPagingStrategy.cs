﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Xml.Sitemap.Paging
{
    public class AutomaticXmlSitemapPagingStrategy
        : IXmlSitemapPagingStrategy
    {
        public AutomaticXmlSitemapPagingStrategy(
            IXmlSitemapProvider[] xmlSitemapProviders,
            IPagingInstructionFactory pagingInstructionFactory,
            IXmlSitemapRecordInfoHelperFactory xmlSitemapRecordInfoHelperFactory,
            IXmlSitemapPageInfoFactory xmlSitemapPageInfoFactory
            )
        {
            if (xmlSitemapProviders == null)
                throw new ArgumentNullException("xmlSitemapProviders");
            if (pagingInstructionFactory == null)
                throw new ArgumentNullException("pagingInstructionFactory");
            if (xmlSitemapRecordInfoHelperFactory == null)
                throw new ArgumentNullException("xmlSitemapRecordInfoHelperFactory");
            if (xmlSitemapPageInfoFactory == null)
                throw new ArgumentNullException("xmlSitemapPageInfoFactory");

            this.xmlSitemapProviders = xmlSitemapProviders;
            this.pagingInstructionFactory = pagingInstructionFactory;
            this.xmlSitemapRecordInfoHelperFactory = xmlSitemapRecordInfoHelperFactory;
            this.xmlSitemapPageInfoFactory = xmlSitemapPageInfoFactory;

            // This number should be 50,000 in theory, however because the Sitemap protocol
            // states that the maximum file size is 50 MB and there is no reasonable way to
            // calculate the size during streaming, an average cap of 35,000 has been chosen.
            this.MaximumPageSize = 35000;
        }
        private readonly IXmlSitemapProvider[] xmlSitemapProviders;
        private readonly IPagingInstructionFactory pagingInstructionFactory;
        private readonly IXmlSitemapRecordInfoHelperFactory xmlSitemapRecordInfoHelperFactory;
        private readonly IXmlSitemapPageInfoFactory xmlSitemapPageInfoFactory;

        public int MaximumPageSize { get; set; }

        public IEnumerable<IXmlSitemapPageInfo> GetPageInfo(string feedName)
        {
            int totalRecordCount = 0;
            var recordInfoList = this.GetRecordInfoList(feedName);

            foreach (var recordInfo in recordInfoList)
            {
                totalRecordCount += recordInfo.TotalRecordCount;
            }

            int pageCount = (int)Math.Ceiling((double)totalRecordCount / this.MaximumPageSize);

            // Create an array from 1 to x
            var pageNumbers = Enumerable.Range(1, pageCount);

            var result = new List<IXmlSitemapPageInfo>();

            foreach (var page in pageNumbers)
            {
                DateTime lastModifiedDate = this.GetLastModifiedDate(page, recordInfoList);
                result.Add(this.xmlSitemapPageInfoFactory.Create(page, lastModifiedDate));
            }

            return result;
        }

        public IEnumerable<IPagingInstruction> GetPagingInstructions(string feedName, int page)
        {
            var result = new List<IPagingInstruction>();

            int startAfter = this.GetStartAfterCount(page, this.MaximumPageSize);

            // Preview what the record counts will be up to and including the current page
            int totalRecordCount = 0;
            int totalTake = 0;
            foreach (var provider in this.xmlSitemapProviders)
            {
                //var recordInfo = this.xmlSitemapRecordInfoFactory.Create();
                //provider.GetRecordInfo(feedName, recordInfo);

                var helper = this.xmlSitemapRecordInfoHelperFactory.Create(feedName);
                var recordInfo = provider.GetRecordInfo(helper);

                int currentRecordCount = recordInfo.TotalRecordCount;

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

        protected virtual IEnumerable<IXmlSitemapRecordInfo> GetRecordInfoList(string feedName)
        {
            var result = new List<IXmlSitemapRecordInfo>();

            foreach (var provider in this.xmlSitemapProviders)
            {
                //// TODO: Request cache the record info and pass that in instead
                //var recordInfo = this.xmlSitemapRecordInfoFactory.Create();

                //// TODO: fix the provider so it returns the value from the function so we don't have to instantiate the recordInfo here
                //provider.GetRecordInfo(feedName, recordInfo);

                var helper = this.xmlSitemapRecordInfoHelperFactory.Create(feedName);
                var recordInfo = provider.GetRecordInfo(helper);

                result.Add(recordInfo);
            }

            return result;
        }

        protected virtual DateTime GetLastModifiedDate(int page, IEnumerable<IXmlSitemapRecordInfo> recordInfoList)
        {
            var result = DateTime.MinValue;

            int startAfter = this.GetStartAfterCount(page, this.MaximumPageSize);

            // Preview what the record counts will be up to and including the current page
            int totalRecordCount = 0;
            int totalTake = 0;
            foreach (var recordInfo in recordInfoList)
            {
                int currentRecordCount = recordInfo.TotalRecordCount;

                // increment the total record count
                totalRecordCount += currentRecordCount;

                if (totalRecordCount > startAfter)
                {
                    int skip = this.GetSkipCount(totalRecordCount, currentRecordCount, startAfter);
                    int take = this.GetTakeCount(currentRecordCount, this.MaximumPageSize, skip, totalTake);

                    // increment the total take
                    totalTake += take;

                    // Change the result if the last modified date is newer
                    if (recordInfo.LastModifiedDate > result)
                    {
                        result = recordInfo.LastModifiedDate;
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