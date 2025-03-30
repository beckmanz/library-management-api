using library_management_api.Data;
using library_management_api.Exceptions;
using library_management_api.Models.Dto;
using library_management_api.Models.Entity;
using Microsoft.EntityFrameworkCore;
using ValidationException = System.ComponentModel.DataAnnotations.ValidationException;

namespace library_management_api.Services.Library;

public class LibraryService : ILibraryInterface
{
    private readonly ApplicationDbContext _context;

    public LibraryService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseModel<LibraryResponseDto>> GetLibrary(LibraryModel library)
    {
        ResponseModel<LibraryResponseDto> response = new ResponseModel<LibraryResponseDto>();
        var libraryAuthenticated = new LibraryResponseDto()
        {
            Id = library.Id,
            Name = library.Name,
            Email = library.Email,
            CreatedAt = library.CreatedAt,
            TotalBooks = library.Books.Count,
            AvailableBooks = library.Books.Where(b => b.IsAvailable == true).Count(),
            BorrowedBooks = library.Loans.Where(x=> x.IsReturned == false).Count(),
        };
        
        response.Data = libraryAuthenticated;
        response.Message = "Detalhes da biblioteca retornados com sucesso!";
        return response;
    }

    public async Task<ResponseModel<LibraryResponseDto>> EditLibrary(LibraryModel library, EditLibraryRequestDto request)
    {
        ResponseModel<LibraryResponseDto> response = new ResponseModel<LibraryResponseDto>();
        if (string.IsNullOrWhiteSpace(request.Name) &&
            string.IsNullOrWhiteSpace(request.Email) &&
            string.IsNullOrWhiteSpace(request.Password)&&
            string.IsNullOrWhiteSpace(request.NewPassword))
        {
            throw new BadRequestException("Nenhuma informação foi fornecida para atualização.");
        }

        if (request.Password is not null && request.NewPassword is not null)
        {
            if (!BCrypt.Net.BCrypt.Verify(request.Password, library.PasswordHash))
                    throw new UnauthorizedException("Senha incorreta!");
            library.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
        }
        if (!string.IsNullOrWhiteSpace(request.Name)) library.Name = request.Name;
        if (!string.IsNullOrWhiteSpace(request.Email))
        {
            var exist = await _context.Librarys
                .Where(x=> x.Email == request.Email)
                .FirstOrDefaultAsync();
            if (exist is not null)
                throw new ValidationException("Email já cadastrado!");
            library.Email = request.Email;
        }
        
        _context.Update(library);
        await _context.SaveChangesAsync();

        var libraryUpdated = new LibraryResponseDto()
        {
            Id = library.Id,
            Name = library.Name,
            Email = library.Email,
            CreatedAt = library.CreatedAt,
            TotalBooks = library.Books.Count,
            AvailableBooks = library.Books.Where(b => b.IsAvailable == true).Count(),
            BorrowedBooks = library.Loans.Where(x => x.IsReturned == false).Count(),
        };
        
        response.Data = libraryUpdated;
        response.Message = "Informações da biblioteca atualizadas com sucesso!";
        return response;
    }

    public async Task<ResponseModel<LibraryResponseDto>> DeleteLibrary(LibraryModel library, string password)
    {
        ResponseModel<LibraryResponseDto> response = new ResponseModel<LibraryResponseDto>();
        if (!BCrypt.Net.BCrypt.Verify(password, library.PasswordHash))
            throw new UnauthorizedException("Senha incorreta!");
        
        _context.RemoveRange(library.Loans);
        _context.RemoveRange(library.Books);
        _context.RemoveRange(library.Authors);
        _context.RemoveRange(library.Readers);
        _context.Librarys.Remove(library);
        await _context.SaveChangesAsync();
        response.Message = "Biblioteca removida com sucesso!";
        return response;
    }
}