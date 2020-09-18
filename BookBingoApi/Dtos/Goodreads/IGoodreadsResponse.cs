
namespace BookBingoApi.Dtos.Goodreads
{
    public interface IGoodreadsResponse<TResult>
    {
        TResult Result { get; set; }
    }
}
