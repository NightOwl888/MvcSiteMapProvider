using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using MvcSiteMapProvider.Xml.Sitemap;
using MvcSiteMapProvider.Xml.Sitemap.Paging;

namespace MvcSiteMapProvider.Tests.Unit.Xml.Sitemap.Paging
{
    [TestFixture]
    public class XmlSitemapPagerTest
    {
        #region SetUp / TearDown

        private Mock<IPagingInstructionFactory> pagingInstructionFactory = null;
        private Mock<IXmlSitemapProvider> xmlSitemapProvider1 = null;
        private Mock<IXmlSitemapProvider> xmlSitemapProvider2 = null;
        private Mock<IXmlSitemapProvider> xmlSitemapProvider3 = null;
        private Mock<IXmlSitemapProvider> xmlSitemapProvider4 = null;
        private Mock<IXmlSitemapProvider> xmlSitemapProvider5 = null;
        private Mock<IXmlSitemapPageDataFactory> xmlSitemapPageDataFactory = null;
        private Mock<IXmlSitemapPageInfoFactory> xmlSitemapPageInfoFactory = null;

        [SetUp]
        public void Init()
        {
            pagingInstructionFactory = new Mock<IPagingInstructionFactory>();
            xmlSitemapProvider1 = new Mock<IXmlSitemapProvider>();
            xmlSitemapProvider2 = new Mock<IXmlSitemapProvider>();
            xmlSitemapProvider3 = new Mock<IXmlSitemapProvider>();
            xmlSitemapProvider4 = new Mock<IXmlSitemapProvider>();
            xmlSitemapProvider5 = new Mock<IXmlSitemapProvider>();
            xmlSitemapPageDataFactory = new Mock<IXmlSitemapPageDataFactory>();
            xmlSitemapPageInfoFactory = new Mock<IXmlSitemapPageInfoFactory>();

            this.pagingInstructionFactory
                .Setup(x => x.Create(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<IXmlSitemapProvider>()))
                .Returns((int skip, int take, IXmlSitemapProvider xmlSitemapProvider) =>
                {
                    var instruction = new Mock<IPagingInstruction>();
                    instruction.Setup(i => i.Skip).Returns(skip);
                    instruction.Setup(i => i.Take).Returns(take);
                    instruction.Setup(i => i.XmlSitemapProvider).Returns(xmlSitemapProvider);

                    return instruction.Object;
                });
        }

        [TearDown]
        public void Dispose()
        {
            pagingInstructionFactory = null;
            xmlSitemapProvider1 = null;
            xmlSitemapProvider2 = null;
            xmlSitemapProvider3 = null;
            xmlSitemapProvider4 = null;
            xmlSitemapProvider5 = null;
            xmlSitemapPageDataFactory = null;
            xmlSitemapPageInfoFactory = null;
        }


        private IXmlSitemapPager NewXmlSitemapPager()
        {
            return new XmlSitemapPager(
                this.pagingInstructionFactory.Object,
                this.xmlSitemapPageInfoFactory.Object,
                this.xmlSitemapPageDataFactory.Object) 
                { 
                    MaximumPageSize = 40000 
                };
        }

