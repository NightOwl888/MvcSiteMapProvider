using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Builder.Fluent
{
    public interface IDescendantDynamicOptionalValueBuilder
        : IFluentInterface
    {
        IDescendantDynamicOptionalValueBuilder WithInheritableDisplayValues(Action<IDynamicDisplayBuilder> expression);

        IDescendantDynamicOptionalValueBuilder WithInheritableRouteValues(Action<IDescendantRouteBuilder> expression);

        IDescendantDynamicOptionalValueBuilder WithInheritableSeoValues(Action<ISearchEngineBuilder> expression);

        IDescendantDynamicOptionalValueBuilder WithInheritableUrlResolutionValues(Action<IUrlResolutionBuilder> expression);

        IDescendantDynamicOptionalValueBuilder WithInheritableImplicitResourceKey(string value);

        IDescendantDynamicOptionalValueBuilder WithInheritableAspNetRole(string value);

        IDescendantDynamicOptionalValueBuilder WithInheritableAspNetRoles(IEnumerable<string> values);

        IDescendantDynamicOptionalValueBuilder WithInheritableCustomAttribute(string key, object value);

        IDescendantDynamicOptionalValueBuilder WithInheritableCustomAttributes(IDictionary<string, object> values);

        IDescendantDynamicOptionalValueBuilder WithChildNodes(Action<IDescendantNodeBuilder> values);
    }
}
