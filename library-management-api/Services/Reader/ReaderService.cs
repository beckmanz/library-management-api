using library_management_api.Data;

namespace library_management_api.Services.Reader;

public class ReaderService : IReaderInterface
{
    private readonly ApplicationDbContext _context;

    public ReaderService(ApplicationDbContext context)
    {
        _context = context;
    }
}