using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Xml.Sitemaps
{
    public interface IUrlEntryProvider
    {
        // Total record count of all potential pages
        int GetTotalRecordCount();
        void GetEntries(IUrlEntryHelper helper); // TODO: Define helper
        // TODO: Do we need to return the feedback to the provider or can some messaging system be implemented?
    }
}
