using library_management_api.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace library_management_api.Data.Map;

public class ReaderMap : IEntityTypeConfiguration<ReaderModel>
{
    public void Configure(EntityTypeBuilder<ReaderModel> builder)
    {
        builder.HasKey(x => x.Id);
        builder.HasIndex(x => x.Email).IsUnique();
        builder.HasIndex(x => x.Phone).IsUnique();

        builder.HasOne(x => x.Library)
            .WithMany(x => x.Readers)
            .HasForeignKey(x => x.LibraryId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasMany(x => x.Loans)
            .WithOne(x => x.Reader)
            .HasForeignKey(x => x.ReaderId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}