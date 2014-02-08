using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Builder.Fluent
{
    public interface IUrlResolutionBuilder
        : IFluentInterface
    {
        IUrlResolutionBuilder WithUrlResolver(string value);

        IUrlResolutionBuilder CacheResolvedUrl(bool value);
    }
}
