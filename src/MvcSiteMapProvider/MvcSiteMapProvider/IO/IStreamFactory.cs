using System;
using System.IO;

namespace MvcSiteMapProvider.IO
{
    public interface IStreamFactory
    {
        Stream Create();
        void Release(Stream stream);
    }
}
