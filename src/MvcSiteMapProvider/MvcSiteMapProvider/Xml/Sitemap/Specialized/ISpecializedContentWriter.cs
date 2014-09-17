using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized
{
    public interface ISpecializedContentWriter
    {
        void WriteNamespaces(XmlWriter writer);
        void WriteSpecializedContent(XmlWriter writer, IEnumerable<ISpecializedContent> specializedContent);
        bool ContainsMatchingContentType(IEnumerable<ISpecializedContent> specializedContent);
    }
}
