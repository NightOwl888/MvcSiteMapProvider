using System;
using System.Xml;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized.News
{
    public class NewsContentXmlWriterFactory
        : ISpecializedContentXmlWriterFactory
    {
        public NewsContentXmlWriterFactory(
            IPreparedNewsContentFactory preparedNewsContentFactory
            )
        {
            if (preparedNewsContentFactory == null)
                throw new ArgumentNullException("preparedNewsContentFactory");

            this.preparedNewsContentFactory = preparedNewsContentFactory;
            this.syncRoot = new object();
        }
        private readonly IPreparedNewsContentFactory preparedNewsContentFactory;
        private readonly object syncRoot;

        public ISpecializedContentXmlWriter Create(XmlWriter writer)
        {
            return new NewsContentXmlWriter(writer, this.preparedNewsContentFactory);
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
            get { return typeof(INewsContent); }
        }
    }
}
