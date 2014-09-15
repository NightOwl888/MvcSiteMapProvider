using System;
using System.IO;

namespace MvcSiteMapProvider.IO
{
    public class FileStreamFactory
        : IStreamFactory
    {
        public Stream CreateReadable(string fileName)
        {
            return new FileStream(fileName, FileMode.Open, FileAccess.Read);
        }

        public Stream CreateWriteable(string fileName)
        {
            return new FileStream(fileName, FileMode.Create, FileAccess.Write);
        }
    }
}
