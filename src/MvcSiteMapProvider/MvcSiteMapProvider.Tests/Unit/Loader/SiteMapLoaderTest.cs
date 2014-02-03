﻿using System;
using NUnit;
using NUnit.Framework;
using Moq;
using Moq.Matchers;
using MvcSiteMapProvider.Loader;
using MvcSiteMapProvider.Caching;
using MvcSiteMapProvider.Builder;

namespace MvcSiteMapProvider.Tests.Unit
{
    [TestFixture]
    public class SiteMapLoaderTest
    {
        #region Setup / Teardown

        private Mock<ISiteMapCache> siteMapCache = null;
        private Mock<ISiteMapCacheKeyGenerator> siteMapCacheKeyGenerator = null;
        private Mock<ISiteMapCreator> siteMapCreator = null;
        //private Mock<ISiteMapSpooler> siteMapSpooler = null;

        [SetUp]
        public void Setup()
        {
            siteMapCache = new Mock<ISiteMapCache>();
            siteMapCacheKeyGenerator = new Mock<ISiteMapCacheKeyGenerator>();
            siteMapCreator = new Mock<ISiteMapCreator>();
            //siteMapSpooler = new Mock<ISiteMapSpooler>();
        }

        [TearDown]
        public void TearDown()
        {
            siteMapCache = null;
            siteMapCacheKeyGenerator = null;
            siteMapCreator = null;
            //siteMapSpooler = null;
        }

        private ISiteMapLoader NewSiteMapLoader()
        {
            return new SiteMapLoader(
                siteMapCache.Object,
                siteMapCacheKeyGenerator.Object,
                siteMapCreator.Object);

            //return new SiteMapLoader(
            //    siteMapCache.Object,
            //    siteMapCacheKeyGenerator.Object,
            //    siteMapCreator.Object,
            //    siteMapSpooler.Object);
        }

        #endregion

        [Test]
        public void GetSiteMap_NoParameterOverload_ShouldCallGenerateKeyAndPassResultToGetOrAdd()
        {
            // arrange
            var target = NewSiteMapLoader();
            siteMapCacheKeyGenerator
                .Setup(x => x.GenerateKey())
                .Returns("theKey");

            // act
            var result = target.GetSiteMap();

            // assert
            siteMapCacheKeyGenerator
                .Verify(x => x.GenerateKey(),
                Times.Once());
            //siteMapSpooler
            //    .Verify(x => x.GetOrAdd("theKey", It.IsAny<Func<ISiteMap>>()),
            //    Times.Once());
            siteMapCache
                .Verify(x => x.GetOrAdd("theKey", It.IsAny<Func<ISiteMap>>(), It.IsAny<Func<ICacheDetails>>()),
                Times.Once());
        }

        [Test]
        public void GetSiteMap_EmptySiteMapCacheKey_ShouldCallGenerateKeyAndPassResultToGetOrAdd()
        {
            // arrange
            var siteMapCacheKey = "";
            var target = NewSiteMapLoader();
            siteMapCacheKeyGenerator
                .Setup(x => x.GenerateKey())
                .Returns("theKey");

            // act
            var result = target.GetSiteMap(siteMapCacheKey);

            // assert
            siteMapCacheKeyGenerator
                .Verify(x => x.GenerateKey(), 
                Times.Once());
            //siteMapSpooler
            //    .Verify(x => x.GetOrAdd("theKey", It.IsAny<Func<ISiteMap>>()),
            //    Times.Once());
            siteMapCache
                .Verify(x => x.GetOrAdd("theKey", It.IsAny<Func<ISiteMap>>(), It.IsAny<Func<ICacheDetails>>()),
                Times.Once());
        }

        [Test]
        public void GetSiteMap_NullSiteMapCacheKey_ShouldCallGenerateKeyAndPassResultToGetOrAdd()
        {
            // arrange
            string siteMapCacheKey = null;
            var target = NewSiteMapLoader();
            siteMapCacheKeyGenerator
                .Setup(x => x.GenerateKey())
                .Returns("theKey");

            // act
            var result = target.GetSiteMap(siteMapCacheKey);

            // assert
            siteMapCacheKeyGenerator
                .Verify(x => x.GenerateKey(),
                Times.Once());
            //siteMapSpooler
            //    .Verify(x => x.GetOrAdd("theKey", It.IsAny<Func<ISiteMap>>()),
            //    Times.Once());
            siteMapCache
                .Verify(x => x.GetOrAdd("theKey", It.IsAny<Func<ISiteMap>>(), It.IsAny<Func<ICacheDetails>>()),
                Times.Once());
        }

        [Test]
        public void GetSiteMap_WithSiteMapCacheKey_ShouldCallGetOrAddWithSiteMapCacheKey()
        {
            // arrange
            var siteMapCacheKey = "theKey";
            var target = NewSiteMapLoader();

            // act
            var result = target.GetSiteMap(siteMapCacheKey);

            // assert
            //siteMapSpooler
            //    .Verify(x => x.GetOrAdd(siteMapCacheKey, It.IsAny<Func<ISiteMap>>()),
            //    Times.Once());
            siteMapCache
                .Verify(x => x.GetOrAdd(siteMapCacheKey, It.IsAny<Func<ISiteMap>>(), It.IsAny<Func<ICacheDetails>>()),
                Times.Once());
        }




        [Test]
        public void ReleaseSiteMap_NoParameterOverload_ShouldCallGenerateKeyAndPassResultToRemove()
        {
            // arrange
            var target = NewSiteMapLoader();
            siteMapCacheKeyGenerator
                .Setup(x => x.GenerateKey())
                .Returns("theKey");

            // act
            target.ReleaseSiteMap();

            // assert
            siteMapCacheKeyGenerator
                .Verify(x => x.GenerateKey(),
                Times.Once());
            siteMapCache
                .Verify(x => x.Remove("theKey"),
                Times.Once());
        }

        [Test]
        public void ReleaseSiteMap_EmptySiteMapCacheKey_ShouldCallGenerateKeyAndPassResultToRemove()
        {
            // arrange
            var siteMapCacheKey = "";
            var target = NewSiteMapLoader();
            siteMapCacheKeyGenerator
                .Setup(x => x.GenerateKey())
                .Returns("theKey");

            // act
            target.ReleaseSiteMap(siteMapCacheKey);

            // assert
            siteMapCacheKeyGenerator
                .Verify(x => x.GenerateKey(),
                Times.Once());
            siteMapCache
                .Verify(x => x.Remove("theKey"),
                Times.Once());
        }

        [Test]
        public void ReleaseSiteMap_NullSiteMapCacheKey_ShouldCallGenerateKeyAndPassResultToRemove()
        {
            // arrange
            string siteMapCacheKey = null;
            var target = NewSiteMapLoader();
            siteMapCacheKeyGenerator
                .Setup(x => x.GenerateKey())
                .Returns("theKey");

            // act
            target.ReleaseSiteMap(siteMapCacheKey);

            // assert
            siteMapCacheKeyGenerator
                .Verify(x => x.GenerateKey(),
                Times.Once());
            siteMapCache
                .Verify(x => x.Remove("theKey"),
                Times.Once());
        }

        [Test]
        public void ReleaseSiteMap_WithSiteMapCacheKey_ShouldCallGetOrAddWithSiteMapCacheKey()
        {
            // arrange
            var siteMapCacheKey = "theKey";
            var target = NewSiteMapLoader();

            // act
            target.ReleaseSiteMap(siteMapCacheKey);

            // assert
            siteMapCache
                .Verify(x => x.Remove(siteMapCacheKey),
                Times.Once());
        }
    }
}
