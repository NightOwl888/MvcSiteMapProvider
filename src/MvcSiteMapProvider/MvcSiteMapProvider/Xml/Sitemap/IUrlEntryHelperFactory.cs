﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Xml.Sitemap
{
    public interface IUrlEntryHelperFactory
    {
        IUrlEntryHelper Create(int skip, int take, Action<IUrlEntry> addUrlEntryMethod);
    }
}