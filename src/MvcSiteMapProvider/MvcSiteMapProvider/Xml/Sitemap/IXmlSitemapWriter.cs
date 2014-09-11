using System;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    public interface IXmlSitemapWriter
    {
        void WriteStartDocument();
        void WriteEndDocument();
        void WriteEntry(IUrlEntry urlEntry);
    }
}
