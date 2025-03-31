using library_management_api.Data.Map;
using library_management_api.Models.Entity;
using Microsoft.EntityFrameworkCore;

namespace library_management_api.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<LibraryModel> Librarys { get; set; }
    public DbSet<BookModel> Books { get; set; }
    public DbSet<ReaderModel> Readers { get; set; }
    public DbSet<AuthorModel> Authors  { get; set; }
    public DbSet<LoanModel> Loans { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new LibraryMap());
        modelBuilder.ApplyConfiguration(new BookMap());
        modelBuilder.ApplyConfiguration(new ReaderMap());
        modelBuilder.ApplyConfiguration(new LoanMap());
        modelBuilder.ApplyConfiguration(new AuthorMap());
        base.OnModelCreating(modelBuilder);
    }
}