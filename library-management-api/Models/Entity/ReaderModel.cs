using System.Text.Json.Serialization;

namespace library_management_api.Models.Entity;

public class ReaderModel
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;

    public Guid LibraryId { get; set; }
    [JsonIgnore]
    public LibraryModel Library { get; set; }
    [JsonIgnore]
    public IEnumerable<LoanModel> Loans { get; set; }
}