using library_management_api.Models.Dto;
using library_management_api.Models.Entity;
using library_management_api.Services.Auth;
using library_management_api.Services.Book;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace library_management_api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IBookInterface _bookInterface;
        private readonly IAuthInterface _authInterface;

        public BookController(IBookInterface bookInterface, IAuthInterface authInterface)
        {
            _bookInterface = bookInterface;
            _authInterface = authInterface;
        }

        [HttpPost]
        public async Task<ActionResult<ResponseModel<BookModel>>> AddBook(AddBookRequestDto request)
        {
            var authorizationHeader = Request.Headers["Authorization"].ToString();
            var token = authorizationHeader.Substring("Bearer ".Length).Trim();
            var library = await _authInterface.VerifyAccessToken(token);
            if (library is null)
            {
                return Unauthorized("Acesso negado!");
            }
            var response = await _bookInterface.AddBook(library, request);
            if (response.Success is false)
            {
                return BadRequest(response);
            }
            
            return Ok(response);
        }
        [HttpGet]
        public async Task<ActionResult<ResponseModel<object>>> GetAllBooks()
        {
            var authorizationHeader = Request.Headers["Authorization"].ToString();
            var token = authorizationHeader.Substring("Bearer ".Length).Trim();
            var library = await _authInterface.VerifyAccessToken(token);
            if (library is null)
            {
                return Unauthorized("Acesso negado!");
            }
            var response = await _bookInterface.GetAllBooks(library);
            if (response.Success is false)
            {
                return BadRequest(response);
            }
            
            return Ok(response);
        }
    }
}
