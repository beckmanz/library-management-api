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
            var authorizationHeader = Request.Headers["Authorization"].ToString();
            var token = authorizationHeader.Substring("Bearer ".Length).Trim();
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
            var authorizationHeader = Request.Headers["Authorization"].ToString();
            var token = authorizationHeader.Substring("Bearer ".Length).Trim();
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
            var authorizationHeader = Request.Headers["Authorization"].ToString();
            var token = authorizationHeader.Substring("Bearer ".Length).Trim();
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
            var authorizationHeader = Request.Headers["Authorization"].ToString();
            var token = authorizationHeader.Substring("Bearer ".Length).Trim();
            var library = await _authInterface.VerifyAccessToken(token);
            if (library is null)
            {
                throw new UnauthorizedException("Acesso negado!");
            }
            var response = await _readerInterface.GetReaderByName(library, Name);
            return Ok(response);
        }
        [HttpPut("{Id}")]
        public async Task<ActionResult<ResponseModel<ReaderModel>>> EditReader(Guid Id, EditReaderRequestDto request)
        {
            var authorizationHeader = Request.Headers["Authorization"].ToString();
            var token = authorizationHeader.Substring("Bearer ".Length).Trim();
            var library = await _authInterface.VerifyAccessToken(token);
            if (library is null)
            {
                throw new UnauthorizedException("Acesso negado!");
            }
            var response = await _readerInterface.EditReader(library, Id, request);
            return Ok(response);
        }
        [HttpDelete("{Id}")]
        public async Task<ActionResult<ResponseModel<ReaderModel>>> DeleteReader(Guid Id)
        {
            var authorizationHeader = Request.Headers["Authorization"].ToString();
            var token = authorizationHeader.Substring("Bearer ".Length).Trim();
            var library = await _authInterface.VerifyAccessToken(token);
            if (library is null)
            {
                throw new UnauthorizedException("Acesso negado!");
            }
            var response = await _readerInterface.DeleteReader(library, Id);
            return Ok(response);
        }
    }
}
