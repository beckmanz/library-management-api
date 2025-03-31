using library_management_api.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace library_management_api.Data.Map;

public class AuthorMap : IEntityTypeConfiguration<AuthorModel>
{
    public void Configure(EntityTypeBuilder<AuthorModel> builder)
    {
        builder.HasKey(x => x.Id);
        
        builder.HasMany(x => x.Books)
            .WithOne(x => x.Author)
            .HasForeignKey(x => x.AuthorId)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.HasOne(x => x.Library)
            .WithMany(x => x.Authors)
            .HasForeignKey(x => x.LibraryId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}