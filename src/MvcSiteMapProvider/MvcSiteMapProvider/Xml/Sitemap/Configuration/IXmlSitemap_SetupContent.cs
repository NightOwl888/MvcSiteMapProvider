using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvcSiteMapProvider.ComponentModel;
using MvcSiteMapProvider.Xml.Sitemap.Specialized;
using MvcSiteMapProvider.Xml.Sitemap.Specialized.Image;
using MvcSiteMapProvider.Xml.Sitemap.Specialized.Mobile;
using MvcSiteMapProvider.Xml.Sitemap.Specialized.News;
using MvcSiteMapProvider.Xml.Sitemap.Specialized.Video;

namespace MvcSiteMapProvider.Xml.Sitemap.Configuration
{
    public class XmlSitemap_SetupContent_Builder : XmlSitemap_SetupContent_WithNews,
        IXmlSitemap_SetupContent_Starter
    {

        public XmlSitemap_SetupContent_Builder(
            bool omitUrlsWithoutMatchingContent,
            IDictionary<Type, ISpecializedContentXmlWriterFactory> specializedContentDictionary
            )
        {
            if (specializedContentDictionary == null)
                throw new ArgumentNullException("specializedContentDictionary");

            this.omitUrlsWithoutMatchingContent = omitUrlsWithoutMatchingContent;
            this.specializedContentDictionary = specializedContentDictionary;
        }
        private readonly bool omitUrlsWithoutMatchingContent;
        private readonly IDictionary<Type, ISpecializedContentXmlWriterFactory> specializedContentDictionary;

        public override IXmlSitemap_SetupContent_WithVideo Video()
        {
            var videoContentXmlWriterFactory = new VideoContentXmlWriterFactory();
            if (!this.specializedContentDictionary.ContainsKey(videoContentXmlWriterFactory.ContentType))
            {
                this.specializedContentDictionary.Add(videoContentXmlWriterFactory.ContentType, videoContentXmlWriterFactory);
            }
            return new XmlSitemap_SetupContent_Builder(this.omitUrlsWithoutMatchingContent, this.specializedContentDictionary);
        }

        public override IXmlSitemap_SetupContent_WithImage Image()
        {
            var imageContentXmlWriterFactory = new ImageContentXmlWriterFactory();
            if (!this.specializedContentDictionary.ContainsKey(imageContentXmlWriterFactory.ContentType))
            {
                this.specializedContentDictionary.Add(imageContentXmlWriterFactory.ContentType, imageContentXmlWriterFactory);
            }
            return new XmlSitemap_SetupContent_Builder(this.omitUrlsWithoutMatchingContent, this.specializedContentDictionary);
        }

        public override IXmlSitemap_SetupContent_WithMobile Mobile()
        {
            var mobileContentXmlWriterFactory = new MobileContentXmlWriterFactory();
            if (!this.specializedContentDictionary.ContainsKey(mobileContentXmlWriterFactory.ContentType))
            {
                this.specializedContentDictionary.Add(mobileContentXmlWriterFactory.ContentType, mobileContentXmlWriterFactory);
            }
            return new XmlSitemap_SetupContent_Builder(this.omitUrlsWithoutMatchingContent, this.specializedContentDictionary);
        }

        public override IXmlSitemap_SetupContent_WithNews News()
        {
            var newsContentXmlWriterFactory = new NewsContentXmlWriterFactory();
            if (!this.specializedContentDictionary.ContainsKey(newsContentXmlWriterFactory.ContentType))
            {
                this.specializedContentDictionary.Add(newsContentXmlWriterFactory.ContentType, newsContentXmlWriterFactory);
            }
            return new XmlSitemap_SetupContent_Builder(this.omitUrlsWithoutMatchingContent, this.specializedContentDictionary);
        }

        public override IXmlSitemap_SetupContent_Starter Custom(ISpecializedContentXmlWriterFactory specializedContentXmlWriterFactory)
        {
            if (specializedContentXmlWriterFactory == null)
                throw new ArgumentNullException("specializedContentXmlWriterFactory");

            if (!this.specializedContentDictionary.ContainsKey(specializedContentXmlWriterFactory.ContentType))
            {
                this.specializedContentDictionary.Add(specializedContentXmlWriterFactory.ContentType, specializedContentXmlWriterFactory);
            }
            return new XmlSitemap_SetupContent_Builder(this.omitUrlsWithoutMatchingContent, this.specializedContentDictionary);
        }

        public override IXmlSitemap_SetupContent_Finalizer OmitUrlsWithoutMatchingContent()
        {
            var omitUrlsWithoutMatchingContent = true;
            return new XmlSitemap_SetupContent_Builder(omitUrlsWithoutMatchingContent, this.specializedContentDictionary);
        }

        public override IDictionary<Type, ISpecializedContentXmlWriterFactory> Create()
        {
            return this.specializedContentDictionary;
        }

        public override IEnumerable<ISpecializedContentXmlWriterFactory> SpecializedContentXmlWriterFactories
        {
            get { return this.specializedContentDictionary.Values.ToArray(); }
        }

        public override bool UrlsWithNonMatchingContentOmitted
        {
            get { return this.omitUrlsWithoutMatchingContent; }
        }
    }


