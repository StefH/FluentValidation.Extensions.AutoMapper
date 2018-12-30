using FluentValidation;
using FluentValidationExample.Business.Models.Public;

namespace FluentValidationExample.Business.Validation
{
    internal class AddressValidator : AbstractValidator<AddressDto>
    {
        public AddressValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(dto => dto.Street)
                .NotEmpty()
                .MaximumLength(3);

            RuleFor(dto => dto.City)
                .NotEmpty()
                .MaximumLength(2);
        }
    }
}