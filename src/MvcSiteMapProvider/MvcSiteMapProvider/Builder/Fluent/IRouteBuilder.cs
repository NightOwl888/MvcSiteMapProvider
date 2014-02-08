using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MvcSiteMapProvider.Builder.Fluent
{
    public interface IRouteBuilder
        : IFluentInterface
    {
        IRouteBuilder Named(string name);

        IRouteBuilder WithArea(string name);

        IRouteBuilder WithController(string name);

        IRouteBuilder WithAction(string name);

        IRouteBuilder WithValue(string key, object value);

        IRouteBuilder WithValues(IDictionary<string, object> dictionary);

        IRouteBuilder WithHttpMethod(HttpVerbs method);

        IRouteBuilder AlwaysMatchingKey(string key);
    }
}
