using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Builder.Fluent
{
    public interface IDisplayBuilder
        : IFluentInterface
    {
        IDisplayBuilder WithDescription(string value);

        IDisplayBuilder WithVisibilityProvider(string name);

        IDisplayBuilder WithVisibility(string expression);

        IDisplayBuilder WithSortOrder(int value);

        IDisplayBuilder WithImageUrl(string value);

        IDisplayBuilder WithTargetFrame(string value);
    }
}
