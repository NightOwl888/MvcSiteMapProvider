﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    public interface ISitemapPageNameProvider
    {
        string FirstPageNameTemplate { get; set; }
        string PageNameTemplate { get; set; }
    }
}
