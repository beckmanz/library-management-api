using FluentValidation;
using library_management_api.Models.Dto;

namespace library_management_api.Validators;

public class RegisterNewLoanRequestDtoValidator : AbstractValidator<RegisterNewLoanRequestDto>
{
    public RegisterNewLoanRequestDtoValidator()
    {
        RuleFor(x => x.returnDate)
            .NotNull().WithMessage("BookId é obrigatório.")
            .Matches(@"^\d{4}-\d{2}-\d{2}$").WithMessage("ReturnDate deve ser uma data válida no formato YYYY-MM-DD.")
            .Must(x => DateOnly.Parse(x) > DateOnly.FromDateTime(DateTime.Now))
            .WithMessage("A data de Devolução deve ser após hoje.");
        
        RuleFor(x => x.bookId)
            .NotNull().WithMessage("BookId é obrigatório.")
            .NotEqual(Guid.Empty).WithMessage("BookId é obrigatorio.");
        
        RuleFor(x => x.readerId)
            .NotNull().WithMessage("ReaderId é obrigatório.")
            .NotEqual(Guid.Empty).WithMessage("ReaderId é obrigatorio.");
    }
}