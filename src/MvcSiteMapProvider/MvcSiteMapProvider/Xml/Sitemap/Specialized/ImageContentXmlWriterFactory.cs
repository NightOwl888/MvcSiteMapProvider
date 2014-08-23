using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized
{
    public class ImageContentXmlWriterFactory
        : ISpecializedContentXmlWriterFactory
    {
        public ImageContentXmlWriterFactory()
        {
            this.syncRoot = new object();
        }
        private readonly object syncRoot;

        public ISpecializedContentXmlWriter Create(XmlWriter writer)
        {
            return new ImageContentXmlWriter(writer);
        }

        public void Release(ISpecializedContentXmlWriter specializedContentXmlWriter)
        {
            var disposable = specializedContentXmlWriter as IDisposable;
            if (disposable != null)
            {
                lock (this.syncRoot)
                {
                    disposable.Dispose();
                }
            }
        }

        //public bool AppliesTo(ISpecializedContent content)
        //{
        //    //return typeof(IImageContent).IsAssignableFrom(content.GetType());
        //    return content is IImageContent;
        //}

        public Type ContentType
        {
            get { return typeof(IImageContent); }
        }
    }
}
