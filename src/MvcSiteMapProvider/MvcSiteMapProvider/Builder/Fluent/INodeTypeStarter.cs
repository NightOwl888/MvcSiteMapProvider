using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Builder.Fluent
{
    public interface INodeTypeStarter
        : IFluentInterface
    {
        IDisplayStarter MatchingRoute(Action<IRouteBuilder> expression);

        IUrlNodeDisplayStarter MatchingUrl(string value);

        IDynamicOptionalValueBuilder WithDynamicNodeProvider(string value);

        IGroupingNodeOptionalValueBuilder AsGroupingNodeTitled(string value);
    }
}
