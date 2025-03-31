using FluentValidation;
using library_management_api.Models.Dto;

namespace library_management_api.Validators;

public class EditReaderRequestDtoValidator : AbstractValidator<EditReaderRequestDto>
{
    public EditReaderRequestDtoValidator()
    {
        RuleFor(x => x.Name)
            .MinimumLength(3).WithMessage("O nome deve ter pelo menos 3 caracteres!");
        RuleFor(x=> x.Email)
            .EmailAddress().WithMessage("Email inválido!");
        RuleFor(x => x.Phone)
            .Matches(@"^\d{10,11}$")
            .WithMessage("O número de telefone deve conter apenas números e ter 10 ou 11 dígitos.");
    }
}