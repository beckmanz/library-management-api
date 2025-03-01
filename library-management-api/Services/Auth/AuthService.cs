using library_management_api.Data;
using library_management_api.Models;
using library_management_api.Models.Dto;
using library_management_api.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace library_management_api.Services.Auth;

public class AuthService : IAuthInterface
{
    private readonly ApplicationDbContext _context;

    public AuthService(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task<ResponseModel<AuthResponseDto>> Signup(SignupRequestDto signupRequest)
    {
        throw new NotImplementedException();
    }

    public string GetAccessToken(string Id, string Name)
    {
        throw new NotImplementedException();
    }

    public Task<LibraryModel> VerifyAccessToken(string token)
    {
        throw new NotImplementedException();
    }
}