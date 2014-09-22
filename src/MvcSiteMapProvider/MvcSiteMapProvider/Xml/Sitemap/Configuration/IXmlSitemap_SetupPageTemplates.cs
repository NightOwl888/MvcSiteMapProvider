using System;
using System.Collections.Generic;
using System.Linq;
using MvcSiteMapProvider.ComponentModel;

namespace MvcSiteMapProvider.Xml.Sitemap.Configuration
{
    public class XmlSitemap_SetupPageTemplates_Builder : XmlSitemap_SetupPageTemplates_WithDefaultFeedRoot,
        IXmlSitemap_SetupPageTemplates_Starter
    {
        public XmlSitemap_SetupPageTemplates_Builder()
            : this(
            defaultFeedRootPageName: "sitemap.xml",
            defaultFeedPageName: "sitemap-{page}.xml",
            namedFeedRootPageName: "{feedName}-sitemap.xml",
            namedFeedPageName: "{feedName}-sitemap-{page}.xml")
        {
        }

        private XmlSitemap_SetupPageTemplates_Builder(
            string defaultFeedRootPageName,
            string defaultFeedPageName,
            string namedFeedRootPageName,
            string namedFeedPageName
            )
        {
            if (string.IsNullOrEmpty(defaultFeedRootPageName))
                throw new ArgumentNullException("defaultFeedRootPageName");
            if (string.IsNullOrEmpty(defaultFeedPageName))
                throw new ArgumentNullException("defaultFeedPageName");
            if (string.IsNullOrEmpty(namedFeedRootPageName))
                throw new ArgumentNullException("namedFeedRootPageName");
            if (string.IsNullOrEmpty(namedFeedPageName))
                throw new ArgumentNullException("namedFeedPageName");

            this.defaultFeedRootPageName = defaultFeedRootPageName;
            this.defaultFeedPageName = defaultFeedPageName;
            this.namedFeedRootPageName = namedFeedRootPageName;
            this.namedFeedPageName = namedFeedPageName;
        }
        private readonly string defaultFeedRootPageName;
        private readonly string defaultFeedPageName;
        private readonly string namedFeedRootPageName;
        private readonly string namedFeedPageName;

        //public XmlSitemap_SetupPageTemplates_Builder()
        //{
        //    this.defaultFeedRootPageName = "sitemap.xml";
        //    this.defaultFeedPageName = "sitemap-{page}.xml";
        //    this.namedFeedRootPageName = "{feedName}-sitemap.xml";
        //    this.namedFeedPageName = "{feedName}-sitemap-{page}.xml";
        //    this.buildActions = new Stack<Action<XmlSitemap_SetupPageTemplates_Builder>>();
        //}    
        //private string defaultFeedRootPageName;
        //private string defaultFeedPageName;
        //private string namedFeedRootPageName;
        //private string namedFeedPageName;
        //private readonly Stack<Action<XmlSitemap_SetupPageTemplates_Builder>> buildActions;


        //public override IXmlSitemap_SetupPageTemplates_WithDefaultFeedRoot WithDefaultFeedRoot(string defaultFeedRootPageName)
        //{
        //    this.buildActions.Push(x => x.defaultFeedRootPageName = defaultFeedRootPageName);
        //    return this;
        //}

        //public override IXmlSitemap_SetupPageTemplates_WithDefaultFeedPaged WithDefaultFeedPaged(string defaultFeedPageName)
        //{
        //    this.buildActions.Push(x => x.defaultFeedPageName = defaultFeedPageName);
        //    return this;
        //}

        //public override IXmlSitemap_SetupPageTemplates_WithNamedFeedRoot WithNamedFeedRoot(string namedFeedRootPageName)
        //{
        //    this.buildActions.Push(x => x.namedFeedRootPageName = namedFeedRootPageName);
        //    return this;
        //}

        //public override IXmlSitemap_SetupPageTemplates_WithNamedFeedPaged WithNamedFeedPaged(string namedFeedPageName)
        //{
        //    this.buildActions.Push(x => x.namedFeedPageName = namedFeedPageName);
        //    return this;
        //}

        //public override IXmlSitemapFeedPageNameProvider Create()
        //{
        //    while (this.buildActions.Count > 0)
        //    {
        //        this.buildActions.Pop()(this);
        //    }
        //    return new XmlSitemapFeedPageNameProvider(this.defaultFeedRootPageName, this.defaultFeedPageName, this.namedFeedRootPageName, this.namedFeedPageName);
        //}


        public override IXmlSitemap_SetupPageTemplates_WithDefaultFeedRoot WithDefaultFeedRoot(string defaultFeedRootPageName)
        {
            return new XmlSitemap_SetupPageTemplates_Builder(defaultFeedRootPageName,
                this.defaultFeedPageName, this.namedFeedRootPageName, this.namedFeedPageName);
        }

        public override IXmlSitemap_SetupPageTemplates_WithDefaultFeedPaged WithDefaultFeedPaged(string defaultFeedPageName)
        {
            return new XmlSitemap_SetupPageTemplates_Builder(this.defaultFeedRootPageName,
                defaultFeedPageName, this.namedFeedRootPageName, this.namedFeedPageName);
        }

        public override IXmlSitemap_SetupPageTemplates_WithNamedFeedRoot WithNamedFeedRoot(string namedFeedRootPageName)
        {
            return new XmlSitemap_SetupPageTemplates_Builder(this.defaultFeedRootPageName,
                this.defaultFeedPageName, namedFeedRootPageName, this.namedFeedPageName);
        }

        public override IXmlSitemap_SetupPageTemplates_WithNamedFeedPaged WithNamedFeedPaged(string namedFeedPageName)
        {
            return (IXmlSitemap_SetupPageTemplates_WithNamedFeedPaged)new XmlSitemap_SetupPageTemplates_Builder(this.defaultFeedRootPageName,
                this.defaultFeedPageName, this.namedFeedRootPageName, namedFeedPageName);
        }

        public override IXmlSitemapFeedPageNameProvider Create()
        {
            return new XmlSitemapFeedPageNameProvider(this.defaultFeedRootPageName, this.defaultFeedPageName, this.namedFeedRootPageName, this.namedFeedPageName);
        }

    }

