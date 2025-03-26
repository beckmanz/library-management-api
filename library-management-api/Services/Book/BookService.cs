using library_management_api.Data;
using library_management_api.Exceptions;
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
        var Author = await _context.Authors.FirstOrDefaultAsync(x => x.Id == request.AuthorId && x.LibraryId == library.Id);
        if (Author is null)
        {
            throw new NotFoundException("Nenhum author encontrado!");
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

    public async Task<ResponseModel<object>> GetAllBooks(LibraryModel library)
    {
        ResponseModel<object> response = new ResponseModel<object>();
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

    public async Task<ResponseModel<BookModel>> GetBook(LibraryModel library, Guid Id)
    {
        ResponseModel<BookModel> response = new ResponseModel<BookModel>();
        var Book = await _context.Books.FirstOrDefaultAsync(x => x.Id == Id && x.LibraryId == library.Id);
        if (Book is null)
        {
            throw new NotFoundException("Nenhum livro encontrado!");
        }

        response.Message = "Book retornado com sucesso!";
        response.Data = Book;
        return response;
    }

    public async Task<ResponseModel<List<BookModel>>> GetBookByTitle(LibraryModel library, string Title)
    {
        ResponseModel<List<BookModel>> response = new ResponseModel<List<BookModel>>();

        var books = await _context.Books
            .Where(x => x.Title.ToLower().Contains(Title.ToLower()) && x.LibraryId == library.Id)
            .ToListAsync();

        if (books.Count == 0)
        {
            throw new NotFoundException("Nenhum Livro encontrado!");
        }
        response.Message = "Livros encontrados com sucesso!";
        response.Data = books;
        return response;
    }

    public async Task<ResponseModel<BookModel>> EditBook(LibraryModel library, EditBookRequestDto request)
    {
        ResponseModel<BookModel> response = new ResponseModel<BookModel>();
        if (string.IsNullOrWhiteSpace(request.Title) &&
            !request.PublicationYear.HasValue &&
            string.IsNullOrWhiteSpace(request.AuthorId) &&
            string.IsNullOrWhiteSpace(request.Genre))
        {
            throw new BadRequestException("Nenhuma informação foi fornecida para atualização.");
        }
        var Book = await _context.Books.FirstOrDefaultAsync(x => x.Id == request.Id && x.LibraryId == library.Id);
        if (Book is null)
        {
            throw new NotFoundException("Nenhum livro encontrado!");
        }
        if (!string.IsNullOrWhiteSpace(request.Title)) Book.Title = request.Title;
        if (!string.IsNullOrWhiteSpace(request.Genre)) Book.Genre = request.Genre;
        if (request.PublicationYear.HasValue) Book.PublicationYear = request.PublicationYear.Value;
        if (!string.IsNullOrWhiteSpace(request.AuthorId))
        {
            var Author = await _context.Authors.FirstOrDefaultAsync(x => x.Id == new Guid(request.AuthorId) && x.LibraryId == library.Id);
            if (Author is null)
            {
                throw new NotFoundException("Nenhum author encontrado!");
            }
            Book.AuthorId = Author.Id;
            Book.Author = Author;
        }
        _context.Update(Book);
        await _context.SaveChangesAsync();
        response.Message = "Livro editado com sucesso!";
        response.Data = Book;
        return response;
    }

    public async Task<ResponseModel<BookModel>> DeleteBook(LibraryModel library, Guid Id)
    {
        ResponseModel<BookModel> response = new ResponseModel<BookModel>();
        var book = await _context.Books.FirstOrDefaultAsync(x => x.Id == Id && x.LibraryId == library.Id);
    
        if (book is null)
        {
            throw new NotFoundException("Nenhum livro encontrado!");
        }
        if (!book.IsAvailable)
        {
            throw new ValidationException("O livro está emprestado e não pode ser excluído!");
        }
    
        _context.Remove(book);
        await _context.SaveChangesAsync();
    
        response.Message = "Livro excluído com sucesso!";
        return response;
    }
}