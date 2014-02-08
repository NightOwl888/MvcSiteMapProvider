using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Builder.Fluent
{
    public interface IDynamicOptionalValueBuilder
        : IFluentInterface
    {
        IDynamicOptionalValueBuilder WithInheritableDisplayValues(Action<IDynamicDisplayBuilder> expression);

        IDynamicOptionalValueBuilder WithInheritableRouteValues(Action<IDescendantRouteBuilder> expression);

        IDynamicOptionalValueBuilder WithInheritableSeoValues(Action<ISearchEngineBuilder> expression);

        IDynamicOptionalValueBuilder WithInheritableUrlResolutionValues(Action<IUrlResolutionBuilder> expression);

        IDynamicOptionalValueBuilder WithInheritableImplicitResourceKey(string value);

        IDynamicOptionalValueBuilder WithInheritableAspNetRole(string value);

        IDynamicOptionalValueBuilder WithInheritableAspNetRoles(IEnumerable<string> values);

        IDynamicOptionalValueBuilder WithInheritableCustomAttribute(string key, object value);

        IDynamicOptionalValueBuilder WithInheritableCustomAttributes(IDictionary<string, object> values);

        IDynamicOptionalValueBuilder WithChildNodes(Action<IDescendantNodeBuilder> values);

        ISiteMapNodeToParentRelation Single();

        IEnumerable<ISiteMapNodeToParentRelation> ToList();
    }
}
