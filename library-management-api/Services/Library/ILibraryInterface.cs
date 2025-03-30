using library_management_api.Models.Dto;
using library_management_api.Models.Entity;

namespace library_management_api.Services.Library;

public interface ILibraryInterface
{
    Task<ResponseModel<LibraryResponseDto>> GetLibrary(LibraryModel library);
    Task<ResponseModel<LibraryResponseDto>> EditLibrary(LibraryModel library, EditLibraryRequestDto request);
}