using System;
using System.Xml;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized.Image
{
    public class ImageContentXmlWriterFactory
        : ISpecializedContentXmlWriterFactory
    {
        public ImageContentXmlWriterFactory(
            IPreparedImageContentFactory preparedImageContentFactory
            )
        {
            if (preparedImageContentFactory == null)
                throw new ArgumentNullException("preparedImageContentFactory");

            this.preparedImageContentFactory = preparedImageContentFactory;
            this.syncRoot = new object();
        }
        private readonly IPreparedImageContentFactory preparedImageContentFactory;
        private readonly object syncRoot;

        public ISpecializedContentXmlWriter Create(XmlWriter writer)
        {
            return new ImageContentXmlWriter(writer, this.preparedImageContentFactory);
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
            get { return typeof(IPreparedImageContent); }
        }
    }
}
