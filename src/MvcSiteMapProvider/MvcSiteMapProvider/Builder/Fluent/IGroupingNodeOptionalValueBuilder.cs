using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Builder.Fluent
{
    public interface IGroupingNodeOptionalValueBuilder
        : IFluentInterface
    {
        IGroupingNodeOptionalValueBuilder WithDisplayValues(Action<IDisplayBuilder> expression);

        IGroupingNodeOptionalValueBuilder WithInheritableRouteValues(Action<IRouteBuilder> expression);

        // NOTE: This might be useful if we make inheritance available on some built-in properties
        //IGroupingNodeOptionalValueBuilder WithInheritableSeoValues(Action<ISearchEngineBuilder> expression);

        IGroupingNodeOptionalValueBuilder WithImplicitResourceKey(string value);

        IGroupingNodeOptionalValueBuilder WithCustomAttribute(string key, object value);

        IGroupingNodeOptionalValueBuilder WithCustomAttributes(IDictionary<string, object> values);

        IGroupingNodeOptionalValueBuilder WithKey(string value);

        IGroupingNodeOptionalValueBuilder WithParentKey(string value);

        IGroupingNodeOptionalValueBuilder WithChildNodes(Action<IDescendantNodeBuilder> values);

        ISiteMapNodeToParentRelation Single();

        IEnumerable<ISiteMapNodeToParentRelation> ToList();
    }
}
