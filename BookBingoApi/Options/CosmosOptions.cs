namespace BookBingoApi.Options
{
    public class CosmosOptions
    {
        public string ConnectionString { get; set; }
        public string Database { get; set; }
        public string Container { get; set; }
        public int RequestTokenTimeToLive { get; set; }
        public int AccessTokenTimeToLive { get; set; }
    }
}
