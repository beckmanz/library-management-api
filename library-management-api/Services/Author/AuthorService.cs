using library_management_api.Data;
using library_management_api.Exceptions;
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

    public async Task<ResponseModel<object>> GetAuthor(LibraryModel library, Guid Id)
    {
        ResponseModel<object> response = new ResponseModel<object>();
        var author = await _context.Authors
            .Include(a => a.Books)
            .FirstOrDefaultAsync(a => a.LibraryId == library.Id && a.Id == Id);
        if (author is null)
        {
            throw new NotFoundException("Nenhum autor encontrado!");
        }

        var dataAuthor = new
        {
            author.Id,
            author.Name,
            author.Nationality,
            author.Books
        };
        
        response.Data = dataAuthor;
        response.Message = "Autor encontrado com sucesso!";
        return response;
    }

    public async Task<ResponseModel<List<AuthorModel>>> GetAuthorByName(LibraryModel library, string Name)
    {
        ResponseModel<List<AuthorModel>> response = new ResponseModel<List<AuthorModel>>();

        var authors = await _context.Authors
            .Where(x => x.Name.ToLower().Contains(Name.ToLower()) && x.LibraryId == library.Id)
            .ToListAsync();

        if (authors.Count == 0)
        {
            throw new NotFoundException("Nenhum autor encontrado!");
        }
        response.Message = "Autores encontrados com sucesso!";
        response.Data = authors;
        return response;
    }

    public async Task<ResponseModel<AuthorModel>> EditAuthor(LibraryModel library, Guid Id, EditAuthorRequestDto request)
    {
        ResponseModel<AuthorModel> response = new ResponseModel<AuthorModel>();
        var author = await _context.Authors
            .FirstOrDefaultAsync(a => a.LibraryId == library.Id && a.Id == Id);
        if (author is null)
        {
            throw new NotFoundException("Nenhum autor encontrado!");
        }

        if (string.IsNullOrWhiteSpace(request.Name) && string.IsNullOrWhiteSpace(request.Nationality))
        {
            throw new BadRequestException("Nenhuma informação foi fornecida para atualização.");
        }
        if(!string.IsNullOrWhiteSpace(request.Name)) author.Name = request.Name;
        if(!string.IsNullOrWhiteSpace(request.Nationality)) author.Nationality = request.Nationality;
        
        _context.Update(author);
        await _context.SaveChangesAsync();
        
        response.Data = author;
        response.Message = "Autor atualizado com sucesso!";
        return response;
    }

    public async Task<ResponseModel<AuthorModel>> DeleteAuthor(LibraryModel library, Guid Id)
    {
        ResponseModel<AuthorModel> response = new ResponseModel<AuthorModel>();
        var author = await _context.Authors
            .FirstOrDefaultAsync(a => a.LibraryId == library.Id && a.Id == Id);
        if (author is null)
        {
            throw new NotFoundException("Nenhum autor encontrado!");
        }
        _context.Remove(author);
        await _context.SaveChangesAsync();
        
        response.Message = "Autor excluído com sucesso!";
        return response;
    }
}