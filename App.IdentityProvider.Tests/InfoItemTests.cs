using com.b_velop.App.IdentityProvider.Model;
using Xunit;
using com.b_velop.App.IdentityProvider;
using System.ComponentModel;
using Xunit2.Should;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

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
                "jupiter.linux",
                "gpuEavEWvdfAr7a6FdK-YWpoGD9mbyetiYw!kaL9-r8RQtTYZW2J8kxj-XaGgnSmZSEae",
                "hub",
                "https://identity.qaybe.de");

            var target = new IdentityProviderService(
                new System.Net.Http.HttpClient(),
                new Logger<IdentityProviderService>(factory));
            var actual = target.GetTokenAsync(ii).GetAwaiter().GetResult();

        }
    }
}
