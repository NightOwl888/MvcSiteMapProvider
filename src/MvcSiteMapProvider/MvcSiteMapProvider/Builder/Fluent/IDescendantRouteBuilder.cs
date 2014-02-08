using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Builder.Fluent
{
    public interface IDescendantRouteBuilder
        : IRouteBuilder
    {
        IDescendantRouteBuilder InheritValue(string key);

        IDescendantRouteBuilder InheritValues(IEnumerable<string> keys);
    }
}