    public abstract class XmlSitemap_SetupContent_WithNews : XmlSitemap_SetupContent_WithMobile,
        IXmlSitemap_SetupContent_WithNews,
        IXmlSitemap_SetupContent_WithNews_WithImage,
        IXmlSitemap_SetupContent_WithNews_WithVideo,
        IXmlSitemap_SetupContent_WithNews_WithMobile,
        IXmlSitemap_SetupContent_WithNews_WithImage_WithVideo,
        IXmlSitemap_SetupContent_WithNews_WithMobile_WithImage,
        IXmlSitemap_SetupContent_WithNews_WithMobile_WithVideo
    {
        IXmlSitemap_SetupContent_WithNews_WithVideo IXmlSitemap_SetupContent_Video<IXmlSitemap_SetupContent_WithNews_WithVideo>.Video()
        {
            return (IXmlSitemap_SetupContent_WithNews_WithVideo)this.Video();
        }

        IXmlSitemap_SetupContent_WithNews_WithImage IXmlSitemap_SetupContent_Image<IXmlSitemap_SetupContent_WithNews_WithImage>.Image()
        {
            return (IXmlSitemap_SetupContent_WithNews_WithImage)this.Image();
        }

        IXmlSitemap_SetupContent_WithNews_WithMobile IXmlSitemap_SetupContent_Mobile<IXmlSitemap_SetupContent_WithNews_WithMobile>.Mobile()
        {
            return (IXmlSitemap_SetupContent_WithNews_WithMobile)this.Mobile();
        }

        IXmlSitemap_SetupContent_WithNews IXmlSitemap_SetupContent_Custom<IXmlSitemap_SetupContent_WithNews>.Custom(ISpecializedContentXmlWriterFactory specializedContentXmlWriterFactory)
        {
            return (IXmlSitemap_SetupContent_WithNews)this.Custom(specializedContentXmlWriterFactory);
        }




        IXmlSitemap_SetupContent_WithNews_WithImage_WithVideo IXmlSitemap_SetupContent_Video<IXmlSitemap_SetupContent_WithNews_WithImage_WithVideo>.Video()
        {
            return (IXmlSitemap_SetupContent_WithNews_WithImage_WithVideo)this.Video();
        }

        IXmlSitemap_SetupContent_WithNews_WithMobile_WithImage IXmlSitemap_SetupContent_Mobile<IXmlSitemap_SetupContent_WithNews_WithMobile_WithImage>.Mobile()
        {
            return (IXmlSitemap_SetupContent_WithNews_WithMobile_WithImage)this.Mobile();
        }

        IXmlSitemap_SetupContent_WithNews_WithImage IXmlSitemap_SetupContent_Custom<IXmlSitemap_SetupContent_WithNews_WithImage>.Custom(ISpecializedContentXmlWriterFactory specializedContentXmlWriterFactory)
        {
            return (IXmlSitemap_SetupContent_WithNews_WithImage)this.Custom(specializedContentXmlWriterFactory);
        }




        IXmlSitemap_SetupContent_WithNews_WithImage_WithVideo IXmlSitemap_SetupContent_Image<IXmlSitemap_SetupContent_WithNews_WithImage_WithVideo>.Image()
        {
            return (IXmlSitemap_SetupContent_WithNews_WithImage_WithVideo)this.Image();
        }

        IXmlSitemap_SetupContent_WithNews_WithMobile_WithVideo IXmlSitemap_SetupContent_Mobile<IXmlSitemap_SetupContent_WithNews_WithMobile_WithVideo>.Mobile()
        {
            return (IXmlSitemap_SetupContent_WithNews_WithMobile_WithVideo)this.Mobile();
        }

        IXmlSitemap_SetupContent_WithNews_WithVideo IXmlSitemap_SetupContent_Custom<IXmlSitemap_SetupContent_WithNews_WithVideo>.Custom(ISpecializedContentXmlWriterFactory specializedContentXmlWriterFactory)
        {
            return (IXmlSitemap_SetupContent_WithNews_WithVideo)this.Custom(specializedContentXmlWriterFactory);
        }




        IXmlSitemap_SetupContent_WithNews_WithMobile_WithVideo IXmlSitemap_SetupContent_Video<IXmlSitemap_SetupContent_WithNews_WithMobile_WithVideo>.Video()
        {
            return (IXmlSitemap_SetupContent_WithNews_WithMobile_WithVideo)this.Video();
        }

        IXmlSitemap_SetupContent_WithNews_WithMobile_WithImage IXmlSitemap_SetupContent_Image<IXmlSitemap_SetupContent_WithNews_WithMobile_WithImage>.Image()
        {
            return (IXmlSitemap_SetupContent_WithNews_WithMobile_WithImage)this.Image();
        }

        IXmlSitemap_SetupContent_WithNews_WithMobile IXmlSitemap_SetupContent_Custom<IXmlSitemap_SetupContent_WithNews_WithMobile>.Custom(ISpecializedContentXmlWriterFactory specializedContentXmlWriterFactory)
        {
            return (IXmlSitemap_SetupContent_WithNews_WithMobile)this.Custom(specializedContentXmlWriterFactory);
        }



        IXmlSitemap_SetupContent_Finalizer IXmlSitemap_SetupContent_Mobile<IXmlSitemap_SetupContent_Finalizer>.Mobile()
        {
            return this.Mobile();
        }

        IXmlSitemap_SetupContent_WithNews_WithImage_WithVideo IXmlSitemap_SetupContent_Custom<IXmlSitemap_SetupContent_WithNews_WithImage_WithVideo>.Custom(ISpecializedContentXmlWriterFactory specializedContentXmlWriterFactory)
        {
            return (IXmlSitemap_SetupContent_WithNews_WithImage_WithVideo)this.Custom(specializedContentXmlWriterFactory);
        }



        IXmlSitemap_SetupContent_Finalizer IXmlSitemap_SetupContent_Video<IXmlSitemap_SetupContent_Finalizer>.Video()
        {
            return this.Video();
        }

        IXmlSitemap_SetupContent_WithNews_WithMobile_WithImage IXmlSitemap_SetupContent_Custom<IXmlSitemap_SetupContent_WithNews_WithMobile_WithImage>.Custom(ISpecializedContentXmlWriterFactory specializedContentXmlWriterFactory)
        {
            return (IXmlSitemap_SetupContent_WithNews_WithMobile_WithImage)this.Custom(specializedContentXmlWriterFactory);
        }



        IXmlSitemap_SetupContent_Finalizer IXmlSitemap_SetupContent_Image<IXmlSitemap_SetupContent_Finalizer>.Image()
        {
            return this.Image();
        }

        IXmlSitemap_SetupContent_WithNews_WithMobile_WithVideo IXmlSitemap_SetupContent_Custom<IXmlSitemap_SetupContent_WithNews_WithMobile_WithVideo>.Custom(ISpecializedContentXmlWriterFactory specializedContentXmlWriterFactory)
        {
            return (IXmlSitemap_SetupContent_WithNews_WithMobile_WithVideo)this.Custom(specializedContentXmlWriterFactory);
        }


    }


