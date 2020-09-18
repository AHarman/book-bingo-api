
using Newtonsoft.Json;

namespace BookBingoApi.Repository.Cosmos.Daos
{
    public class OAuthTokenDao
    {
        [JsonProperty(PropertyName = "id")]
        public string Id => Token;
        public string Provider { get; set; }
        public string PartitionKey => Provider;
        public string Token { get; set; }
        public string Secret { get; set; }

        [JsonProperty(PropertyName = "ttl")]
        public int TimeToLive { get; set; }
    }
}
