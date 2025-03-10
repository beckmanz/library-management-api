using library_management_api.Data;
using library_management_api.Models.Dto;
using library_management_api.Models.Entity;
using Microsoft.EntityFrameworkCore;

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

    public async Task<ResponseModel<object>> GetAllAuthors(LibraryModel library)
    {
        ResponseModel<object> response = new ResponseModel<object>();
        var authors = await _context.Authors.Where(a => a.LibraryId == library.Id).ToListAsync();
        
        var Data = new
        {
            library.Id,
            library.Name,
            library.CreatedAt,
            Authors = authors
        };
        
        response.Message = "Autores retornados com sucesso!";
        response.Data = Data;
        return response;
    }
}