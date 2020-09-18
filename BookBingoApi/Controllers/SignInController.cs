using System.Threading.Tasks;
using BookBingoApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookBingoApi.Controllers
{
    [ApiController]
    [Route("api")]
    public class SignInController : ControllerBase
    {
        private readonly IOAuthService _oauthService;
        private readonly IBooksService _booksService;

        public SignInController(
            IOAuthService oauthService,
            IBooksService booksService
        )
        {
            _oauthService = oauthService;
            _booksService = booksService;
        }

        [HttpPost]
        [Route("oauth/request_token")]
        public async Task<IActionResult> GetRequestTokenAsync()
        {
            var response = await _oauthService.GetRequestTokenAsync();
            return Ok(response);
        }

        [HttpPost]
        [Route("oauth/access_token")]
        public async Task<IActionResult> GetAccessTokenAsync([FromQuery(Name = "request_token")] string requestToken)
        {
            var token = await _oauthService.GetAccessTokenAsync(requestToken);

            if (token == null)
            {
                return Unauthorized("Unable to gain access token");
            }

            var opts = new CookieOptions
            {
                //Secure = true,
                //HttpOnly = true,
                //IsEssential = true,
            };

            Response.Cookies.Append("session", token, opts);

            return NoContent();
        }

        [HttpGet]
        [Route("user")]
        public async Task<IActionResult> GetLoggedInUserAsync()
        {
            var session = Request.Cookies["session"];
            if (session == null) return Unauthorized();

            var user = await _booksService.GetLoggedInUserAsync(session);

            return Ok(user);
        }
    }
}
