using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Builder.Fluent
{
    public interface IGroupingNodeTitleBuilder
        : IFluentInterface
    {
        IGroupingNodeOptionalValueBuilder WithTitle(string value);
    }
}
