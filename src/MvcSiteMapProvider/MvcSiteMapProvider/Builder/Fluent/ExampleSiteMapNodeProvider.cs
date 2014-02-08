using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcSiteMapProvider.Builder.Fluent
{
    public class ExampleSiteMapNodeProvider
        : ISiteMapNodeProvider
    {

        #region ISiteMapNodeProvider Members

        public IEnumerable<ISiteMapNodeToParentRelation> GetSiteMapNodes(ISiteMapNodeHelper helper)
        {
            //return helper.RegisterNode()
            //    .WithTitle("Home")
            //    .Matching(
            //        Route
            //            .Named("TheHomeRoute")
            //            .WithController("Home")
            //            .WithAction("Index")
            //    )
            //    .Description("This is the home page")
            //    .Key("Home")
            //    .ToList();

            //return helper.RegisterNode()
            //    .WithTitle("Home")
            //    .Matching(
            //        Route.Named("HomeRoute")
            //            .WithController("Home")
            //            .WithAction("Index")
            //            .WithValue("id", 1)
            //            .AlwaysMatchingKey("userId"))
            //    .WithDescription("This is the home page.")
            //    .WithKey("Home")
            //    .ToList();

            //return helper.RegisterNode()
            //    .WithTitle("Account Home")
            //    .Matching(Url.WithValue("/Account"))
            //    .WithDescription("This is the account home page.")
            //    .WithKey("AccountHome")
            //    .ToList();

            //return helper.RegisterNode()
            //    .WithTitle("Home")
            //    .MatchingRoute(x => x.Named("HomeRoute").WithController("Home").WithAction("Index").WithValue("id", 8).AlwaysMatchingKey("userId"))
            //    .WithDescription("This is the home page.")
            //    .WithKey("Home")
            //    .ToList();

            //return helper.RegisterNode()
            //    .WithDisplay(x => x.WithTitle("Home").WithDescription("This is the home page").WithVisibility("MenuHelper,SiteMapPathHelper,!*"))
            //    .MatchingRoute(x => x.Named("HomeRoute").WithController("Home").WithAction("Index").WithValue("id", 8).AlwaysMatchingKey("userId"))
            //    .WithDescription("This is the home page.")
            //    .WithKey("Home")
            //    .ToList();

            //return helper.RegisterNode()
            //    .MatchingRoute(x => x.Named("AccountRoute").WithController("Account").WithAction("Index").WithValue("id", 8).AlwaysMatchingKey("userId"))
            //    .WithDisplayValues(x => x.WithTitle("Account Home").WithDescription("This is the account home page").WithVisibility("MenuHelper,SiteMapPathHelper,!*"))
            //    .WithSeoValues(x => x.WithCanonicalKey("AccountHome2").WithSiteMaps(y => y.WithChangeFrequency(ChangeFrequency.Daily).WithUpdatePriority(UpdatePriority.Normal)))
            //    .WithUrlResolutionValues(x => x.CacheResolvedUrl(false))
            //    .WithKey("AccountHome")
            //    .WithParentKey("Home")
            //    .WithChildNodes(c => 
            //        {
            //            c.RegisterNode()
            //                .MatchingUrl("/some-url")
            //                .WithDisplayValues(x => x.WithTitle("Some Url").WithDescription("This is some URL").WithSortOrder(2))
            //                .WithKey("SomeUrlKey")
            //                .WithAspNetRole("Administrator")
            //                .WithChildNodes(d =>
            //                    {
            //                        d.RegisterNode()
            //                            .WithDynamicNodeProvider("MyProject.MyDynamicNodeProvider, MyProject")
            //                                .WithInheritableDisplayValues(x => x.WithTitle("Default Title").WithVisibility("SiteMapPathHelper,!*").WithTargetFrame("Main"))
            //                                .WithInheritableSeoValues(x => x.WithSiteMaps(y => y.WithChangeFrequency(ChangeFrequency.Monthly).WithUpdatePriority(UpdatePriority.Low)));

            //                        d.RegisterNode()
            //                            .AsGroupingNodeTitled("Inventory")
            //                                .WithKey("InventoryGroup")
            //                                .WithInheritableRouteValues(x => x.WithController("Account"))
            //                                .WithChildNodes(e => 
            //                                    {
            //                                        e.RegisterNode()
            //                                            .WithDynamicNodeProvider("MyProject.InventoryDynamicNodeProvider, MyProject");
            //                                    });
            //                    });
            //        })
            //    .ToList();


            return helper.RegisterNode()
                .MatchingRoute(x => x.WithController("Products").WithAction("Details").WithValue("id", 3456))
                .WithDisplayValues(x => x.WithTitle("Some Cool Product").WithDescription("This is the cool product"))
                .WithKey("Product_3456")
                .WithUrlResolutionValues(x => x.WithUrlResolver("MyCustomResolver"))
                .WithSeoValues(x => x.WithCanonicalKey("Product_1234").WithMetaRobotsValues(MetaRobots.NoIndex | MetaRobots.NoFollow))
                .WithChildNodes(a =>
                    {
                        a.RegisterNode()
                            .MatchingRoute(x => x.WithController("Products").WithAction("Edit").AlwaysMatchingKey("id"))
                            .WithDisplayValues(x => x.WithTitle("Edit Product").WithDescription("Edit the product").WithVisibility("SiteMapPathHelper,!*"))
                            .WithKey("ProductEdit");
                        a.RegisterNode()
                            .MatchingUrl("test")
                            .WithDisplayValues(x => x.WithTitle("TheTitle"));
                        a.RegisterNode()
                            .AsGroupingNodeTitled("Another Group")
                            .WithDisplayValues(x => x.WithVisibility("SiteMapPathHelper,!*"));
                        a.RegisterNode()
                            .MatchingUrl("")
                            .WithDisplayValues(x => x.WithTitle("the title"))
                            .WithInheritableRouteValues(x => x.WithController("TheController"));
                    })
                .ToList();

            //return helper.RegisterNode()
            //    .MatchingUrl("test")
            //    .WithDisplayValues(x => x.WithTitle("The title").WithDescription("The description"))
            //    .WithKey("Test")
            //    .ToList();

            //return helper.RegisterNode()
            //    .WithDynamicNodeProvider("TheProvider")
            //    .WithInheritableDisplayValues(x => x.WithTitle("Inherit this").WithVisibility("MenuHelper,!*"))
            //    .ToList();

            //return helper.RegisterNode()
            //    .AsGroupingNodeTitled("Some Group")
            //    .WithDisplayValues(x => x.WithVisibility("MenuHelper,!*").WithSortOrder(10))
            //    .ToList();

            //return helper.RegisterNode()
            //    .MathingRoute(x => x.WithController("TheController").WithAction("TheAction"))
            //    .WithDisplayValues(x => x.WithTitle("The Action").WithDescription("This is the description"))
            //    .WithChildNodes(a =>
            //        {
            //            a.RegisterNode()
            //                .WithDynamicNodeProvider("the provider")
            //                .WithInheritableDisplayValues(x => x.WithTitle("The default title"))
            //                .WithInheritableImplicitResourceKey("The Key")
            //                .WithInheritableUrlResolutionValues(x => x.CacheResolvedUrl(false));
            //        })
            //    .ToList();
        }

        #endregion
    }
}
