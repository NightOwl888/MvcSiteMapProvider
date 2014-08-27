using System;

namespace MvcSiteMapProvider.Xml.Sitemap.Index
{
    public interface IXmlSitemapIndexWriter
    {
        void WriteStartDocument();
        void WriteEndDocument();
        void WriteEntry(ISitemapEntry indexEntry);
    }
}
