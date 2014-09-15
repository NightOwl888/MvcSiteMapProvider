using System;
using System.IO;

namespace MvcSiteMapProvider.IO
{
    public interface IStreamFactory
    {
        Stream CreateReadable(string identifier);
        Stream CreateWriteable(string identifier);
    }
}
