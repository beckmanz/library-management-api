using FluentValidation;
using library_management_api.Models.Dto;

namespace library_management_api.Validators;

public class SigninRequestDtoValidator : AbstractValidator<SigninRequestDto>
{
    public SigninRequestDtoValidator()
    {
        RuleFor(s => s.Email)
            .NotEmpty().WithMessage("Email é obrigatório!")
            .EmailAddress().WithMessage("Email inválido!");

        RuleFor(s => s.Password)
            .NotEmpty().WithMessage("Senha é obrigatória!");
    }
}