    public abstract class XmlSitemap_SetupPageTemplates_WithDefaultFeedRoot : XmlSitemap_SetupPageTemplates_WithDefaultFeedPaged,
        IXmlSitemap_SetupPageTemplates_WithDefaultFeedRoot,
        IXmlSitemap_SetupPageTemplates_WithDefaultFeedRoot_WithDefaultFeedPaged,
        IXmlSitemap_SetupPageTemplates_WithDefaultFeedRoot_WithNamedFeedRoot,
        IXmlSitemap_SetupPageTemplates_WithDefaultFeedRoot_WithNamedFeedPaged,
        IXmlSitemap_SetupPageTemplates_WithDefaultFeedRoot_WithNamedFeedRoot_WithNamedFeedPaged,
        IXmlSitemap_SetupPageTemplates_WithDefaultFeedRoot_WithDefaultFeedPaged_WithNamedFeedRoot,
        IXmlSitemap_SetupPageTemplates_WithDefaultFeedRoot_WithDefaultFeedPaged_WithNamedFeedPaged
    {

        IXmlSitemap_SetupPageTemplates_WithDefaultFeedRoot_WithDefaultFeedPaged IXmlSitemap_SetupPageTemplates_DefaultFeedPaged<IXmlSitemap_SetupPageTemplates_WithDefaultFeedRoot_WithDefaultFeedPaged>.WithDefaultFeedPaged(string defaultFeedPageName)
        {
            return (IXmlSitemap_SetupPageTemplates_WithDefaultFeedRoot_WithDefaultFeedPaged)this.WithDefaultFeedPaged(defaultFeedPageName);
        }

        IXmlSitemap_SetupPageTemplates_WithDefaultFeedRoot_WithNamedFeedRoot IXmlSitemap_SetupPageTemplates_NamedFeedRoot<IXmlSitemap_SetupPageTemplates_WithDefaultFeedRoot_WithNamedFeedRoot>.WithNamedFeedRoot(string namedFeedRootPageName)
        {
            return (IXmlSitemap_SetupPageTemplates_WithDefaultFeedRoot_WithNamedFeedRoot)this.WithNamedFeedRoot(namedFeedRootPageName);
        }

        IXmlSitemap_SetupPageTemplates_WithDefaultFeedRoot_WithNamedFeedPaged IXmlSitemap_SetupPageTemplates_NamedFeedPaged<IXmlSitemap_SetupPageTemplates_WithDefaultFeedRoot_WithNamedFeedPaged>.WithNamedFeedPaged(string namedFeedPageName)
        {
            return (IXmlSitemap_SetupPageTemplates_WithDefaultFeedRoot_WithNamedFeedPaged)this.WithNamedFeedPaged(namedFeedPageName);
        }


        IXmlSitemap_SetupPageTemplates_WithDefaultFeedRoot_WithDefaultFeedPaged_WithNamedFeedPaged IXmlSitemap_SetupPageTemplates_NamedFeedPaged<IXmlSitemap_SetupPageTemplates_WithDefaultFeedRoot_WithDefaultFeedPaged_WithNamedFeedPaged>.WithNamedFeedPaged(string namedFeedPageName)
        {
            return (IXmlSitemap_SetupPageTemplates_WithDefaultFeedRoot_WithDefaultFeedPaged_WithNamedFeedPaged)this.WithNamedFeedPaged(namedFeedPageName);
        }

        IXmlSitemap_SetupPageTemplates_WithDefaultFeedRoot_WithDefaultFeedPaged_WithNamedFeedRoot IXmlSitemap_SetupPageTemplates_NamedFeedRoot<IXmlSitemap_SetupPageTemplates_WithDefaultFeedRoot_WithDefaultFeedPaged_WithNamedFeedRoot>.WithNamedFeedRoot(string namedFeedRootPageName)
        {
            return (IXmlSitemap_SetupPageTemplates_WithDefaultFeedRoot_WithDefaultFeedPaged_WithNamedFeedRoot)this.WithNamedFeedRoot(namedFeedRootPageName);
        }



        IXmlSitemap_SetupPageTemplates_WithDefaultFeedRoot_WithDefaultFeedPaged_WithNamedFeedPaged IXmlSitemap_SetupPageTemplates_DefaultFeedPaged<IXmlSitemap_SetupPageTemplates_WithDefaultFeedRoot_WithDefaultFeedPaged_WithNamedFeedPaged>.WithDefaultFeedPaged(string defaultFeedPageName)
        {
            return (IXmlSitemap_SetupPageTemplates_WithDefaultFeedRoot_WithDefaultFeedPaged_WithNamedFeedPaged)this.WithDefaultFeedPaged(defaultFeedPageName);
        }

        IXmlSitemap_SetupPageTemplates_WithDefaultFeedRoot_WithNamedFeedRoot_WithNamedFeedPaged IXmlSitemap_SetupPageTemplates_NamedFeedPaged<IXmlSitemap_SetupPageTemplates_WithDefaultFeedRoot_WithNamedFeedRoot_WithNamedFeedPaged>.WithNamedFeedPaged(string namedFeedPageName)
        {
            return (IXmlSitemap_SetupPageTemplates_WithDefaultFeedRoot_WithNamedFeedRoot_WithNamedFeedPaged)this.WithNamedFeedPaged(namedFeedPageName);
        }




        IXmlSitemap_SetupPageTemplates_WithDefaultFeedRoot_WithNamedFeedRoot_WithNamedFeedPaged IXmlSitemap_SetupPageTemplates_NamedFeedRoot<IXmlSitemap_SetupPageTemplates_WithDefaultFeedRoot_WithNamedFeedRoot_WithNamedFeedPaged>.WithNamedFeedRoot(string namedFeedRootPageName)
        {
            return (IXmlSitemap_SetupPageTemplates_WithDefaultFeedRoot_WithNamedFeedRoot_WithNamedFeedPaged)this.WithNamedFeedRoot(namedFeedRootPageName);
        }



        IXmlSitemap_SetupPageTemplates_Finalizer IXmlSitemap_SetupPageTemplates_DefaultFeedPaged<IXmlSitemap_SetupPageTemplates_Finalizer>.WithDefaultFeedPaged(string defaultFeedPageName)
        {
            return this.WithDefaultFeedPaged(defaultFeedPageName);
        }



        IXmlSitemap_SetupPageTemplates_Finalizer IXmlSitemap_SetupPageTemplates_NamedFeedPaged<IXmlSitemap_SetupPageTemplates_Finalizer>.WithNamedFeedPaged(string namedFeedPageName)
        {
            return this.WithNamedFeedPaged(namedFeedPageName);
        }



        IXmlSitemap_SetupPageTemplates_Finalizer IXmlSitemap_SetupPageTemplates_NamedFeedRoot<IXmlSitemap_SetupPageTemplates_Finalizer>.WithNamedFeedRoot(string namedFeedRootPageName)
        {
            return this.WithNamedFeedRoot(namedFeedRootPageName);
        }


    }


