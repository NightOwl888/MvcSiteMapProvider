using System;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    public interface ISitemapXmlWriter
    {
        void WriteStartDocument();
        void WriteEndDocument();
        void WriteEntry(IUrlEntry urlEntry);
    }
}
