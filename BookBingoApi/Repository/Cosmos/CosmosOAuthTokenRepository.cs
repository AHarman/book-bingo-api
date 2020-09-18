using AutoMapper;
using BookBingoApi.Models;
using BookBingoApi.Options;
using BookBingoApi.Repository.Cosmos.Daos;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace BookBingoApi.Repository.Cosmos
{
    public class CosmosOAuthTokenRepository : IOAuthTokenRepository
    {
        public readonly IOptions<CosmosOptions> _options;
        public readonly IMapper _mapper;
        public readonly Container _container;

        public CosmosOAuthTokenRepository(
            IOptions<CosmosOptions> options,
            IMapper mapper
        ) 
        {
            _options = options;
            _mapper = mapper;
            _container = new CosmosClient(options.Value.ConnectionString)
                .GetContainer(_options.Value.Database, _options.Value.Container);
        }

        public async Task<OAuthToken> GetToken(string id, string provider)
        {
            var response = await _container.ReadItemAsync<OAuthTokenDao>(id, new PartitionKey(provider));
            return _mapper.Map<OAuthToken>(response.Resource);
        }

        public async Task StoreAccessTokenAsync(OAuthToken token)
        {
            var dao = _mapper.Map<OAuthTokenDao>(token);
            dao.TimeToLive = _options.Value.AccessTokenTimeToLive;
            await _container.UpsertItemAsync(dao);
        }

        public async Task StoreRequestTokenAsync(OAuthToken token)
        {
            var dao = _mapper.Map<OAuthTokenDao>(token);
            dao.TimeToLive = _options.Value.RequestTokenTimeToLive;
            await _container.CreateItemAsync(dao);
        }

        public async Task RemoveTokenAsync(string id, string provider) =>
            await _container.DeleteItemAsync<OAuthTokenDao>(id, new PartitionKey(provider));
    }
}
