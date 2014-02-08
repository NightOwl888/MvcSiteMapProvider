using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Builder.Fluent
{
    public interface ISearchEngineBuilderCanonicalSet
        : IFluentInterface
    {
        //ISearchEngineBuilderCanonicalSet WithMetaRobotsValue(string value);

        //ISearchEngineBuilderCanonicalSet WithMetaRobotsValues(IEnumerable<string> values);

        ISearchEngineBuilderCanonicalSet WithMetaRobotsValues(MetaRobots flags);

        ISearchEngineBuilderCanonicalSet WithSiteMaps(Action<ISiteMapsBuilder> siteMapsExpression);
    }
}
