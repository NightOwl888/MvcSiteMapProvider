using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Builder.Fluent
{
    public interface ISearchEngineBuilder
        : IFluentInterface
    {
        ISearchEngineBuilderCanonicalSet WithCanonicalUrl(string value);

        ISearchEngineBuilderCanonicalSet WithCanonicalKey(string value);

        //ISearchEngineBuilder WithMetaRobotsValue(string value);

        //ISearchEngineBuilder WithMetaRobotsValues(IEnumerable<string> values);

        ISearchEngineBuilder WithMetaRobotsValues(MetaRobots flags);

        ISearchEngineBuilder WithSiteMaps(Action<ISiteMapsBuilder> siteMapsExpression);
    }
}
