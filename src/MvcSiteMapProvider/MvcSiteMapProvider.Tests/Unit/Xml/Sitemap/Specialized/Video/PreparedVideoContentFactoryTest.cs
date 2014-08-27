using System;
using System.Linq;
using System.Text;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using NUnit.Framework;
using Moq;
using MvcSiteMapProvider.Globalization;
using MvcSiteMapProvider.Xml.Sitemap;
using MvcSiteMapProvider.Xml.Sitemap.Specialized;
using MvcSiteMapProvider.Xml.Sitemap.Specialized.Video;

namespace MvcSiteMapProvider.Tests.Unit.Xml.Sitemap.Specialized.Video
{
    /// <summary>
    /// Summary description for PreparedVideoContentFactoryTest
    /// </summary>
    [TestFixture]
    public class PreparedVideoContentFactoryTest
    {
        #region SetUp / TearDown

        private Mock<IVideoContent> videoContent = null;
        private Mock<IXmlSitemapUrlResolver> urlResolver = null;
        private Mock<ICultureContext> cultureContext = null;

        [SetUp]
        public void Init()
        {
            // Set the culture to the invariant context (the application runs under invariant context)
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            Thread.CurrentThread.CurrentUICulture = CultureInfo.InvariantCulture;

            this.videoContent = new Mock<IVideoContent>();
            this.urlResolver = new Mock<IXmlSitemapUrlResolver>();
            this.cultureContext = new Mock<ICultureContext>();

            // Setup the required properties
            this.videoContent.Setup(x => x.Title).Returns("video title");
            this.videoContent.Setup(x => x.Description).Returns("video description");
            this.urlResolver.Setup(x => x.ResolveUrlToAbsolute(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns("http://somewhere.com/resolvedUrl.html");

            //videoContent.Setup(x => x.IsFamilyFriendly).Returns(true);
        }

        [TearDown]
        public void Dispose()
        {
            this.videoContent = null;
            this.urlResolver = null;
            this.cultureContext = null;
        }

        private void MockAllCollections()
        {
            // Setup the collections so they won't choke.
            this.videoContent.Setup(x => x.CountriesAllowed).Returns(new List<string>());
            this.videoContent.Setup(x => x.CountriesNotAllowed).Returns(new List<string>());
            this.videoContent.Setup(x => x.Prices).Returns(new List<IVideoContentPrice>());
        }

        private IPreparedSpecializedContentFactory NewPreparedVideoContentFactory()
        {
            return new PreparedVideoContentFactory();
        }

        #endregion

        #region Tests

        [Test]
        public void Create_VideoWithThumbnail_ShouldReturnCallResolveToAbsoluteWithSameValue()
        {
            // arrange
            this.MockAllCollections();
            this.videoContent.Setup(x => x.ThumbnailUrl).Returns("~/images/thumbnail.png");
            var target = this.NewPreparedVideoContentFactory();

            // act
            var result = target.Create(this.videoContent.Object, this.urlResolver.Object, this.cultureContext.Object);

            // assert
            this.urlResolver.Verify(x => x.ResolveUrlToAbsolute("~/images/thumbnail.png", null, null), Times.Once());
        }

        [Test]
        public void Create_AnyVideo_ShouldReturnResolvedUrl()
        {
            // arrange
            this.MockAllCollections();
            this.videoContent.Setup(x => x.ThumbnailUrl).Returns("~/images/thumbnail.png");
            var target = this.NewPreparedVideoContentFactory();
            // NOTE: URL resolver setup in initializer

            // act
            PreparedVideoContent result = (PreparedVideoContent)target.Create(this.videoContent.Object, this.urlResolver.Object, this.cultureContext.Object);

            // assert
            var actual = result.ThumbnailLocation;
            var expected = "http://somewhere.com/resolvedUrl.html";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Create_VideoWithTitle_ShouldReturnSameTitle()
        {
            // arrange
            this.MockAllCollections();
            var target = this.NewPreparedVideoContentFactory();
            // NOTE: Title setup in initializer

            // act
            PreparedVideoContent result = (PreparedVideoContent)target.Create(this.videoContent.Object, this.urlResolver.Object, this.cultureContext.Object);

            // assert
            var actual = result.Title;
            var expected = "video title";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Create_VideoWithDescription_ShouldReturnSameDescription()
        {
            // arrange
            this.MockAllCollections();
            var target = this.NewPreparedVideoContentFactory();
            // NOTE: Description setup in initializer

            // act
            PreparedVideoContent result = (PreparedVideoContent)target.Create(this.videoContent.Object, this.urlResolver.Object, this.cultureContext.Object);

            // assert
            var actual = result.Description;
            var expected = "video description";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Create_VideoWithContentLocation_ShouldReturnCallResolveToAbsoluteWithSameValue()
        {
            // arrange
            this.MockAllCollections();
            this.videoContent.Setup(x => x.ContentLocation).Returns("http://www.mysite.com/contentLocation.html");
            var target = this.NewPreparedVideoContentFactory();

            // act
            var result = target.Create(this.videoContent.Object, this.urlResolver.Object, this.cultureContext.Object);

            // assert
            this.urlResolver.Verify(x => x.ResolveUrlToAbsolute("http://www.mysite.com/contentLocation.html", null, null), Times.Once());
        }

        [Test]
        public void Create_VideoWithContentLocation_ShouldReturnResolvedUrl()
        {
            // arrange
            this.MockAllCollections();
            this.videoContent.Setup(x => x.ContentLocation).Returns("http://www.mysite.com/contentLocation.html");
            var target = this.NewPreparedVideoContentFactory();
            // NOTE: URL resolver setup in initializer

            // act
            PreparedVideoContent result = (PreparedVideoContent)target.Create(this.videoContent.Object, this.urlResolver.Object, this.cultureContext.Object);

            // assert
            var actual = result.ContentLocation;
            var expected = "http://somewhere.com/resolvedUrl.html";
            Assert.AreEqual(expected, actual);
        }


        [Test]
        public void Create_VideoWithPlayerLocation_ShouldReturnCallResolveToAbsoluteWithSameValue()
        {
            // arrange
            this.MockAllCollections();
            this.videoContent.Setup(x => x.ContentLocation).Returns("http://www.mysite.com/playerLocation.html");
            var target = this.NewPreparedVideoContentFactory();

            // act
            var result = target.Create(this.videoContent.Object, this.urlResolver.Object, this.cultureContext.Object);

            // assert
            this.urlResolver.Verify(x => x.ResolveUrlToAbsolute("http://www.mysite.com/playerLocation.html", null, null), Times.Once());
        }

        [Test]
        public void Create_VideoWithPlayerLocation_ShouldReturnResolvedUrl()
        {
            // arrange
            this.MockAllCollections();
            this.videoContent.Setup(x => x.PlayerLocation).Returns("http://www.mysite.com/playerLocation.html");
            var target = this.NewPreparedVideoContentFactory();
            // NOTE: URL resolver setup in initializer

            // act
            PreparedVideoContent result = (PreparedVideoContent)target.Create(this.videoContent.Object, this.urlResolver.Object, this.cultureContext.Object);

            // assert
            var actual = result.PlayerLocation;
            var expected = "http://somewhere.com/resolvedUrl.html";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Create_VideoWithPlayerLocationAllowEmbedNull_ShouldReturnEmptyString()
        {
            // arrange
            this.MockAllCollections();
            this.videoContent.Setup(x => x.PlayerLocationAllowEmbed).Returns((bool?)null);
            var target = this.NewPreparedVideoContentFactory();

            // act
            PreparedVideoContent result = (PreparedVideoContent)target.Create(this.videoContent.Object, this.urlResolver.Object, this.cultureContext.Object);

            // assert
            var actual = result.PlayerLocationAllowEmbed;
            var expected = string.Empty;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Create_VideoWithPlayerLocationAllowEmbedTrue_ShouldReturnYes()
        {
            // arrange
            this.MockAllCollections();
            this.videoContent.Setup(x => x.PlayerLocationAllowEmbed).Returns((bool?)true);
            var target = this.NewPreparedVideoContentFactory();

            // act
            PreparedVideoContent result = (PreparedVideoContent)target.Create(this.videoContent.Object, this.urlResolver.Object, this.cultureContext.Object);

            // assert
            var actual = result.PlayerLocationAllowEmbed;
            var expected = "Yes";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Create_VideoWithPlayerLocationAllowEmbedFalse_ShouldReturnNo()
        {
            // arrange
            this.MockAllCollections();
            this.videoContent.Setup(x => x.PlayerLocationAllowEmbed).Returns((bool?)false);
            var target = this.NewPreparedVideoContentFactory();

            // act
            PreparedVideoContent result = (PreparedVideoContent)target.Create(this.videoContent.Object, this.urlResolver.Object, this.cultureContext.Object);

            // assert
            var actual = result.PlayerLocationAllowEmbed;
            var expected = "No";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Create_VideoWithPlayerLocationAutoPlay_ShouldReturnSameValue()
        {
            // arrange
            this.MockAllCollections();
            this.videoContent.Setup(x => x.PlayerLocationAutoPlay).Returns("ap=1");
            var target = this.NewPreparedVideoContentFactory();

            // act
            PreparedVideoContent result = (PreparedVideoContent)target.Create(this.videoContent.Object, this.urlResolver.Object, this.cultureContext.Object);

            // assert
            var actual = result.PlayerLocationAutoPlay;
            var expected = "ap=1";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Create_VideoWithDuration0_ShouldReturnEmptyString()
        {
            // arrange
            this.MockAllCollections();
            this.videoContent.Setup(x => x.Duration).Returns(0);
            var target = this.NewPreparedVideoContentFactory();

            // act
            PreparedVideoContent result = (PreparedVideoContent)target.Create(this.videoContent.Object, this.urlResolver.Object, this.cultureContext.Object);

            // assert
            var actual = result.Duration;
            var expected = string.Empty;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Create_VideoWithDuration100_ShouldReturnString100()
        {
            // arrange
            this.MockAllCollections();
            this.videoContent.Setup(x => x.Duration).Returns(100);
            var target = this.NewPreparedVideoContentFactory();

            // act
            PreparedVideoContent result = (PreparedVideoContent)target.Create(this.videoContent.Object, this.urlResolver.Object, this.cultureContext.Object);

            // assert
            var actual = result.Duration;
            var expected = "100";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Create_VideoWithExpirationDateMinValue_ShouldReturnEmptyString()
        {
            // arrange
            this.MockAllCollections();
            this.videoContent.Setup(x => x.ExpirationDate).Returns(DateTime.MinValue);
            var target = this.NewPreparedVideoContentFactory();

            // act
            PreparedVideoContent result = (PreparedVideoContent)target.Create(this.videoContent.Object, this.urlResolver.Object, this.cultureContext.Object);

            // assert
            var actual = result.ExpirationDate;
            var expected = string.Empty;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Create_VideoWithExpirationDate_ShouldReturnW3CFormattedUTCDate()
        {
            // arrange
            this.MockAllCollections();
            this.videoContent.Setup(x => x.ExpirationDate).Returns(new DateTime(2000, 11, 13, 15, 35, 12));
            var target = this.NewPreparedVideoContentFactory();

            // act
            PreparedVideoContent result = (PreparedVideoContent)target.Create(this.videoContent.Object, this.urlResolver.Object, this.cultureContext.Object);

            // assert
            var actual = result.ExpirationDate;
            var expected = "2000-11-13T08:35:12.0000000+07:00";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Create_VideoWithRating0_ShouldReturnEmptyString()
        {
            // arrange
            this.MockAllCollections();
            this.videoContent.Setup(x => x.Rating).Returns(0.0);
            var target = this.NewPreparedVideoContentFactory();

            // act
            PreparedVideoContent result = (PreparedVideoContent)target.Create(this.videoContent.Object, this.urlResolver.Object, this.cultureContext.Object);

            // assert
            var actual = result.Rating;
            var expected = string.Empty;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Create_VideoWithRating2Point6_ShouldReturnString2Point6()
        {
            // arrange
            this.MockAllCollections();
            this.videoContent.Setup(x => x.Rating).Returns(2.6);
            var target = this.NewPreparedVideoContentFactory();

            // act
            PreparedVideoContent result = (PreparedVideoContent)target.Create(this.videoContent.Object, this.urlResolver.Object, this.cultureContext.Object);

            // assert
            var actual = result.Rating;
            var expected = "2.6";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Create_VideoWithViewCount0_ShouldReturnEmptyString()
        {
            // arrange
            this.MockAllCollections();
            this.videoContent.Setup(x => x.ViewCount).Returns(0);
            var target = this.NewPreparedVideoContentFactory();

            // act
            PreparedVideoContent result = (PreparedVideoContent)target.Create(this.videoContent.Object, this.urlResolver.Object, this.cultureContext.Object);

            // assert
            var actual = result.ViewCount;
            var expected = string.Empty;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Create_VideoWithViewCount339_ShouldReturnString339()
        {
            // arrange
            this.MockAllCollections();
            this.videoContent.Setup(x => x.ViewCount).Returns(339);
            var target = this.NewPreparedVideoContentFactory();

            // act
            PreparedVideoContent result = (PreparedVideoContent)target.Create(this.videoContent.Object, this.urlResolver.Object, this.cultureContext.Object);

            // assert
            var actual = result.ViewCount;
            var expected = "339";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Create_VideoWithFamilyIsFriendlyTrue_ShouldReturnEmptyString()
        {
            // arrange
            this.MockAllCollections();
            this.videoContent.Setup(x => x.IsFamilyFriendly).Returns(true);
            var target = this.NewPreparedVideoContentFactory();

            // act
            PreparedVideoContent result = (PreparedVideoContent)target.Create(this.videoContent.Object, this.urlResolver.Object, this.cultureContext.Object);

            // assert
            var actual = result.FamilyFriendly;
            var expected = string.Empty;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Create_VideoWithFamilyIsFriendlyFalse_ShouldReturnNo()
        {
            // arrange
            this.MockAllCollections();
            this.videoContent.Setup(x => x.IsFamilyFriendly).Returns(false);
            var target = this.NewPreparedVideoContentFactory();

            // act
            PreparedVideoContent result = (PreparedVideoContent)target.Create(this.videoContent.Object, this.urlResolver.Object, this.cultureContext.Object);

            // assert
            var actual = result.FamilyFriendly;
            var expected = "No";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Create_VideoWithCountriesAllowedStringUSSpaceUK_ShouldReturnSameValue()
        {
            // arrange
            this.MockAllCollections();
            this.videoContent.Setup(x => x.CountriesAllowedString).Returns("US UK");

            var target = this.NewPreparedVideoContentFactory();

            // act
            PreparedVideoContent result = (PreparedVideoContent)target.Create(this.videoContent.Object, this.urlResolver.Object, this.cultureContext.Object);

            // assert
            var actual = result.Restriction;
            var expected = "US UK";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Create_VideoWithCountriesAllowedStringUSSpaceUK_ShouldReturnRestrictionRelationshipAllow()
        {
            // arrange
            this.MockAllCollections();
            this.videoContent.Setup(x => x.CountriesAllowedString).Returns("US UK");

            var target = this.NewPreparedVideoContentFactory();

            // act
            PreparedVideoContent result = (PreparedVideoContent)target.Create(this.videoContent.Object, this.urlResolver.Object, this.cultureContext.Object);

            // assert
            var actual = result.RestrictionRelationship;
            var expected = "allow";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Create_VideoWithCountriesNotAllowedStringUSSpaceUKSpaceDE_ShouldReturnSameValue()
        {
            // arrange
            this.MockAllCollections();
            this.videoContent.Setup(x => x.CountriesNotAllowedString).Returns("US UK DE");

            var target = this.NewPreparedVideoContentFactory();

            // act
            PreparedVideoContent result = (PreparedVideoContent)target.Create(this.videoContent.Object, this.urlResolver.Object, this.cultureContext.Object);

            // assert
            var actual = result.Restriction;
            var expected = "US UK DE";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Create_VideoWithCountriesNotAllowedStringUSSpaceUKSpaceDE_ShouldReturnRestrictionRelationshipDeny()
        {
            // arrange
            this.MockAllCollections();
            this.videoContent.Setup(x => x.CountriesNotAllowedString).Returns("US UK DE");

            var target = this.NewPreparedVideoContentFactory();

            // act
            PreparedVideoContent result = (PreparedVideoContent)target.Create(this.videoContent.Object, this.urlResolver.Object, this.cultureContext.Object);

            // assert
            var actual = result.RestrictionRelationship;
            var expected = "deny";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Create_VideoWithGalleryLocation_ShouldReturnCallResolveToAbsoluteWithSameValue()
        {
            // arrange
            this.MockAllCollections();
            this.videoContent.Setup(x => x.GalleryLocation).Returns("http://www.mysite.com/galleryLocation.html");
            var target = this.NewPreparedVideoContentFactory();

            // act
            var result = target.Create(this.videoContent.Object, this.urlResolver.Object, this.cultureContext.Object);

            // assert
            this.urlResolver.Verify(x => x.ResolveUrlToAbsolute("http://www.mysite.com/galleryLocation.html", null, null), Times.Once());
        }

        [Test]
        public void Create_VideoWithGalleryLocation_ShouldReturnResolvedUrl()
        {
            // arrange
            this.MockAllCollections();
            this.videoContent.Setup(x => x.GalleryLocation).Returns("http://www.mysite.com/galleryLocation.html");
            var target = this.NewPreparedVideoContentFactory();
            // NOTE: URL resolver setup in initializer

            // act
            PreparedVideoContent result = (PreparedVideoContent)target.Create(this.videoContent.Object, this.urlResolver.Object, this.cultureContext.Object);

            // assert
            var actual = result.GalleryLocation;
            var expected = "http://somewhere.com/resolvedUrl.html";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Create_VideoWithGalleryLocationTitle_ShouldReturnSameTitle()
        {
            // arrange
            this.MockAllCollections();
            this.videoContent.Setup(x => x.GalleryLocationTitle).Returns("my gallery");
            var target = this.NewPreparedVideoContentFactory();

            // act
            PreparedVideoContent result = (PreparedVideoContent)target.Create(this.videoContent.Object, this.urlResolver.Object, this.cultureContext.Object);

            // assert
            var actual = result.GalleryLocationTitle;
            var expected = "my gallery";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Create_VideoWithPrices_ShouldReturnPricesFormattedToCorrectLengthForCurrency()
        {
            // arrange
            var videoContentPrice1 = new Mock<IVideoContentPrice>();
            var videoContentPrice2 = new Mock<IVideoContentPrice>();

            videoContentPrice1.Setup(x => x.Price).Returns((decimal)89.9601);
            videoContentPrice1.Setup(x => x.Currency).Returns("USD");
            videoContentPrice1.Setup(x => x.Resolution).Returns(VideoResolution.HD);
            videoContentPrice1.Setup(x => x.Type).Returns(VideoPriceType.Own);

            videoContentPrice2.Setup(x => x.Price).Returns((decimal)34.9584);
            videoContentPrice2.Setup(x => x.Currency).Returns("IQD");
            videoContentPrice2.Setup(x => x.Resolution).Returns(VideoResolution.Undefined);
            videoContentPrice2.Setup(x => x.Type).Returns(VideoPriceType.Undefined);

            // Setup the collections so they won't choke.
            this.videoContent.Setup(x => x.CountriesAllowed).Returns(new List<string>());
            this.videoContent.Setup(x => x.CountriesNotAllowed).Returns(new List<string>());
            this.videoContent.Setup(x => x.Prices).Returns(new List<IVideoContentPrice>() { videoContentPrice1.Object, videoContentPrice2.Object });

            var target = this.NewPreparedVideoContentFactory();

            // act
            PreparedVideoContent result = (PreparedVideoContent)target.Create(this.videoContent.Object, this.urlResolver.Object, this.cultureContext.Object);

            // assert
            var actualCount = result.Prices.Count();
            var expectedCount = 2;
            Assert.AreEqual(expectedCount, actualCount);

            // verify first record
            var actualPrice1 = result.Prices.ElementAt(0).Price;
            var expectedPrice1 = "89.96"; // Verify 2 decimal places
            Assert.AreEqual(expectedPrice1, actualPrice1);

            var actualCurrency1 = result.Prices.ElementAt(0).Currency;
            var expectedCurrency1 = "USD";
            Assert.AreEqual(expectedCurrency1, actualCurrency1);

            var actualResolution1 = result.Prices.ElementAt(0).Resolution;
            var expectedResolution1 = "HD";
            Assert.AreEqual(expectedResolution1, actualResolution1);

            var actualType1 = result.Prices.ElementAt(0).Type;
            var expectedType1 = "own";
            Assert.AreEqual(expectedType1, actualType1);

            // verify second record
            var actualPrice2 = result.Prices.ElementAt(1).Price;
            var expectedPrice2 = "34.958"; // Verify 3 decimal places
            Assert.AreEqual(expectedPrice2, actualPrice2);

            var actualCurrency2 = result.Prices.ElementAt(1).Currency;
            var expectedCurrency2 = "IQD";
            Assert.AreEqual(expectedCurrency2, actualCurrency2);

            var actualResolution2 = result.Prices.ElementAt(1).Resolution;
            var expectedResolution2 = string.Empty;
            Assert.AreEqual(expectedResolution2, actualResolution2);

            var actualType2 = result.Prices.ElementAt(1).Type;
            var expectedType2 = string.Empty;
            Assert.AreEqual(expectedType2, actualType2);
        }

        [Test]
        public void Create_VideoWithRequiresSubscriptionFalse_ShouldReturnEmptyString()
        {
            // arrange
            this.MockAllCollections();
            this.videoContent.Setup(x => x.RequiresSubscription).Returns(false);
            var target = this.NewPreparedVideoContentFactory();

            // act
            PreparedVideoContent result = (PreparedVideoContent)target.Create(this.videoContent.Object, this.urlResolver.Object, this.cultureContext.Object);

            // assert
            var actual = result.RequiresSubscription;
            var expected = string.Empty;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Create_VideoWithRequiresSubscriptionTrue_ShouldReturnYes()
        {
            // arrange
            this.MockAllCollections();
            this.videoContent.Setup(x => x.RequiresSubscription).Returns(true);
            var target = this.NewPreparedVideoContentFactory();

            // act
            PreparedVideoContent result = (PreparedVideoContent)target.Create(this.videoContent.Object, this.urlResolver.Object, this.cultureContext.Object);

            // assert
            var actual = result.RequiresSubscription;
            var expected = "yes";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Create_VideoWithUploader_ShouldReturnSameValue()
        {
            // arrange
            this.MockAllCollections();
            this.videoContent.Setup(x => x.Uploader).Returns("kirk");
            var target = this.NewPreparedVideoContentFactory();

            // act
            PreparedVideoContent result = (PreparedVideoContent)target.Create(this.videoContent.Object, this.urlResolver.Object, this.cultureContext.Object);

            // assert
            var actual = result.Uploader;
            var expected = "kirk";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Create_VideoWithUploaderInfo_ShouldCallUrlResolverWithSameValue()
        {
            // arrange
            this.MockAllCollections();
            this.videoContent.Setup(x => x.UploaderInfo).Returns("http://www.somewhere.com/uploaderProfile.html");
            var target = this.NewPreparedVideoContentFactory();

            // act
            PreparedVideoContent result = (PreparedVideoContent)target.Create(this.videoContent.Object, this.urlResolver.Object, this.cultureContext.Object);

            // assert
            this.urlResolver.Verify(x => x.ResolveUrlToAbsolute("http://www.somewhere.com/uploaderProfile.html", null, null), Times.Once());
        }

        [Test]
        public void Create_VideoWithUploaderInfo_ShouldReturnResolvedUrl()
        {
            // arrange
            this.MockAllCollections();
            this.videoContent.Setup(x => x.UploaderInfo).Returns("http://www.somewhere.com/uploaderProfile.html");
            var target = this.NewPreparedVideoContentFactory();
            // NOTE: URL resolver setup in initializer

            // act
            PreparedVideoContent result = (PreparedVideoContent)target.Create(this.videoContent.Object, this.urlResolver.Object, this.cultureContext.Object);

            // assert
            var actual = result.UploaderInfo;
            var expected = "http://somewhere.com/resolvedUrl.html";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Create_VideoWithPlatformsAllowedStringwebSpacetv_ShouldReturnSameValue()
        {
            // arrange
            this.MockAllCollections();
            this.videoContent.Setup(x => x.PlatformsAllowedString).Returns("web tv");

            var target = this.NewPreparedVideoContentFactory();

            // act
            PreparedVideoContent result = (PreparedVideoContent)target.Create(this.videoContent.Object, this.urlResolver.Object, this.cultureContext.Object);

            // assert
            var actual = result.Platform;
            var expected = "web tv";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Create_VideoWithPlatformsAllowedStringwebSpacetv_ShouldReturnRestrictionRelationshipAllow()
        {
            // arrange
            this.MockAllCollections();
            this.videoContent.Setup(x => x.PlatformsAllowedString).Returns("web tv");

            var target = this.NewPreparedVideoContentFactory();

            // act
            PreparedVideoContent result = (PreparedVideoContent)target.Create(this.videoContent.Object, this.urlResolver.Object, this.cultureContext.Object);

            // assert
            var actual = result.PlatformRelationship;
            var expected = "allow";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Create_VideoWithPlatformsNotAllowedStringmobile_ShouldReturnSameValue()
        {
            // arrange
            this.MockAllCollections();
            this.videoContent.Setup(x => x.PlatformsNotAllowedString).Returns("mobile");

            var target = this.NewPreparedVideoContentFactory();

            // act
            PreparedVideoContent result = (PreparedVideoContent)target.Create(this.videoContent.Object, this.urlResolver.Object, this.cultureContext.Object);

            // assert
            var actual = result.Platform;
            var expected = "mobile";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Create_VideoWithPlatformsNotAllowedStringmobile_ShouldReturnRestrictionRelationshipDeny2()
        {
            // arrange
            this.MockAllCollections();
            this.videoContent.Setup(x => x.PlatformsNotAllowedString).Returns("mobile");

            var target = this.NewPreparedVideoContentFactory();

            // act
            PreparedVideoContent result = (PreparedVideoContent)target.Create(this.videoContent.Object, this.urlResolver.Object, this.cultureContext.Object);

            // assert
            var actual = result.PlatformRelationship;
            var expected = "deny";
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Create_VideoWithLiveFalse_ShouldReturnEmptyString()
        {
            // arrange
            this.MockAllCollections();
            this.videoContent.Setup(x => x.Live).Returns(false);
            var target = this.NewPreparedVideoContentFactory();

            // act
            PreparedVideoContent result = (PreparedVideoContent)target.Create(this.videoContent.Object, this.urlResolver.Object, this.cultureContext.Object);

            // assert
            var actual = result.Live;
            var expected = string.Empty;
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Create_VideoWithLiveTrue_ShouldReturnYes()
        {
            // arrange
            this.MockAllCollections();
            this.videoContent.Setup(x => x.Live).Returns(true);
            var target = this.NewPreparedVideoContentFactory();

            // act
            PreparedVideoContent result = (PreparedVideoContent)target.Create(this.videoContent.Object, this.urlResolver.Object, this.cultureContext.Object);

            // assert
            var actual = result.Live;
            var expected = "yes";
            Assert.AreEqual(expected, actual);
        }

        #endregion
    }
}
