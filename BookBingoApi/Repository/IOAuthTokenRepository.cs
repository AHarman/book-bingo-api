using BookBingoApi.Models;
using System.Threading.Tasks;

namespace BookBingoApi.Repository
{
    public interface IOAuthTokenRepository
    {
        public Task<OAuthToken> GetToken(string id, string provider);
        public Task StoreRequestTokenAsync(OAuthToken token);
        public Task StoreAccessTokenAsync(OAuthToken token);
        public Task RemoveTokenAsync(string id, string provider);
    }
}
