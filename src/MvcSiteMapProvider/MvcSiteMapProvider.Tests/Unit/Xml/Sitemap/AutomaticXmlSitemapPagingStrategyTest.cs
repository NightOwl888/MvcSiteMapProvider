using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using MvcSiteMapProvider.Xml.Sitemap;
using MvcSiteMapProvider.Xml.Sitemap.Paging;

namespace MvcSiteMapProvider.Tests.Unit.Xml.Sitemap
{
    [TestFixture]
    public class AutomaticXmlSitemapPagingStrategyTest
    {
        #region SetUp / TearDown

        private Mock<IPagingInstructionFactory> pagingInstructionFactory = null;
        private Mock<IUrlEntryProvider> urlEntryProvider1 = null;
        private Mock<IUrlEntryProvider> urlEntryProvider2 = null;
        private Mock<IUrlEntryProvider> urlEntryProvider3 = null;
        private Mock<IUrlEntryProvider> urlEntryProvider4 = null;
        private Mock<IUrlEntryProvider> urlEntryProvider5 = null;

        [SetUp]
        public void Init()
        {
            pagingInstructionFactory = new Mock<IPagingInstructionFactory>();
            urlEntryProvider1 = new Mock<IUrlEntryProvider>();
            urlEntryProvider2 = new Mock<IUrlEntryProvider>();
            urlEntryProvider3 = new Mock<IUrlEntryProvider>();
            urlEntryProvider4 = new Mock<IUrlEntryProvider>();
            urlEntryProvider5 = new Mock<IUrlEntryProvider>();

            this.pagingInstructionFactory
                .Setup(x => x.Create(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<IUrlEntryProvider>()))
                .Returns((int skip, int take, IUrlEntryProvider urlEntryProvider) => 
                {
                    var instruction = new Mock<IPagingInstruction>();
                    instruction.Setup(i => i.Skip).Returns(skip);
                    instruction.Setup(i => i.Take).Returns(take);
                    instruction.Setup(i => i.UrlEntryProvider).Returns(urlEntryProvider);

                    return instruction.Object;
                });
        }

        [TearDown]
        public void Dispose()
        {
            pagingInstructionFactory = null;
            urlEntryProvider1 = null;
            urlEntryProvider2 = null;
            urlEntryProvider3 = null;
            urlEntryProvider4 = null;
            urlEntryProvider5 = null;
        }


        // 3 providers with 100,000 records each, 35,000 maximum per page
        private IXmlSitemapPagingStrategy NewAutomaticXmlSitemmapPagingStrategy_TestCase1()
        {
            urlEntryProvider1.Setup(x => x.GetTotalRecordCount()).Returns(100000);
            urlEntryProvider2.Setup(x => x.GetTotalRecordCount()).Returns(100000);
            urlEntryProvider3.Setup(x => x.GetTotalRecordCount()).Returns(100000);

            return new AutomaticXmlSitemapPagingStrategy(new IUrlEntryProvider[] 
            {
                urlEntryProvider1.Object,
                urlEntryProvider2.Object,
                urlEntryProvider3.Object
            }, this.pagingInstructionFactory.Object);
        }

        // 5 providers with 25,000 records each, 35,000 maximum per page
        private IXmlSitemapPagingStrategy NewAutomaticXmlSitemmapPagingStrategy_TestCase2()
        {
            urlEntryProvider1.Setup(x => x.GetTotalRecordCount()).Returns(25000);
            urlEntryProvider2.Setup(x => x.GetTotalRecordCount()).Returns(25000);
            urlEntryProvider3.Setup(x => x.GetTotalRecordCount()).Returns(25000);
            urlEntryProvider4.Setup(x => x.GetTotalRecordCount()).Returns(25000);
            urlEntryProvider5.Setup(x => x.GetTotalRecordCount()).Returns(25000);

            return new AutomaticXmlSitemapPagingStrategy(new IUrlEntryProvider[] 
            {
                urlEntryProvider1.Object,
                urlEntryProvider2.Object,
                urlEntryProvider3.Object,
                urlEntryProvider4.Object,
                urlEntryProvider5.Object,
            }, this.pagingInstructionFactory.Object);
        }

        #endregion

        #region Tests

