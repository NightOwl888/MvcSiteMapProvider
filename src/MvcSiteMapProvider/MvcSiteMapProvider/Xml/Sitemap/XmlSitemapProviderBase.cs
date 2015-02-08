using System;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    /// <summary>
    /// Base class to make it easier to implement a custom <see cref="T:MvcSiteMapProvider.Xml.Sitemap.IXmlSitemapProvider"/>.
    /// </summary>
    public abstract class XmlSitemapProviderBase
        : IXmlSitemapProvider, IDisposable
    {
        public abstract int GetTotalRecordCount(string feedName);

        public virtual DateTime GetLastModifiedDate(string feedName, int skip, int take)
        {
            return DateTime.MinValue;
        }

        public abstract void GetUrlEntries(IUrlEntryHelper helper);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// May be overridden in a derived class to dispose unmanaged resources.
        /// </summary>
        /// <remarks>See http://stackoverflow.com/questions/151051/when-should-i-use-gc-suppressfinalize</remarks>
        /// <param name="disposing"><b>true</b> to indicate the method is being called by the <see cref="M:Dispose"/> method; 
        /// <b>false</b> to indicate that it is being called by the class finalizer.</param>
        protected virtual void Dispose(bool disposing)
        {
        }
    }
}
