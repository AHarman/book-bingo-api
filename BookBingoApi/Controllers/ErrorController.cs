using BookBingoApi.Exceptions;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookBingoApi.Controllers
{
    [ApiController]
    public class ErrorController : ControllerBase
    {
        [Route("error")]
        public IActionResult HandleError([FromServices] IWebHostEnvironment webHostEnvironment)
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            switch (context.Error)
            {
                case TokenNotFoundException e:
                    return Unauthorized("Session not valid. It may have expired.");
                case ApiNotAuthorisedException e:
                    return Forbid("Forbidden by upstream API. Please ensure the data is public or that you are signed in.");
            }

            if (webHostEnvironment.EnvironmentName == "Development")
            {
                return Problem(detail: context.Error.StackTrace, title: context.Error.Message);
            }

            return Problem("Unknown error");
        }
    }
}
