using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized
{
    public interface IPreparedSpecializedContentFactoryStrategy
    {
        IPreparedSpecializedContentFactory GetFactory(Type specializedContentType);
        //IEnumerable<Type> GetRegisteredContentTypes(); // TODO: Check to see if it makes sense to move this here
    }
}
