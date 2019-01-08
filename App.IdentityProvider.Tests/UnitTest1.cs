using System;
using com.b_velop.App.IdentityProvider.Model;
using Xunit;

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
    }
}