    public abstract class XmlSitemap_SetupPageTemplates_WithDefaultFeedPaged : XmlSitemap_SetupPageTemplates_WithNamedFeedRoot,
        IXmlSitemap_SetupPageTemplates_WithDefaultFeedPaged,
        IXmlSitemap_SetupPageTemplates_WithDefaultFeedPaged_WithNamedFeedRoot, 
        IXmlSitemap_SetupPageTemplates_WithDefaultFeedPaged_WithNamedFeedPaged,
        IXmlSitemap_SetupPageTemplates_WithDefaultFeedPaged_WithNamedFeedRoot_WithNamedFeedPaged
    {
        IXmlSitemap_SetupPageTemplates_WithDefaultFeedRoot_WithDefaultFeedPaged IXmlSitemap_SetupPageTemplates_DefaultFeedRoot<IXmlSitemap_SetupPageTemplates_WithDefaultFeedRoot_WithDefaultFeedPaged>.WithDefaultFeedRoot(string defaultFeedRootPageName)
        {
            return (IXmlSitemap_SetupPageTemplates_WithDefaultFeedRoot_WithDefaultFeedPaged)this.WithDefaultFeedRoot(defaultFeedRootPageName);
        }

        IXmlSitemap_SetupPageTemplates_WithDefaultFeedPaged_WithNamedFeedRoot IXmlSitemap_SetupPageTemplates_NamedFeedRoot<IXmlSitemap_SetupPageTemplates_WithDefaultFeedPaged_WithNamedFeedRoot>.WithNamedFeedRoot(string namedFeedRootPageName)
        {
            return (IXmlSitemap_SetupPageTemplates_WithDefaultFeedPaged_WithNamedFeedRoot)this.WithNamedFeedRoot(namedFeedRootPageName);
        }

        IXmlSitemap_SetupPageTemplates_WithDefaultFeedRoot_WithNamedFeedPaged IXmlSitemap_SetupPageTemplates_NamedFeedPaged<IXmlSitemap_SetupPageTemplates_WithDefaultFeedRoot_WithNamedFeedPaged>.WithNamedFeedPaged(string namedFeedPageName)
        {
            return (IXmlSitemap_SetupPageTemplates_WithDefaultFeedRoot_WithNamedFeedPaged)this.WithNamedFeedPaged(namedFeedPageName);
        }

        IXmlSitemap_SetupPageTemplates_WithDefaultFeedRoot_WithDefaultFeedPaged_WithNamedFeedRoot IXmlSitemap_SetupPageTemplates_DefaultFeedRoot<IXmlSitemap_SetupPageTemplates_WithDefaultFeedRoot_WithDefaultFeedPaged_WithNamedFeedRoot>.WithDefaultFeedRoot(string defaultFeedRootPageName)
        {
            return (IXmlSitemap_SetupPageTemplates_WithDefaultFeedRoot_WithDefaultFeedPaged_WithNamedFeedRoot)this.WithDefaultFeedRoot(defaultFeedRootPageName);
        }

        IXmlSitemap_SetupPageTemplates_WithDefaultFeedPaged_WithNamedFeedRoot_WithNamedFeedPaged IXmlSitemap_SetupPageTemplates_NamedFeedPaged<IXmlSitemap_SetupPageTemplates_WithDefaultFeedPaged_WithNamedFeedRoot_WithNamedFeedPaged>.WithNamedFeedPaged(string namedFeedPageName)
        {
            return (IXmlSitemap_SetupPageTemplates_WithDefaultFeedPaged_WithNamedFeedRoot_WithNamedFeedPaged)this.WithNamedFeedPaged(namedFeedPageName);
        }


        IXmlSitemap_SetupPageTemplates_WithDefaultFeedRoot_WithDefaultFeedPaged_WithNamedFeedPaged IXmlSitemap_SetupPageTemplates_DefaultFeedRoot<IXmlSitemap_SetupPageTemplates_WithDefaultFeedRoot_WithDefaultFeedPaged_WithNamedFeedPaged>.WithDefaultFeedRoot(string defaultFeedRootPageName)
        {
            return (IXmlSitemap_SetupPageTemplates_WithDefaultFeedRoot_WithDefaultFeedPaged_WithNamedFeedPaged)this.WithDefaultFeedRoot(defaultFeedRootPageName);
        }

        IXmlSitemap_SetupPageTemplates_WithDefaultFeedPaged_WithNamedFeedRoot_WithNamedFeedPaged IXmlSitemap_SetupPageTemplates_NamedFeedRoot<IXmlSitemap_SetupPageTemplates_WithDefaultFeedPaged_WithNamedFeedRoot_WithNamedFeedPaged>.WithNamedFeedRoot(string namedFeedRootPageName)
        {
            return (IXmlSitemap_SetupPageTemplates_WithDefaultFeedPaged_WithNamedFeedRoot_WithNamedFeedPaged)this.WithNamedFeedRoot(namedFeedRootPageName);
        }

        IXmlSitemap_SetupPageTemplates_Finalizer IXmlSitemap_SetupPageTemplates_DefaultFeedRoot<IXmlSitemap_SetupPageTemplates_Finalizer>.WithDefaultFeedRoot(string defaultFeedRootPageName)
        {
            return this.WithDefaultFeedRoot(defaultFeedRootPageName);
        }
    }


