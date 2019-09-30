using Xunit;
using System.ComponentModel;
using Xunit2.Should;
using com.b_velop.IdentityProvider.Model;
using com.b_velop.IdentityProvider;
using Microsoft.Extensions.Logging;

namespace App.IdentityProvider.Tests
{
    public class InfoItemTests
    {
        [Fact]
        public void InfoItem_Url_ReturnsUrlWithoutTrailingSlash()
        {
            // Arrange
            var sut = new InfoItem("foo", "bar", "fii")
            {
                Url = "https://test.de/"
            };

            // Act
            var actual = sut.Url;

            // Assert
            Assert.Equal("https://test.de", actual);
        }


        [Fact]
        public void InfoItem_Url_ReturnsUrlWitCorrectPrefix()
        {
            // Arrange
            var sut = new InfoItem("foo", "bar", "fii")
            {
                Url = "https://test.de/"
            };

            // Act
            var actual = sut.GetUrlTokenPrefix();

            // Assert
            actual.ShouldBe("https://test.de/connect/token");
        }

        [Fact, Category("IntegartionTest")]
        public void Integration()
        {
            MyLoggerFactory factory = new MyLoggerFactory();
            var ii = new InfoItem(
                "test.test",
                "asdf",
                "hub",
                "http://localhost:5050/");

            var target = new IdentityProviderService(
                new System.Net.Http.HttpClient(),
                new Logger<IdentityProviderService>(factory));
            var actual = target.GetTokenAsync(ii).GetAwaiter().GetResult();

        }
    }
}
