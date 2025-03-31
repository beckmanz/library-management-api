using FluentValidation;
using library_management_api.Models.Dto;

namespace library_management_api.Validators;

public class SignupRequestDtoValidator : AbstractValidator<SignupRequestDto>
{
    public SignupRequestDtoValidator()
    {
        RuleFor(s => s.Name)
            .NotEmpty().WithMessage("Nome é obrigatório!")
            .MinimumLength(2).WithMessage("O nome deve ter pelo menos 2 caracteres!");

        RuleFor(s => s.Email)
            .NotEmpty().WithMessage("Email é obrigatório!")
            .EmailAddress().WithMessage("Email inválido!");
        
        RuleFor(s => s.Password)
            .NotEmpty().WithMessage("Senha é obrigatória!")
            .MinimumLength(6).WithMessage("A senha deve ter pelo menos 6 caracteres!");
    }
}