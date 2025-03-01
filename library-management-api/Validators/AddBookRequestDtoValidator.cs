using FluentValidation;
using library_management_api.Models.Dto;

namespace library_management_api.Validators;

public class AddBookRequestDtoValidator : AbstractValidator<AddBookRequestDto>
{
    public AddBookRequestDtoValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Title é obrigatório!");

        RuleFor(x => x.Genre)
            .NotEmpty().WithMessage("Genre é obrigatório!");
        
        RuleFor(x => x.PublicationYear)
            .NotEmpty().WithMessage("PublicationYaer é obrigatório!");
        
        RuleFor(x => x.AuthorId)
            .NotEmpty().WithMessage("AuthorId é obrigatório!");
    }
}