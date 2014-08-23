using System;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    public interface ISitemapIndexXmlWriter
    {
        void WriteStartDocument();
        void WriteEndDocument();
        void WriteEntry(IIndexEntry indexEntry);
    }
}
