using System;
using System.IO;
using System.Xml;
using MvcSiteMapProvider.Xml.Sitemap.Paging;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    public interface IXmlSitemapFeed
    {
        string Name { get; }
        bool WritePage(int page, Stream output);
        bool WritePage(int page, Stream output, XmlWriterSettings settings);
        IXmlSitemapPageData GetPageData();
    }
}
