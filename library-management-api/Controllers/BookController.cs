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
            var token = HttpContext.Request.Cookies["AuthCookie"];
            var library = await _authInterface.VerifyAccessToken(token);
            if (library is null)
            {
                return Unauthorized("Acesso negado!");
            }
            var response = await _bookInterface.AddBook(library, request);
            if (response.Success is false)
            {
                return NotFound(response);
            }

            return Ok(response);
        }
        [HttpGet]
        public async Task<ActionResult<ResponseModel<object>>> GetAllBooks()
        {
            var token = HttpContext.Request.Cookies["AuthCookie"];
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
        [HttpGet("{Id}")]
        public async Task<ActionResult<ResponseModel<BookModel>>> GetBook(Guid Id)
        {
            var token = HttpContext.Request.Cookies["AuthCookie"];
            var library = await _authInterface.VerifyAccessToken(token);
            if (library is null)
            {
                return Unauthorized("Acesso negado!");
            }
            var response = await _bookInterface.GetBook(library, Id);
            if (response.Success is false)
            {
                return NotFound(response);
            }

            return Ok(response);
        }
        [HttpPut]
        public async Task<ActionResult<ResponseModel<BookModel>>> EditBook(EditBookRequestDto request)
        {
            var token = HttpContext.Request.Cookies["AuthCookie"];
            var library = await _authInterface.VerifyAccessToken(token);
            if (library is null)
            {
                return Unauthorized("Acesso negado!");
            }
            
            if (string.IsNullOrWhiteSpace(request.Title) &&
                !request.PublicationYear.HasValue &&
                string.IsNullOrWhiteSpace(request.AuthorId) &&
                string.IsNullOrWhiteSpace(request.Genre))
            {
                return BadRequest("Nenhuma informação foi fornecida para atualização.");
            }

            var response = await _bookInterface.EditBook(library, request);
            if (response.Success is false)
            {
                return NotFound(response);
            }

            return Ok(response);
        }
    }
}
