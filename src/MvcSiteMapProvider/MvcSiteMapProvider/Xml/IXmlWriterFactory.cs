using System;
using System.IO;
using System.Xml;

namespace MvcSiteMapProvider.Xml
{
    /// <summary>
    /// Contract for abstract factory that creates new instances of <see cref="T:System.Xml.XmlWriter"/>.
    /// </summary>
    public interface IXmlWriterFactory
    {
        XmlWriter Create(Stream output);
        XmlWriter Create(Stream output, XmlWriterSettings settings);
    }
}
