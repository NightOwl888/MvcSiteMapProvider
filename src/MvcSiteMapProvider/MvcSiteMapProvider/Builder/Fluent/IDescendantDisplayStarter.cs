using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Builder.Fluent
{
    public interface IDescendantDisplayStarter
        : IFluentInterface
    {
        IDescendantOptionalValueBuilder WithDisplayValues(Action<ITitleBuilder> expression);
    }
}
