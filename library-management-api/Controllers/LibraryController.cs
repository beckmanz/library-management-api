using library_management_api.Exceptions;
using library_management_api.Models.Dto;
using library_management_api.Services.Auth;
using library_management_api.Services.Author;
using library_management_api.Services.Library;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace library_management_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibraryController : ControllerBase
    {
        private readonly ILibraryInterface _libraryInterface;
        private readonly IAuthInterface _authInterface;

        public LibraryController(ILibraryInterface libraryInterface, IAuthInterface authInterface)
        {
            _libraryInterface = libraryInterface;
            _authInterface = authInterface;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseModel<LibraryResponseDto>>> GetLibrary()
        {
            var token = HttpContext.Request.Cookies["AuthCookie"];
            var library = await _authInterface.VerifyAccessToken(token);
            if (library is null)
            {
                throw new UnauthorizedException("Acesso negado!");
            }
            
            var response = await _libraryInterface.GetLibrary(library);
            return Ok(response);
        }
    }
}