    public abstract class XmlSitemap_SetupContent_WithMobile : XmlSitemap_SetupContent_WithImage,
        IXmlSitemap_SetupContent_WithMobile,
        IXmlSitemap_SetupContent_WithMobile_WithImage,
        IXmlSitemap_SetupContent_WithMobile_WithVideo,
        IXmlSitemap_SetupContent_WithMobile_WithImage_WithVideo
    {
        IXmlSitemap_SetupContent_WithMobile_WithVideo IXmlSitemap_SetupContent_Video<IXmlSitemap_SetupContent_WithMobile_WithVideo>.Video()
        {
            return (IXmlSitemap_SetupContent_WithMobile_WithVideo)this.Video();
        }

        IXmlSitemap_SetupContent_WithMobile_WithImage IXmlSitemap_SetupContent_Image<IXmlSitemap_SetupContent_WithMobile_WithImage>.Image()
        {
            return (IXmlSitemap_SetupContent_WithMobile_WithImage)this.Image();
        }

        IXmlSitemap_SetupContent_WithNews_WithMobile IXmlSitemap_SetupContent_News<IXmlSitemap_SetupContent_WithNews_WithMobile>.News()
        {
            return (IXmlSitemap_SetupContent_WithNews_WithMobile)this.News();
        }

        IXmlSitemap_SetupContent_WithMobile IXmlSitemap_SetupContent_Custom<IXmlSitemap_SetupContent_WithMobile>.Custom(ISpecializedContentXmlWriterFactory specializedContentXmlWriterFactory)
        {
            return (IXmlSitemap_SetupContent_WithMobile)this.Custom(specializedContentXmlWriterFactory);
        }



        IXmlSitemap_SetupContent_WithMobile_WithImage_WithVideo IXmlSitemap_SetupContent_Video<IXmlSitemap_SetupContent_WithMobile_WithImage_WithVideo>.Video()
        {
            return (IXmlSitemap_SetupContent_WithMobile_WithImage_WithVideo)this.Video();
        }

        IXmlSitemap_SetupContent_WithNews_WithMobile_WithImage IXmlSitemap_SetupContent_News<IXmlSitemap_SetupContent_WithNews_WithMobile_WithImage>.News()
        {
            return (IXmlSitemap_SetupContent_WithNews_WithMobile_WithImage)this.News();
        }

        IXmlSitemap_SetupContent_WithMobile_WithImage IXmlSitemap_SetupContent_Custom<IXmlSitemap_SetupContent_WithMobile_WithImage>.Custom(ISpecializedContentXmlWriterFactory specializedContentXmlWriterFactory)
        {
            return (IXmlSitemap_SetupContent_WithMobile_WithImage)this.Custom(specializedContentXmlWriterFactory);
        }


        IXmlSitemap_SetupContent_WithMobile_WithImage_WithVideo IXmlSitemap_SetupContent_Image<IXmlSitemap_SetupContent_WithMobile_WithImage_WithVideo>.Image()
        {
            return (IXmlSitemap_SetupContent_WithMobile_WithImage_WithVideo)this.Image();
        }

        IXmlSitemap_SetupContent_WithNews_WithMobile_WithVideo IXmlSitemap_SetupContent_News<IXmlSitemap_SetupContent_WithNews_WithMobile_WithVideo>.News()
        {
            return (IXmlSitemap_SetupContent_WithNews_WithMobile_WithVideo)this.News();
        }

        IXmlSitemap_SetupContent_WithMobile_WithVideo IXmlSitemap_SetupContent_Custom<IXmlSitemap_SetupContent_WithMobile_WithVideo>.Custom(ISpecializedContentXmlWriterFactory specializedContentXmlWriterFactory)
        {
            return (IXmlSitemap_SetupContent_WithMobile_WithVideo)this.Custom(specializedContentXmlWriterFactory);
        }




        IXmlSitemap_SetupContent_Finalizer IXmlSitemap_SetupContent_News<IXmlSitemap_SetupContent_Finalizer>.News()
        {
            return this.News();
        }

        IXmlSitemap_SetupContent_WithMobile_WithImage_WithVideo IXmlSitemap_SetupContent_Custom<IXmlSitemap_SetupContent_WithMobile_WithImage_WithVideo>.Custom(ISpecializedContentXmlWriterFactory specializedContentXmlWriterFactory)
        {
            return (IXmlSitemap_SetupContent_WithMobile_WithImage_WithVideo)this.Custom(specializedContentXmlWriterFactory);
        }
        
    }


