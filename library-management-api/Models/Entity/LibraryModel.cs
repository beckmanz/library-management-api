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
    public IEnumerable<BookModel> Books { get; set; }
    [JsonIgnore]
    public IEnumerable<LoanModel> Loans { get; set; }
    [JsonIgnore]
    public IEnumerable<ReaderModel> Readers { get; set; }
    [JsonIgnore]
    public IEnumerable<AuthorModel> Authors { get; set; }
}