using System;
using System.Xml;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    public interface IXmlSitemapFileGenerator
    {
        void GenerateFiles(IXmlSitemapFeedStrategy feedStrategy);
        void GenerateFiles(IXmlSitemapFeedStrategy feedStrategy, string feedName);
        void GenerateFiles(IXmlSitemapFeedStrategy feedStrategy, XmlWriterSettings settings);
        void GenerateFiles(IXmlSitemapFeedStrategy feedStrategy, string feedName, XmlWriterSettings settings);
        void GenerateFiles(IXmlSitemapFeedStrategy feedStrategy, string feedName, string outputDirectory);
        void GenerateFiles(IXmlSitemapFeedStrategy feedStrategy, string feedName, string outputDirectory, XmlWriterSettings settings);
        void GenerateFiles(IXmlSitemapFeed feed);
        void GenerateFiles(IXmlSitemapFeed feed, XmlWriterSettings settings);
        void GenerateFiles(IXmlSitemapFeed feed, string outputDirectory);
        void GenerateFiles(IXmlSitemapFeed feed, string outputDirectory, XmlWriterSettings settings);
    }
}
