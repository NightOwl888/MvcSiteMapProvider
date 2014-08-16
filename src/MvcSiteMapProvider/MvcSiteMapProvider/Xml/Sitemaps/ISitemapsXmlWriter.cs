using System;

namespace MvcSiteMapProvider.Xml.Sitemaps
{
    public interface ISitemapsXmlWriter
    {
        void WriteStartDocument();
        void WriteEndDocument();
        void WriteUrlEntry(IUrlEntry url);
    }
}
