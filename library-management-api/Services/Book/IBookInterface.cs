using library_management_api.Models.Dto;
using library_management_api.Models.Entity;

namespace library_management_api.Services.Book;

public interface IBookInterface
{
    Task<ResponseModel<BookModel>> AddBook(LibraryModel library, AddBookRequestDto request);
    Task<ResponseModel<List<BookModel>>> GetAllBooks(LibraryModel library);
    Task<ResponseModel<BookModel>> GetBook(LibraryModel library, Guid Id);
    Task<ResponseModel<List<BookModel>>> GetBookByTitle(LibraryModel library, string Title);
    Task<ResponseModel<BookModel>> EditBook(LibraryModel library, EditBookRequestDto request); 
    Task<ResponseModel<BookModel>> DeleteBook(LibraryModel library, Guid Id);
}