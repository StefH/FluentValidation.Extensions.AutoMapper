using FluentValidation;
using FluentValidationExample.Business6.Models.Public;

namespace FluentValidationExample.Business6.Validation;

internal class AddressValidator : AbstractValidator<AddressDto>
{
    public AddressValidator()
    {
        RuleFor(dto => dto.Street)
            .NotEmpty()
            .MaximumLength(3);

        RuleFor(dto => dto.City)
            .NotEmpty()
            .MaximumLength(2);
    }
}