using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using com.b_velop.App.IdentityProvider.Model;
using Microsoft.Extensions.Logging;

namespace com.b_velop.App.IdentityProvider
{
    public class IdentityProviderService : IIdentityProviderService
    {
        private HttpClient _client;
        private readonly ILogger<IdentityProviderService> _logger;

        public IdentityProviderService(
            HttpClient client,
            ILogger<IdentityProviderService> logger)
        {
            _client = client;
            _logger = logger;
        }

        public async Task<Token> GetToken(
            InfoItem infoItem)
        {
            var body = new List<KeyValuePair<string, string>> {
                    new KeyValuePair<string, string> ("grant_type", "client_credentials"),
                    new KeyValuePair<string, string> ("scope", infoItem.Scope),
                };
            Token token = null;
            using (var content = new FormUrlEncodedContent(body))
            {
                _client.DefaultRequestHeaders.Clear();
                content.Headers.Clear();
                content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                _client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Basic", infoItem.GetBase64Credentials());
                var result = await _client.PostAsync(infoItem.GetUrlTokenPrefix(), content);
                if (result.IsSuccessStatusCode)
                {
                    var str = await result.Content.ReadAsStringAsync();
                    token = Token.FromJson(str);
                }
                else
                {
                    _logger?.Log(LogLevel.Error, $"Error occured while calling '{infoItem.Url}'. Message: '{result.ReasonPhrase}' Server Status Code: '{result.StatusCode}'");
                }
            }
            return token ?? null;
        }
    }
}
