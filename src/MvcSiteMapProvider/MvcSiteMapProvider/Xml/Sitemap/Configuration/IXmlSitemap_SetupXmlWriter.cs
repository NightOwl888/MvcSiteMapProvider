using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using MvcSiteMapProvider.ComponentModel;

namespace MvcSiteMapProvider.Xml.Sitemap.Configuration
{

    public class XmlSitemap_SetupXmlWriter_Builder
        : IXmlSitemap_SetupXmlWriter_Starter
    {
        public XmlSitemap_SetupXmlWriter_Builder()
            : this(
                // Default encoding to get rid of the BOM
            xmlWriterSettings: new XmlWriterSettings() { Encoding = new UTF8Encoding(false) })
        {
        }

        public XmlSitemap_SetupXmlWriter_Builder(
            XmlWriterSettings xmlWriterSettings
            )
        {
            if (xmlWriterSettings == null)
                throw new ArgumentNullException("xmlWriterSettings");

            this.xmlWriterSettings = xmlWriterSettings;
        }
        private readonly XmlWriterSettings xmlWriterSettings;

        public IXmlSitemap_SetupXmlWriter_Starter WithEncoding(Encoding encoding)
        {
            if (encoding == null)
                throw new ArgumentNullException("encoding");

            this.xmlWriterSettings.Encoding = encoding;
            return new XmlSitemap_SetupXmlWriter_Builder(this.xmlWriterSettings);
        }

        public IXmlSitemap_SetupXmlWriter_Starter WithIndentation()
        {
            this.xmlWriterSettings.Indent = true;
            return new XmlSitemap_SetupXmlWriter_Builder(this.xmlWriterSettings);
        }

        public IXmlSitemap_SetupXmlWriter_Starter WithIndentationCharacters(string indentChars)
        {
            this.xmlWriterSettings.IndentChars = indentChars;
            return new XmlSitemap_SetupXmlWriter_Builder(this.xmlWriterSettings);
        }

        public IXmlSitemap_SetupXmlWriter_Starter WithNewLineHandling(NewLineHandling newLineHandling)
        {
            this.xmlWriterSettings.NewLineHandling = newLineHandling;
            return new XmlSitemap_SetupXmlWriter_Builder(this.xmlWriterSettings);
        }

        public IXmlSitemap_SetupXmlWriter_Starter WithNewLineCharacters(string newLineChars)
        {
            this.xmlWriterSettings.NewLineChars = newLineChars;
            return new XmlSitemap_SetupXmlWriter_Builder(this.xmlWriterSettings);
        }

        public IXmlSitemap_SetupXmlWriter_Starter OmitXmlDeclaration()
        {
            this.xmlWriterSettings.OmitXmlDeclaration = true;
            return new XmlSitemap_SetupXmlWriter_Builder(this.xmlWriterSettings);
        }

        public XmlWriterSettings Create()
        {
            return this.xmlWriterSettings;
        }
    }

    //interface IXmlSitemap_SetupXmlWriter
    //{
    //}

    public interface IXmlSitemap_SetupXmlWriter_WithEncoding<TRemainder>
    {
        TRemainder WithEncoding(Encoding encoding);
    }

    public interface IXmlSitemap_SetupXmlWriter_WithIndentation<TRemainder>
    {
        TRemainder WithIndentation();
    }

    public interface IXmlSitemap_SetupXmlWriter_WithIndentationCharacters<TRemainder>
    {
        TRemainder WithIndentationCharacters(string indentChars); 
    }

    public interface IXmlSitemap_SetupXmlWriter_WithNewLineHandling<TRemainder>
    {
        TRemainder WithNewLineHandling(NewLineHandling newLineHandling);
    }

    public interface IXmlSitemap_SetupXmlWriter_WithNewLineCharacters<TRemainder>
    {
        TRemainder WithNewLineCharacters(string newLineChars);
    }

    public interface IXmlSitemap_SetupXmlWriter_OmitXmlDeclaration<TRemainder>
    {
        TRemainder OmitXmlDeclaration();
    }

    public interface IXmlSitemap_SetupXmlWriter_Finalizer
        : IFluentInterface
    {
        XmlWriterSettings Create();
    }

    // NOTE: At some point, maybe these could be broken into a builder that enforces the
    // only set once rule, but to save time they were lumped together.
    public interface IXmlSitemap_SetupXmlWriter_Starter
        : IXmlSitemap_SetupXmlWriter_WithEncoding<IXmlSitemap_SetupXmlWriter_Starter>,
        IXmlSitemap_SetupXmlWriter_WithIndentation<IXmlSitemap_SetupXmlWriter_Starter>,
        IXmlSitemap_SetupXmlWriter_WithIndentationCharacters<IXmlSitemap_SetupXmlWriter_Starter>,
        IXmlSitemap_SetupXmlWriter_WithNewLineHandling<IXmlSitemap_SetupXmlWriter_Starter>,
        IXmlSitemap_SetupXmlWriter_WithNewLineCharacters<IXmlSitemap_SetupXmlWriter_Starter>,
        IXmlSitemap_SetupXmlWriter_OmitXmlDeclaration<IXmlSitemap_SetupXmlWriter_Starter>,
        IXmlSitemap_SetupXmlWriter_Finalizer
    {
    }

}
