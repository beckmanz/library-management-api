using library_management_api.Models;
using library_management_api.Models.Dto;
using library_management_api.Models.Entity;

namespace library_management_api.Services.Auth;

public interface IAuthInterface
{
    Task<ResponseModel<AuthResponseDto>> Signup(SignupRequestDto signupRequest);
    Task<ResponseModel<AuthResponseDto>> Signin(SigninRequestDto signinRequest);
    string GetAccessToken(string Id, string Name);
    Task<LibraryModel> VerifyAccessToken(string token);
}