    public abstract class XmlSitemap_SetupContent_WithImage : XmlSitemap_SetupContent_WithVideo,
        IXmlSitemap_SetupContent_WithImage,
        IXmlSitemap_SetupContent_WithImage_WithVideo
    {

        IXmlSitemap_SetupContent_WithImage_WithVideo IXmlSitemap_SetupContent_Video<IXmlSitemap_SetupContent_WithImage_WithVideo>.Video()
        {
            return (IXmlSitemap_SetupContent_WithImage_WithVideo)this.Video();
        }

        IXmlSitemap_SetupContent_WithMobile_WithImage IXmlSitemap_SetupContent_Mobile<IXmlSitemap_SetupContent_WithMobile_WithImage>.Mobile()
        {
            return (IXmlSitemap_SetupContent_WithMobile_WithImage)this.Mobile();
        }

        IXmlSitemap_SetupContent_WithNews_WithImage IXmlSitemap_SetupContent_News<IXmlSitemap_SetupContent_WithNews_WithImage>.News()
        {
            return (IXmlSitemap_SetupContent_WithNews_WithImage)this.News();
        }

        IXmlSitemap_SetupContent_WithImage IXmlSitemap_SetupContent_Custom<IXmlSitemap_SetupContent_WithImage>.Custom(ISpecializedContentXmlWriterFactory specializedContentXmlWriterFactory)
        {
            return (IXmlSitemap_SetupContent_WithImage)this.Custom(specializedContentXmlWriterFactory);
        }





        IXmlSitemap_SetupContent_WithMobile_WithImage_WithVideo IXmlSitemap_SetupContent_Mobile<IXmlSitemap_SetupContent_WithMobile_WithImage_WithVideo>.Mobile()
        {
            return (IXmlSitemap_SetupContent_WithMobile_WithImage_WithVideo)this.Mobile();
        }

        IXmlSitemap_SetupContent_WithNews_WithImage_WithVideo IXmlSitemap_SetupContent_News<IXmlSitemap_SetupContent_WithNews_WithImage_WithVideo>.News()
        {
            return (IXmlSitemap_SetupContent_WithNews_WithImage_WithVideo)this.News();
        }

        IXmlSitemap_SetupContent_WithImage_WithVideo IXmlSitemap_SetupContent_Custom<IXmlSitemap_SetupContent_WithImage_WithVideo>.Custom(ISpecializedContentXmlWriterFactory specializedContentXmlWriterFactory)
        {
            return (IXmlSitemap_SetupContent_WithImage_WithVideo)this.Custom(specializedContentXmlWriterFactory);
        }

    }


