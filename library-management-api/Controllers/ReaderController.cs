using library_management_api.Exceptions;
using library_management_api.Models.Dto;
using library_management_api.Models.Entity;
using library_management_api.Services.Auth;
using library_management_api.Services.Reader;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace library_management_api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ReaderController : ControllerBase
    {
        private readonly IReaderInterface _readerInterface;
        private readonly IAuthInterface _authInterface;

        public ReaderController(IReaderInterface readerInterface, IAuthInterface authInterface)
        {
            _readerInterface = readerInterface;
            _authInterface = authInterface;
        }

        [HttpPost]
        public async Task<ActionResult<ResponseModel<ReaderModel>>> AddReader(AddReaderRequestDto request)
        {
            var token = HttpContext.Request.Cookies["AuthCookie"];
            var library = await _authInterface.VerifyAccessToken(token);
            if (library is null)
            {
                throw new UnauthorizedException("Acesso negado!");
            }
            var response = await _readerInterface.AddReader(library, request);
            return Ok(response);
        }
        [HttpGet]
        public async Task<ActionResult<ResponseModel<Object>>> GetAllReaders()
        {
            var token = HttpContext.Request.Cookies["AuthCookie"];
            var library = await _authInterface.VerifyAccessToken(token);
            if (library is null)
            {
                throw new UnauthorizedException("Acesso negado!");
            }
            var response = await _readerInterface.GetAllReaders(library);
            return Ok(response);
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<ResponseModel<Object>>> GetReaderById(Guid Id)
        {
            var token = HttpContext.Request.Cookies["AuthCookie"];
            var library = await _authInterface.VerifyAccessToken(token);
            if (library is null)
            {
                throw new UnauthorizedException("Acesso negado!");
            }
            var response = await _readerInterface.GetReaderById(library, Id);
            return Ok(response);
        }
        [HttpGet("byname/{Name}")]
        public async Task<ActionResult<ResponseModel<List<ReaderModel>>>> GetReaderByName(string Name)
        {
            var token = HttpContext.Request.Cookies["AuthCookie"];
            var library = await _authInterface.VerifyAccessToken(token);
            if (library is null)
            {
                throw new UnauthorizedException("Acesso negado!");
            }
            var response = await _readerInterface.GetReaderByName(library, Name);
            return Ok(response);
        }
    }
}
