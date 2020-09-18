using BookBingoApi.Models;
using BookBingoApi.Options;
using BookBingoApi.Repository;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BookBingoApi.Services
{
    public class GoodReadsOAuthService : IOAuthService
    {
        private const string _provider = "Goodreads";
        private readonly HttpClient _httpClient;
        private readonly IOAuthTokenRepository _repository;
        private readonly IOptionsSnapshot<GoodreadsOptions> _options;

        public GoodReadsOAuthService(
            HttpClient httpClient, 
            IOptionsSnapshot<GoodreadsOptions> options,
            IOAuthTokenRepository repository
        )
        {
            _httpClient = httpClient;
            _options = options;
            _repository = repository;
        }

        public async Task<string> GetRequestTokenAsync()
        {
            var request = new HttpRequestMessage(
                HttpMethod.Post,
                new Uri(_httpClient.BaseAddress, $"oauth/request_token")
            );

            await SignRequestAsync(request, null);

            var response = await _httpClient.SendAsync(request);
            var requestToken = await ParseOAuthResponseAsync(response);

            await _repository.StoreRequestTokenAsync(requestToken);

            return requestToken.Token;
        }

        public async Task<string> GetAccessTokenAsync(string requestToken)
        {
            var request = new HttpRequestMessage(
                HttpMethod.Post,
                new Uri(_httpClient.BaseAddress, $"oauth/access_token")
            );

            await SignRequestAsync(request, requestToken);

            var response = await _httpClient.SendAsync(request);
            var accessToken = await ParseOAuthResponseAsync(response);

            await _repository.RemoveTokenAsync(requestToken, _provider);

            await _repository.StoreAccessTokenAsync(accessToken);

            return accessToken.Token;
        }

        public async Task<HttpRequestMessage> SignRequestAsync(HttpRequestMessage message, string session)
        {
            OAuthToken token = null;
            var oauthParams = new Dictionary<string, string>
            {
                { "oauth_consumer_key", _options.Value.ApiKey },
                { "oauth_version", "1.0" },
                { "oauth_signature_method", "HMAC-SHA1" },
                { "oauth_timestamp", DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString() },
                { "oauth_nonce", GenerateNonce() }
            };

            if (session != null)
            {
                token = await _repository.GetToken(session, _provider);
                oauthParams.Add("oauth_token", token?.Token);
            }

            var parameters = QueryHelpers.ParseQuery(message.RequestUri.Query)
                .Select(pair => new KeyValuePair<string, string>(pair.Key, pair.Value.First()))
                .Concat(oauthParams.ToList())
                .ToDictionary(it => it.Key, it => it.Value);

            parameters.Add("oauth_signature", CreateSignature(message.RequestUri.GetLeftPart(UriPartial.Path), parameters, message.Method, token?.Secret));

            message.RequestUri = new Uri(QueryHelpers.AddQueryString(message.RequestUri.GetLeftPart(UriPartial.Path), parameters));

            return message;
        }

        private async Task<OAuthToken> ParseOAuthResponseAsync(HttpResponseMessage response)
        {
            var content = HttpUtility.ParseQueryString(await response.Content.ReadAsStringAsync());

            return new OAuthToken
            {
                Provider = _provider,
                Secret = content["oauth_token_secret"],
                Token = content["oauth_token"]
            };
        }

        private string CreateQueryString (IEnumerable<KeyValuePair<string, string>> parameters) => parameters
                .Select(param => KeyValuePair.Create(Uri.EscapeDataString(param.Key), Uri.EscapeDataString(param.Value)))
                .OrderBy(param => param.Key)
                .ThenBy(param => param.Value)
                .Aggregate("", (agg, next) => $"{agg}&{next.Key}={next.Value}")[1..^0];

        private string CreateSignature(string requestUrl, IEnumerable<KeyValuePair<string, string>> parameters, HttpMethod method, string tokenSecret)
        {
            var paramString = CreateQueryString(parameters);
            var stringToHash = $"{method}&{Uri.EscapeDataString(requestUrl.ToString())}&{Uri.EscapeDataString(paramString)}";
            var hasher = new HMACSHA1(Encoding.ASCII.GetBytes(_options.Value.ApiSecret + "&" + tokenSecret));
            var hash = hasher.ComputeHash(Encoding.ASCII.GetBytes(stringToHash));
            return Convert.ToBase64String(hash);
        }

        private string GenerateNonce()
        {
            var randoBytes = new byte[16];
            var rng = new RNGCryptoServiceProvider();
            rng.GetBytes(randoBytes);
            rng.Dispose();
            return Convert.ToBase64String(randoBytes);
        }
    }
}