    public abstract class XmlSitemap_SetupPageTemplates_WithNamedFeedRoot : XmlSitemap_SetupPageTemplates_WithNamedFeedPaged,
        IXmlSitemap_SetupPageTemplates_WithNamedFeedRoot,
        IXmlSitemap_SetupPageTemplates_WithNamedFeedRoot_WithNamedFeedPaged
    {
        IXmlSitemap_SetupPageTemplates_WithDefaultFeedRoot_WithNamedFeedRoot IXmlSitemap_SetupPageTemplates_DefaultFeedRoot<IXmlSitemap_SetupPageTemplates_WithDefaultFeedRoot_WithNamedFeedRoot>.WithDefaultFeedRoot(string defaultFeedRootPageName)
        {
            return (IXmlSitemap_SetupPageTemplates_WithDefaultFeedRoot_WithNamedFeedRoot)this.WithDefaultFeedRoot(defaultFeedRootPageName);
        }

        IXmlSitemap_SetupPageTemplates_WithDefaultFeedPaged_WithNamedFeedRoot IXmlSitemap_SetupPageTemplates_DefaultFeedPaged<IXmlSitemap_SetupPageTemplates_WithDefaultFeedPaged_WithNamedFeedRoot>.WithDefaultFeedPaged(string defaultFeedPageName)
        {
            return (IXmlSitemap_SetupPageTemplates_WithDefaultFeedPaged_WithNamedFeedRoot)this.WithDefaultFeedPaged(defaultFeedPageName);
        }

        IXmlSitemap_SetupPageTemplates_WithNamedFeedRoot_WithNamedFeedPaged IXmlSitemap_SetupPageTemplates_NamedFeedPaged<IXmlSitemap_SetupPageTemplates_WithNamedFeedRoot_WithNamedFeedPaged>.WithNamedFeedPaged(string namedFeedPageName)
        {
            return (IXmlSitemap_SetupPageTemplates_WithNamedFeedRoot_WithNamedFeedPaged)this.WithNamedFeedPaged(namedFeedPageName);
        }

        IXmlSitemap_SetupPageTemplates_WithDefaultFeedRoot_WithNamedFeedRoot_WithNamedFeedPaged IXmlSitemap_SetupPageTemplates_DefaultFeedRoot<IXmlSitemap_SetupPageTemplates_WithDefaultFeedRoot_WithNamedFeedRoot_WithNamedFeedPaged>.WithDefaultFeedRoot(string defaultFeedRootPageName)
        {
            return (IXmlSitemap_SetupPageTemplates_WithDefaultFeedRoot_WithNamedFeedRoot_WithNamedFeedPaged)this.WithDefaultFeedRoot(defaultFeedRootPageName);
        }

        IXmlSitemap_SetupPageTemplates_WithDefaultFeedPaged_WithNamedFeedRoot_WithNamedFeedPaged IXmlSitemap_SetupPageTemplates_DefaultFeedPaged<IXmlSitemap_SetupPageTemplates_WithDefaultFeedPaged_WithNamedFeedRoot_WithNamedFeedPaged>.WithDefaultFeedPaged(string defaultFeedPageName)
        {
            return (IXmlSitemap_SetupPageTemplates_WithDefaultFeedPaged_WithNamedFeedRoot_WithNamedFeedPaged)this.WithDefaultFeedPaged(defaultFeedPageName);
        }
    }


