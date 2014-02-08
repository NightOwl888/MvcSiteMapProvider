using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Builder.Fluent
{
    public interface IUrlNodeOptionalValueBuilder
        : IOptionalValueBuilder
    {
        IUrlNodeOptionalValueBuilder WithInheritableRouteValues(Action<IDescendantRouteBuilder> expression);
    }
}
