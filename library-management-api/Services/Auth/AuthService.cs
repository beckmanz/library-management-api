using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using library_management_api.Data;
using library_management_api.Models;
using library_management_api.Models.Dto;
using library_management_api.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace library_management_api.Services.Auth;

public class AuthService : IAuthInterface
{
    private readonly ApplicationDbContext _context;
    private readonly IConfiguration _configuration;

    public AuthService(ApplicationDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
    }

    public async Task<ResponseModel<AuthResponseDto>> Signup(SignupRequestDto signupRequest)
    {
        ResponseModel<AuthResponseDto> response = new ResponseModel<AuthResponseDto>();
        try
        {
            var validEmail = await _context.Librarys.FirstOrDefaultAsync(l => l.Email == signupRequest.Email);
            if (validEmail is not null)
            {
                response.Success = false;
                response.Message = "Email já existe!";
                return response;
            }

            var hashPass = BCrypt.Net.BCrypt.HashPassword(signupRequest.Password);

            var newLibrary = new LibraryModel()
            {
                Name = signupRequest.Name,
                Email = signupRequest.Email,
                PasswordHash = hashPass,
            };

            _context.Add(newLibrary);
            await _context.SaveChangesAsync();
            
            var token = GetAccessToken(newLibrary.Id.ToString(), newLibrary.Name);

            var data = new AuthResponseDto()
            {
                Name = newLibrary.Name,
                Id = newLibrary.Id.ToString(),
                Token = token,
            };
            
            response.Message = "Livraria registrada com sucesso!";
            response.Data = data;
            return response;
        }
        catch (Exception e)
        {
            response.Message = e.Message;
            response.Success = false;
            return response;
        }
    }

    public async Task<ResponseModel<AuthResponseDto>> Signin(SigninRequestDto signinRequest)
    {
        ResponseModel<AuthResponseDto> response = new ResponseModel<AuthResponseDto>();
        try
        {
            var validUser = await _context.Librarys.FirstOrDefaultAsync(l => l.Email == signinRequest.Email);
            if (validUser is null)
            {
                response.Success = false;
                response.Message = "Acesso negado!";
                return response;
            }

            if (!BCrypt.Net.BCrypt.Verify(signinRequest.Password, validUser.PasswordHash))
            {
                response.Success = false;
                response.Message = "Acesso negado!";
                return response;
            }
            
            var token = GetAccessToken(validUser.Id.ToString(), validUser.Name);

            var data = new AuthResponseDto()
            {
                Name = validUser.Name,
                Id = validUser.Id.ToString(),
                Token = token,
            };
            
            response.Message = "Login realizado com sucesso!";
            response.Data = data;
            return response;
        }
        catch (Exception e)
        {
            response.Message = e.Message;
            response.Success = false;
            return response;
        }
    }

    public string GetAccessToken(string Id, string Name)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? String.Empty));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, Name),
            new Claim(ClaimTypes.NameIdentifier, Id),
        };

        var token = new JwtSecurityToken(
            _configuration["Jwt:Issuer"],
            _configuration["Jwt:Audience"],
            claims,
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public async Task<LibraryModel> VerifyAccessToken(string token)
    {
        var handler = new JwtSecurityTokenHandler();
    
        if (!handler.CanReadToken(token))
        {
            return null;
        }
            
        var jwtToken = handler.ReadJwtToken(token);
        var Id = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        
        var Library = await _context.Librarys.FirstOrDefaultAsync(u => u.Id == new Guid(Id));
        return Library;
    }
}