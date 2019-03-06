using System;
using System.Net.Http;
using com.b_velop.App.IdentityProvider;
using Xunit;
using Microsoft.Extensions.Logging;
using com.b_velop.App.IdentityProvider.Model;
using Moq;

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
