using library_management_api.Exceptions;
using library_management_api.Models.Dto;
using library_management_api.Models.Entity;
using library_management_api.Services.Auth;
using library_management_api.Services.Loan;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace library_management_api.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class LoanController : ControllerBase
    {
        private readonly ILoanInterface _loanInterface;
        private readonly IAuthInterface _authInterface;

        public LoanController(IAuthInterface authInterface, ILoanInterface loanService)
        {
            _authInterface = authInterface;
            _loanInterface = loanService;
        }

        [HttpPost]
        public async Task<ActionResult<ResponseModel<LoanModel>>> RegisterNewLoan(RegisterNewLoanRequestDto request)
        {
            var authorizationHeader = Request.Headers["Authorization"].ToString();
            var token = authorizationHeader.Substring("Bearer ".Length).Trim();
            var library = await _authInterface.VerifyAccessToken(token);
            if (library is null)
            {
                throw new UnauthorizedException("Acesso negado!");
            }
            var response = await _loanInterface.RegisterNewLoan(library, request);
            return Ok(response);
        }
        [HttpGet]
        public async Task<ActionResult<ResponseModel<List<LoanResponseDto>>>> GetAllLoans()
        {
            var authorizationHeader = Request.Headers["Authorization"].ToString();
            var token = authorizationHeader.Substring("Bearer ".Length).Trim();
            var library = await _authInterface.VerifyAccessToken(token);
            if (library is null)
            {
                throw new UnauthorizedException("Acesso negado!");
            }
            var response = await _loanInterface.GetAllLoans(library);
            return Ok(response);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseModel<LoanResponseDto>>> GetLoan(Guid id)
        {
            var authorizationHeader = Request.Headers["Authorization"].ToString();
            var token = authorizationHeader.Substring("Bearer ".Length).Trim();
            var library = await _authInterface.VerifyAccessToken(token);
            if (library is null)
            {
                throw new UnauthorizedException("Acesso negado!");
            }
            var response = await _loanInterface.GetLoan(library, id);
            return Ok(response);
        }
        [HttpPut("return/{id}")]
        public async Task<ActionResult<ResponseModel<LoanResponseDto>>> ReturnBook(Guid id)
        {
            var authorizationHeader = Request.Headers["Authorization"].ToString();
            var token = authorizationHeader.Substring("Bearer ".Length).Trim();
            var library = await _authInterface.VerifyAccessToken(token);
            if (library is null)
            {
                throw new UnauthorizedException("Acesso negado!");
            }
            var response = await _loanInterface.ReturnBook(library, id);
            return Ok(response);
        }
    }
}
