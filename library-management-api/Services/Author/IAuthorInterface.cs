using library_management_api.Models.Dto;
using library_management_api.Models.Entity;

namespace library_management_api.Services.Author;

public interface IAuthorInterface
{
    Task<ResponseModel<AuthorModel>> AddAuthor(LibraryModel library, AddAuthorRequestDto author);
    Task<ResponseModel<List<AuthorModel>>> GetAllAuthors(LibraryModel library);
    Task<ResponseModel<object>> GetAuthor(LibraryModel library, Guid Id);
    Task<ResponseModel<List<AuthorModel>>> GetAuthorByName(LibraryModel library, string Name);
    Task<ResponseModel<AuthorModel>> EditAuthor(LibraryModel library, Guid Id, EditAuthorRequestDto request);
    Task<ResponseModel<AuthorModel>> DeleteAuthor(LibraryModel library, Guid Id);
}