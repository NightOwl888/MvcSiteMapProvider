using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Builder.Fluent
{
    public interface IDescendantUrlNodeOptionalValueBuilder
    : IDescendantOptionalValueBuilder
    {
        IDescendantUrlNodeOptionalValueBuilder WithInheritableRouteValues(Action<IDescendantRouteBuilder> expression);
    }
}
