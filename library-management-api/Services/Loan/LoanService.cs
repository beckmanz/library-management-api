using library_management_api.Data;
using library_management_api.Exceptions;
using library_management_api.Models.Dto;
using library_management_api.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace library_management_api.Services.Loan;

public class LoanService : ILoanInterface
{
    private readonly ApplicationDbContext _context;

    public LoanService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseModel<LoanModel>> RegisterNewLoan(LibraryModel library, RegisterNewLoanRequestDto request)
    {
        ResponseModel<LoanModel> response = new ResponseModel<LoanModel>();
        var reader = await _context.Readers
            .FirstOrDefaultAsync(x=> x.Id == request.readerId && x.LibraryId == library.Id);
        var book = await _context.Books
            .FirstOrDefaultAsync(x=> x.Id == request.bookId && x.LibraryId == library.Id);
        if (reader is null)
        {
            throw new NotFoundException("Nenhum leitor encontrado");
        }
        if (reader is null)
        {
            throw new NotFoundException("Nenhum livro encontrado");
        }
        if (!book.IsAvailable)
        {
            throw new ValidationException("O livro não esta disponivel para emprestimos no momento.");
        }

        var date = DateTime.TryParse(request.returnDate, out var returnDate) ? returnDate : DateTime.Now.AddDays(14);
        var newLoan = new LoanModel()
        {
            ReturnDate = date,
            BookId = book.Id,
            Book = book,
            ReaderId = reader.Id,
            Reader = reader,
            LibraryId = library.Id,
            Library = library,
        };
        book.IsAvailable = false;
        _context.Add(newLoan);
        await _context.SaveChangesAsync();
        
        response.Data = newLoan;
        response.Message = "Novo emprestimo cadastrado com sucesso!";
        return response;
    }

    public async Task<ResponseModel<List<LoanResponseDto>>> GetAllLoans(LibraryModel library)
    {
        ResponseModel<List<LoanResponseDto>> response = new ResponseModel<List<LoanResponseDto>>();
        var loans = await _context.Loans
            .AsNoTracking()
            .Include(x => x.Book)
            .Include(x => x.Reader)
            .Where(x=> x.LibraryId == library.Id)
            .Select(x=> new LoanResponseDto()
            {
                Id = x.Id,
                LoanDate = x.LoanDate,
                ReturnDate = x.ReturnDate,
                ReturnedAt = x.ReturnedAt,
                IsReturned = x.IsReturned,
                Book = x.Book,
                Reader = x.Reader,
            })
            .ToListAsync();
        
        response.Data = loans;
        response.Message = "Emprestimos retornados com sucesso!";
        return response;
    }
}