using System;
using com.b_velop.App.IdentityProvider.Model;
using Xunit;
using System.Net.Sockets;
using com.b_velop.App.IdentityProvider;
using Microsoft.Extensions.Logging;

namespace App.IdentityProvider.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void InfoItem_Url_ReturnsUrlWithoutTrailingSlash()
        {
            // Arrange
            var sut = new InfoItem("foo", "bar");
            sut.Url = "https://test.de/";
            var actual = sut.Url;

            Assert.Equal("https://test.de", actual);
        }


        [Fact]
        public void InfoItem_Url_ReturnsUrlWitCorrectPrefix()
        {
            // Arrange
            var sut = new InfoItem("foo", "bar");
            sut.Url = "https://test.de/";
            var actual = sut.GetUrlTokenPrefix();

            Assert.Equal("https://test.de/connect/token", actual);
        }

        [Fact]
        public async void Integration()
        {
            var ii = new InfoItem("jupiter.linux", "gpuEavEWvdfAr7a6FdK-YWpoGD9mbyetiYw!kaL9-r8RQtTYZW2J8kxj-XaGgnSmZSEae", "https://identity.phlex.de")
            {
                Scope = "com.bvelop.homeapi"
            };
            var id = new IdentityProviderService(new System.Net.Http.HttpClient(), null);
            var token = await id.GetToken(ii);

        }
    }
}
