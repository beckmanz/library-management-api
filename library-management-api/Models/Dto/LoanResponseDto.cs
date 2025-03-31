using library_management_api.Models.Entity;

namespace library_management_api.Models.Dto;

public class LoanResponseDto
{
    public Guid Id { get; set; }
    public DateTime LoanDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public DateTime? ReturnedAt { get; set; }
    public bool IsReturned { get; set; }
    public BookModel Book { get; set; }
    public ReaderModel Reader { get; set; }
}