    public abstract class XmlSitemap_SetupContent_WithVideo : XmlSitemap_SetupContent_BuilderBase,
        IXmlSitemap_SetupContent_WithVideo
    {

        IXmlSitemap_SetupContent_WithImage_WithVideo IXmlSitemap_SetupContent_Image<IXmlSitemap_SetupContent_WithImage_WithVideo>.Image()
        {
            return (IXmlSitemap_SetupContent_WithImage_WithVideo)this.Image();
        }

        IXmlSitemap_SetupContent_WithMobile_WithVideo IXmlSitemap_SetupContent_Mobile<IXmlSitemap_SetupContent_WithMobile_WithVideo>.Mobile()
        {
            return (IXmlSitemap_SetupContent_WithMobile_WithVideo)this.Mobile();
        }

        IXmlSitemap_SetupContent_WithNews_WithVideo IXmlSitemap_SetupContent_News<IXmlSitemap_SetupContent_WithNews_WithVideo>.News()
        {
            return (IXmlSitemap_SetupContent_WithNews_WithVideo)this.News();
        }

        IXmlSitemap_SetupContent_WithVideo IXmlSitemap_SetupContent_Custom<IXmlSitemap_SetupContent_WithVideo>.Custom(ISpecializedContentXmlWriterFactory specializedContentXmlWriterFactory)
        {
            return (IXmlSitemap_SetupContent_WithVideo)this.Custom(specializedContentXmlWriterFactory);
        }

        // Not shown unless 1 piece of content is selected
        public abstract IXmlSitemap_SetupContent_Finalizer OmitUrlsWithoutMatchingContent();  
    }

