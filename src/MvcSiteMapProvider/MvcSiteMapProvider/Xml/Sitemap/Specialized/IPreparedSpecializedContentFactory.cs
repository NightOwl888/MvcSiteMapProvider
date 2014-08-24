using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvcSiteMapProvider.Globalization;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized
{
    public interface IPreparedSpecializedContentFactory
    {
        IPreparedSpecializedContent Create(ISpecializedContent specializedContent, ISitemapUrlResolver urlResolver, ICultureContext cultureContext);
        void Release(IPreparedSpecializedContent preparedSpecializedContent);
        Type ContentType { get; }
    }
}
