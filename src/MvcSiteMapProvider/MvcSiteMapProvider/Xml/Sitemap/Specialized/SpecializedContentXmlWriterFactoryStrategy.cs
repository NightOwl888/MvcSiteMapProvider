using System;
using System.Collections.Generic;
using System.Linq;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized
{
    public class SpecializedContentXmlWriterFactoryStrategy
        : ISpecializedContentXmlWriterFactoryStrategy
    {
        public SpecializedContentXmlWriterFactoryStrategy(ISpecializedContentXmlWriterFactory[] specializedContentXmlWriterFactories)
        {
            if (specializedContentXmlWriterFactories == null)
                throw new ArgumentNullException("specializedContentXmlWriterFactories");

            this.specializedContentXmlWriterFactories = specializedContentXmlWriterFactories;
        }

        private readonly ISpecializedContentXmlWriterFactory[] specializedContentXmlWriterFactories;

        public ISpecializedContentXmlWriterFactory GetFactory(Type specializedContentType)
        {
            var result = this.specializedContentXmlWriterFactories.FirstOrDefault(x => x.ContentType.IsAssignableFrom(specializedContentType));
            if (result == null)
            {
                // TODO: Throw appropriate error message indicating the content type does not have a factory registered with the strategy class
            }

            return result;
        }

        public IEnumerable<Type> GetRegisteredContentTypes()
        {
            return (from factories in this.specializedContentXmlWriterFactories
                    select factories.ContentType).ToList();
        }
    }
}
