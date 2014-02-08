using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Builder.Fluent
{
    public interface IDescendantGroupingNodeOptionalValueBuilder
        : IFluentInterface
    {
        IDescendantGroupingNodeOptionalValueBuilder WithDisplayValues(Action<IDisplayBuilder> expression);

        IDescendantGroupingNodeOptionalValueBuilder WithInheritableRouteValues(Action<IRouteBuilder> expression);

        //IDescendantGroupingNodeOptionalValueBuilder WithInheritableSeoValues(Action<ISearchEngineBuilder> expression);

        IDescendantGroupingNodeOptionalValueBuilder WithImplicitResourceKey(string value);

        IDescendantGroupingNodeOptionalValueBuilder WithCustomAttribute(string key, object value);

        IDescendantGroupingNodeOptionalValueBuilder WithCustomAttributes(IDictionary<string, object> values);

        IDescendantGroupingNodeOptionalValueBuilder WithKey(string value);

        IDescendantGroupingNodeOptionalValueBuilder WithChildNodes(Action<IDescendantNodeBuilder> values);

        IEnumerable<ISiteMapNodeToParentRelation> ToList();
    }
}
