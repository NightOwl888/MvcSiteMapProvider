using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Builder.Fluent
{
    public interface IDescendantOptionalValueBuilder
        : IFluentInterface
    {
        IDescendantOptionalValueBuilder WithSeoValues(Action<ISearchEngineBuilder> expression);

        IDescendantOptionalValueBuilder WithUrlResolutionValues(Action<IUrlResolutionBuilder> expression);

        IDescendantOptionalValueBuilder WithImplicitResourceKey(string value);

        IDescendantOptionalValueBuilder WithAspNetRole(string value);

        IDescendantOptionalValueBuilder WithAspNetRoles(IEnumerable<string> values);

        IDescendantOptionalValueBuilder WithCustomAttribute(string key, object value);

        IDescendantOptionalValueBuilder WithCustomAttributes(IDictionary<string, object> values);

        IDescendantOptionalValueBuilder InheritCustomAttribute(string key);

        IDescendantOptionalValueBuilder InheritCustomAttributes(IEnumerable<string> keys);

        IDescendantOptionalValueBuilder WithKey(string value);

        IDescendantOptionalValueBuilder WithChildNodes(Action<IDescendantNodeBuilder> values);
    }
}
