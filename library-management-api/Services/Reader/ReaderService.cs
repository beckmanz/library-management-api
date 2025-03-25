using library_management_api.Data;
using library_management_api.Exceptions;
using library_management_api.Models.Dto;
using library_management_api.Models.Entity;
using Microsoft.EntityFrameworkCore;
using ValidationException = System.ComponentModel.DataAnnotations.ValidationException;

namespace library_management_api.Services.Reader;

public class ReaderService : IReaderInterface
{
    private readonly ApplicationDbContext _context;

    public ReaderService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseModel<ReaderModel>> AddReader(LibraryModel library, AddReaderRequestDto request)
    {
        ResponseModel<ReaderModel> response = new ResponseModel<ReaderModel>();
        var readerExist = await _context.Readers
            .FirstOrDefaultAsync(x=> x.Phone == request.Phone || x.Email == request.Email && x.LibraryId == library.Id);
        if (readerExist is not null)
        {
            throw new ValidationException("Email ou Telefone já existe!");
        }
        
        var newReader = new ReaderModel
        {
            Name = request.Name,
            Email = request.Email,
            Phone = request.Phone,
            LibraryId = library.Id,
            Library = library,
        };
        
        _context.Add(newReader);
        await _context.SaveChangesAsync();
        
        response.Data = newReader;
        response.Message = "Leitor cadastrado com sucesso!";
        return response;
    }

    public async Task<ResponseModel<object>> GetAllReaders(LibraryModel library)
    {
        ResponseModel<object> response = new ResponseModel<object>();
        var readers = await _context.Readers.Where(x=> x.LibraryId == library.Id).ToListAsync();
        var Data = new
        {
            library.Id,
            library.Name,
            library.CreatedAt,
            Readers = readers
        };
        response.Message = "Readers retornados com sucesso!";
        response.Data = Data;
        return response;
    }

    public async Task<ResponseModel<Object>> GetReaderById(LibraryModel library, Guid Id)
    {
        ResponseModel<Object> response = new ResponseModel<Object>();
        var reader = await _context.Readers
            .Include(x => x.Loans)
            .FirstOrDefaultAsync(x=> x.Id == Id && x.LibraryId == library.Id);
        if (reader is null)
        {
            throw new NotFoundException("Nenhum reader encontrado!");
        }

        var Data = new
        {
            reader.Id,
            reader.Name,
            reader.Email,
            reader.Phone,
            reader.RegisteredAt,
            reader.LibraryId,
            reader.Loans
        };
        
        response.Message = "Reader Buscado com sucesso!";
        response.Data = Data;
        return response;
    }

    public async Task<ResponseModel<List<ReaderModel>>> GetReaderByName(LibraryModel library, string Name)
    {
        ResponseModel<List<ReaderModel>> response = new ResponseModel<List<ReaderModel>>();

        var readers = await _context.Readers
            .Where(x => x.Name.ToLower().Contains(Name.ToLower()) && x.LibraryId == library.Id)
            .ToListAsync();

        if (readers.Count == 0)
        {
            throw new NotFoundException("Nenhum reader encontrado!");
        }
        response.Message = "Leitores encontrados com sucesso!";
        response.Data = readers;
        return response;
    }

    public async Task<ResponseModel<ReaderModel>> EditReader(LibraryModel library, Guid Id, EditReaderRequestDto request)
    {
        ResponseModel<ReaderModel> response = new ResponseModel<ReaderModel>();
        if (string.IsNullOrWhiteSpace(request.Name) &&
            string.IsNullOrWhiteSpace(request.Email) &&
            string.IsNullOrWhiteSpace(request.Phone))
        {
            throw new BadRequestException("Nenhuma informação foi fornecida para atualização.");
        }
        var reader = await _context.Readers
            .FirstOrDefaultAsync(x=> x.Id == Id && x.LibraryId == library.Id);
        if (reader is null)
        {
            throw new NotFoundException("Nenhum reader encontrado!");
        }
        if (!string.IsNullOrWhiteSpace(request.Name)) reader.Name = request.Name;
        if (!string.IsNullOrWhiteSpace(request.Email)) reader.Email = request.Email;
        if (!string.IsNullOrWhiteSpace(request.Phone)) reader.Phone = request.Phone;
        _context.Update(reader);
        await _context.SaveChangesAsync();
        response.Data = reader;
        response.Message = "Leitor atualizado com sucesso!";
        return response;
    }
}