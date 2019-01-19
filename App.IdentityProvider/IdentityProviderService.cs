﻿using System.Net.Http;
using System.Threading.Tasks;
using com.b_velop.App.IdentityProvider.Model;
using Microsoft.Extensions.Logging;
using IdentityModel.Client;

namespace com.b_velop.App.IdentityProvider
{
    public class IdentityProviderService : IIdentityProviderService
    {
        private HttpClient _client;
        private readonly ILogger<IdentityProviderService> _logger;
        public string Adress { get; set; }

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
            var disco = await _client.GetDiscoveryDocumentAsync(infoItem.Url);
            if (disco.IsError)
            {
                _logger.LogError($"Failed to connect to '{infoItem.Url}'. No DiscoveryDocument could be loaded");
                return null;
            }

            var tokenResponse = await _client.RequestClientCredentialsTokenAsync(
                new ClientCredentialsTokenRequest
                {
                    Scope = infoItem.Scope,
                    Address = disco.TokenEndpoint,
                    ClientId = infoItem.ClientId,
                    ClientSecret = infoItem.Secret
                });
            infoItem.Delete(); 
            if (tokenResponse.IsError)
            {
                _logger.LogError($"Failed to download token from '{infoItem.Url}'. Error Description:'{tokenResponse.ErrorDescription}' Error Reason: '{tokenResponse.HttpErrorReason}' Http Status: '{tokenResponse.HttpStatusCode}'");
                return null;
            }

            Token token = Token.FromJson(tokenResponse.Raw);
            return token ?? null;
        }
    }
}