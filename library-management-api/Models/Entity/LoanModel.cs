namespace library_management_api.Models.Entity;

public class LoanModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime LoanDate { get; set; } = DateTime.UtcNow;
    public DateTime ReturnDate { get; set; }
    public DateTime? ReturnedAt { get; set; }
    public bool IsReturned { get; set; } = false;

    public Guid BookId { get; set; }
    public BookModel Book { get; set; }

    public Guid ReaderId { get; set; }
    public ReaderModel Reader { get; set; }

    public Guid LibraryId { get; set; }
    public LibraryModel Library { get; set; }
}