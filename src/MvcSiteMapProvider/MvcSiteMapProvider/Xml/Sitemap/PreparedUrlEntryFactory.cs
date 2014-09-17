﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvcSiteMapProvider.Globalization;
using MvcSiteMapProvider.Xml.Sitemap.Specialized;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    public class PreparedUrlEntryFactory
        : IPreparedUrlEntryFactory
    {
        public PreparedUrlEntryFactory(
            IXmlSitemapUrlResolverFactory urlResolverFactory,
            ICultureContextFactory cultureContextFactory
            )
        {
            if (urlResolverFactory == null)
                throw new ArgumentNullException("urlResolverFactory");
            if (cultureContextFactory == null)
                throw new ArgumentNullException("cultureContextFactory");

            this.urlResolverFactory = urlResolverFactory;
            this.cultureContextFactory = cultureContextFactory;
        }
        private readonly IXmlSitemapUrlResolverFactory urlResolverFactory;
        private readonly ICultureContextFactory cultureContextFactory;

        private const string W3CDateFormat = "yyyy-MM-ddTHH:mm:ss.fffffffzzz";

        //public IXmlSitemapUrlResolver UrlResolver { get { return this.urlResolver; } }

        //public ICultureContextFactory CultureContextFactory { get { return this.cultureContextFactory; } }

        public IPreparedUrlEntry Create(IUrlEntry urlEntry)
        {
            // Prepare the objects (formatting and URLs) in the invariant culture.
            using (var cultureContext = this.cultureContextFactory.CreateInvariant())
            {
                var urlResolver = this.urlResolverFactory.Create();
                try
                {
                    string location = urlResolver.ResolveUrlToAbsolute(urlEntry.Url, urlEntry.Protocol, urlEntry.HostName);
                    string lastModified = string.Empty;
                    string changeFrequency = string.Empty;
                    string priority = string.Empty;

                    if (urlEntry.LastModifiedDate > DateTime.MinValue)
                    {
                        lastModified = urlEntry.LastModifiedDate.ToString(W3CDateFormat);
                    }

                    if (urlEntry.ChangeFrequency != ChangeFrequency.Undefined)
                    {
                        changeFrequency = urlEntry.ChangeFrequency.ToString().ToLower();
                    }

                    if (urlEntry.UpdatePriority != UpdatePriority.Undefined)
                    {
                        priority = string.Format("{0:0.0}", ((double)urlEntry.UpdatePriority / 100));
                    }

                    return new PreparedUrlEntry(location, lastModified, changeFrequency, priority);
                }
                finally
                {
                    this.urlResolverFactory.Release(urlResolver);
                }
            }
        }
    }
}