    public abstract class XmlSitemap_SetupContent_BuilderBase
        : IXmlSitemap_SetupContent_Starter
    {
        public abstract IXmlSitemap_SetupContent_WithVideo Video();

        public abstract IXmlSitemap_SetupContent_WithImage Image();

        public abstract IXmlSitemap_SetupContent_WithMobile Mobile();

        public abstract IXmlSitemap_SetupContent_WithNews News();

        public abstract IXmlSitemap_SetupContent_Starter Custom(ISpecializedContentXmlWriterFactory specializedContentXmlWriterFactory);

        public abstract IDictionary<Type, ISpecializedContentXmlWriterFactory> Create();

        public abstract IEnumerable<ISpecializedContentXmlWriterFactory> SpecializedContentXmlWriterFactories { get; }

        public abstract bool UrlsWithNonMatchingContentOmitted { get; }
    }

    //public interface IXmlSitemap_SetupContent
    //{
    //}

    // one time
    public interface IXmlSitemap_SetupContent_News<TRemainder>
    {
        TRemainder News();
    }

    // one time
    public interface IXmlSitemap_SetupContent_Mobile<TRemainder>
    {
        TRemainder Mobile();
    }

    // one time
    public interface IXmlSitemap_SetupContent_Image<TRemainder>
    {
        TRemainder Image();
    }

    // one time
    public interface IXmlSitemap_SetupContent_Video<TRemainder>
    {
        TRemainder Video();
    }

    // one time - force this to be last by calling finalizer
    public interface IXmlSitemap_SetupContent_OmitUrlsWithoutMatchingContent<TRemainder>
    {
        TRemainder OmitUrlsWithoutMatchingContent();
    }

    // multiple times
    public interface IXmlSitemap_SetupContent_Custom<TRemainder>
    {
        TRemainder Custom(ISpecializedContentXmlWriterFactory specializedContentXmlWriterFactory);
    }



    public interface IXmlSitemap_SetupContent_Finalizer
        : IFluentInterface
    {
        IDictionary<Type, ISpecializedContentXmlWriterFactory> Create();

        IEnumerable<ISpecializedContentXmlWriterFactory> SpecializedContentXmlWriterFactories { get; }

        bool UrlsWithNonMatchingContentOmitted { get; }
    }

    public interface IXmlSitemap_SetupContent_Starter
        : IXmlSitemap_SetupContent_Video<IXmlSitemap_SetupContent_WithVideo>,
        IXmlSitemap_SetupContent_Image<IXmlSitemap_SetupContent_WithImage>,
        IXmlSitemap_SetupContent_Mobile<IXmlSitemap_SetupContent_WithMobile>,
        IXmlSitemap_SetupContent_News<IXmlSitemap_SetupContent_WithNews>,
        IXmlSitemap_SetupContent_Custom<IXmlSitemap_SetupContent_Starter>,
        IXmlSitemap_SetupContent_Finalizer
    {
    }


    // 1 item set

    public interface IXmlSitemap_SetupContent_WithNews
        : IXmlSitemap_SetupContent_Video<IXmlSitemap_SetupContent_WithNews_WithVideo>,
        IXmlSitemap_SetupContent_Image<IXmlSitemap_SetupContent_WithNews_WithImage>,
        IXmlSitemap_SetupContent_Mobile<IXmlSitemap_SetupContent_WithNews_WithMobile>,
        IXmlSitemap_SetupContent_Custom<IXmlSitemap_SetupContent_WithNews>,
        IXmlSitemap_SetupContent_OmitUrlsWithoutMatchingContent<IXmlSitemap_SetupContent_Finalizer>,
        IXmlSitemap_SetupContent_Finalizer
    {
    }

