using System;
using System.IO;
using System.Xml;

namespace MvcSiteMapProvider.Xml
{
    /// <summary>
    /// Abstract factory that creates new instances of <see cref="T:System.Xml.XmlWriter"/>. 
    /// This makes it possible to extend or replace the XmlWriter class or to mock it for unit testing.
    /// </summary>
    public class XmlWriterFactory
        : IXmlWriterFactory
    {
        public XmlWriter Create(Stream output)
        {
            return XmlWriter.Create(output);
        }

        public XmlWriter Create(Stream output, XmlWriterSettings settings)
        {
            return XmlWriter.Create(output, settings);
        }
    }
}
