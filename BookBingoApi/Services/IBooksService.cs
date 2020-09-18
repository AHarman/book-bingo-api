using System.Collections.Generic;
using System.Threading.Tasks;
using BookBingoApi.Models;

namespace BookBingoApi.Services
{
    public interface IBooksService
    {
        public Task<IEnumerable<Book>> SearchBooksAsync(string query);
        public Task<IEnumerable<BookshelfEntry>> GetShelf(string shelfName, string userId, string session);
        public Task<User> GetLoggedInUserAsync(string session);
    }
}
