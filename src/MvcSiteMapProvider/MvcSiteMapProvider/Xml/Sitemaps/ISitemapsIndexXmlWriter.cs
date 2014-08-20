using System;

namespace MvcSiteMapProvider.Xml.Sitemaps
{
    public interface ISitemapsIndexXmlWriter
    {
        void WriteStartDocument();
        void WriteEndDocument();
        void WriteEntry(IIndexEntry indexEntry);
    }
}
