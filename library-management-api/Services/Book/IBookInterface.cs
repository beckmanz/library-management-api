using library_management_api.Models.Dto;
using library_management_api.Models.Entity;

namespace library_management_api.Services.Book;

public interface IBookInterface
{
    Task<ResponseModel<BookModel>> AddBook(LibraryModel library, AddBookRequestDto request);
    Task<ResponseModel<object>> GetAllBooks(LibraryModel library);
    Task<ResponseModel<BookModel>> GetBook(LibraryModel library, Guid Id);
}