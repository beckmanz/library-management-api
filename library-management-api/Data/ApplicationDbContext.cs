using Microsoft.EntityFrameworkCore;

namespace library_management_api.Data;

public class ApplicationDbContext : DbContext
{
    ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
}