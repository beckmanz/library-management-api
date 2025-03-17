using library_management_api.Models.Dto;
using library_management_api.Models.Entity;

namespace library_management_api.Services.Author;

public interface IAuthorInterface
{
    Task<ResponseModel<AuthorModel>> AddAuthor(LibraryModel library, AddAuthorRequestDto author);
    Task<ResponseModel<object>> GetAllAuthors(LibraryModel library);
    Task<ResponseModel<object>> GetAuthor(LibraryModel library, Guid Id);
}