    public abstract class XmlSitemap_SetupPageTemplates_WithNamedFeedPaged : XmlSitemap_SetupPageTemplates_BuilderBase,
        IXmlSitemap_SetupPageTemplates_WithNamedFeedPaged
    {
        IXmlSitemap_SetupPageTemplates_WithDefaultFeedRoot_WithNamedFeedPaged IXmlSitemap_SetupPageTemplates_DefaultFeedRoot<IXmlSitemap_SetupPageTemplates_WithDefaultFeedRoot_WithNamedFeedPaged>.WithDefaultFeedRoot(string defaultFeedRootPageName)
        {
            return (IXmlSitemap_SetupPageTemplates_WithDefaultFeedRoot_WithNamedFeedPaged)this.WithDefaultFeedRoot(defaultFeedRootPageName);
        }

        IXmlSitemap_SetupPageTemplates_WithDefaultFeedPaged_WithNamedFeedPaged IXmlSitemap_SetupPageTemplates_DefaultFeedPaged<IXmlSitemap_SetupPageTemplates_WithDefaultFeedPaged_WithNamedFeedPaged>.WithDefaultFeedPaged(string defaultFeedPageName)
        {
            return (IXmlSitemap_SetupPageTemplates_WithDefaultFeedPaged_WithNamedFeedPaged)this.WithDefaultFeedPaged(defaultFeedPageName);
        }

        IXmlSitemap_SetupPageTemplates_WithNamedFeedRoot_WithNamedFeedPaged IXmlSitemap_SetupPageTemplates_NamedFeedRoot<IXmlSitemap_SetupPageTemplates_WithNamedFeedRoot_WithNamedFeedPaged>.WithNamedFeedRoot(string namedFeedRootPageName)
        {
            return (IXmlSitemap_SetupPageTemplates_WithNamedFeedRoot_WithNamedFeedPaged)this.WithNamedFeedRoot(namedFeedRootPageName);
        }
    }

