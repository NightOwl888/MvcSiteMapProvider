using System;
using System.Xml;

namespace MvcSiteMapProvider.Xml.Sitemaps.Specialized
{
    public class VideoContentXmlWriterFactory
        : ISpecializedContentXmlWriterFactory
    {
        public VideoContentXmlWriterFactory()
        {
            this.syncRoot = new object();
        }
        private readonly object syncRoot;

        public ISpecializedContentXmlWriter Create(XmlWriter writer)
        {
            return new VideoContentXmlWriter(writer);
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

        public Type ContentType
        {
            get { return typeof(IVideoContent); }
        }
    }
}