    public interface IXmlSitemap_SetupContent_WithMobile
        : IXmlSitemap_SetupContent_Video<IXmlSitemap_SetupContent_WithMobile_WithVideo>,
        IXmlSitemap_SetupContent_Image<IXmlSitemap_SetupContent_WithMobile_WithImage>,
        IXmlSitemap_SetupContent_News<IXmlSitemap_SetupContent_WithNews_WithMobile>,
        IXmlSitemap_SetupContent_Custom<IXmlSitemap_SetupContent_WithMobile>,
        IXmlSitemap_SetupContent_OmitUrlsWithoutMatchingContent<IXmlSitemap_SetupContent_Finalizer>,
        IXmlSitemap_SetupContent_Finalizer
    {
    }

    public interface IXmlSitemap_SetupContent_WithImage
        : IXmlSitemap_SetupContent_Video<IXmlSitemap_SetupContent_WithImage_WithVideo>,
        IXmlSitemap_SetupContent_Mobile<IXmlSitemap_SetupContent_WithMobile_WithImage>,
        IXmlSitemap_SetupContent_News<IXmlSitemap_SetupContent_WithNews_WithImage>,
        IXmlSitemap_SetupContent_Custom<IXmlSitemap_SetupContent_WithImage>,
        IXmlSitemap_SetupContent_OmitUrlsWithoutMatchingContent<IXmlSitemap_SetupContent_Finalizer>,
        IXmlSitemap_SetupContent_Finalizer
    {
    }

    public interface IXmlSitemap_SetupContent_WithVideo
        : IXmlSitemap_SetupContent_Image<IXmlSitemap_SetupContent_WithImage_WithVideo>,
        IXmlSitemap_SetupContent_Mobile<IXmlSitemap_SetupContent_WithMobile_WithVideo>,
        IXmlSitemap_SetupContent_News<IXmlSitemap_SetupContent_WithNews_WithVideo>,
        IXmlSitemap_SetupContent_Custom<IXmlSitemap_SetupContent_WithVideo>,
        IXmlSitemap_SetupContent_OmitUrlsWithoutMatchingContent<IXmlSitemap_SetupContent_Finalizer>,
        IXmlSitemap_SetupContent_Finalizer
    {
    }

    // 2 items set

    public interface IXmlSitemap_SetupContent_WithNews_WithMobile
        : IXmlSitemap_SetupContent_Video<IXmlSitemap_SetupContent_WithNews_WithMobile_WithVideo>,
        IXmlSitemap_SetupContent_Image<IXmlSitemap_SetupContent_WithNews_WithMobile_WithImage>,
        IXmlSitemap_SetupContent_Custom<IXmlSitemap_SetupContent_WithNews_WithMobile>,
        IXmlSitemap_SetupContent_OmitUrlsWithoutMatchingContent<IXmlSitemap_SetupContent_Finalizer>,
        IXmlSitemap_SetupContent_Finalizer
    {
    }

    public interface IXmlSitemap_SetupContent_WithNews_WithImage
        : IXmlSitemap_SetupContent_Video<IXmlSitemap_SetupContent_WithNews_WithImage_WithVideo>,
        IXmlSitemap_SetupContent_Mobile<IXmlSitemap_SetupContent_WithNews_WithMobile_WithImage>,
        IXmlSitemap_SetupContent_Custom<IXmlSitemap_SetupContent_WithNews_WithImage>,
        IXmlSitemap_SetupContent_OmitUrlsWithoutMatchingContent<IXmlSitemap_SetupContent_Finalizer>,
        IXmlSitemap_SetupContent_Finalizer
    {
    }

    public interface IXmlSitemap_SetupContent_WithNews_WithVideo
        : IXmlSitemap_SetupContent_Image<IXmlSitemap_SetupContent_WithNews_WithImage_WithVideo>,
        IXmlSitemap_SetupContent_Mobile<IXmlSitemap_SetupContent_WithNews_WithMobile_WithVideo>,
        IXmlSitemap_SetupContent_Custom<IXmlSitemap_SetupContent_WithNews_WithVideo>,
        IXmlSitemap_SetupContent_OmitUrlsWithoutMatchingContent<IXmlSitemap_SetupContent_Finalizer>,
        IXmlSitemap_SetupContent_Finalizer
    {
    }