        // 3 providers with 100,000 records each, 40,000 maximum per page
        private IEnumerable<IXmlSitemapProvider> GetProviders_TestCase1()
        {
            xmlSitemapProvider1.Setup(x => x.GetTotalRecordCount(It.IsAny<string>())).Returns(100000);
            xmlSitemapProvider1.Setup(x => x.GetLastModifiedDate(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).Returns(DateTime.MinValue);

            xmlSitemapProvider2.Setup(x => x.GetTotalRecordCount(It.IsAny<string>())).Returns(100000);
            xmlSitemapProvider2.Setup(x => x.GetLastModifiedDate(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).Returns(DateTime.MinValue);

            xmlSitemapProvider3.Setup(x => x.GetTotalRecordCount(It.IsAny<string>())).Returns(100000);
            xmlSitemapProvider3.Setup(x => x.GetLastModifiedDate(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).Returns(DateTime.MinValue);

            return new IXmlSitemapProvider[] {
                xmlSitemapProvider1.Object,
                xmlSitemapProvider2.Object,
                xmlSitemapProvider3.Object
            };
        }

        // 5 providers with 25,000 records each, 40,000 maximum per page
        private IEnumerable<IXmlSitemapProvider> GetProviders_TestCase2()
        {
            xmlSitemapProvider1.Setup(x => x.GetTotalRecordCount(It.IsAny<string>())).Returns(25000);
            xmlSitemapProvider1.Setup(x => x.GetLastModifiedDate(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).Returns(DateTime.MinValue);

            xmlSitemapProvider2.Setup(x => x.GetTotalRecordCount(It.IsAny<string>())).Returns(25000);
            xmlSitemapProvider2.Setup(x => x.GetLastModifiedDate(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).Returns(DateTime.MinValue);

            xmlSitemapProvider3.Setup(x => x.GetTotalRecordCount(It.IsAny<string>())).Returns(25000);
            xmlSitemapProvider3.Setup(x => x.GetLastModifiedDate(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).Returns(new DateTime(2000, 4, 1));

            xmlSitemapProvider4.Setup(x => x.GetTotalRecordCount(It.IsAny<string>())).Returns(25000);
            xmlSitemapProvider4.Setup(x => x.GetLastModifiedDate(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).Returns(DateTime.MinValue);

            xmlSitemapProvider5.Setup(x => x.GetTotalRecordCount(It.IsAny<string>())).Returns(25000);
            xmlSitemapProvider5.Setup(x => x.GetLastModifiedDate(It.IsAny<string>(), It.IsAny<int>(), It.IsAny<int>())).Returns(new DateTime(2000, 4, 1));

            return new IXmlSitemapProvider[] {
                xmlSitemapProvider1.Object,
                xmlSitemapProvider2.Object,
                xmlSitemapProvider3.Object,
                xmlSitemapProvider4.Object,
                xmlSitemapProvider5.Object
            };
        }

        #endregion

        #region Tests

        [Test]
        public void GetPagingInstructions_WithPage3AndTestCase1_ShouldReturn2InstructionWith_Skip80000AndTake20000_Skip0AndTake20000()
        {
            // arrange
            var providers = this.GetProviders_TestCase1();
            var target = this.NewXmlSitemapPager();

            // act
            var result = target.GetPagingInstructions(providers, "default", 3);

            // assert
            var actualCount = result.Count();
            var expectedCount = 2;
            Assert.AreEqual(expectedCount, actualCount);

            var actualSkip1 = result.ElementAt(0).Skip;
            var expectedSkip1 = 80000;
            Assert.AreEqual(expectedSkip1, actualSkip1);

            var actualTake1 = result.ElementAt(0).Take;
            var expectedTake1 = 20000;
            Assert.AreEqual(expectedTake1, actualTake1);

            var actualSkip2 = result.ElementAt(1).Skip;
            var expectedSkip2 = 0;
            Assert.AreEqual(expectedSkip2, actualSkip2);

            var actualTake2 = result.ElementAt(1).Take;
            var expectedTake2 = 20000;
            Assert.AreEqual(expectedTake2, actualTake2);
        }

        [Test]
        public void GetPagingInstructions_WithPage4AndTestCase1_ShouldReturn1InstructionsWith_Skip20000AndTake40000()
        {
            // arrange
            var providers = this.GetProviders_TestCase1();
            var target = this.NewXmlSitemapPager();

            // act
            var result = target.GetPagingInstructions(providers, "default", 4);

            // assert
            var actualCount = result.Count();
            var expectedCount = 1;
            Assert.AreEqual(expectedCount, actualCount);

            var actualSkip1 = result.ElementAt(0).Skip;
            var expectedSkip1 = 20000;
            Assert.AreEqual(expectedSkip1, actualSkip1);

            var actualTake1 = result.ElementAt(0).Take;
            var expectedTake1 = 40000;
            Assert.AreEqual(expectedTake1, actualTake1);
        }

        [Test]
        public void GetPagingInstructions_WithPage2AndTestCase2_ShouldReturn2InstructionsWith_Skip15000AndTake10000_Skip0AndTake25000_Skip0AndTake5000()
        {
            // arrange
            var providers = this.GetProviders_TestCase2();
            var target = this.NewXmlSitemapPager();

            // act
            var result = target.GetPagingInstructions(providers, "default", 2);

            // assert
            var actualCount = result.Count();
            var expectedCount = 3;
            Assert.AreEqual(expectedCount, actualCount);

            var actualSkip1 = result.ElementAt(0).Skip;
            var expectedSkip1 = 15000;
            Assert.AreEqual(expectedSkip1, actualSkip1);

            var actualTake1 = result.ElementAt(0).Take;
            var expectedTake1 = 10000;
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

        [Test]
        public void GetPagingInstructions_WithPage3AndTestCase2_ShouldReturn3InstructionsWith_Skip5000AndTake20000_Skip0AndTake20000()
        {
            // arrange
            var providers = this.GetProviders_TestCase2();
            var target = this.NewXmlSitemapPager();

            // act
            var result = target.GetPagingInstructions(providers, "default", 3);

            // assert
            var actualCount = result.Count();
            var expectedCount = 2;
            Assert.AreEqual(expectedCount, actualCount);

            var actualSkip1 = result.ElementAt(0).Skip;
            var expectedSkip1 = 5000;
            Assert.AreEqual(expectedSkip1, actualSkip1);

            var actualTake1 = result.ElementAt(0).Take;
            var expectedTake1 = 20000;
            Assert.AreEqual(expectedTake1, actualTake1);

            var actualSkip2 = result.ElementAt(1).Skip;
            var expectedSkip2 = 0;
            Assert.AreEqual(expectedSkip2, actualSkip2);

            var actualTake2 = result.ElementAt(1).Take;
            var expectedTake2 = 20000;
            Assert.AreEqual(expectedTake2, actualTake2);
        }

        #endregion
    }
}
