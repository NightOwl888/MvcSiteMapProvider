using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvcSiteMapProvider.Globalization;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized
{
    public class PreparedVideoContentFactory
        : IPreparedSpecializedContentFactory
    {
        // TODO: Finish implementation

        public IPreparedSpecializedContent Create(ISpecializedContent specializedContent, ISitemapUrlResolver urlResolver, ICultureContext cultureContext)
        {
            throw new NotImplementedException();
        }

        public void Release(IPreparedSpecializedContent preparedSpecializedContent)
        {
            throw new NotImplementedException();
        }

        public Type ContentType
        {
            get { throw new NotImplementedException(); }
        }
    }
}
