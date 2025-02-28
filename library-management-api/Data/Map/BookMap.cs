using library_management_api.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace library_management_api.Data.Map;

public class BookMap : IEntityTypeConfiguration<BookModel>
{
    public void Configure(EntityTypeBuilder<BookModel> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasOne(x => x.Author)
            .WithMany(x => x.Books)
            .HasForeignKey(x => x.AuthorId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasOne(x => x.Library)
            .WithMany(x => x.Books)
            .HasForeignKey(x => x.LibraryId)
            .OnDelete(DeleteBehavior.NoAction);
        
    }
}