    public abstract class XmlSitemap_SetupPageTemplates_BuilderBase
        : IXmlSitemap_SetupPageTemplates_Starter
    {
        public abstract IXmlSitemap_SetupPageTemplates_WithDefaultFeedRoot WithDefaultFeedRoot(string defaultFeedRootPageName);

        public abstract IXmlSitemap_SetupPageTemplates_WithDefaultFeedPaged WithDefaultFeedPaged(string defaultFeedPageName);

        public abstract IXmlSitemap_SetupPageTemplates_WithNamedFeedRoot WithNamedFeedRoot(string namedFeedRootPageName);

        public abstract IXmlSitemap_SetupPageTemplates_WithNamedFeedPaged WithNamedFeedPaged(string namedFeedPageName);

        public abstract IXmlSitemapFeedPageNameProvider Create();
    }

    public interface IXmlSitemap_SetupPageTemplates_DefaultFeedRoot<TRemainder>
    {
        TRemainder WithDefaultFeedRoot(string defaultFeedRootPageName);
    }

    public interface IXmlSitemap_SetupPageTemplates_DefaultFeedPaged<TRemainder>
    {
        TRemainder WithDefaultFeedPaged(string defaultFeedPageName);
    }

    public interface IXmlSitemap_SetupPageTemplates_NamedFeedRoot<TRemainder>
    {
        TRemainder WithNamedFeedRoot(string namedFeedRootPageName);
    }

    public interface IXmlSitemap_SetupPageTemplates_NamedFeedPaged<TRemainder>
    {
        TRemainder WithNamedFeedPaged(string namedFeedPageName);
    }


    public interface IXmlSitemap_SetupPageTemplates_Finalizer
        : IFluentInterface
    {
        IXmlSitemapFeedPageNameProvider Create();
    }

    public interface IXmlSitemap_SetupPageTemplates_Starter
        : IXmlSitemap_SetupPageTemplates_DefaultFeedRoot<IXmlSitemap_SetupPageTemplates_WithDefaultFeedRoot>,
        IXmlSitemap_SetupPageTemplates_DefaultFeedPaged<IXmlSitemap_SetupPageTemplates_WithDefaultFeedPaged>,
        IXmlSitemap_SetupPageTemplates_NamedFeedRoot<IXmlSitemap_SetupPageTemplates_WithNamedFeedRoot>,
        IXmlSitemap_SetupPageTemplates_NamedFeedPaged<IXmlSitemap_SetupPageTemplates_WithNamedFeedPaged>,
        IXmlSitemap_SetupPageTemplates_Finalizer
    {
    }
    

