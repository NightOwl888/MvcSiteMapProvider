using System;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    /// <summary>
    /// Base class to make it easier to implement a custom <see cref="T:MvcSiteMapProvider.Xml.Sitemap.IXmlSitemapProvider"/>.
    /// </summary>
    public abstract class XmlSitemapProviderBase
        : IXmlSitemapProvider
    {
        public abstract int GetTotalRecordCount(string feedName);

        public virtual DateTime GetLastModifiedDate(string feedName, int skip, int take)
        {
            return DateTime.MinValue;
        }

        public abstract void GetUrlEntries(IUrlEntryHelper helper);
    }
}
