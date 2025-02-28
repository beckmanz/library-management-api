namespace library_management_api.Models.Entity;

public class BookModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; }
    public string Genre { get; set; }
    public int PublicationYear { get; set; }
    public bool IsAvailable { get; set; } = true;
    public DateTime AddedAt { get; set; } = DateTime.UtcNow;

    public Guid AuthorId { get; set; }
    public AuthorModel Author { get; set; }

    public Guid LibraryId { get; set; }
    public LibraryModel Library { get; set; }
}