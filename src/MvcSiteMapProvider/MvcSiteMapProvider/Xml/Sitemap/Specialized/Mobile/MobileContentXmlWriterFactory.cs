using System;
using System.Xml;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized.Mobile
{
    public class MobileContentXmlWriterFactory
        : ISpecializedContentXmlWriterFactory
    {
        public MobileContentXmlWriterFactory()
        {
            this.syncRoot = new object();
        }
        private readonly object syncRoot;

        public ISpecializedContentXmlWriter Create(XmlWriter writer)
        {
            return new MobileContentXmlWriter(writer);
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
            get { return typeof(IMobileContent); }
        }
    }
}
