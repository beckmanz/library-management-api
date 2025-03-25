using System.Text.Json.Serialization;

namespace library_management_api.Models.Entity;

public class LibraryModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [JsonIgnore]
    public ICollection<BookModel> Books { get; set; }
    [JsonIgnore]
    public ICollection<LoanModel> Loans { get; set; }
    [JsonIgnore]
    public ICollection<ReaderModel> Readers { get; set; }
    [JsonIgnore]
    public ICollection<AuthorModel> Authors { get; set; }
}