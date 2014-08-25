using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized.Video
{
    [Flags]
    public enum VideoPlatform
    {
        Undefined = 0,

        Web = 1,

        Mobile = 2,

        TV = 4
    }
}
