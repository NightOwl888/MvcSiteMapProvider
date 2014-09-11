using System;
using System.Xml;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized.Video
{
    public class VideoContentXmlWriterFactory
        : ISpecializedContentXmlWriterFactory
    {
        public VideoContentXmlWriterFactory(
            IPreparedVideoContentFactory preparedVideoContentFactory
            )
        {
            if (preparedVideoContentFactory == null)
                throw new ArgumentNullException("preparedVideoContentFactory");

            this.preparedVideoContentFactory = preparedVideoContentFactory;
            this.syncRoot = new object();
        }
        private readonly IPreparedVideoContentFactory preparedVideoContentFactory;
        private readonly object syncRoot;

        public ISpecializedContentXmlWriter Create(XmlWriter writer)
        {
            return new VideoContentXmlWriter(writer, this.preparedVideoContentFactory);
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
            get { return typeof(IPreparedVideoContent); }
        }
    }
}
