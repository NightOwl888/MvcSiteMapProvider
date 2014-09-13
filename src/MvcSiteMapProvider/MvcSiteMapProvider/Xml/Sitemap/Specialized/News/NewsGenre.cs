using System;

namespace MvcSiteMapProvider.Xml.Sitemap.Specialized.News
{
    [Flags]
    public enum NewsGenre
    {
        Undefined = 0,

        PressRelease = 1,

        Satire = 2,

        Blog = 4,

        OpEd = 8,

        Opinion = 16,

        UserGenerated = 32
    }
}
