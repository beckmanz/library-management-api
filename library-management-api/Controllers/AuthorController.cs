using library_management_api.Exceptions;
using library_management_api.Models.Dto;
using library_management_api.Models.Entity;
using library_management_api.Services.Auth;
using library_management_api.Services.Author;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace library_management_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthInterface _authInterface;
        private readonly IAuthorInterface _authorInterface;

        public AuthorController(IAuthInterface authInterface, IAuthorInterface authorInterface)
        {
            _authInterface = authInterface;
            _authorInterface = authorInterface;
        }

        [HttpPost]
        public async Task<ActionResult<ResponseModel<AuthorModel>>> GetAuthors(AddAuthorRequestDto request)
        {
            var token = HttpContext.Request.Cookies["AuthCookie"];
            var library = await _authInterface.VerifyAccessToken(token);
            if (library is null)
            {
                throw new UnauthorizedException("Acesso negado!");
            }
            var response = await _authorInterface.AddAuthor(library, request);
            return Ok(response);
        }
    }
}
