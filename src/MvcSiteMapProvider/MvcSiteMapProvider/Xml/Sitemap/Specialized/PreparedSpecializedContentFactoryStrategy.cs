using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized
{
    public class PreparedSpecializedContentFactoryStrategy
        : IPreparedSpecializedContentFactoryStrategy
    {
        public PreparedSpecializedContentFactoryStrategy(IPreparedSpecializedContentFactory[] preparedSpecializedContentFactories)
        {
            if (preparedSpecializedContentFactories == null)
                throw new ArgumentNullException("preparedSpecializedContentFactories");

            this.preparedSpecializedContentFactories = preparedSpecializedContentFactories;
        }

        private readonly IPreparedSpecializedContentFactory[] preparedSpecializedContentFactories;

        public IPreparedSpecializedContentFactory GetFactory(Type specializedContentType)
        {
            var result = this.preparedSpecializedContentFactories.FirstOrDefault(x => x.ContentType.IsAssignableFrom(specializedContentType));
            if (result == null)
            {
                // TODO: Throw appropriate error message indicating the content type does not have a factory registered with the strategy class
            }

            return result;
        }
    }
}
