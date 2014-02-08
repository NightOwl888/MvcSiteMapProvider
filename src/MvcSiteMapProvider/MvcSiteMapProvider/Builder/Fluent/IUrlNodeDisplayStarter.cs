using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Builder.Fluent
{
    public interface IUrlNodeDisplayStarter
        : IFluentInterface
    {
        IUrlNodeOptionalValueBuilder WithDisplayValues(Action<ITitleBuilder> expression);
    }
}
