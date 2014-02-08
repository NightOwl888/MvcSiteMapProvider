using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Builder.Fluent
{
    public interface IOptionalValueBuilder
        : IFluentInterface
    {
        IOptionalValueBuilder WithSeoValues(Action<ISearchEngineBuilder> expression);

        IOptionalValueBuilder WithUrlResolutionValues(Action<IUrlResolutionBuilder> expression);

        IOptionalValueBuilder WithImplicitResourceKey(string value);

        IOptionalValueBuilder WithAspNetRole(string value);

        IOptionalValueBuilder WithAspNetRoles(IEnumerable<string> values);

        IOptionalValueBuilder WithCustomAttribute(string key, object value);

        IOptionalValueBuilder WithCustomAttributes(IDictionary<string, object> values);

        IOptionalValueBuilder WithKey(string value);

        IOptionalValueBuilder WithParentKey(string value);

        IOptionalValueBuilder WithChildNodes(Action<IDescendantNodeBuilder> values);

        ISiteMapNodeToParentRelation Single();

        IEnumerable<ISiteMapNodeToParentRelation> ToList();
    }
}
