using library_management_api.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace library_management_api.Data.Map;

public class LibraryMap : IEntityTypeConfiguration<LibraryModel>
{
    public void Configure(EntityTypeBuilder<LibraryModel> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Email).IsUnique();
        
        builder.HasMany(x => x.Books)
            .WithOne(x => x.Library)
            .HasForeignKey(x => x.LibraryId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(x => x.Readers)
            .WithOne(x => x.Library)
            .HasForeignKey(x => x.LibraryId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(x => x.Loans)
            .WithOne(x => x.Library)
            .HasForeignKey(x => x.LibraryId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasMany(x => x.Authors)
            .WithOne(x => x.Library)
            .HasForeignKey(x => x.LibraryId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}