﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using MvcSiteMapProvider.Xml.Sitemap.Index;
using MvcSiteMapProvider.Xml.Sitemap.Paging;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    public class XmlSitemapPageManager
        : IXmlSitemapPageManager
    {
        public XmlSitemapPageManager(
            IXmlSitemapPagingStrategy xmlSitemapPagingStrategy,
            IXmlSitemapPageWriter xmlSitemapPageWriter,
            IXmlSitemapIndexPageWriter xmlSitemapIndexPageWriter
            )
        {
            if (xmlSitemapPagingStrategy == null)
                throw new ArgumentNullException("xmlSitemapPagingStrategy");
            if (xmlSitemapPageWriter == null)
                throw new ArgumentNullException("xmlSitemapPageWriter");
            if (xmlSitemapIndexPageWriter == null)
                throw new ArgumentNullException("xmlSitemapIndexPageWriter");

            this.xmlSitemapPagingStrategy = xmlSitemapPagingStrategy;
            this.xmlSitemapPageWriter = xmlSitemapPageWriter;
            this.xmlSitemapIndexPageWriter = xmlSitemapIndexPageWriter;
        }
        private readonly IXmlSitemapPagingStrategy xmlSitemapPagingStrategy;
        private readonly IXmlSitemapPageWriter xmlSitemapPageWriter;
        private readonly IXmlSitemapIndexPageWriter xmlSitemapIndexPageWriter; 


        // TODO: We should request cache the page counts so this doesn't 
        // result in multiple database calls, multiple times per request.
        public IEnumerable<int> GetPageNumbers(string feedName)
        {
            //return this.xmlSitemapPagingStrategy.GetPageNumbers(feedName);

            return this.xmlSitemapPagingStrategy.GetPageInfo(feedName)
                .Select(x => x.Page);
        }

        public bool WritePage(XmlWriter writer, string feedName, int page)
        {
            if (writer == null)
            {
                throw new ArgumentNullException("writer");
            }

            if (page < 0)
            {
                return false;
            }

            // TODO: Need to check total page count for all providers
            // if it is 0, need to return false here (or throw an exception).

            var pageInfo = this.xmlSitemapPagingStrategy.GetPageInfo(feedName);
            bool isIndexPageRequest = page == 0 && pageInfo.Count() > 1;

            if (isIndexPageRequest)
            {
                this.xmlSitemapIndexPageWriter.WritePage(writer, feedName, pageInfo);
            }
            else
            {
                var indexCorrectedPage = (page == 0) ? 1 : page;

                if (!pageInfo.Select(x => x.Page).Contains(indexCorrectedPage))
                {
                    // Request was for an invalid page.
                    // We return false so it can be processed as a 404 not found as appropriate.
                    return false;
                }

                var pagingInstructions = this.xmlSitemapPagingStrategy.GetPagingInstructions(feedName, indexCorrectedPage);
                this.xmlSitemapPageWriter.WritePage(writer, feedName, pagingInstructions);
            }

            // Request was for a valid page
            return true;
        }

        //public bool WritePage(XmlWriter writer, string feedName, int page)
        //{
        //    if (writer == null)
        //    {
        //        throw new ArgumentNullException("writer");
        //    }

        //    if (page < 0)
        //    {
        //        return false;
        //    }

        //    // TODO: Need to check total page count for all providers
        //    // if it is 0, need to return false here (or throw an exception).

        //    IEnumerable<int> pageNumbers = this.GetPageNumbers(feedName);
        //    bool isIndexPageRequest = page == 0 && pageNumbers.Count() > 1;

        //    if (isIndexPageRequest)
        //    {
        //        this.xmlSitemapIndexPageWriter.WritePage(writer, feedName, pageNumbers);
        //    }
        //    else
        //    {
        //        var indexCorrectedPage = (page == 0) ? 1 : page;

        //        if (!pageNumbers.Contains(indexCorrectedPage))
        //        {
        //            // Request was for an invalid page.
        //            // We return false so it can be processed as a 404 not found as appropriate.
        //            return false;
        //        }

        //        var pagingInstructions = this.xmlSitemapPagingStrategy.GetPagingInstructions(feedName, indexCorrectedPage);
        //        this.xmlSitemapPageWriter.WritePage(writer, feedName, pagingInstructions);
        //    }

        //    // Request was for a valid page
        //    return true;
        //}
    }
}