    // 1 item set

    public interface IXmlSitemap_SetupPageTemplates_WithDefaultFeedRoot
        : IXmlSitemap_SetupPageTemplates_DefaultFeedPaged<IXmlSitemap_SetupPageTemplates_WithDefaultFeedRoot_WithDefaultFeedPaged>,
        IXmlSitemap_SetupPageTemplates_NamedFeedRoot<IXmlSitemap_SetupPageTemplates_WithDefaultFeedRoot_WithNamedFeedRoot>,
        IXmlSitemap_SetupPageTemplates_NamedFeedPaged<IXmlSitemap_SetupPageTemplates_WithDefaultFeedRoot_WithNamedFeedPaged>,
        IXmlSitemap_SetupPageTemplates_Finalizer
    {
    }

    public interface IXmlSitemap_SetupPageTemplates_WithDefaultFeedPaged
        : IXmlSitemap_SetupPageTemplates_DefaultFeedRoot<IXmlSitemap_SetupPageTemplates_WithDefaultFeedRoot_WithDefaultFeedPaged>,
        IXmlSitemap_SetupPageTemplates_NamedFeedRoot<IXmlSitemap_SetupPageTemplates_WithDefaultFeedPaged_WithNamedFeedRoot>,
        IXmlSitemap_SetupPageTemplates_NamedFeedPaged<IXmlSitemap_SetupPageTemplates_WithDefaultFeedRoot_WithNamedFeedPaged>,
        IXmlSitemap_SetupPageTemplates_Finalizer
    {
    }

    public interface IXmlSitemap_SetupPageTemplates_WithNamedFeedRoot
        : IXmlSitemap_SetupPageTemplates_DefaultFeedRoot<IXmlSitemap_SetupPageTemplates_WithDefaultFeedRoot_WithNamedFeedRoot>,
        IXmlSitemap_SetupPageTemplates_DefaultFeedPaged<IXmlSitemap_SetupPageTemplates_WithDefaultFeedPaged_WithNamedFeedRoot>,
        IXmlSitemap_SetupPageTemplates_NamedFeedPaged<IXmlSitemap_SetupPageTemplates_WithNamedFeedRoot_WithNamedFeedPaged>,
        IXmlSitemap_SetupPageTemplates_Finalizer
    {
    }

    public interface IXmlSitemap_SetupPageTemplates_WithNamedFeedPaged
        : IXmlSitemap_SetupPageTemplates_DefaultFeedRoot<IXmlSitemap_SetupPageTemplates_WithDefaultFeedRoot_WithNamedFeedPaged>,
        IXmlSitemap_SetupPageTemplates_DefaultFeedPaged<IXmlSitemap_SetupPageTemplates_WithDefaultFeedPaged_WithNamedFeedPaged>,
        IXmlSitemap_SetupPageTemplates_NamedFeedRoot<IXmlSitemap_SetupPageTemplates_WithNamedFeedRoot_WithNamedFeedPaged>,
        IXmlSitemap_SetupPageTemplates_Finalizer
    {
    }


    // 2 items set

    public interface IXmlSitemap_SetupPageTemplates_WithDefaultFeedRoot_WithDefaultFeedPaged
       : IXmlSitemap_SetupPageTemplates_NamedFeedPaged<IXmlSitemap_SetupPageTemplates_WithDefaultFeedRoot_WithDefaultFeedPaged_WithNamedFeedPaged>,
       IXmlSitemap_SetupPageTemplates_NamedFeedRoot<IXmlSitemap_SetupPageTemplates_WithDefaultFeedRoot_WithDefaultFeedPaged_WithNamedFeedRoot>,
       IXmlSitemap_SetupPageTemplates_Finalizer
    {
    }

    public interface IXmlSitemap_SetupPageTemplates_WithDefaultFeedRoot_WithNamedFeedPaged
       : IXmlSitemap_SetupPageTemplates_DefaultFeedPaged<IXmlSitemap_SetupPageTemplates_WithDefaultFeedRoot_WithDefaultFeedPaged_WithNamedFeedPaged>,
       IXmlSitemap_SetupPageTemplates_NamedFeedRoot<IXmlSitemap_SetupPageTemplates_WithDefaultFeedRoot_WithNamedFeedRoot_WithNamedFeedPaged>,
       IXmlSitemap_SetupPageTemplates_Finalizer
    {
    }

