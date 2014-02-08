using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Builder.Fluent
{
    [Flags]
    public enum MetaRobots
    {
        Index = 1,
        NoIndex = 2,
        Follow = 4,
        NoFollow = 8,
        None = 16,
        NoArchive = 32,
        NoCache = 64,
        NoSnippet = 128,
        NoPreview = 256,
        NoOpenDirectoryProject = 512,
        NoYahooDirectory = 1024
    }
}
