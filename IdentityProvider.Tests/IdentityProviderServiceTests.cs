using System.Net.Http;
using Xunit;
using Moq;
using com.b_velop.IdentityProvider;
using com.b_velop.IdentityProvider.Model;
using Microsoft.Extensions.Logging;

namespace App.IdentityProvider.Tests
{
    public class IdentityProviderServiceTests
    {
        public IdentityProviderServiceTests() { }

        public MyLoggerFactory GetLogger()
            => new MyLoggerFactory();

        public InfoItem GetInfoItem()
            => new InfoItem("mb", "mb", "https://mb.de") { Scope = "mb" };

        public Mock<HttpClient> GetHttpClient()
        {
            var mock = new Mock<HttpClient>();
            return mock;
        }

        [Fact]
        public void IdentityProviderService_Foo_Bar()
        {
            // Arrange
            var factory = GetLogger();
            var target = new IdentityProviderService(
                GetHttpClient().Object,
                new Logger<IdentityProviderService>(factory));

            // Act

            // Assert
        }
    }
}
