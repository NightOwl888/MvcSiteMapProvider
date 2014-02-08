//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Web.Mvc;

//namespace MvcSiteMapProvider.Builder
//{
//    /// <summary>
//    /// Contract for the fluent interface that builds a SiteMapNode fluently
//    /// </summary>
//    public interface IFluentSiteMapNodeBuilder
//    {
//        IFluentSiteMapNodeBuilder ChildNodes(Action<IFluentSiteMapNodeFactory> expression);

//        //IList<IFluentSiteMapNodeBuilder> Children { get; }

//        IFluentSiteMapNodeBuilder Attribute(string key, object value);

//        // May need this for inherited attributes
//        IFluentSiteMapNodeBuilder ClearAttributes();

//        IFluentSiteMapNodeBuilder RemoveAttribute(string key);

//        IFluentSiteMapNodeBuilder Area(string value);

//        IFluentSiteMapNodeBuilder Controller(string value);

//        IFluentSiteMapNodeBuilder Action(string value);

//        IFluentSiteMapNodeBuilder HttpMethod(HttpVerbs? method);

//        IFluentSiteMapNodeBuilder Title(string value);

//        IFluentSiteMapNodeBuilder Description(string value);

//        IFluentSiteMapNodeBuilder Key(string value);

//        IFluentSiteMapNodeBuilder Url(string value);

//        IFluentSiteMapNodeBuilder Clickable(bool? clickable);

//        IFluentSiteMapNodeBuilder Roles(string[] values);

//        IFluentSiteMapNodeBuilder ResourceKey(string value);

//        IFluentSiteMapNodeBuilder VisibilityProvider(string value);

//        IFluentSiteMapNodeBuilder DynamicNodeProvider(string value);

//        IFluentSiteMapNodeBuilder ImageUrl(string value);

//        IFluentSiteMapNodeBuilder TargetFrame(string value);

//        IFluentSiteMapNodeBuilder CachedResolvedUrl(bool? cacheResolvedUrl);

//        IFluentSiteMapNodeBuilder CanonicalUrl(string value);

//        IFluentSiteMapNodeBuilder CanonicalKey(string value);

//        IFluentSiteMapNodeBuilder MetaRobotsValues(string[] values);

//        IFluentSiteMapNodeBuilder MetaRobots(string value);

//        IFluentSiteMapNodeBuilder ChangeFrequency(ChangeFrequency? value);

//        IFluentSiteMapNodeBuilder UpdatePriority(UpdatePriority? value);

//        IFluentSiteMapNodeBuilder LastModifiedDate(DateTime? value);

//        IFluentSiteMapNodeBuilder Order(int order);

//        IFluentSiteMapNodeBuilder Route(string value);

//        IFluentSiteMapNodeBuilder RouteValue(string key, object value);

//        // May need this for inherited route values
//        IFluentSiteMapNodeBuilder ClearRouteValues();

//        IFluentSiteMapNodeBuilder RemoveRouteValue(string key);

//        //IFluentSiteMapNodeBuilder RouteValues(object routeValues);

//        IFluentSiteMapNodeBuilder PreservedRouteParameters(string[] values);

//        IFluentSiteMapNodeBuilder PreservedRouteParameter(string value);

//        IFluentSiteMapNodeBuilder UrlResolver(string value);

//        IFluentSiteMapNodeBuilder InheritedRouteParameters(string[] values);

//        IFluentSiteMapNodeBuilder InheritedRouteParameter(string value);

//        //ISiteMapNodeToParentRelation CreateNode(ISiteMapNodeHelper helper, ISiteMapNode parentNode);
//    }
//}