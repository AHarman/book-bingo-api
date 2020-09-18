using System.Threading.Tasks;
using BookBingoApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace BookBingoApi.Controllers
{
    [Route("api")]
    [ApiController]
    public class ShelvesController : ControllerBase
    {
        private readonly IBooksService _booksService;

        public ShelvesController(IBooksService booksService)
        {
            _booksService = booksService;
        }

        [HttpGet]
        [Route("user/{userId}/shelves/{shelf}")]
        public async Task<IActionResult> GetShelvesAsync([FromRoute] string userId, [FromRoute] string shelf)
        {
            var session = Request.Cookies["session"];
            return  Ok(await _booksService.GetShelf(shelf, userId, session));
        }
    }
}
