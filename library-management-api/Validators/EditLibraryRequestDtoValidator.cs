using FluentValidation;
using library_management_api.Models.Dto;

namespace library_management_api.Validators;

public class EditLibraryRequestDtoValidator : AbstractValidator<EditLibraryRequestDto>
{
    public EditLibraryRequestDtoValidator()
    {
        RuleFor(s => s.Name)
            .MinimumLength(2).WithMessage("O nome deve ter pelo menos 2 caracteres!");

        RuleFor(s => s.Email)
            .EmailAddress().WithMessage("Email inválido!");
        
        RuleFor(s => s.NewPassword)
            .MinimumLength(6).WithMessage("A senha deve ter pelo menos 6 caracteres!");
    }
}