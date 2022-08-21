using FluentValidation;
using FluentValidationExample.Business6.Models.Public;
using Stef.Validation;

namespace FluentValidationExample.Business6.Validation;

internal class PersonValidator : AbstractValidator<PersonDto>
{
    public PersonValidator(IValidator<AddressDto> addressValidator)
    {
        Guard.NotNull(addressValidator);

        RuleFor(dto => dto.First)
            .NotEmpty()
            .Must(CheckFirst).WithMessage("no 'a' allowed");

        RuleFor(dto => dto.Last)
            .NotEmpty();

        RuleFor(dto => dto.Address)
            .SetValidator(addressValidator);
    }

    private static bool CheckFirst(string value)
    {
        return !value.Contains('a');
    }
}