using System.Collections.Generic;
using System.Threading.Tasks;
using BookBingoApi.Models;
using BookBingoApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookBingoApi.Controllers
{
    [ApiController]
    [Route("api/search")]
    public class BookSearchController : ControllerBase
    {
        private readonly IBooksService _booksService;

        public BookSearchController(IBooksService booksService)
        {
            _booksService = booksService;
        }

        [HttpGet]
        [Route("books/{query}")]
        public async Task<IEnumerable<Book>> SearchBooksAsync(string query)
        {
            return await _booksService.SearchBooksAsync(query);
        }
    }
}
