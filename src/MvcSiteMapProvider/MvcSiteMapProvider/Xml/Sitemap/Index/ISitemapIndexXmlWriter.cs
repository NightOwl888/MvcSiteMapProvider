using System;

namespace MvcSiteMapProvider.Xml.Sitemap.Index
{
    public interface ISitemapIndexXmlWriter
    {
        void WriteStartDocument();
        void WriteEndDocument();
        void WriteEntry(ISitemapEntry indexEntry);
    }
}
