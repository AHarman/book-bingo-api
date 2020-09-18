using System.Net.Http;
using System.Threading.Tasks;

namespace BookBingoApi.Services
{
    public interface IOAuthService
    {
        public Task<string> GetRequestTokenAsync();
        public Task<string> GetAccessTokenAsync(string requestToken);
        public Task<HttpRequestMessage> SignRequestAsync(HttpRequestMessage message, string session);
    }
}