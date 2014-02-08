using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Builder.Fluent
{
    public interface IDynamicDisplayBuilder
        : IFluentInterface
    {
        IDynamicDisplayBuilder WithTitle(string value);

        IDynamicDisplayBuilder WithDescription(string value);

        IDynamicDisplayBuilder WithVisibilityProvider(string name);

        IDynamicDisplayBuilder WithVisibility(string expression);

        IDynamicDisplayBuilder WithSortOrder(int value);

        IDynamicDisplayBuilder WithImageUrl(string value);

        IDynamicDisplayBuilder WithTargetFrame(string value);
    }
}