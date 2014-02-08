//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Web.Mvc;

//// TODO: Break dictionaries, collections, and routes into their own builder (and figure out how to finalize each)

//namespace MvcSiteMapProvider.Builder.Fluent
//{
//    public interface IFluentNodeBuilder<TBuilder, TChildStarter, TSubject>
//    {
//        TBuilder ChildNodes(Action<TChildStarter> expression);

//        [EditorBrowsable(EditorBrowsableState.Never)]
//        IEnumerable<TBuilder> Children { get; } // Used to get the children when building the nodes.

//        TBuilder Attribute(string key, object value);

//        TBuilder Attributes(IDictionary<string, object> collection);

//        TBuilder Attributes(object collection);

//        TBuilder Area(string value);

//        TBuilder Controller(string value);

//        TBuilder Action(string value);

//        TBuilder HttpMethod(HttpVerbs method);

//        TBuilder Title(string value);

//        TBuilder Description(string value);

//        TBuilder Key(string value);

//        TBuilder Url(string value);

//        TBuilder Clickable(bool clickable);

//        TBuilder Role(string value);

//        TBuilder ResourceKey(string value);

//        TBuilder VisibilityProvider(string value);

//        TBuilder DynamicNodeProvider(string value);

//        TBuilder ImageUrl(string value);

//        TBuilder TargetFrame(string value);

//        TBuilder CacheResolvedUrl(bool cacheResolvedUrl);

//        TBuilder CanonicalUrl(string value);

//        TBuilder CanonicalKey(string value);

//        TBuilder MetaRobotsValue(string value);

//        TBuilder ChangeFrequency(ChangeFrequency value);

//        TBuilder UpdatePriority(UpdatePriority value);

//        TBuilder LastModifiedDate(DateTime value);

//        TBuilder Order(int order);

//        TBuilder Route(string value);

//        TBuilder RouteValue(string key, object value);

//        TBuilder RouteValues(IDictionary<string, object> collection);

//        TBuilder RouteValues(object collection);

//        TBuilder AlwaysMatchRouteValue(string value);

//        //TBuilder CopyRouteValueFromRequest(string value);

//        TBuilder UrlResolver(string value);

//        [EditorBrowsable(EditorBrowsableState.Never)]
//        TSubject CreateNode();

//        //ISiteMapNodeToParentRelation CreateNode(ISiteMapNodeHelper helper, ISiteMapNode parentNode);

//        // helper.AddNode()
//        //     .WithTitle("The Node")
//        //     .WithController("Home")
//        //     .WithAction("TheNode")
//        //     .WithRouteValue("id", 1234)
//        //     .WithRouteValues(valuesDictionary);

//        // helper.AddNode()
//        //     .Title("The Node")
//        //     .Controller("Home")
//        //     .Action("TheNode")
//        //     .Attribute("id", 1234)
//        //     .Attributes(valuesDictionary);

//        // BuildNode().With.Title("The Node").With.Controller("Home").With.Action("TheNode").Add.Attribute("id", 1234)
//    }
//}
