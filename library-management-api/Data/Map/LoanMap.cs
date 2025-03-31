using library_management_api.Models.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace library_management_api.Data.Map;

public class LoanMap : IEntityTypeConfiguration<LoanModel>
{
    public void Configure(EntityTypeBuilder<LoanModel> builder)
    {
        builder.HasKey(x => x.Id);

        builder.HasOne(x=> x.Book)
            .WithMany()
            .HasForeignKey(x => x.BookId)
            .OnDelete(DeleteBehavior.NoAction);
        
        builder.HasOne(x=> x.Reader)
            .WithMany(x=> x.Loans)
            .HasForeignKey(x=> x.ReaderId)
            .OnDelete(DeleteBehavior.NoAction);;

        builder.HasOne(x => x.Library)
            .WithMany(x => x.Loans)
            .HasForeignKey(x=> x.LibraryId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}