using library_management_api.Exceptions;
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
                throw new UnauthorizedException("Acesso negado!");
            }
            var response = await _bookInterface.AddBook(library, request);
            return Ok(response);
        }
        [HttpGet]
        public async Task<ActionResult<ResponseModel<List<BookModel>>>> GetAllBooks()
        {
            var token = HttpContext.Request.Cookies["AuthCookie"];
            var library = await _authInterface.VerifyAccessToken(token);
            if (library is null)
            {
                throw new UnauthorizedException("Acesso negado!");
            }
            var response = await _bookInterface.GetAllBooks(library);
            return Ok(response);
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<ResponseModel<BookModel>>> GetBook(Guid Id)
        {
            var token = HttpContext.Request.Cookies["AuthCookie"];
            var library = await _authInterface.VerifyAccessToken(token);
            if (library is null)
            {
                throw new UnauthorizedException("Acesso negado!");
            }
            var response = await _bookInterface.GetBook(library, Id);
            return Ok(response);
        }
        [HttpGet("bytitle/{Title}")]
        public async Task<ActionResult<ResponseModel<BookModel>>> GetBookByTitle(string Title)
        {
            var token = HttpContext.Request.Cookies["AuthCookie"];
            var library = await _authInterface.VerifyAccessToken(token);
            if (library is null)
            {
                throw new UnauthorizedException("Acesso negado!");
            }
            var response = await _bookInterface.GetBookByTitle(library, Title);
            return Ok(response);
        }
        [HttpPut]
        public async Task<ActionResult<ResponseModel<BookModel>>> EditBook(EditBookRequestDto request)
        {
            var token = HttpContext.Request.Cookies["AuthCookie"];
            var library = await _authInterface.VerifyAccessToken(token);
            if (library is null)
            {
                throw new UnauthorizedException("Acesso negado!");
            }
            var response = await _bookInterface.EditBook(library, request);
            return Ok(response);
        }
        [HttpDelete("{Id}")]
        public async Task<ActionResult<ResponseModel<BookModel>>> DeleteBook(Guid Id)
        {
            var token = HttpContext.Request.Cookies["AuthCookie"];
            var library = await _authInterface.VerifyAccessToken(token);
            if (library is null)
            {
                throw new UnauthorizedException("Acesso negado!");
            }
            var response = await _bookInterface.DeleteBook(library, Id);
            return Ok(response);
        }
    }
}
