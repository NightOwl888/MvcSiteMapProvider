﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18047
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MvcSiteMapProvider.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Messages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Messages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("MvcSiteMapProvider.Resources.Messages", typeof(Messages).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Ambiguous controller. Found multiple controller types for {0}Controller. Consider narrowing the places to search by adding you controller namespaces to ControllerBuilder.Current.DefaultNamespaces..
        /// </summary>
        internal static string AmbiguousControllerFoundMultipleControllers {
            get {
                return ResourceManager.GetString("AmbiguousControllerFoundMultipleControllers", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot enumerate a threadsafe dictionary. Instead, enumerate the keys or values collection..
        /// </summary>
        internal static string CannotEnumerateThreadSafeDictionary {
            get {
                return ResourceManager.GetString("CannotEnumerateThreadSafeDictionary", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Cannot use a leading .. to exit above the top directory..
        /// </summary>
        internal static string CannotExitUpTopDirectory {
            get {
                return ResourceManager.GetString("CannotExitUpTopDirectory", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Collection is read-only..
        /// </summary>
        internal static string CollectionReadOnly {
            get {
                return ResourceManager.GetString("CollectionReadOnly", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Copy operation does not support reference types. If you need this functionality, override CopyTo() and provide your own implementation..
        /// </summary>
        internal static string CopyOperationDoesNotSupportReferenceTypes {
            get {
                return ResourceManager.GetString("CopyOperationDoesNotSupportReferenceTypes", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An item with the same key has already been added..
        /// </summary>
        internal static string DictionaryAlreadyContainsKey {
            get {
                return ResourceManager.GetString("DictionaryAlreadyContainsKey", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to An invalid element was found in the sitemap..
        /// </summary>
        internal static string InvalidSiteMapElement {
            get {
                return ResourceManager.GetString("InvalidSiteMapElement", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &apos;{0}&apos; is not a valid virtual path..
        /// </summary>
        internal static string InvalidVirtualPath {
            get {
                return ResourceManager.GetString("InvalidVirtualPath", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The &apos;{0}&apos; value for the robots meta tag has already been added for node. &apos;follow&apos; and &apos;nofollow&apos; may not be used at the same time..
        /// </summary>
        internal static string MetaRobotsValueFollowAmbiguous {
            get {
                return ResourceManager.GetString("MetaRobotsValueFollowAmbiguous", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The &apos;{0}&apos; value for the robots meta tag has already been added for node. &apos;index&apos; and &apos;noindex&apos; may not be used at the same time..
        /// </summary>
        internal static string MetaRobotsValueIndexAmbiguous {
            get {
                return ResourceManager.GetString("MetaRobotsValueIndexAmbiguous", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The &apos;{0}&apos; value for the robots meta tag has already been added for node. &apos;none&apos; cannot be used in conjunction with &apos;index&apos;, &apos;noindex&apos;, &apos;follow&apos;, or &apos;nofollow&apos;. You may use &apos;none&apos; to indicate &apos;noindex&apos; and &apos;nofollow&apos; (not recommended), but do not combine it with these values..
        /// </summary>
        internal static string MetaRobotsValueNoneAmbiguous {
            get {
                return ResourceManager.GetString("MetaRobotsValueNoneAmbiguous", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The value &apos;{0}&apos; is not a valid meta robots value..
        /// </summary>
        internal static string MetaRobotsValueUnrecognized {
            get {
                return ResourceManager.GetString("MetaRobotsValueUnrecognized", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Multiple nodes with the same key &apos;{0}&apos; were found. SiteMap requires that sitemap nodes have unique keys..
        /// </summary>
        internal static string MultipleNodesWithIdenticalKey {
            get {
                return ResourceManager.GetString("MultipleNodesWithIdenticalKey", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Multiple nodes with the same URL &apos;{0}&apos; were found. SiteMap requires that sitemap nodes have unique URLs..
        /// </summary>
        internal static string MultipleNodesWithIdenticalUrl {
            get {
                return ResourceManager.GetString("MultipleNodesWithIdenticalUrl", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The builder set instance named &apos;{0}&apos; was not found. Check your DI configuration to ensure a builder set instance with this name exists..
        /// </summary>
        internal static string NamedBuilderSetNotFound {
            get {
                return ResourceManager.GetString("NamedBuilderSetNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The dynamic title provider instance named &apos;{0}&apos; was not found. Check your DI configuration to ensure a dynamic title provider instance with this name exists and is configured correctly..
        /// </summary>
        internal static string NamedSiteMapNodeDynamicTitleProviderNotFound {
            get {
                return ResourceManager.GetString("NamedSiteMapNodeDynamicTitleProviderNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The visibility provider instance named &apos;{0}&apos; was not found. Check your DI configuration to ensure a visibility provider instance with this name exists and is configured correctly..
        /// </summary>
        internal static string NamedSiteMapNodeVisibilityProviderNotFound {
            get {
                return ResourceManager.GetString("NamedSiteMapNodeVisibilityProviderNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The URL resolver instance named &apos;{0}&apos; was not found. Check your DI configuration to ensure a URL resolver instance with this name exists and is configured correctly..
        /// </summary>
        internal static string NamedUrlResolverNotFound {
            get {
                return ResourceManager.GetString("NamedUrlResolverNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Multiple root nodes defined. Are you missing a ParentKey definition on the &apos;{0}&apos; controller, &apos;{1}&apos;?.
        /// </summary>
        internal static string NoParentKeyDefined {
            get {
                return ResourceManager.GetString("NoParentKeyDefined", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Field &apos;{0}&apos; was not found in Type &apos;{1}&apos;..
        /// </summary>
        internal static string ObjectFieldNotFound {
            get {
                return ResourceManager.GetString("ObjectFieldNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Property &apos;{0}&apos; was not found in Type &apos;{1}&apos;..
        /// </summary>
        internal static string ObjectPropertyNotFound {
            get {
                return ResourceManager.GetString("ObjectPropertyNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to &apos;{0}&apos; is a physical path, but a virtual path was expected..
        /// </summary>
        internal static string PhysicalPathNotAllowed {
            get {
                return ResourceManager.GetString("PhysicalPathNotAllowed", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The resource object with classname &apos;{0}&apos; and key &apos;{1}&apos; was not found..
        /// </summary>
        internal static string ResourceNotFoundWithClassAndKey {
            get {
                return ResourceManager.GetString("ResourceNotFoundWithClassAndKey", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Security trimming cannot be disabled again after it is enabled..
        /// </summary>
        internal static string SecurityTrimmingCannotBeDisabled {
            get {
                return ResourceManager.GetString("SecurityTrimmingCannotBeDisabled", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The sitemap loader may only be set in the Application_Start event of Global.asax and must not be set again. Set the &apos;MvcSiteMapProvider_UseExternalDIContainer&apos; in the AppSettings section of the web.config file to &apos;true&apos; if you are using an external dependency injection container..
        /// </summary>
        internal static string SiteMapLoaderAlreadySet {
            get {
                return ResourceManager.GetString("SiteMapLoaderAlreadySet", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The SiteMapLoader has not been initialized. You must set the SiteMaps.Loader property during Application_Start in Global.asax if the &apos;MvcSiteMapProvider_UseExternalDIContainer&apos; setting is set to &apos;true&apos; in the AppSettings section of web.config..
        /// </summary>
        internal static string SiteMapLoaderNotInitialized {
            get {
                return ResourceManager.GetString("SiteMapLoaderNotInitialized", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The &apos;{0}&apos; has already been set. Simultaneous use of both CanonicalUrl and CanonicalKey is not allowed..
        /// </summary>
        internal static string SiteMapNodeCanonicalValueAlreadySet {
            get {
                return ResourceManager.GetString("SiteMapNodeCanonicalValueAlreadySet", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SiteMapNode is readonly, property &apos;{0}&apos; cannot be modified..
        /// </summary>
        internal static string SiteMapNodeReadOnly {
            get {
                return ResourceManager.GetString("SiteMapNodeReadOnly", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SiteMap is readonly, nodes cannot be modified..
        /// </summary>
        internal static string SiteMapReadOnly {
            get {
                return ResourceManager.GetString("SiteMapReadOnly", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The site map builders returned null for root node. Root node defined in SiteMap cannot be null. Please make sure you return a root node for each SiteMapBuilderSet and that a root node is set in your sitemap XML file if you are using one..
        /// </summary>
        internal static string SiteMapRootNodeNotDefined {
            get {
                return ResourceManager.GetString("SiteMapRootNodeNotDefined", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Unknown SiteMap: &apos;{0}&apos;..
        /// </summary>
        internal static string UnknownSiteMap {
            get {
                return ResourceManager.GetString("UnknownSiteMap", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to VisitingSiteMapBuilder requires a root node to be passed. Make sure there is a builder configured to run before it that returns a valid root node..
        /// </summary>
        internal static string VisitingSiteMapBuilderRequiresRootNode {
            get {
                return ResourceManager.GetString("VisitingSiteMapBuilderRequiresRootNode", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Xml file &apos;{0}&apos; could not be found. Verify that the path provided is correct..
        /// </summary>
        internal static string XmlFileNotFound {
            get {
                return ResourceManager.GetString("XmlFileNotFound", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The file at &apos;{0}&apos; contains XML validation errors. See the inner exception for complete details..
        /// </summary>
        internal static string XmlValidationFailed {
            get {
                return ResourceManager.GetString("XmlValidationFailed", resourceCulture);
            }
        }
    }
}
