using library_management_api.Data;
using library_management_api.Models.Dto;
using library_management_api.Models.Entity;

namespace library_management_api.Services.Author;

public class AuthorService : IAuthorInterface
{
    private readonly ApplicationDbContext _context;

    public AuthorService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseModel<AuthorModel>> AddAuthor(LibraryModel library, AddAuthorRequestDto author)
    {
        ResponseModel<AuthorModel> response = new ResponseModel<AuthorModel>();
        var newAuthor = new AuthorModel
        {
            Name = author.Name,
            Nationality = author.Nacionality,
            LibraryId = library.Id,
            Library = library
        };
        _context.Add(newAuthor);
        await _context.SaveChangesAsync();
        
        response.Data = newAuthor;
        response.Message = "Autor cadastrado com sucesso!";
        return response;
    }
}