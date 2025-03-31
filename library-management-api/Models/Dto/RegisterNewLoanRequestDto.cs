namespace library_management_api.Models.Dto;

public record RegisterNewLoanRequestDto
{
    public string returnDate { get; set; }
    public Guid? bookId { get; set; }
    public Guid? readerId { get; set; }
}