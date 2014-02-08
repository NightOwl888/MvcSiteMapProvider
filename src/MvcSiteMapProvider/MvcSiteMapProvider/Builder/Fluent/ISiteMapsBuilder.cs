using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Builder.Fluent
{
    public interface ISiteMapsBuilder
        : IFluentInterface
    {
        ISiteMapsBuilder WithChangeFrequency(ChangeFrequency value);

        ISiteMapsBuilder WithUpdatePriority(UpdatePriority value);

        ISiteMapsBuilder WithLastModifiedDate(DateTime value);
    }
}
