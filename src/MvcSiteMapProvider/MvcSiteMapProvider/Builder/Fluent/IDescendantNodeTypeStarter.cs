using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Builder.Fluent
{
    public interface IDescendantNodeTypeStarter
        : IFluentInterface
    {
        IDescendantDisplayStarter MatchingRoute(Action<IDescendantRouteBuilder> expression);

        IDescendantUrlNodeDisplayStarter MatchingUrl(string value);

        IDescendantDynamicOptionalValueBuilder WithDynamicNodeProvider(string value);

        IDescendantGroupingNodeOptionalValueBuilder AsGroupingNodeTitled(string value);
    }
}
