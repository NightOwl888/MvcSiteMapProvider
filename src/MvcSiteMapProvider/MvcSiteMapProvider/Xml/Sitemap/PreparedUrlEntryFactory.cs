using System;
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
            IPreparedSpecializedContentFactoryStrategy preparedSpecializedContentFactoryStrategy,
            ISitemapUrlResolver urlResolver,
            ICultureContextFactory cultureContextFactory
            )
        {
            if (preparedSpecializedContentFactoryStrategy == null)
                throw new ArgumentNullException("preparedSpecializedContentFactoryStrategy");
            if (urlResolver == null)
                throw new ArgumentNullException("urlResolver");
            if (cultureContextFactory == null)
                throw new ArgumentNullException("cultureContextFactory");

            this.preparedSpecializedContentFactoryStrategy = preparedSpecializedContentFactoryStrategy;
            this.urlResolver = urlResolver;
            this.cultureContextFactory = cultureContextFactory;
        }
        private readonly IPreparedSpecializedContentFactoryStrategy preparedSpecializedContentFactoryStrategy;
        private readonly ISitemapUrlResolver urlResolver;
        private readonly ICultureContextFactory cultureContextFactory;

        private const string W3CDateFormat = "yyyy-MM-ddTHH:mm:ss.fffffffzzz";

        public IPreparedUrlEntry Create(IUrlEntry urlEntry)
        {
            // Prepare the objects (formatting and URLs) in the invariant culture.
            using (var cultureContext = this.cultureContextFactory.CreateInvariant())
            {
                var preparedSpecializedContents = new List<IPreparedSpecializedContent>();

                foreach (var content in urlEntry.SpecializedContent)
                {
                    var contentFactory = this.preparedSpecializedContentFactoryStrategy.GetFactory(content.GetType());
                    var preparedContent = contentFactory.Create(content, this.urlResolver, cultureContext);
                    if (preparedContent != null)
                    {
                        preparedSpecializedContents.Add(preparedContent);
                    }
                }

                string location = this.urlResolver.ResolveUrlToAbsolute(urlEntry.Url, urlEntry.Protocol, urlEntry.HostName);
                string lastModified = string.Empty;
                string changeFrequency = string.Empty;
                string priority = string.Empty;

                if (urlEntry.LastModifiedDate > DateTime.MinValue)
                {
                    lastModified = urlEntry.LastModifiedDate.ToUniversalTime().ToString(W3CDateFormat);
                }

                if (urlEntry.ChangeFrequency != ChangeFrequency.Undefined)
                {
                    changeFrequency = urlEntry.ChangeFrequency.ToString().ToLower();
                }

                if (urlEntry.UpdatePriority != UpdatePriority.Undefined)
                {
                    priority = string.Format("{0:0.0}", ((double)urlEntry.UpdatePriority / 100));
                }

                return new PreparedUrlEntry(location, lastModified, changeFrequency, priority, preparedSpecializedContents);
            }
        }
    }
}
