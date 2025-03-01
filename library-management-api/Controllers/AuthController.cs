using library_management_api.Models;
using library_management_api.Models.Dto;
using library_management_api.Services.Auth;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace library_management_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthInterface _authInterface;

        public AuthController(IAuthInterface authInterface)
        {
            _authInterface = authInterface;
        }
        [HttpPost("signup")]
        public async Task<ActionResult<ResponseModel<AuthResponseDto>>> Signup(SignupRequestDto signupRequest)
        {
            var response = await _authInterface.Signup(signupRequest);

            if (response.Success is false)
            {
                return BadRequest(response);
            }
            
            return Ok(response);
        }
        [HttpPost("signin")]
        public async Task<ActionResult<ResponseModel<AuthResponseDto>>> Signup(SigninRequestDto signinRequest)
        {
            var response = await _authInterface.Signin(signinRequest);

            if (response.Success is false)
            {
                return BadRequest(response);
            }
            
            return Ok(response);
        }
    }
}