    public interface IXmlSitemap_SetupPageTemplates_WithDefaultFeedPaged_WithNamedFeedPaged
       : IXmlSitemap_SetupPageTemplates_DefaultFeedRoot<IXmlSitemap_SetupPageTemplates_WithDefaultFeedRoot_WithDefaultFeedPaged_WithNamedFeedPaged>,
       IXmlSitemap_SetupPageTemplates_NamedFeedRoot<IXmlSitemap_SetupPageTemplates_WithDefaultFeedPaged_WithNamedFeedRoot_WithNamedFeedPaged>,
       IXmlSitemap_SetupPageTemplates_Finalizer
    {
    }

    public interface IXmlSitemap_SetupPageTemplates_WithDefaultFeedRoot_WithNamedFeedRoot
       : IXmlSitemap_SetupPageTemplates_DefaultFeedPaged<IXmlSitemap_SetupPageTemplates_WithDefaultFeedRoot_WithDefaultFeedPaged_WithNamedFeedPaged>,
       IXmlSitemap_SetupPageTemplates_NamedFeedPaged<IXmlSitemap_SetupPageTemplates_WithDefaultFeedRoot_WithNamedFeedRoot_WithNamedFeedPaged>,
       IXmlSitemap_SetupPageTemplates_Finalizer
    {
    }

    

    public interface IXmlSitemap_SetupPageTemplates_WithDefaultFeedPaged_WithNamedFeedRoot
       : IXmlSitemap_SetupPageTemplates_DefaultFeedRoot<IXmlSitemap_SetupPageTemplates_WithDefaultFeedRoot_WithDefaultFeedPaged_WithNamedFeedRoot>,
       IXmlSitemap_SetupPageTemplates_NamedFeedPaged<IXmlSitemap_SetupPageTemplates_WithDefaultFeedPaged_WithNamedFeedRoot_WithNamedFeedPaged>,
       IXmlSitemap_SetupPageTemplates_Finalizer
    {
    }

    public interface IXmlSitemap_SetupPageTemplates_WithNamedFeedRoot_WithNamedFeedPaged
       : IXmlSitemap_SetupPageTemplates_DefaultFeedRoot<IXmlSitemap_SetupPageTemplates_WithDefaultFeedRoot_WithNamedFeedRoot_WithNamedFeedPaged>,
       IXmlSitemap_SetupPageTemplates_DefaultFeedPaged<IXmlSitemap_SetupPageTemplates_WithDefaultFeedPaged_WithNamedFeedRoot_WithNamedFeedPaged>,
       IXmlSitemap_SetupPageTemplates_Finalizer
    {
    }


    // 3 items set

    public interface IXmlSitemap_SetupPageTemplates_WithDefaultFeedPaged_WithNamedFeedRoot_WithNamedFeedPaged
       : IXmlSitemap_SetupPageTemplates_DefaultFeedRoot<IXmlSitemap_SetupPageTemplates_Finalizer>,
       IXmlSitemap_SetupPageTemplates_Finalizer
    {
    }

    public interface IXmlSitemap_SetupPageTemplates_WithDefaultFeedRoot_WithNamedFeedRoot_WithNamedFeedPaged
       : IXmlSitemap_SetupPageTemplates_DefaultFeedPaged<IXmlSitemap_SetupPageTemplates_Finalizer>,
       IXmlSitemap_SetupPageTemplates_Finalizer
    {
    }

    public interface IXmlSitemap_SetupPageTemplates_WithDefaultFeedRoot_WithDefaultFeedPaged_WithNamedFeedPaged
       : IXmlSitemap_SetupPageTemplates_NamedFeedRoot<IXmlSitemap_SetupPageTemplates_Finalizer>,
       IXmlSitemap_SetupPageTemplates_Finalizer
    {
    }

    public interface IXmlSitemap_SetupPageTemplates_WithDefaultFeedRoot_WithDefaultFeedPaged_WithNamedFeedRoot
        : IXmlSitemap_SetupPageTemplates_NamedFeedPaged<IXmlSitemap_SetupPageTemplates_Finalizer>,
        IXmlSitemap_SetupPageTemplates_Finalizer
    {
    }
}
