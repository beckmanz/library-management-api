using System.Runtime.InteropServices.JavaScript;
using library_management_api.Models.Dto;
using library_management_api.Models.Entity;

namespace library_management_api.Services.Reader;

public interface IReaderInterface
{
    Task<ResponseModel<ReaderModel>> AddReader(LibraryModel library, AddReaderRequestDto request);
    Task<ResponseModel<Object>> GetAllReaders(LibraryModel library);
    Task<ResponseModel<Object>> GetReaderById(LibraryModel library, Guid Id);
    Task<ResponseModel<List<ReaderModel>>> GetReaderByName(LibraryModel library, string Name);
    Task<ResponseModel<ReaderModel>> EditReader(LibraryModel library, Guid Id, EditReaderRequestDto request);
    Task<ResponseModel<ReaderModel>> DeleteReader(LibraryModel library, Guid Id);
}