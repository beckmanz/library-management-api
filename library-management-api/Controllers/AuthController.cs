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
            var token = _authInterface.GetAccessToken(response.Data.Id, response.Data.Name);

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(7)
            };

            Response.Cookies.Append("AuthCookie", token, cookieOptions);
            return Ok(response);
        }
        [HttpPost("signin")]
        public async Task<ActionResult<ResponseModel<AuthResponseDto>>> Signup(SigninRequestDto signinRequest)
        {
            var response = await _authInterface.Signin(signinRequest);
            var token = _authInterface.GetAccessToken(response.Data.Id, response.Data.Name);

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(7)
            };

            Response.Cookies.Append("AuthCookie", token, cookieOptions);
            return Ok(response);
        }
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(-1)
            };
            Response.Cookies.Delete("AuthCookie", cookieOptions);
            return Ok(new { Message = "Logout realizado com sucesso" });
        }
    }
}
