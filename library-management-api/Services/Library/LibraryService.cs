using library_management_api.Data;
using library_management_api.Models.Dto;
using library_management_api.Models.Entity;

namespace library_management_api.Services.Library;

public class LibraryServices : ILibraryInterface
{
    private readonly ApplicationDbContext _context;

    public LibraryServices(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseModel<LibraryResponseDto>> GetLibrary(LibraryModel library)
    {
        ResponseModel<LibraryResponseDto> response = new ResponseModel<LibraryResponseDto>();
        var libraryAuthenticated = new LibraryResponseDto()
        {
            Id = library.Id,
            Name = library.Name,
            Email = library.Email,
            CreatedAt = library.CreatedAt,
            TotalBooks = library.Books.Count,
            AvailableBooks = library.Books.Where(b => b.IsAvailable == true).Count(),
            BorrowedBooks = library.Loans.Where(x=> x.IsReturned == false).Count(),
        };
        
        response.Data = libraryAuthenticated;
        response.Message = "Detalhes da biblioteca retornados com sucesso!";
        return response;
    }
}