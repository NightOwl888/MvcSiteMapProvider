using System;
using System.Collections.Generic;
using System.Linq;
using MvcSiteMapProvider.Globalization;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized.Image
{
    public interface IPreparedImageContentFactory
    {
        IPreparedImageContent Create(IImageContent imageContent, IXmlSitemapUrlResolver urlResolver, ICultureContext cultureContext);
        void Release(IPreparedImageContent preparedImageContent);
    }
}
