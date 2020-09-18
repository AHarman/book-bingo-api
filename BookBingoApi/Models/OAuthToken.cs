namespace BookBingoApi.Models
{
    public class OAuthToken
    {
        public string Provider { get; set; }
        public string Token { get; set; }
        public string Secret { get; set; }
    }
}
