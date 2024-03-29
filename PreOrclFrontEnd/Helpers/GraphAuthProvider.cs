﻿using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Microsoft.Graph;
using Microsoft.Identity.Client;
using PreOrclFrontEnd.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PreOrclFrontEnd.Helpers
{
    public class GraphAuthProvider : IGraphAuthProvider
    {
        private readonly IMemoryCache _memoryCache;
        private TokenCache _userTokenCache;

        // Properties used to get and manage an access token.
        private readonly string _appId;
        private readonly ClientCredential _credential;
        private readonly string[] _scopes;
        private readonly string _redirectUri;

        public GraphAuthProvider(IMemoryCache memoryCache, IConfiguration configuration)
        {
            var azureOptions = new AzureAdOptions();
            configuration.Bind("AzureAd", azureOptions);

            _appId = azureOptions.ClientId;
            _credential = new ClientCredential(azureOptions.ClientSecret);
            _scopes = azureOptions.GraphScopes.Split(new[] { ' ' });
            _redirectUri = azureOptions.BaseUrl + azureOptions.CallbackPath;

            _memoryCache = memoryCache;
        }

        // Gets an access token. First tries to get the access token from the token cache.
        // Using password (secret) to authenticate. Production apps should use a certificate.
        public async Task<string> GetUserAccessTokenAsync(string userId)
        {
            _userTokenCache = new SessionTokenCache(userId, _memoryCache).GetCacheInstance();

            var cca = new ConfidentialClientApplication(
                _appId,
                _redirectUri,
                _credential,
                _userTokenCache,
                null);

            var accounts = (await cca.GetAccountsAsync()).ToList();
            if (!accounts.Any()) throw new ServiceException(new Error
            {
                Code = "TokenNotFound",
                Message = "User not found in token cache. Maybe the server was restarted."
            });

            try
            {
                var result = await cca.AcquireTokenSilentAsync(_scopes, accounts.First());
                return result.AccessToken;
            }

            // Unable to retrieve the access token silently.
            catch (Exception)
            {
                throw new ServiceException(new Error
                {
                    Code = GraphErrorCode.AuthenticationFailure.ToString(),
                    Message = "Caller needs to authenticate. Unable to retrieve the access token silently."
                });
            }
        }
    }

    public interface IGraphAuthProvider
    {
        Task<string> GetUserAccessTokenAsync(string userId);
    }
}
