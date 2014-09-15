using System;
using System.Collections.Generic;
using System.Linq;
using MvcSiteMapProvider.Xml.Sitemap.Specialized;

namespace MvcSiteMapProvider.Xml.Sitemap.Configuration
{
    public class SpecializedContentXmlWriterFactoryStrategyBuilder
        : ISpecializedContentXmlWriterFactoryStrategyBuilder
    {
        public SpecializedContentXmlWriterFactoryStrategyBuilder()
            : this(new Dictionary<Type, ISpecializedContentXmlWriterFactory>())
        {
        }

        private SpecializedContentXmlWriterFactoryStrategyBuilder(
            IDictionary<Type, ISpecializedContentXmlWriterFactory> specializedContentXmlWriterFactories
            )
        {
            if (specializedContentXmlWriterFactories == null)
                throw new ArgumentNullException("specializedContentXmlWriterFactories");

            this.specializedContentXmlWriterFactories = specializedContentXmlWriterFactories;
        }

        private readonly IDictionary<Type, ISpecializedContentXmlWriterFactory> specializedContentXmlWriterFactories;

        public ISpecializedContentXmlWriterFactoryStrategyBuilder AddSpecializedContentXmlWriterFactory(ISpecializedContentXmlWriterFactory specializedContentXmlWriterFactory)
        {
            if (specializedContentXmlWriterFactory != null)
            {
                this.specializedContentXmlWriterFactories.Add(specializedContentXmlWriterFactory.ContentType, specializedContentXmlWriterFactory);
            }
            return new SpecializedContentXmlWriterFactoryStrategyBuilder(this.specializedContentXmlWriterFactories);
        }

        public ISpecializedContentXmlWriterFactoryStrategyBuilder AddSpecializedContentXmlWriterFactories(IEnumerable<ISpecializedContentXmlWriterFactory> specializedContentXmlWriterFactories)
        {
            foreach (var specializedContentXmlWriterFactory in specializedContentXmlWriterFactories)
            {
                if (specializedContentXmlWriterFactory != null)
                {
                    this.specializedContentXmlWriterFactories.Add(specializedContentXmlWriterFactory.ContentType, specializedContentXmlWriterFactory);
                }
            }
            return new SpecializedContentXmlWriterFactoryStrategyBuilder(this.specializedContentXmlWriterFactories);
        }

        public ISpecializedContentXmlWriterFactoryStrategyBuilder RemoveSpecializedContentXmlWriterFactory(Type contentType)
        {
            this.specializedContentXmlWriterFactories.Remove(contentType);
            return new SpecializedContentXmlWriterFactoryStrategyBuilder(this.specializedContentXmlWriterFactories);
        }

        public ISpecializedContentXmlWriterFactoryStrategyBuilder ClearSpecializedContentXmlWriterFactories()
        {
            this.specializedContentXmlWriterFactories.Clear();
            return new SpecializedContentXmlWriterFactoryStrategyBuilder(this.specializedContentXmlWriterFactories);
        }

        public ISpecializedContentXmlWriterFactoryStrategy Create()
        {
            return new SpecializedContentXmlWriterFactoryStrategy(this.specializedContentXmlWriterFactories.Values.ToArray());
        }
    }
}