    public interface IXmlSitemap_SetupContent_WithMobile_WithImage
        : IXmlSitemap_SetupContent_Video<IXmlSitemap_SetupContent_WithMobile_WithImage_WithVideo>,
        IXmlSitemap_SetupContent_News<IXmlSitemap_SetupContent_WithNews_WithMobile_WithImage>,
        IXmlSitemap_SetupContent_Custom<IXmlSitemap_SetupContent_WithMobile_WithImage>,
        IXmlSitemap_SetupContent_OmitUrlsWithoutMatchingContent<IXmlSitemap_SetupContent_Finalizer>,
        IXmlSitemap_SetupContent_Finalizer
    {
    }

    public interface IXmlSitemap_SetupContent_WithMobile_WithVideo
        : IXmlSitemap_SetupContent_Image<IXmlSitemap_SetupContent_WithMobile_WithImage_WithVideo>,
        IXmlSitemap_SetupContent_News<IXmlSitemap_SetupContent_WithNews_WithMobile_WithVideo>,
        IXmlSitemap_SetupContent_Custom<IXmlSitemap_SetupContent_WithMobile_WithVideo>,
        IXmlSitemap_SetupContent_OmitUrlsWithoutMatchingContent<IXmlSitemap_SetupContent_Finalizer>,
        IXmlSitemap_SetupContent_Finalizer
    {
    }

    public interface IXmlSitemap_SetupContent_WithImage_WithVideo
        : IXmlSitemap_SetupContent_Mobile<IXmlSitemap_SetupContent_WithMobile_WithImage_WithVideo>,
        IXmlSitemap_SetupContent_News<IXmlSitemap_SetupContent_WithNews_WithImage_WithVideo>,
        IXmlSitemap_SetupContent_Custom<IXmlSitemap_SetupContent_WithImage_WithVideo>,
        IXmlSitemap_SetupContent_OmitUrlsWithoutMatchingContent<IXmlSitemap_SetupContent_Finalizer>,
        IXmlSitemap_SetupContent_Finalizer
    {
    }


    // 3 items set

    public interface IXmlSitemap_SetupContent_WithMobile_WithImage_WithVideo
        : IXmlSitemap_SetupContent_News<IXmlSitemap_SetupContent_Finalizer>,
        IXmlSitemap_SetupContent_Custom<IXmlSitemap_SetupContent_WithMobile_WithImage_WithVideo>,
        IXmlSitemap_SetupContent_OmitUrlsWithoutMatchingContent<IXmlSitemap_SetupContent_Finalizer>,
        IXmlSitemap_SetupContent_Finalizer
    {
    }

    public interface IXmlSitemap_SetupContent_WithNews_WithImage_WithVideo
        : IXmlSitemap_SetupContent_Mobile<IXmlSitemap_SetupContent_Finalizer>,
        IXmlSitemap_SetupContent_Custom<IXmlSitemap_SetupContent_WithNews_WithImage_WithVideo>,
        IXmlSitemap_SetupContent_OmitUrlsWithoutMatchingContent<IXmlSitemap_SetupContent_Finalizer>,
        IXmlSitemap_SetupContent_Finalizer
    {
    }

    public interface IXmlSitemap_SetupContent_WithNews_WithMobile_WithVideo
        : IXmlSitemap_SetupContent_Image<IXmlSitemap_SetupContent_Finalizer>,
        IXmlSitemap_SetupContent_Custom<IXmlSitemap_SetupContent_WithNews_WithMobile_WithVideo>,
        IXmlSitemap_SetupContent_OmitUrlsWithoutMatchingContent<IXmlSitemap_SetupContent_Finalizer>,
        IXmlSitemap_SetupContent_Finalizer
    {
    }

    public interface IXmlSitemap_SetupContent_WithNews_WithMobile_WithImage
        : IXmlSitemap_SetupContent_Video<IXmlSitemap_SetupContent_Finalizer>,
        IXmlSitemap_SetupContent_Custom<IXmlSitemap_SetupContent_WithNews_WithMobile_WithImage>,
        IXmlSitemap_SetupContent_OmitUrlsWithoutMatchingContent<IXmlSitemap_SetupContent_Finalizer>,
        IXmlSitemap_SetupContent_Finalizer
    {
    }
    
}