        [Test]
        public void GetPagingInstructions_WithPage3AndTestCase1_ShouldReturn2InstructionWith_Skip70000AndTake30000_Skip0AndTake5000()
        {
            // arrange
            var target = this.NewAutomaticXmlSitemmapPagingStrategy_TestCase1();

            // act
            var result = target.GetPagingInstructions(3);

            // assert
            var actualCount = result.Count();
            var expectedCount = 2;
            Assert.AreEqual(expectedCount, actualCount);

            var actualSkip1 = result.ElementAt(0).Skip;
            var expectedSkip1 = 70000;
            Assert.AreEqual(expectedSkip1, actualSkip1);

            var actualTake1 = result.ElementAt(0).Take;
            var expectedTake1 = 30000;
            Assert.AreEqual(expectedTake1, actualTake1);

            var actualSkip2 = result.ElementAt(1).Skip;
            var expectedSkip2 = 0;
            Assert.AreEqual(expectedSkip2, actualSkip2);

            var actualTake2 = result.ElementAt(1).Take;
            var expectedTake2 = 5000;
            Assert.AreEqual(expectedTake2, actualTake2);
        }

        [Test]
        public void GetPagingInstructions_WithPage4AndTestCase1_ShouldReturn1InstructionsWith_Skip5000AndTake35000()
        {
            // arrange
            var target = this.NewAutomaticXmlSitemmapPagingStrategy_TestCase1();

            // act
            var result = target.GetPagingInstructions(4);

            // assert
            var actualCount = result.Count();
            var expectedCount = 1;
            Assert.AreEqual(expectedCount, actualCount);

            var actualSkip1 = result.ElementAt(0).Skip;
            var expectedSkip1 = 5000;
            Assert.AreEqual(expectedSkip1, actualSkip1);

            var actualTake1 = result.ElementAt(0).Take;
            var expectedTake1 = 35000;
            Assert.AreEqual(expectedTake1, actualTake1);
        }

        [Test]
        public void GetPagingInstructions_WithPage2AndTestCase2_ShouldReturn2InstructionsWith_Skip10000AndTake15000_Skip0AndTake20000()
        {
            // arrange
            var target = this.NewAutomaticXmlSitemmapPagingStrategy_TestCase2();

            // act
            var result = target.GetPagingInstructions(2);

            // assert
            var actualCount = result.Count();
            var expectedCount = 2;
            Assert.AreEqual(expectedCount, actualCount);

            var actualSkip1 = result.ElementAt(0).Skip;
            var expectedSkip1 = 10000;
            Assert.AreEqual(expectedSkip1, actualSkip1);

            var actualTake1 = result.ElementAt(0).Take;
            var expectedTake1 = 15000;
            Assert.AreEqual(expectedTake1, actualTake1);

            var actualSkip2 = result.ElementAt(1).Skip;
            var expectedSkip2 = 0;
            Assert.AreEqual(expectedSkip2, actualSkip2);

            var actualTake2 = result.ElementAt(1).Take;
            var expectedTake2 = 20000;
            Assert.AreEqual(expectedTake2, actualTake2);
        }

        [Test]
        public void GetPagingInstructions_WithPage3AndTestCase2_ShouldReturn3InstructionsWith_Skip20000AndTake5000_Skip0AndTake25000_Skip0AndTake5000()
        {
            // arrange
            var target = this.NewAutomaticXmlSitemmapPagingStrategy_TestCase2();

            // act
            var result = target.GetPagingInstructions(3);

            // assert
            var actualCount = result.Count();
            var expectedCount = 3;
            Assert.AreEqual(expectedCount, actualCount);

            var actualSkip1 = result.ElementAt(0).Skip;
            var expectedSkip1 = 20000;
            Assert.AreEqual(expectedSkip1, actualSkip1);

            var actualTake1 = result.ElementAt(0).Take;
            var expectedTake1 = 5000;
            Assert.AreEqual(expectedTake1, actualTake1);

            var actualSkip2 = result.ElementAt(1).Skip;
            var expectedSkip2 = 0;
            Assert.AreEqual(expectedSkip2, actualSkip2);

            var actualTake2 = result.ElementAt(1).Take;
            var expectedTake2 = 25000;
            Assert.AreEqual(expectedTake2, actualTake2);

            var actualSkip3 = result.ElementAt(2).Skip;
            var expectedSkip3 = 0;
            Assert.AreEqual(expectedSkip3, actualSkip3);

            var actualTake3 = result.ElementAt(2).Take;
            var expectedTake3 = 5000;
            Assert.AreEqual(expectedTake3, actualTake3);
        }

        #endregion
    }
}
