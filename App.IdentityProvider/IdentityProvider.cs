using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using com.b_velop.App.IdentityProvider.Model;
using NLog;

namespace com.b_velop.App.IdentityProvider
{
    public class IdentityProvider : IIdentityProvider
    {
        public IdentityProvider(
            ILogger logger = null)
        {
            _logger = logger;
        }

        private readonly ILogger _logger;
        public async Task<string> GetIdentityToken(
            string clientId,
            string secret,
            string scope,
            string authorityUrl)
        {
            var body = new List<KeyValuePair<string, string>> {
                    new KeyValuePair<string, string> ("grant_type", "client_credentials"),
                    new KeyValuePair<string, string> ("scope", scope),
                };
            Token token = null;
            using (var content = new FormUrlEncodedContent(body))
            using (var client = new HttpClient())
            {
                var credentials = Convert.ToBase64String(
                    Encoding.ASCII.GetBytes($"{clientId}:{secret}"));

                content.Headers.Clear();
                content.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
                client.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Basic", credentials);
                var result = await client.PostAsync(authorityUrl, content);
                if (result.IsSuccessStatusCode)
                {
                    var str = await result.Content.ReadAsStringAsync();
                    token = Token.FromJson(str);
                }
                else
                {
                    _logger?.Log(LogLevel.Error, $"Error occured while calling '{authorityUrl}'. Message: '{result.ReasonPhrase}' Server Status Code: '{result.StatusCode}'");
                }
            }
            return token != null ? token.AccessToken : string.Empty;
        }

        public async Task<string> GetIdentityToken(
            string clientId,
            string secret,
            string scope)
        {
            const string identityUrl = "https://identity.marcelbenders.de/connect/token";
            return await GetIdentityToken(clientId, secret, scope, identityUrl);
        }
    }
}