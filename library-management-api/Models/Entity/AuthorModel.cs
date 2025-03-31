using System.Text.Json.Serialization;

namespace library_management_api.Models.Entity;

public class AuthorModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public string Nationality { get; set; }

    public Guid LibraryId { get; set; }
    [JsonIgnore]
    public LibraryModel Library { get; set; }
    [JsonIgnore]
    public ICollection<BookModel> Books { get; set; }
}