using library_management_api.Data;
using library_management_api.Models.Dto;
using library_management_api.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace library_management_api.Services.Book;

public class BookService : IBookInterface
{
    private readonly ApplicationDbContext _context;

    public BookService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseModel<BookModel>> AddBook(LibraryModel library, AddBookRequestDto request)
    {
        ResponseModel<BookModel> response = new ResponseModel<BookModel>();
        try
        {
            var Author = await _context.Authors.FirstOrDefaultAsync(x => x.Id == request.AuthorId && x.LibraryId == library.Id);
            if (Author is null)
            {
                response.Success = false;
                response.Message = "Nenhum Author encontrado!";
                return response;
            }

            var newBook = new BookModel()
            {
                Title = request.Title,
                Genre = request.Genre,
                PublicationYear = Convert.ToInt32(request.PublicationYear),
                AuthorId = Author.Id,
                Author = Author,
                LibraryId = library.Id,
                Library = library,
            };
            _context.Add(newBook);
            await _context.SaveChangesAsync();

            response.Message = "Book cadastrado com sucesso!";
            response.Data = newBook;
            return response;
        }
        catch (Exception e)
        {
            response.Success = false;
            response.Message = e.Message;
            return response;
        }
    }

    public async Task<ResponseModel<object>> GetAllBooks(LibraryModel library)
    {
        ResponseModel<object> response = new ResponseModel<object>();
        try
        {
            var Books = await _context.Books.Where(x => x.LibraryId == library.Id).ToListAsync();
            var Data = new
            {
                library.Id,
                library.Name,
                library.CreatedAt,
                Books = Books
            };
            response.Message = "Books retornados com sucesso!";
            response.Data = Data;
            return response;
        }
        catch (Exception e)
        {
            response.Success = false;
            response.Message = e.Message;
            return response;
        }
    }

    public async Task<ResponseModel<BookModel>> GetBook(LibraryModel library, Guid Id)
    {
        ResponseModel<BookModel> response = new ResponseModel<BookModel>();
        try
        {
            var Book = await _context.Books.FirstOrDefaultAsync(x => x.Id == Id && x.LibraryId == library.Id);
            if (Book is null)
            {
                response.Success = false;
                response.Message = "Nenhum livro encontrado!";
                return response;
            }

            response.Message = "Book retornado com sucesso!";
            response.Data = Book;
            return response;
        }
        catch (Exception e)
        {
            response.Success = false;
            response.Message = e.Message;
            return response;
        }
    }
}