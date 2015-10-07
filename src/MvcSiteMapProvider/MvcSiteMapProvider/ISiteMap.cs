﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
#if MVC6
using Microsoft.AspNet.Mvc;
using MvcSiteMapProvider.Web;
using MvcSiteMapProvider.Web.Mvc;
#else
using System.Web.Mvc;
#endif
using MvcSiteMapProvider.Collections.Specialized;

namespace MvcSiteMapProvider
{
    /// <summary>
    /// Contract for the specialized class that manages the hierarchical relationship between different 
    /// <see cref="T:MvcSiteMapProvider.ISiteMapNode"/> instances.
    /// </summary>
    public interface ISiteMap
    {
        // Data structure management
        bool IsReadOnly { get; }
        void AddNode(ISiteMapNode node);
        void AddNode(ISiteMapNode node, ISiteMapNode parentNode);
        void RemoveNode(ISiteMapNode node);
        void Clear();
        ISiteMapNode RootNode { get; }

        // Hide the BuildSiteMap method because it is only for use by MvcSiteMapProvider.
        [EditorBrowsable(EditorBrowsableState.Never)]
        void BuildSiteMap();

        ISiteMapNode CurrentNode { get; }
        bool EnableLocalization { get; }
        ISiteMapNode FindSiteMapNode(string rawUrl);
        ISiteMapNode FindSiteMapNodeFromCurrentContext();
        ISiteMapNode FindSiteMapNode(ControllerContext context);
        ISiteMapNode FindSiteMapNodeFromKey(string key);
        ISiteMapNodeCollection GetChildNodes(ISiteMapNode node);
        ISiteMapNodeCollection GetDescendants(ISiteMapNode node);
        ISiteMapNodeCollection GetAncestors(ISiteMapNode node);
        ISiteMapNode GetCurrentNodeAndHintAncestorNodes(int upLevel);
        ISiteMapNode GetCurrentNodeAndHintNeighborhoodNodes(int upLevel, int downLevel);
        ISiteMapNode GetParentNode(ISiteMapNode node);
        ISiteMapNode GetParentNodeRelativeToCurrentNodeAndHintDownFromParent(int walkupLevels, int relativeDepthFromWalkup);
        ISiteMapNode GetParentNodeRelativeToNodeAndHintDownFromParent(ISiteMapNode node, int walkupLevels, int relativeDepthFromWalkup);
        void HintAncestorNodes(ISiteMapNode node, int upLevel);
        void HintNeighborhoodNodes(ISiteMapNode node, int upLevel, int downLevel);
        bool IsAccessibleToUser(ISiteMapNode node);
        string CacheKey { get; }
        string ResourceKey { get; set; } // TODO: Remove setter in version 5.
        bool SecurityTrimmingEnabled { get; }
        bool UseTitleIfDescriptionNotProvided { get; }
        bool VisibilityAffectsDescendants { get; }
        Type ResolveControllerType(string areaName, string controllerName);
#if !MVC6
        [Obsolete("ResolveActionMethodParameters is deprecated and will be removed in version 5.")]
        IEnumerable<string> ResolveActionMethodParameters(string areaName, string controllerName, string actionMethodName);
#endif
    }

#if MVC6
    public static class SiteMapExtensions
    {
        /// <summary>
        /// Finds the site map node.
        /// </summary>
        /// <param name="context">The controller context.</param>
        /// <returns></returns>
        public static ISiteMapNode FindSiteMapNode(this ISiteMap siteMap, ActionContext context)
        {
            var controllerContext = new ControllerContext(new MvcContextFactory().CreateRequestContext(new HttpContextWrapper(context.HttpContext)), null);
            return siteMap.FindSiteMapNode(controllerContext);
        }
    }
#endif

}
