using System;
using System.ComponentModel;

namespace MvcSiteMapProvider.Builder.Fluent
{
    public interface ITitleBuilder
        : IFluentInterface
    {
        IDisplayBuilder WithTitle(string title);
    }
}
